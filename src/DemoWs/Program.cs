using System.ServiceProcess;

namespace DemoWs
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            //LogHelper.Instance.Trace("ABC {0}", DateTime.Now);
            //LogHelper.Instance.Trace("ABC");
            //Console.ReadLine();
            var servicesToRun = new ServiceBase[]
            {
                new MyTraceService()
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
