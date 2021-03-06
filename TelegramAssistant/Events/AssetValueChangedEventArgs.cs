﻿using System;

namespace TelegramAssistant.Events
{
    public class AssetValueChangedEventArgs : EventArgs
    {
        public string Asset { get; set; }
        public long ChatId { get; set; }
        public decimal Value { get; set; }
    }
}
