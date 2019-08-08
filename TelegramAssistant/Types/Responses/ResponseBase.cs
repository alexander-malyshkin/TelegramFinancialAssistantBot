namespace TelegramAssistant.Types.Responses
{
    internal abstract class ResponseBase
    {
        internal string ResultMessage { get; set; }
        internal bool Success { get; set; }
    }
}
