using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrayRabbit.MMS.Domain.Model
{
    /// <summary>
    /// 业务日志
    /// </summary>
    public class Log
    {
        public int LogId { get; set; }
        public DateTime Date { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string LogType { get; set; }
        public string Message { get; set; }
        public string CreateUserId { get; set; }
    }
}
