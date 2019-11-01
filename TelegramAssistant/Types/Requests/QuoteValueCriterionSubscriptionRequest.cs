using System;
using System.Collections.Generic;
using System.Linq;

namespace TelegramAssistant.Types.Requests
{
    internal class QuoteValueCriterionSubscriptionRequest : RequestBase
    {
        private const int _minArgsQuantity = 4;
        private const int _assetInd = 1;

        internal const string CommandShortcutStatic = "/quote";

        internal override string CommandShortcut => CommandShortcutStatic;

        public QuoteValueCriterionSubscriptionRequest(IReadOnlyList<string> args, long chatId)
        {
            
            if(args.Count < _minArgsQuantity
               
               || args.All(a => !a.Equals("then", StringComparison.InvariantCultureIgnoreCase)))
                throw new ArgumentException();

            Asset = args[_assetInd];
            ChatId = chatId;
            ConstructCriterionDelegate(args);
        }

        private void ConstructCriterionDelegate(IReadOnlyList<string> args)
        {
            if (args[_assetInd].Contains(">="))
            {
                var value = decimal.Parse(args[_assetInd].Split('=').Last());
                Predicate = d => d >= value;
                Operator = ">=";
            }
            else if (args[_assetInd].Contains("<="))
            {
                var value = decimal.Parse(args[_assetInd].Split('=').Last());
                Predicate = d => d <= value;
                Operator = "<=";
            }
            else if (args[_assetInd].Contains(">"))
            {
                var value = decimal.Parse(args[_assetInd].Split('>').Last());
                Predicate = d => d > value;
                Operator = ">";
            }
            else if (args[_assetInd].Contains("<"))
            {
                var value = decimal.Parse(args[_assetInd].Split('<').Last());
                Predicate = d => d < value;
                Operator = "<";
            }
            else if (args[_assetInd].Contains("="))
            {
                var value = decimal.Parse(args[_assetInd].Split('=').Last());
                Predicate = d => d == value;
                Operator = "=";
            }
            else
            {
                throw new ArgumentException("Invalid operator");
            }
        }

        public string Asset { get; set; }
        public long ChatId { get; set; }
        public Func<decimal, bool> Predicate { get; set; }
        public string Operator { get; set; }
    }
}
