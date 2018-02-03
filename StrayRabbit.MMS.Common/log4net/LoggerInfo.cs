using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;
using log4net.Layout;
using log4net.Layout.Pattern;

namespace StrayRabbit.MMS.Common.log4net
{
    public class LoggerInfo
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        public string LogType { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 创建人账号
        /// </summary>
        public string CreateUserId { get; set; }

    }

    internal sealed class LogTypePatternConverter : PatternLayoutConverter
    {
        protected override void Convert(System.IO.TextWriter writer, LoggingEvent loggingEvent)
        {
            var messageLog = loggingEvent.MessageObject as LoggerInfo;
            if (messageLog != null)
            {
                writer.Write(messageLog.LogType);
            }
        }
    }

    internal sealed class MessagePatternConverter : PatternLayoutConverter
    {
        protected override void Convert(System.IO.TextWriter writer, LoggingEvent loggingEvent)
        {
            var messageLog = loggingEvent.MessageObject as LoggerInfo;
            if (messageLog != null)
            {
                writer.Write(messageLog.Message);
            }
        }
    }

    internal sealed class CreateUserIdPatternConverter : PatternLayoutConverter
    {
        protected override void Convert(System.IO.TextWriter writer, LoggingEvent loggingEvent)
        {
            var messageLog = loggingEvent.MessageObject as LoggerInfo;
            if (messageLog != null)
            {
                writer.Write(messageLog.CreateUserId);
            }
        }
    }

    public class LoggerInfoLayout : PatternLayout
    {
        public LoggerInfoLayout()
        {
            this.AddConverter("LogType", typeof(LogTypePatternConverter));
            this.AddConverter("Message", typeof(MessagePatternConverter));
            this.AddConverter("CreateUserId", typeof(CreateUserIdPatternConverter));
        }
    }
}
