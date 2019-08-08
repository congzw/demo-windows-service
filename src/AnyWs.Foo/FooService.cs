using System;
using System.ServiceProcess;
using Common.WindowsServices;

namespace AnyWs.Foo
{
    public partial class FooService : ServiceBase
    {
        public FooService()
        {
            InitializeComponent();
        }
        
        protected override void OnStart(string[] args)
        {
            LogHelper.Instance.Trace("{0} OnStart {1}", this.GetType().Name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        protected override void OnStop()
        {
            LogHelper.Instance.Trace("{0} OnStop {1}", this.GetType().Name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
