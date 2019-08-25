using System;
using System.Collections.Generic;

namespace TelegramAssistant.db_adapter
{
    public partial class Portfolio1
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public DateTime CreateDate { get; set; }
        public string PortfolioName { get; set; }
        public string RootSecurity { get; set; }
        public bool IsVirt { get; set; }
    }
}
