using System;
using System.Collections.Generic;
using System.Linq;
using StockSharp.BusinessEntities;

namespace TelegramAssistant.Types.Requests
{
    internal class QuoteValueCriterionRequest : RequestBase
    {
        

        private const int _minArgsQuantity = 4;
        private const int _assetInd = 1;

        internal const string CommandShortcutStatic = "/quote";

        internal override string CommandShortcut => CommandShortcutStatic;

        public Func<decimal, bool> Predicate { get; private set; }
        public string Operator { get; private set; }
        public long ChatId { get; }
        public Security Security { get; }

        public override string ToString()
        {
            return $"{Security.Name}_{ChatId}_{Operator}";
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                
                hash = hash * 23 + Security.Name.GetHashCode();
                hash = hash * 23 + ChatId.GetHashCode();
                hash = hash * 23 + Operator.GetHashCode();
                return hash;
            }
        }


        public QuoteValueCriterionRequest(IReadOnlyList<string> args, long chatId, Security security)
        {
            
            if(args.Count < _minArgsQuantity || args.All(a => !a.Equals("then", StringComparison.InvariantCultureIgnoreCase)))
                throw new ArgumentException();
            ConstructCriterionDelegate(args);

            
            ChatId = chatId;
            Security = security;
        }

        public bool IsValid { get; private set; }
        private void ConstructCriterionDelegate(IReadOnlyList<string> args)
        {
            IsValid = true;
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
            else IsValid = false;

        }

        
        
    }
}
