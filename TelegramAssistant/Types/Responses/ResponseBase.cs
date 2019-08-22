using StockSharp.BusinessEntities;

namespace TelegramAssistant.Types.Responses
{
    public abstract class ResponseBase: IResponse
    {
        public string ResultMessage { get; set; }
        public bool Success { get; set; }
        public bool IsValid { get; set; }

        public Security Content { get; set; }
    }

    public interface IResponse
    {
        string ResultMessage { get; set; }
        bool Success { get; set; }
        bool IsValid { get; set; }
        Security Content { get; set; }
    }
}