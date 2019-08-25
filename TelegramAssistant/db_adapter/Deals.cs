using System;
using System.Collections.Generic;

namespace TelegramAssistant.db_adapter
{
    public partial class Deals
    {
        public long Id { get; set; }
        public long? DealIdExch { get; set; }
        public TimeSpan? DealTime { get; set; }
        public string DerCode { get; set; }
        public string AccountCode { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime TradeDate { get; set; }
        public decimal? ExchFee { get; set; }
        public int DirectionalQuantity { get; set; }
        public string DealSource { get; set; }
        public long OrderId { get; set; }
        public bool? IsVirtual { get; set; }
        public int? PortfolioId { get; set; }
        public string AddInfo { get; set; }
    }
}
