using System.Security.Principal;
using Common;
using Common.WindowsServices;

namespace AnyWsManage
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

        public static bool IsRunAsAdmin()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
