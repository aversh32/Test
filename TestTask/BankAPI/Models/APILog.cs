using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BankAPI.Models
{
    public static class APILog
    {
        public static void LogWrite(string text)
        {
            string[] lines = new string[2];
            DateTime localDate = DateTime.Now;
            lines[0] = localDate.ToString();
            lines[1] = text;
           // string appendText = text + Environment.NewLine;
            System.IO.File.AppendAllLines(@"C:\WORK_FOLDER\TestFolder\WriteLines.txt", lines, Encoding.UTF8);
            return;
        }
    }
}
