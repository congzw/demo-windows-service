using System;

namespace Common.WindowsServices
{
    public class LogHelper
    {
        public LogHelper()
        {
            Prefix = "[LogHelper] ";
            TraceIt = s => { System.Diagnostics.Trace.WriteLine(s); };
        }

        public Action<string> TraceIt { get; set; }

        public void Trace(string format, params object[] args)
        {
            TraceIt(Prefix + string.Format(format, args));
        }

        public string Prefix { get; set; }

        public static LogHelper Instance = new LogHelper();
        public static Func<LogHelper> Resolve = () => new LogHelper();
    }
}
