using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using StockSharp.BusinessEntities;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TelegramAssistant.Commands;
using TelegramAssistant.Contracts;
using TelegramAssistant.Events;
using TelegramAssistant.Settings;
using TelegramAssistant.Types.Requests;
using TelegramAssistant.Types.Responses;
using ExceptionHelper = TelegramAssistant.Helpers.ExceptionHelper;

namespace TelegramAssistant
{
    public class BotService : IHostedService, IDisposable, IQuoteReceiver
    {
        private readonly BotServiceSettings _botServiceSettings;
        private readonly SensitiveSettings _sensitiveConfiguration;

        private ITelegramBotClient _botClient;
        private readonly ISubscriptionsManager _subscriptionsManager;


        private ConcurrentDictionary<Security, HashSet<QuoteValueCriterionRequest>> _quoteValueCriterionRequests;

        public BotService(ISubscriptionsManager ntSubscriber, BotServiceSettings botServiceSettings,
            SensitiveSettings sensitiveConfiguration, ITelegramBotClient botClient)
        {
            ExceptionHelper.CheckIfNull(botServiceSettings);
            ExceptionHelper.CheckIfNull(sensitiveConfiguration);


            _botServiceSettings = botServiceSettings;
            _sensitiveConfiguration = sensitiveConfiguration;
            _botClient = botClient;
            QuickTerminalConsole.QuickTerminalProgram.SomethingHappennedEvent += (sender, args) => HandleQuote(new MarketDepth(new Security()));

            _subscriptionsManager =
                ntSubscriber ?? throw new ArgumentException($"Не указан {nameof(ISubscriptionsManager)}");

            _quoteValueCriterionRequests = new ConcurrentDictionary<Security, HashSet<QuoteValueCriterionRequest>>();

            ConfigureBotClient();
        }


        private void ConfigureBotClient()
        {
            _botClient.OnMessage += BotOnMessage;
        }


        public void HandleQuote(MarketDepth marketDepth)
        {
            //TODO проблемы с асинхронностью, удаление из коллекции во течении итерации
            if (!_quoteValueCriterionRequests.ContainsKey(marketDepth.Security)) return;


            var midQuote = 0.5M * (marketDepth.BestAsk.Price + marketDepth.BestBid.Price);
            var requests = _quoteValueCriterionRequests[marketDepth.Security];


            foreach (var request in requests)
            {
                if (request.Predicate(midQuote))
                {
                    _botClient.SendTextMessageAsync(request.ChatId,
                        $"условие {marketDepth.Security.Name} выполняется, с мид-котировкой {midQuote}");
                    requests.RemoveWhere(item => item == request);
                }
            }
        }


        //public async Task<QuoteValueCriterionSubscriptionResponse> ValidateAndProcess()
        //{

        //    var result = new QuoteValueCriterionSubscriptionResponse();

        //    if (_request.ChatId == 0)
        //    {
        //        result.IsValid = false;

        //    };
        //    var supportedAssets = await _exchangeRatesProvider.GetAssets();
        //    if (supportedAssets.All(supportedAsset => !_request.Asset.StartsWith(supportedAsset)))
        //    {
        //        return false;
        //    }

        //    _asset = _request.Asset.Split(_request.Operator).First();

        //    try
        //    {
        //        var conditionAlreadyApplies = await _exchangeRatesProvider.ConditionAlreadyApplies(_asset, _request.ChatId, _request.Predicate);
        //        if (conditionAlreadyApplies)
        //        {
        //            return new QuoteValueCriterionSubscriptionResponse
        //            {
        //                Success = false,
        //                ResultMessage = "Указанный актив уже удовлетворяет условию"
        //            };
        //        }

        //        var alreadySubscribed = await _subscriptionsManager.AlreadySubscribed(_asset, _request.ChatId, _request.Predicate);
        //        if (alreadySubscribed)
        //        {
        //            return new QuoteValueCriterionSubscriptionResponse
        //            {
        //                Success = true,
        //                ResultMessage = "Вы уже подписаны на данное событие"
        //            };
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return new QuoteValueCriterionSubscriptionResponse
        //        {
        //            Success = false,
        //            ResultMessage = "Не получилось проверить выполнение условия по указанному активу"
        //        };
        //    }

        //    try
        //    {
        //        await _subscriptionsManager.Subscribe(_asset, _request.ChatId, _request.Predicate);
        //        return new QuoteValueCriterionSubscriptionResponse
        //        {
        //            Success = true,
        //            ResultMessage = "Вы успешно подписались на событие"
        //        };
        //    }
        //    catch (Exception e)
        //    {
        //        return new QuoteValueCriterionSubscriptionResponse
        //        {
        //            Success = false,
        //            ResultMessage = $"Не удалось подписаться на событие. {e.Message}"
        //        };
        //    }


        //}


        private async void BotOnMessage(object sender, MessageEventArgs e)
        {
            //TODO переделать все на регулярных выражениях

            var message = e.Message;

            if (message == null || message.Type != MessageType.Text || message.Text.Length > 50 || message.Chat.Id <= 0)

            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, BotServiceMessages.MessageTypeNotSupportedMsg);
                return;
            }

            var cmdArgs = message.Text.Split(' ').Select(t => t.Trim()).ToArray();

            Func<Task> typingWithInteracionDelay = () =>
            {
                _botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                Task.Delay(BotServiceMessages.ChatInteractionDelay);
                return Task.CompletedTask;
            };

            try
            {
                switch (cmdArgs.First())
                {
                    case QuoteValueCriterionRequest.CommandShortcutStatic: // e.g. - /quote Sberbank>250 then signal
                    {
                        await typingWithInteracionDelay();

                        var secCmd = GetSecurityCommand<BotService>.Command(this, cmdArgs[1]);
                        var secResponse = await _subscriptionsManager.ConsumeCommand(secCmd);
                        if (!secResponse.Success)
                        {
                            await _botClient.SendTextMessageAsync(message.Chat.Id,
                                BotServiceMessages.SecurityNotFoundMsg);
                            return;
                        }


                        var quoteRequest =
                            new QuoteValueCriterionRequest(cmdArgs, message.Chat.Id, secResponse.Content);

                        if (!quoteRequest.IsValid)
                        {
                            await _botClient.SendTextMessageAsync(message.Chat.Id, BotServiceMessages.QuoteUsageFormat);
                            return;
                        }

                        var quoteCmd = SubscribeToQuote<BotService>.Command(this, quoteRequest.Security);

                        var response = await _subscriptionsManager.ConsumeCommand(quoteCmd);


                        await _botClient.SendTextMessageAsync(message.Chat.Id, response.ResultMessage);

                        //  if (response.Success)


                        return;
                    }

                    default:
                    {
                        await typingWithInteracionDelay();
                        await _botClient.SendTextMessageAsync(message.Chat.Id, BotServiceMessages.QuoteUsageFormat);
                        return;
                    }
                }
            }
            catch (ArgumentException)
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, BotServiceMessages.QuoteUsageFormat);
            }
            catch (NotImplementedException)
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, BotServiceMessages.NotImplementedExcMsg);
            }
            catch (Exception)
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, BotServiceMessages.InternalErrorMsg);
            }
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _botClient.StartReceiving(Array.Empty<UpdateType>());
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _botClient.StopReceiving();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //  throw new NotImplementedException();
        }
    }
}