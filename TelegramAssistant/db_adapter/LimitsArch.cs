using System;
using System.Collections.Generic;

namespace TelegramAssistant.db_adapter
{
    public partial class LimitsArch
    {
        public int Id { get; set; }
        public string AccountCode { get; set; }
        public string FirmCode { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LimitType { get; set; }
        public decimal? PrevLimit { get; set; }
        public decimal? Limit { get; set; }
        public decimal? CurrentOpenPos { get; set; }
        public decimal? VarMargin { get; set; }
        public decimal? RealMargin { get; set; }
        public decimal? AccumPnl { get; set; }
        public decimal? OptionPremium { get; set; }
        public decimal? ExchFee { get; set; }
        public string Currency { get; set; }
    }
}
