using System;
using System.ServiceProcess;
using Common.WindowsServices;

namespace DemoWs
{
    public partial class MyTraceService : ServiceBase
    {
        public MyTraceService()
        {
            InitializeComponent();
        }
        
        protected override void OnStart(string[] args)
        {
            LogHelper.Instance.Trace("MyTraceService OnStart {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        protected override void OnStop()
        {
            LogHelper.Instance.Trace("MyTraceService OnStop {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
