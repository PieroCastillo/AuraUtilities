using System;
using System.IO;
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

        private static Logger Instance
        {
            get;
        }

        private Logger()
        {
            LogsPath = Path.GetTempPath();
        }

        private string LogsPath
        {
            get;
            set;
        }

        private string actualLogFilePath;

        private void start(string? logsPath)
        {
            if (logsPath is null)
            {
                LogsPath = Path.GetTempPath();
            }
            else
            {
                LogsPath = logsPath;
            }

            actualLogFilePath = CreatePathName(LogsPath);
            File.CreateText(actualLogFilePath).Close();
        }

        internal string CreatePathName(string path)
            => new StringBuilder().Append(LogsPath)
                                  .Append('\\')
                                  .Append("log_")
                                  .Append(Assembly.GetExecutingAssembly().GetName().Name)
                                  .Append('_')
                                  .Append(new RandomGenerator().RandomPassword())
                                  .Append("_[")
                                  .Append(DateTime.Now.ToString("d.MMMM.yy  HH.mm.ss"))
                                  .Append("].log")
                                  .ToString();

        private void append(string message, MessageType gravity)
        {
            var str_build = new StringBuilder();
            str_build.Append('[').Append(DateTime.Now.ToString()).Append("][").Append(gravity.ToString()).Append("]: ").Append(message);
            using (var sw = new StreamWriter(actualLogFilePath, true))
            {
                sw.WriteLine(str_build.ToString());
            }
        }

        private async Task appendAsync(string message, MessageType gravity)
        {
            var str_build = new StringBuilder();
            str_build.Append('[').Append(DateTime.Now.ToString()).Append("][").Append(gravity.ToString()).Append("]: ").Append(message);
            using (var sw = new StreamWriter(actualLogFilePath, true))
            {
                await sw.WriteLineAsync(str_build.ToString());
            }
        }

        public static void Start(string? path) => Instance.start(path);

        public static void WriteLine(string message, MessageType gravity = MessageType.Info) => Instance.append(message, gravity);

        public static async Task WriteLineAsync(string message, MessageType gravity = MessageType.Info) => await Instance.appendAsync(message, gravity);

        public static void Assert(bool condition, string message, MessageType gravity = MessageType.Info)
        {
            if (condition)
                WriteLine(message, gravity);
        }
    }

    public enum MessageType
    {
        Info,
        Warning,
        Error,
        FatalError
    }
}