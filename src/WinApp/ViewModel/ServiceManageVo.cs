using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Common;
using Common.WindowsServices;

namespace WinApp.ViewModel
{
    public class ServiceManageVo
    {
        public WindowServiceInfo ServiceInfo { get; set; }

        public void LoadConfig()
        {
            //todo read from config
            ServiceInfo = new WindowServiceInfo();
            ServiceInfo.ServiceName = "DemoService";
            ServiceInfo.ServicePath = "DemoWs.exe";
            ServiceInfo.ServiceFriendlyName = "000-DemoService";
        }

        public string FormatConfig(WindowServiceInfo serviceInfo)
        {
            if (serviceInfo == null)
            {
                return string.Empty;
            }
            return MyModelHelper.MakeIniStringExt(serviceInfo, lastSplit: Environment.NewLine);
        }

        public MessageResult TryGetStatus()
        {
            var serviceName = ServiceInfo.ServiceName;
            var serviceState = GetServiceState(serviceName);
            if (serviceState == ServiceState.NotFound)
            {
                return AppendLogsAndResult(false, string.Format("{0} not installed!", serviceName));
            }
            return AppendLogsAndResult(true, string.Format("{0} state: {1}", serviceName, serviceState));
        }
        
        public MessageResult TryInstall()
        {
            var serviceName = ServiceInfo.ServiceName;
            var serviceFriendlyName = ServiceInfo.ServiceFriendlyName;
            var servicePath = ServiceInfo.ServicePath;
            
            var exePath = Path.GetFullPath(servicePath);
            if (!File.Exists(exePath))
            {
                return AppendLogsAndResult(false, string.Format("{0} is not found!", exePath));
            }

            var serviceState = GetServiceState(serviceName);
            if (serviceState != ServiceState.NotFound)
            {
                return AppendLogsAndResult(true, string.Format("{0} is already installed!", exePath));
            }

            //this is a bug, todo
            //ServiceInstaller.Install(serviceName, serviceFriendlyName, servicePath);

            AppendLogs("----------");
            AppendLogs(serviceName);
            AppendLogs(serviceFriendlyName);
            AppendLogs(exePath);
            AppendLogs("----------");
            ServiceInstaller.InstallAndStart(serviceName,serviceFriendlyName, exePath);

            GetServiceState(serviceName);
            return AppendLogsAndResult(true, string.Format("{0} install completed!", serviceName));
        }

        public MessageResult TryUninstall()
        {
            var serviceName = ServiceInfo.ServiceName;
            var serviceState = GetServiceState(serviceName);
            if (serviceState == ServiceState.NotFound)
            {
                return AppendLogsAndResult(true, string.Format("{0} not installed!", serviceName));
            }

            ServiceInstaller.Uninstall(serviceName);
            GetServiceState(serviceName);
            return AppendLogsAndResult(true, string.Format("{0} uninstall completed!", serviceName));
        }

        public MessageResult TryStart()
        {
            var serviceName = ServiceInfo.ServiceName;
            var serviceState = GetServiceState(serviceName);
            if (serviceState == ServiceState.NotFound)
            {
                return AppendLogsAndResult(false, string.Format("{0} not installed!", serviceName));
            }

            if (serviceState == ServiceState.Running || serviceState == ServiceState.StartPending)
            {
                return AppendLogsAndResult(true, string.Format("{0} is already running!", serviceName));
            }

            ServiceInstaller.StartService(serviceName);
            return AppendLogsAndResult(true, string.Format("{0} start completed!", serviceName));
        }

        public MessageResult TryStop()
        {
            var serviceName = ServiceInfo.ServiceName;
            var serviceState = GetServiceState(serviceName);
            if (serviceState == ServiceState.NotFound)
            {
                return AppendLogsAndResult(true, string.Format("{0} not installed!", serviceName));
            }

            if (serviceState == ServiceState.Stopped || serviceState == ServiceState.StopPending)
            {
                return AppendLogsAndResult(true, string.Format("{0} is stopping!", serviceName));
            }

            ServiceInstaller.StopService(serviceName);
            GetServiceState(serviceName);
            return AppendLogsAndResult(true, string.Format("{0} stop completed!", serviceName));
        }


        private ServiceState GetServiceState(string serviceName)
        {
            var serviceStatus = ServiceInstaller.GetServiceState(serviceName);
            AppendLogs(string.Format("{0} current state: {1}", serviceName, serviceStatus));
            return serviceStatus;
        }

        private void AppendLogs(object message)
        {
            //Log.LogInfo(message);
            LogHelper.Instance.Trace(message.ToString());
        }

        private MessageResult AppendLogsAndResult(bool success, string message)
        {
            AppendLogs(message);
            return MessageResult.Create(success, message);
        }
    }
    

    public class WindowServiceInfo
    {
        public string ServiceName { get; set; }
        public string ServicePath { get; set; }
        public string ServiceFriendlyName { get; set; }
    }
}
