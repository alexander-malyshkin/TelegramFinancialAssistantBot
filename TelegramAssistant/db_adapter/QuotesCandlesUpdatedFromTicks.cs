using System;
using System.Collections.Generic;

namespace TelegramAssistant.db_adapter
{
    public partial class QuotesCandlesUpdatedFromTicks
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public string DerCode { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public decimal? Open { get; set; }
        public decimal? Close { get; set; }
        public DateTime? Date { get; set; }
        public string Period { get; set; }
    }
}
