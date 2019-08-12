using System.ServiceProcess;
using AnyWs.Helpers;

namespace AnyWs
{
    static class Program
    {
        static void Main()
        {
            //this is only a wrapper for any windows service
            var services = ServiceFactory.Instance.CreateServices();
            ServiceBase.Run(services);
        }
    }
}
