
using System;
using System.IO;
using System.ServiceProcess;
using AnyWs.Bar;
using AnyWs.Foo;
using Common;
using Common.WindowsServices;

namespace AnyWs.Helpers
{
    public class ServiceFactory
    {
        private readonly LogHelper _logHelper = LogHelper.Resolve();
        public ServiceFactory()
        {
            _logHelper.Prefix = "[SimpleLog][ServiceFactory] ";
        }


        public ServiceBase[] CreateServices()
        {
            //todo create by config
            var wsMeta = new WindowsServiceMeta();
            var dllPath = AppDomain.CurrentDomain.Combine("AnyWs.Bar.dll");
            wsMeta.ServiceAssemblyPath = Path.GetFullPath(dllPath);
            wsMeta.ServiceClassName = "AnyWs.Bar.BarService";

            var reflectHelper = ReflectHelper.Instance;
            _logHelper.Trace(string.Format("create service {0} from {1}", wsMeta.ServiceClassName, wsMeta.ServiceAssemblyPath));
            try
            {
                if (!File.Exists(wsMeta.ServiceAssemblyPath))
                {
                    throw new ApplicationException("文件丢失：" + wsMeta.ServiceAssemblyPath);
                }
                var service = reflectHelper.CreateInstance(wsMeta.ServiceAssemblyPath, wsMeta.ServiceClassName);
                var servicesToRun = new ServiceBase[]
                {
                    service as ServiceBase
                };
                return servicesToRun;
            }
            catch (Exception e)
            {
                 _logHelper.Trace(e.Message);
                throw;
            }
        }

        public static ServiceFactory Instance = new ServiceFactory();
    }

    public class WindowsServiceMeta
    {
        public string ServiceAssemblyPath { get; set; }
        public string ServiceClassName { get; set; }
    }
}
