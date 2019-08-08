using System.ServiceProcess;
using AnyWs.Foo;

namespace AnyWs.Helpers
{
    public class ServiceFactory
    {
        public ServiceBase[] CreateServices()
        {
            //todo create by config
            var servicesToRun = new ServiceBase[]
            {
                new FooService()
            };
            return servicesToRun;
        }

        public static ServiceFactory Instance = new ServiceFactory();
    }
}
