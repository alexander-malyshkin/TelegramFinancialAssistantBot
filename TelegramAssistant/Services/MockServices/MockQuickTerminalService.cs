using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using StockSharp.BusinessEntities;
using StockSharp.Quik;
using TelegramAssistant.Events;
using TelegramAssistant.Services.QuikTerminalService;
using TelegramAssistant.Types.Responses;

namespace TelegramAssistant.Services.MockServices
{
    public class MockQuickTerminalService : IQuikTerminalService, IDisposable, IHostedService
    {
        private IConnector _quikTrader;

        private readonly Dictionary<Security, HashSet<IQuoteReceiver>> _subscribersToQuotes;
        private readonly Dictionary<string, Security> _securities;

        public MockQuickTerminalService(IConnector quikTrader)
        {
            _quikTrader = quikTrader;
            _subscribersToQuotes = new Dictionary<Security, HashSet<IQuoteReceiver>>();
            _securities = new Dictionary<string, Security>();
        }

        public Task<IResponse> SubscribeToQuote(Security security, IQuoteReceiver quoteReceiver)
        {

            Task<IResponse> result = new Task<IResponse>(() =>
            {
                var response = new SubscribeToQuoteResponse
                {
                    ResultMessage = "Successfully subscribed",
                    Success = true,

                };

                if (!_subscribersToQuotes.ContainsKey(security))
                {
                    try
                    {
                        _quikTrader.RegisterSecurity(security);
                        _quikTrader.RegisterMarketDepth(security);
                        _subscribersToQuotes.Add(security, new HashSet<IQuoteReceiver>() { quoteReceiver });

                        return response;
                    }
                    catch (Exception e)
                    {
                        response.Success = false;
                        response.ResultMessage = $"error_{e.Message}";
                        return response;
                    }
                }

                if (!_subscribersToQuotes[security].Contains(quoteReceiver))
                {
                    _subscribersToQuotes[security].Add(quoteReceiver);
                    return response;
                }

                response.ResultMessage = "Already subscribed";
                response.Success = false;
                return response;
            });

            return result;
        }

        public Task<IResponse> GetSecurity(string ticker)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _quikTrader.Disconnect();
            return Task.CompletedTask;
        }
    }
}
