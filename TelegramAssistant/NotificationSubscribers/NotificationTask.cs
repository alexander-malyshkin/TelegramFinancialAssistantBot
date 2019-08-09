using System;
using TelegramAssistant.Types;

namespace TelegramAssistant.NotificationSubscribers
{
    class NotificationTask
    {
        public string Asset { get; set; }
        public Func<decimal, bool> Predicate { get; set; }
    }
}
