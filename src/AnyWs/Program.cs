using System.ServiceProcess;
using AnyWs.Helpers;

namespace AnyWs
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            var services = ServiceFactory.Instance.CreateServices();
            ServiceBase.Run(services);
        }
    }
}
