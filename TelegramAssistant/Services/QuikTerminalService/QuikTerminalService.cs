using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using Microsoft.Extensions.Hosting;
using StockSharp.BusinessEntities;
using StockSharp.Logging;
using StockSharp.Quik;
using TelegramAssistant.db_adapter;
using TelegramAssistant.Events;
using TelegramAssistant.Types.Responses;

namespace TelegramAssistant.Services.QuikTerminalService
{
  
    public class QuikTerminalService : TimedHostedServiceBase
    {
        private QuikTrader _quikTrader;
        private readonly ConsoleLogListener _consoleLogListener;
        // private readonly LogManager _logManager;

        internal event EventHandler<MarketDepth> MarketDepthsChanged;
        private readonly Dictionary<Security, HashSet<IQuoteReceiver>> _subscribersToQuotes;
        private readonly Dictionary<string, Security> _securities;


        public QuikTerminalService(int intervalMs =1000, int dueTimeSpanSeconds=10):
            base(intervalMs, dueTimeSpanSeconds)
        {
        //  _logManager = new LogManager();
            //_consoleLogListener = new ConsoleLogListener();

            _subscribersToQuotes = new Dictionary<Security, HashSet<IQuoteReceiver>>();
            _securities = new Dictionary<string, Security>();


            _securities.Add("USDRUB_TOM", new Security(){Name = "USDRUB_TOM" });
            this.MarketDepthsChanged += (s, depths) => Notify_Subscribers_MarketDepthsChanged(s, depths);

            //ConfigureAndConnect();
            //private readonly FileLogListener _fileLogListener = new FileLogListener("trades_mxi3_16");
        }
          protected override void DoWork(object state)
          {
              var sec = new Security() {Name = "USDRUB_TOM"};

            decimal res =   trading_db_context_sprocs.GetLasDeal("USDRUB_TOM");
            var depth = new MarketDepth(sec);
            depth.AddAsk(res, 1);
            depth.AddBid(res,1);

               MarketDepthsChanged?.Invoke(this,depth );
        
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
                        _subscribersToQuotes.Add(security, new HashSet<IQuoteReceiver>() {quoteReceiver});

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


        public Task<IResponse>  GetSecurity(string ticker)
        {
            var sec = _securities[ticker];

            var response =  new GetSecurityResponse()
            {
                Success = sec != null,
                Content = sec

            };
            return new Task<IResponse>( (() => response));

        }

        private void ConfigureAndConnect()
        {


            //if (_quikTrader == null)
            //{
            //    _quikTrader = new QuikTrader
            //    {
            //        LuaFixServerAddress = "127.0.0.1:5001".To<EndPoint>(),
            //        LuaLogin = "quik",
            //        LuaPassword = "quik".To<SecureString>()
            //    };


            //    _quikTrader.ClearCache();
            //    _quikTrader.LogLevel = LogLevels.Info;
            //   // _logManager.Sources.Add(_quikTrader);
            //   // _logManager.Listeners.Add(_consoleLogListener);
            //    _quikTrader.RequestAllSecurities = true;


            //    _quikTrader.ReConnectionSettings.WorkingTime = ExchangeBoard.Forts.WorkingTime;
            //    _quikTrader.Connect();


            //    _quikTrader.NewSecurities += securities =>
            //    {
            //        foreach (var security in securities)
            //        {
            //            if (!_securities.ContainsKey(security.Name))
            //            {
            //                _securities.Add(security.Name, security);
            //            }
            //        }
            //    };
                this.MarketDepthsChanged +=    (s, depths) =>  Notify_Subscribers_MarketDepthsChanged( s, depths);
                

            //    //_quikTrader.Connected += () => sender.Tell(ConnectedMsg.Inst());

            //    //_quikTrader.Disconnected += () => sender.Tell(DisconnectedMsg.Inst());

            //    //_quikTrader.Restored += () => sender.Tell(ConnectionRestoredMsg.Inst());

            //    //_quikTrader.TimeOut += () => sender.Tell(ConnectionErrorMsg.Inst("connection timed out"));

            //    //_quikTrader.ConnectionError += error => sender.Tell(ConnectionErrorMsg.Inst(error.Message));


            //    //// подписываемся на ошибку обработки данных (транзакций и маркет)
            //    //_quikTrader.Error += error =>
            //    //    this.GuiAsync(() => MessageBox.Show(this, error.ToString(), "Ошибка обработки данных"));


            //    // подписываемся на ошибку подписки маркет-данных
            //    //_quikTrader.MarketDataSubscriptionFailed += (security, type, error) =>
            //    ////    this.GuiAsync(() => MessageBox.Show(this, error.ToString(), LocalizedStrings.Str2956Params.Put(type, security)));


            //    //_quikTrader.NewSecurities += securities =>
            //    //{
            //    //    var secsFORTS = securities.Where(s=>s.Board == ExchangeBoard.Forts && s.Name.ToUpper().StartsWith("SIH9") ).ToList();


            //    //    if (!secsFORTS.IsNullOrEmpty() )
            //    //    {
            //    //       _quikTrader.RegisterSecurity(secsFORTS.First(), null, null, null, MarketDataBuildModes.Load, null);
            //    //        foreach (var subscriberToSecurity in _subscribersToSecurities)
            //    //        {
            //    //            subscriberToSecurity.Tell(SecListMsg.Inst(secsFORTS));
            //    //        }
            //    //    }

            //    //};
            //}
        }

        private void Notify_Subscribers_MarketDepthsChanged ( object sender, MarketDepth depth)
        {

            if (_subscribersToQuotes.Count==0) return;
            
                foreach (var quoteReceiver in _subscribersToQuotes[depth.Security])
                {
                    quoteReceiver.HandleQuote(depth);
                }
            
        }

        public void Dispose()
        {
            try
            {
                _quikTrader.Disconnect();
            }
            catch (Exception e)
            {
                //TODO
            }
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