namespace TelegramAssistant
{
    internal static class BotServiceMessages
    {
        public static readonly string QuoteUsageFormat =  @"/quote <название_актива> <оператор> <значение_актива> then <действие>. "
                                                          //                 + $"Поддерживаемые активы {string.Join(',', _supportedAssets)}. "
                                                                             + $"Операторы: >, >=, <, <=. "
                                                                             + $"Действия: signal";
        public static readonly string InternalErrorMsg = "Что-то пошло не так";
        public static readonly string MessageTypeNotSupportedMsg = "данный тип не поддерживается";
         public static readonly string SecurityNotFoundMsg = "security с данным тикером не найдена";

        public static readonly string NotImplementedExcMsg = "Данный функционал не поддерживается, свяжитесь с разработчиком";

        public const int ChatInteractionDelay = 500;


    }
}