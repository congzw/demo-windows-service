namespace Common.WindowsServices
{
    //todo
    public class ServiceInvokeChecker
    {
        public ServiceInvokeResult ShouldInvokeInstall(ServiceState serviceState)
        {
            //todo
            return null;
        }

        public ServiceInvokeResult ShouldInvokeUninstall(ServiceState serviceState)
        {
            //todo
            return null;
        }

        public ServiceInvokeResult ShouldInvokeStart(ServiceState serviceState)
        {
            //todo
            return null;
        }

        public ServiceInvokeResult ShouldInvokeStop(ServiceState serviceState)
        {
            //todo
            return null;
        }
    }

    public class ServiceInvokeResult
    {
        public string Success { get; set; }
        public string Message { get; set; }
    }
}
