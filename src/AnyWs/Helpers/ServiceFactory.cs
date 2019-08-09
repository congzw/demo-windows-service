
using System.IO;
using System.ServiceProcess;
using AnyWs.Bar;
using AnyWs.Foo;
using Common.WindowsServices;

namespace AnyWs.Helpers
{
    public class ServiceFactory
    {
        public ServiceBase[] CreateServices()
        {
            ////reflection create service start fail bugs! todo
            ////todo create by config
            //var wsMeta = new WindowsServiceMeta();
            //wsMeta.ServiceAssemblyPath = Path.GetFullPath("AnyWs.Bar.dll");
            //wsMeta.ServiceClassName = "AnyWs.Bar.BarService";

            //var reflectHelper = ReflectHelper.Instance;
            //var logHelper = LogHelper.Instance;

            //logHelper.Trace(string.Format("create service {0} from {1}", wsMeta.ServiceClassName, wsMeta.ServiceAssemblyPath));
            //var service = reflectHelper.CreateInstance(wsMeta.ServiceAssemblyPath, wsMeta.ServiceClassName);

            //var servicesToRun = new ServiceBase[]
            //{
            //    service as ServiceBase
            //};
            //return servicesToRun;

            //var servicesToRun = new ServiceBase[]
            //{
            //    new FooService()
            //};
            //return servicesToRun;

            var servicesToRun = new ServiceBase[]
            {
                new BarService()
            };
            return servicesToRun;

        }

        public static ServiceFactory Instance = new ServiceFactory();
    }

    public class WindowsServiceMeta
    {
        public string ServiceAssemblyPath { get; set; }
        public string ServiceClassName { get; set; }
    }
}
