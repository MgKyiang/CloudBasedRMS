using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.Logger
{
    //singleton pattern class
    public class Log:ILog
    {
        //static var
        private static Log instance;
        //private constructor
        private Log(){

        }
        //public static method
        public static Log getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Log();
                }
                return instance;
            }
        }

        public void LogException(string Message)
        {
            string fileName = string.Format("{0}_{1}.log", "Exception", DateTime.Now.ToShortDateString());
            string logFilePath = string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory,fileName);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("================================");
            sb.AppendLine(DateTime.Now.ToShortDateString());
            sb.AppendLine(Message);
            using (StreamWriter sw =new StreamWriter(logFilePath, true))
            {
                sw.Write(sb.ToString());
                sw.Flush();              
            }
        }
    }
}
