using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TelegramAssistant.db_adapter
{
    public static class trading_db_context_sprocs
    {

        public static decimal GetLasDeal( string ticker)
        {
            using (var context = new trading_der_dbContext())
            {
                var lastDeal = context.FuturesQuotes.FromSql("[quik].get_last_deal @p0", ticker).FirstOrDefault();

                return lastDeal.LastDeal.Value;
            }

            
        }


    }
}