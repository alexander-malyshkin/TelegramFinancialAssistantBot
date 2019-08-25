using System;
using System.Collections.Generic;

namespace TelegramAssistant.db_adapter
{
    public partial class FuturesQuotes
    {
        public string FutName { get; set; }
        public string FutCode { get; set; }
        public decimal? LastDeal { get; set; }
        public int? DaysToExp { get; set; }
        public string ExpDate { get; set; }
        public decimal? StepSize { get; set; }
        public decimal? StepPrice { get; set; }
        public string Ticker { get; set; }
    }
}
