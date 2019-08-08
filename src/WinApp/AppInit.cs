using Common;
using Common.WindowsServices;

namespace WinApp
{
    public class AppInit
    {
        public static void SetupLog()
        {
            var logHelper = LogHelper.Instance;
            var old = logHelper.TraceIt;
            logHelper.TraceIt = s =>
            {
                old(s);
                AsyncFormEventBus.Raise(new AsyncFormMessageEvent(s));
            };
        }

    }
}
