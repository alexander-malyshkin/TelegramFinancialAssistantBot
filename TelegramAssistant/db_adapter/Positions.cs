using System;
using System.Collections.Generic;

namespace TelegramAssistant.db_adapter
{
    public partial class Positions
    {
        public string DerCode { get; set; }
        public decimal EffectiveStrike { get; set; }
        public decimal? Quntity { get; set; }
        public decimal? Margin { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public decimal? ExchFee { get; set; }
        public int PortfolioId { get; set; }
        public bool IsVirtual { get; set; }
    }
}
