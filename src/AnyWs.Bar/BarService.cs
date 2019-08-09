using System;
using System.ServiceProcess;
using System.Threading;
using Common;
using Common.WindowsServices;

namespace AnyWs.Bar
{
    public partial class BarService : ServiceBase
    {
        public BarService()
        {
            InitializeComponent();
            LoopTask = new SimpleLoopTask();
            Init(LoopTask);
        }

        public SimpleLoopTask LoopTask { get; set; }

        private void Init(SimpleLoopTask loopTask)
        {
            loopTask.LoopSpan = TimeSpan.FromSeconds(3);
            loopTask.LoopAction = () =>
            {
                LogHelper.Instance.Trace(string.Format("demo long running task is running at {0:yyyy-MM-dd HH:mm:ss:fff} in thread {1}", DateTime.Now, Thread.CurrentThread.ManagedThreadId));
            };
            loopTask.AfterExitLoopAction = () =>
            {
                LogHelper.Instance.Trace(string.Format(">>> demo long running task is stopping at {0:yyyy-MM-dd HH:mm:ss:fff} in thread {1}", DateTime.Now, Thread.CurrentThread.ManagedThreadId));
            };
        }

        protected override void OnStart(string[] args)
        {
            LoopTask.Start();
        }

        protected override void OnStop()
        {
            LoopTask.Stop();
        }
    }
}
