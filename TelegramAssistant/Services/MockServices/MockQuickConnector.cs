using System;
using System.Collections.Generic;
using System.Text;
using Ecng.Common;
using Ecng.Serialization;
using StockSharp.BusinessEntities;
using StockSharp.Logging;
using StockSharp.Messages;

namespace TelegramAssistant.Services.MockServices
{
    public class MockQuickConnector : IConnector
    {
        public void Load(SettingsStorage storage)
        {
            throw new NotImplementedException();
        }

        public void Save(SettingsStorage storage)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Guid Id { get; }
        public string Name { get; }
        public ILogSource Parent { get; set; }
        public LogLevels LogLevel { get; set; }
        public DateTimeOffset CurrentTime { get; }
        public bool IsRoot { get; }
        public event Action<LogMessage> Log;
        public void AddLog(LogMessage message)
        {
            throw new NotImplementedException();
        }

        public MarketDepth GetMarketDepth(Security security)
        {
            throw new NotImplementedException();
        }

        public object GetSecurityValue(Security security, Level1Fields field)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Level1Fields> GetLevel1Fields(Security security)
        {
            throw new NotImplementedException();
        }

        public event Action<Security, IEnumerable<KeyValuePair<Level1Fields, object>>, DateTimeOffset, DateTimeOffset> ValuesChanged;
        public IEnumerable<Security> Lookup(Security criteria)
        {
            throw new NotImplementedException();
        }

        public int Count { get; }
        public event Action<IEnumerable<Security>> Added;
        public event Action<IEnumerable<Security>> Removed;
        public event Action Cleared;
        public void RequestNewsStory(News news)
        {
            throw new NotImplementedException();
        }

        public Portfolio GetPortfolio(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Portfolio> Portfolios { get; }
        public event Action<Portfolio> NewPortfolio;
        public event Action<Portfolio> PortfolioChanged;
        public Position GetPosition(Portfolio portfolio, Security security, string clientCode = "", string depoName = "")
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Position> Positions { get; }
        public event Action<Position> NewPosition;
        public event Action<Position> PositionChanged;
        public void SendInMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public void SendOutMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public SessionStates? GetSessionState(ExchangeBoard board)
        {
            throw new NotImplementedException();
        }

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
        }

        public void LookupSecurities(Security criteria, IMessageAdapter adapter = null, MessageOfflineModes offlineMode = MessageOfflineModes.None)
        {
            throw new NotImplementedException();
        }

        public void LookupSecurities(SecurityLookupMessage criteria)
        {
            throw new NotImplementedException();
        }

        public SecurityId GetSecurityId(Security security)
        {
            throw new NotImplementedException();
        }

        public void LookupBoards(ExchangeBoard criteria, IMessageAdapter adapter = null, MessageOfflineModes offlineMode = MessageOfflineModes.None)
        {
            throw new NotImplementedException();
        }

        public void LookupBoards(BoardLookupMessage criteria)
        {
            throw new NotImplementedException();
        }

        public void LookupPortfolios(Portfolio criteria, IMessageAdapter adapter = null, MessageOfflineModes offlineMode = MessageOfflineModes.None)
        {
            throw new NotImplementedException();
        }

        public void LookupPortfolios(PortfolioLookupMessage criteria)
        {
            throw new NotImplementedException();
        }

        public void LookupOrders(Order criteria, IMessageAdapter adapter = null)
        {
            throw new NotImplementedException();
        }

        public void LookupOrders(OrderStatusMessage criteria)
        {
            throw new NotImplementedException();
        }

        public Security LookupSecurity(SecurityId securityId)
        {
            throw new NotImplementedException();
        }

        public MarketDepth GetFilteredMarketDepth(Security security)
        {
            throw new NotImplementedException();
        }

        public void RegisterOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void ReRegisterOrder(Order oldOrder, Order newOrder)
        {
            throw new NotImplementedException();
        }

        public Order ReRegisterOrder(Order oldOrder, decimal price, decimal volume)
        {
            throw new NotImplementedException();
        }

        public void CancelOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void CancelOrders(bool? isStopOrder = null, Portfolio portfolio = null, Sides? direction = null,
            ExchangeBoard board = null, Security security = null, SecurityTypes? securityType = null,
            long? transactionId = null)
        {
            throw new NotImplementedException();
        }

        public void SubscribeMarketData(Security security, MarketDataMessage message)
        {
            throw new NotImplementedException();
        }

        public void UnSubscribeMarketData(Security security, MarketDataMessage message)
        {
            throw new NotImplementedException();
        }

        public void RegisterMarketDepth(Security security, DateTimeOffset? @from = null, DateTimeOffset? to = null, long? count = null,
            MarketDataBuildModes buildMode = MarketDataBuildModes.LoadAndBuild, MarketDataTypes? buildFrom = null, int? maxDepth = null)
        {
        }

        public void UnRegisterMarketDepth(Security security)
        {
            throw new NotImplementedException();
        }

        public void RegisterFilteredMarketDepth(Security security)
        {
            throw new NotImplementedException();
        }

        public void UnRegisterFilteredMarketDepth(Security security)
        {
            throw new NotImplementedException();
        }

        public void RegisterTrades(Security security, DateTimeOffset? @from = null, DateTimeOffset? to = null, long? count = null,
            MarketDataBuildModes buildMode = MarketDataBuildModes.LoadAndBuild, MarketDataTypes? buildFrom = null)
        {
            throw new NotImplementedException();
        }

        public void UnRegisterTrades(Security security)
        {
            throw new NotImplementedException();
        }

        public void RegisterSecurity(Security security, DateTimeOffset? @from = null, DateTimeOffset? to = null, long? count = null,
            MarketDataBuildModes buildMode = MarketDataBuildModes.LoadAndBuild, MarketDataTypes? buildFrom = null)
        {
        }

        public void UnRegisterSecurity(Security security)
        {
            throw new NotImplementedException();
        }

        public void RegisterOrderLog(Security security, DateTimeOffset? @from = null, DateTimeOffset? to = null, long? count = null)
        {
            throw new NotImplementedException();
        }

        public void UnRegisterOrderLog(Security security)
        {
            throw new NotImplementedException();
        }

        public void RegisterPortfolio(Portfolio portfolio)
        {
            throw new NotImplementedException();
        }

        public void UnRegisterPortfolio(Portfolio portfolio)
        {
            throw new NotImplementedException();
        }

        public void RegisterNews()
        {
            throw new NotImplementedException();
        }

        public void UnRegisterNews()
        {
            throw new NotImplementedException();
        }

        public void SubscribeBoard(ExchangeBoard board)
        {
            throw new NotImplementedException();
        }

        public void UnSubscribeBoard(ExchangeBoard board)
        {
            throw new NotImplementedException();
        }

        public IdGenerator TransactionIdGenerator { get; }
        public IEnumerable<ExchangeBoard> ExchangeBoards { get; }
        public IEnumerable<Security> Securities { get; }
        public IEnumerable<Order> Orders { get; }
        public IEnumerable<Order> StopOrders { get; }
        public IEnumerable<OrderFail> OrderRegisterFails { get; }
        public IEnumerable<OrderFail> OrderCancelFails { get; }
        public IEnumerable<Trade> Trades { get; }
        public IEnumerable<MyTrade> MyTrades { get; }
        public IEnumerable<News> News { get; }
        public ConnectionStates ConnectionState { get; }
        public IEnumerable<Security> RegisteredSecurities { get; }
        public IEnumerable<Security> RegisteredMarketDepths { get; }
        public IEnumerable<Security> RegisteredTrades { get; }
        public IEnumerable<Security> RegisteredOrderLogs { get; }
        public IEnumerable<Portfolio> RegisteredPortfolios { get; }
        public IMessageAdapter TransactionAdapter { get; }
        public IMessageAdapter MarketDataAdapter { get; }
        public event Action<MyTrade> NewMyTrade;
        public event Action<IEnumerable<MyTrade>> NewMyTrades;
        public event Action<Trade> NewTrade;
        public event Action<IEnumerable<Trade>> NewTrades;
        public event Action<Order> NewOrder;
        public event Action<IEnumerable<Order>> NewOrders;
        public event Action<Order> OrderChanged;
        public event Action<IEnumerable<Order>> OrdersChanged;
        public event Action<OrderFail> OrderRegisterFailed;
        public event Action<OrderFail> OrderCancelFailed;
        public event Action<IEnumerable<OrderFail>> OrdersRegisterFailed;
        public event Action<IEnumerable<OrderFail>> OrdersCancelFailed;
        public event Action<long> MassOrderCanceled;
        public event Action<long, Exception> MassOrderCancelFailed;
        public event Action<long, Exception> OrderStatusFailed;
        public event Action<IEnumerable<OrderFail>> StopOrdersRegisterFailed;
        public event Action<IEnumerable<OrderFail>> StopOrdersCancelFailed;
        public event Action<IEnumerable<Order>> NewStopOrders;
        public event Action<IEnumerable<Order>> StopOrdersChanged;
        public event Action<OrderFail> StopOrderRegisterFailed;
        public event Action<OrderFail> StopOrderCancelFailed;
        public event Action<Order> NewStopOrder;
        public event Action<Order> StopOrderChanged;
        public event Action<Security> NewSecurity;
        public event Action<IEnumerable<Security>> NewSecurities;
        public event Action<Security> SecurityChanged;
        public event Action<IEnumerable<Security>> SecuritiesChanged;
        public event Action<IEnumerable<Portfolio>> NewPortfolios;
        public event Action<IEnumerable<Portfolio>> PortfoliosChanged;
        public event Action<IEnumerable<Position>> NewPositions;
        public event Action<IEnumerable<Position>> PositionsChanged;
        public event Action<MarketDepth> NewMarketDepth;
        public event Action<MarketDepth> MarketDepthChanged;
        public event Action<IEnumerable<MarketDepth>> NewMarketDepths;
        public event Action<IEnumerable<MarketDepth>> MarketDepthsChanged;
        public event Action<OrderLogItem> NewOrderLogItem;
        public event Action<IEnumerable<OrderLogItem>> NewOrderLogItems;
        public event Action<News> NewNews;
        public event Action<News> NewsChanged;
        public event Action<Message> NewMessage;
        public event Action Connected;
        public event Action Disconnected;
        public event Action<Exception> ConnectionError;
        public event Action<IMessageAdapter> ConnectedEx;
        public event Action<IMessageAdapter> DisconnectedEx;
        public event Action<IMessageAdapter, Exception> ConnectionErrorEx;
        public event Action<Exception> Error;
        public event Action<TimeSpan> MarketTimeChanged;
        public event Action<SecurityLookupMessage, IEnumerable<Security>, Exception> LookupSecuritiesResult;
        public event Action<PortfolioLookupMessage, IEnumerable<Portfolio>, Exception> LookupPortfoliosResult;
        public event Action<BoardLookupMessage, IEnumerable<ExchangeBoard>, Exception> LookupBoardsResult;
        public event Action<SecurityLookupMessage, IEnumerable<Security>, IEnumerable<Security>, Exception> LookupSecuritiesResult2;
        public event Action<PortfolioLookupMessage, IEnumerable<Portfolio>, IEnumerable<Portfolio>, Exception> LookupPortfoliosResult2;
        public event Action<BoardLookupMessage, IEnumerable<ExchangeBoard>, IEnumerable<ExchangeBoard>, Exception> LookupBoardsResult2;
        public event Action<Security, MarketDataMessage> MarketDataSubscriptionSucceeded;
        public event Action<Security, MarketDataMessage, Exception> MarketDataSubscriptionFailed;
        public event Action<Security, MarketDataMessage> MarketDataUnSubscriptionSucceeded;
        public event Action<Security, MarketDataMessage, Exception> MarketDataUnSubscriptionFailed;
        public event Action<Security, MarketDataFinishedMessage> MarketDataSubscriptionFinished;
        public event Action<Security, MarketDataMessage, Exception> MarketDataUnexpectedCancelled;
        public event Action<ExchangeBoard, SessionStates> SessionStateChanged;
    }
}
