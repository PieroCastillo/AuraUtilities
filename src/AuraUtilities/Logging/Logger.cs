using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AuraUtilities.Logging
{
    public class Logger
    {        
        static Logger()
        {
            Instance = new Logger();
        }

        public static Logger Instance
        {
            get;
        }

        private Logger() 
        {
            LogsPath = Path.GetTempPath();
        }


        public string LogsPath
        {
            get;
            private set;
        }

        private string actualLogFilePath;

        public void Start(string? logsPath)
        {
            if(logsPath is null)
            {
                LogsPath = Path.GetTempPath();
            }

            RandomGenerator rd = new();

            actualLogFilePath = CreatePathName(LogsPath);
        }

        internal string CreatePathName(string path) 
            => new StringBuilder().Append(LogsPath)
                                  .Append('\\')
                                  .Append("log_")
                                  .Append(Assembly.GetExecutingAssembly().GetName().Name)
                                  .Append("_[")
                                  .Append(DateTime.Now.ToString())
                                  .Append("].log")
                                  .ToString();


        public void Append(string messsage)
        {

        }
    }
}
