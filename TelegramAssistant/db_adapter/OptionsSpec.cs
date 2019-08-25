using System;
using System.Collections.Generic;

namespace TelegramAssistant.db_adapter
{
    public partial class OptionsSpec
    {
        public int Id { get; set; }
        public int UnderlyingId { get; set; }
        public decimal Strike { get; set; }
        public short OptType { get; set; }
        public string Board { get; set; }
        public string ShortName { get; set; }
        public string SecId { get; set; }
        public DateTime ExpDate { get; set; }
        public DateTime FirstTradeDate { get; set; }
        public decimal? StepSize { get; set; }
        public decimal? StepPrice { get; set; }
        public short? Currency { get; set; }
        public decimal? MarginBuy { get; set; }
        public decimal? MarginSynth { get; set; }
        public decimal? MarginSell { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
