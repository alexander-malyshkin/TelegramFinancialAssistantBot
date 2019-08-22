using System;

namespace TelegramAssistant.Helpers
{
   internal static class ExceptionHelper
    {

        public static void CheckIfNull<T>(T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
        }

    }
}