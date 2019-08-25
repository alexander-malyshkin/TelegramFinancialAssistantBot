using System;
using System.Collections.Generic;

namespace TelegramAssistant.db_adapter
{
    public partial class PositionsArch
    {
        public int Id { get; set; }
        public string DerCode { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? Position { get; set; }
        public string AccountCode { get; set; }
        public string FirmCode { get; set; }
        public string DerType { get; set; }
        public decimal? VarMargin { get; set; }
        public decimal? RealMargin { get; set; }
        public decimal? EffectivePrice { get; set; }
        public DateTime? ExpDate { get; set; }
    }
}
