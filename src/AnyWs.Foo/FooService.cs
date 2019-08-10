using System;
using System.ServiceProcess;
using Common.WindowsServices;

namespace AnyWs.Foo
{
    public partial class FooService : ServiceBase
    {
        private readonly LogHelper _logHelper = LogHelper.Instance;
        public FooService()
        {
            InitializeComponent();
            _logHelper.Prefix = "[SimpleLog][FooService] ";
        }
        
        protected override void OnStart(string[] args)
        {
            _logHelper.Trace("{0} OnStart {1}", this.GetType().Name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        protected override void OnStop()
        {
            _logHelper.Trace("{0} OnStop {1}", this.GetType().Name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
