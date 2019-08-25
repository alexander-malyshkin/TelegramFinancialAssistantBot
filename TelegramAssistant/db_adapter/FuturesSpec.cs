using System;
using System.Collections.Generic;

namespace TelegramAssistant.db_adapter
{
    public partial class FuturesSpec
    {
        public int Id { get; set; }
        public string Board { get; set; }
        public string ShortName { get; set; }
        public string SecId { get; set; }
        public DateTime LastTradeDate { get; set; }
        public DateTime FirstTradeDate { get; set; }
        public decimal StepSize { get; set; }
        public decimal StepPrice { get; set; }
        public int? Lot { get; set; }
        public short? Currency { get; set; }
        public decimal? Margin { get; set; }
        public DateTime LastUpdate { get; set; }
        public string BaseSec { get; set; }
    }
}
