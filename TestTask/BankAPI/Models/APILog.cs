using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BankAPI.Models
{
    public static class APILog
    {
        public static  void WriteToLogFile(string url, string message)
        {
            var log = LogManager.GetCurrentClassLogger();
            string text = url + " " + message;
            log.Debug(text);
        }
    }
}
