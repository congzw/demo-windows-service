using System;
using System.IO;
using System.Windows.Forms;
using Common.WindowsServices;

namespace WinApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.txtServiceName.Text = @"MyTraceService";
            this.txtServicePath.Text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DemoWs.exe");
        }

        private void btnGetStatus_Click(object sender, EventArgs e)
        {
            ResetLogs();
            //Checks the status of the service
            var serviceName = GetServiceName();
            GetServiceState(serviceName);
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            ResetLogs();
            //Installs and starts the service
            var serviceName = GetServiceName();
            var servicePath = GetServicePath();
            var serviceState = GetServiceState(serviceName);
            if (serviceState != ServiceState.NotFound)
            {
                MessageBox.Show(string.Format("{0} already installed!", serviceName));
                return;
            }

            AppendLogs("Call InstallAndStart");
            //for display
            ServiceInstaller.InstallAndStart(serviceName, "000-" + serviceName, servicePath);
            GetServiceState(serviceName);
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            ResetLogs();
            //Removes the service
            var serviceName = GetServiceName();
            var serviceState = GetServiceState(serviceName);
            if (serviceState == ServiceState.NotFound)
            {
                MessageBox.Show(string.Format("{0} not installed!", serviceName));
                return;
            }

            AppendLogs("Call Uninstall");
            ServiceInstaller.Uninstall(serviceName);
            GetServiceState(serviceName);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ResetLogs();
            //Starts the service
            var serviceName = GetServiceName();
            var serviceState = GetServiceState(serviceName);

            if (serviceState == ServiceState.NotFound)
            {
                MessageBox.Show(string.Format("{0} not installed!", serviceName));
                return;
            }

            if (serviceState == ServiceState.Running || serviceState == ServiceState.StartPending)
            {
                MessageBox.Show(string.Format("{0} is running!", serviceName));
                return;
            }
            AppendLogs("Call StartService");
            ServiceInstaller.StartService(serviceName);
            GetServiceState(serviceName);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ResetLogs();
            //Stops the service
            var serviceName = GetServiceName();
            var serviceState = GetServiceState(serviceName);
            
            if (serviceState == ServiceState.NotFound)
            {
                MessageBox.Show(string.Format("{0} not installed!", serviceName));
                return;
            }

            if (serviceState == ServiceState.Stopped || serviceState == ServiceState.StopPending)
            {
                MessageBox.Show(string.Format("{0} is stopping!", serviceName));
                return;
            }

            AppendLogs("Call StopService" );
            ServiceInstaller.StopService(serviceName);
            GetServiceState(serviceName);
        }

        private string GetServiceName()
        {
            return this.txtServiceName.Text.Trim();
        }

        private string GetServicePath()
        {
            return this.txtServicePath.Text.Trim();
        }

        private void ResetLogs()
        {
            this.txtLogs.Clear();
        }

        private ServiceState GetServiceState(string serviceName)
        {
            var serviceStatus = ServiceInstaller.GetServiceState(serviceName);
            AppendLogs(serviceStatus.ToString());
            return serviceStatus;
        }

        private void AppendLogs(string message)
        {
            this.txtLogs.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            this.txtLogs.AppendText(" => ");
            this.txtLogs.AppendText(message);
            this.txtLogs.AppendText(Environment.NewLine);
        }
    }
}
