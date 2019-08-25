using System;
using System.Collections.Generic;

namespace TelegramAssistant.db_adapter
{
    public partial class MatlabHestonHedgingCalc
    {
        public int Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public decimal? HestonPortfolioPrice { get; set; }
        public decimal? BsPortfolioPrice { get; set; }
        public decimal? HestonDelta { get; set; }
        public decimal? HestonVega { get; set; }
        public decimal? BsDelta { get; set; }
        public decimal? BsVega { get; set; }
        public decimal? TimeToExp { get; set; }
        public decimal? Sigma { get; set; }
        public decimal? FutPrice { get; set; }
    }
}
