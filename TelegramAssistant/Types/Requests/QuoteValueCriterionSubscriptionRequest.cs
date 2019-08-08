﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TelegramAssistant.Types.Requests
{
    internal class QuoteValueCriterionSubscriptionRequest : RequestBase
    {
        private const int _minArgsQuantity = 4;
        private const int _assetInd = 2;

        public QuoteValueCriterionSubscriptionRequest()
        {
        }

        internal const string CommandShortcutStatic = "quote";

        internal override string CommandShortcut => CommandShortcutStatic;

        public QuoteValueCriterionSubscriptionRequest(IReadOnlyList<string> args)
        {
            
            if(args.Count < _minArgsQuantity
               
               || args.All(a => !a.Equals("then", StringComparison.InvariantCultureIgnoreCase)))
                throw new ArgumentException();

            Asset = args[_assetInd];

            ConstructCriterionDelegate(args);
        }

        private void ConstructCriterionDelegate(IReadOnlyList<string> args)
        {
            if (args[_assetInd].Contains(">="))
            {
                var value = decimal.Parse(args[_assetInd].Split('=').Last());
                Criterion = d => d >= value;
            }
            else if (args[_assetInd].Contains("<="))
            {
                var value = decimal.Parse(args[_assetInd].Split('=').Last());
                Criterion = d => d <= value;
            }
            else if (args[_assetInd].Contains(">"))
            {
                var value = decimal.Parse(args[_assetInd].Split('>').Last());
                Criterion = d => d > value;
            }
            else if (args[_assetInd].Contains("<"))
            {
                var value = decimal.Parse(args[_assetInd].Split('<').Last());
                Criterion = d => d < value;
            }
            else if (args[_assetInd].Contains("="))
            {
                var value = decimal.Parse(args[_assetInd].Split('=').Last());
                Criterion = d => d == value;
            }
        }

        public string Asset { get; set; }
        public Func<decimal, bool> Criterion { get; set; }
    }
}
