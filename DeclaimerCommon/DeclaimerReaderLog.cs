using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeclaimerCommon
{
    /// <summary>
    /// 读取器日志记录器
    /// </summary>
    public static class DeclaimerReaderLog
    {
        //默认值为  Fixed Reader日志记录
        private static log4net.ILog log = log4net.LogManager.GetLogger("DeclaimerWaitingGodotRollingFileAppender");

        public static void InstanceLogger(LoggerClassType loggerType)
        {
            switch (loggerType)
            {
                case LoggerClassType.DeclaimerWaitingGodotRollingFileAppender:
                    log = log4net.LogManager.GetLogger("DeclaimerWaitingGodotRollingFileAppender");
                    break;
                case LoggerClassType.DeclaimerWhiteNightRollingFileAppender:
                    log = log4net.LogManager.GetLogger("DeclaimerWhiteNightRollingFileAppender");
                    break;
                default:
                    break;
            }
        }

        public static void Debug(object message)
        {
            log.Debug(message);
        }

        public static void Debug(object message, Exception exception)
        {
            log.Debug(message, exception);
        }

        public static void Info(object message)
        {
            log.Info(message);
        }

        public static void Info(object message, Exception exception)
        {
            log.Info(message, exception);
        }

        public static void Warn(object message)
        {
            log.Warn(message);
        }

        public static void Warn(object message, Exception exception)
        {
            log.Warn(message, exception);
        }

        public static void Error(object message)
        {
            log.Error(message);
        }

        public static void Error(object message, Exception exception)
        {
            log.Error(message, exception);
        }

        public static void Fatal(object message)
        {
            log.Fatal(message);
        }

        public static void Fatal(object message, Exception exception)
        {
            log.Fatal(message, exception);
        }
    }
}
