using System;

namespace DonVo.MessagingService.Logging
{
    public interface ILogger
    {
        void Debug(string text);
        void Error(string error, Exception exception);
        void Error(string error);
        void Info(string info);
    }
}