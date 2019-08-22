using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TelegramAssistant.Commands;
using TelegramAssistant.Contracts;
using TelegramAssistant.Events;
using TelegramAssistant.Services.QuikTerminalService;
using TelegramAssistant.Types.Responses;

namespace TelegramAssistant.NotificationSubscribers
{
    class SubscriptionsManager : ISubscriptionsManager
    {
        private readonly QuikTerminalService _quikTerminalService;

        public SubscriptionsManager(QuikTerminalService quikTerminalService)
        {
            _quikTerminalService = quikTerminalService;
        }


        public Task<IResponse> ConsumeCommand<Sender, Request>(ICommand<Sender, Request> command)
        {
            switch (command)
            {
                case SubscribeToQuote<IQuoteReceiver> cmd:
                {
                    var response = _quikTerminalService.SubscribeToQuote(cmd.Request, cmd.Sender);

                    return response;
                        
                }

                case GetSecurityCommand<Sender> cmd:
                {
                    var response = _quikTerminalService.GetSecurity(command.Request.ToString());

                    return response;
                }

            }


           throw  new NotImplementedException();
        }



        //public async Task Subscribe(string asset, long chatId, Func<decimal, bool> predicate)
        //{
        //    if(await AlreadySubscribed(asset, chatId, predicate))
        //        throw new NotSupportedException("Вы уже подписаны на данное событие");

        //    Subscriptions.Add(new NotificationTask
        //    {
        //        Asset = asset,
        //        ChatId = chatId,
        //        Predicate = predicate
        //    });
        //}

        //public async Task Unsubscribe(string asset, long chatId, Func<decimal, bool> predicate)
        //{
        //    var ntTask = Subscriptions.FirstOrDefault(s => 
        //        s.Asset.Equals(asset, StringComparison.InvariantCultureIgnoreCase) 
        //            && s.ChatId == chatId 
        //            && s.Predicate == predicate);

        //    if (ntTask != null)
        //    {
        //        Subscriptions.Remove(ntTask);
        //    }
        //}

        //public async Task<bool> AlreadySubscribed(string asset, long chatId, Func<decimal, bool> predicate)
        //{
        //    return Subscriptions.Any(s => s.Asset.Equals(asset, StringComparison.InvariantCultureIgnoreCase)
        //                                        && s.ChatId == chatId && s.Predicate == predicate);
        //}


    }
}
