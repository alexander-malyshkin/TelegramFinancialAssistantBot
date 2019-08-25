using System;
using System.Collections.Generic;

namespace TelegramAssistant.db_adapter
{
    public partial class OptionsQuotes
    {
        public string OptName { get; set; }
        public string OptCode { get; set; }
        public string UnderlyingCode { get; set; }
        public decimal? Bid { get; set; }
        public decimal? Offer { get; set; }
        public int? DaysToExp { get; set; }
        public DateTime? ExpDate { get; set; }
        public string ExpDateQuikRaw { get; set; }
        public decimal? GoLong { get; set; }
        public decimal? Strike { get; set; }
        public decimal? VolatilityExch { get; set; }
        public string OptType { get; set; }
        public decimal? TheorPrice { get; set; }
        public decimal? GoShort { get; set; }
        public decimal? ClearingQuote { get; set; }
    }
}
