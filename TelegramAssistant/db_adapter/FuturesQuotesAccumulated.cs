using System;
using System.Collections.Generic;

namespace TelegramAssistant.db_adapter
{
    public partial class FuturesQuotesAccumulated
    {
        public int Id { get; set; }
        public string FutName { get; set; }
        public string FutCode { get; set; }
        public decimal? LastDeal { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
