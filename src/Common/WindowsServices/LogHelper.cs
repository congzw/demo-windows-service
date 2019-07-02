using System;

namespace Common.WindowsServices
{
    public class LogHelper
    {
        public LogHelper()
        {
            TraceIt = s => { System.Diagnostics.Trace.WriteLine(s); };
        }

        public Action<string> TraceIt { get; set; }

        public void Trace(string format, params object[] args)
        {
            TraceIt(string.Format(format, args));
        }

        public static LogHelper Instance = new LogHelper();
    }
}
