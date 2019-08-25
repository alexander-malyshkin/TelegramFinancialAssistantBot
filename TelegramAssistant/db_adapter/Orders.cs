using System;
using System.Collections.Generic;

namespace TelegramAssistant.db_adapter
{
    public partial class Orders
    {
        public long Id { get; set; }
        public long? OrderIdExch { get; set; }
        public TimeSpan SentTime { get; set; }
        public string DerCode { get; set; }
        public string AccountCode { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime OrderDate { get; set; }
        public int DirectionalQuantity { get; set; }
        public bool? IsVirtual { get; set; }
        public int? PortfolioId { get; set; }
        public string OrderStatus { get; set; }
        public string AddInfo { get; set; }
    }
}
