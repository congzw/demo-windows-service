using System;
using System.Windows.Forms;
using Common;
using WinApp.ViewModel;

namespace WinApp
{
    public partial class ServiceManageForm : AsyncForm
    {
        public ServiceManageForm()
        {
            InitializeComponent();
            MyInitializeComponent();
        }

        private void MyInitializeComponent()
        {
            //this.txtConfig.Enabled = false;
            //this.txtMessage.Enabled = false;
            this.txtConfig.ScrollBars = ScrollBars.Vertical;
            this.txtMessage.ScrollBars = ScrollBars.Vertical;

            Vo = new ServiceManageVo();
        }


        protected override Control GetInvoker()
        {
            return this.txtMessage;
        }

        public override void ShowCallbackMessage(string value)
        {
            this.txtMessage.AppendText(value);
        }

        public ServiceManageVo Vo { get; set; }

        private void ServiceManageForm_Load(object sender, EventArgs e)
        {
            Vo.LoadConfig();
            this.txtConfig.Text = Vo.FormatConfig(Vo.ServiceInfo);
        }
        private void btnGetStatus_Click(object sender, EventArgs e)
        {
            Vo.TryGetStatus();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            var messageResult = Vo.TryInstall();
            MessageBox.Show(messageResult.Message);
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            var messageResult = Vo.TryUninstall();
            MessageBox.Show(messageResult.Message);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var messageResult = Vo.TryStart();
            MessageBox.Show(messageResult.Message);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            var messageResult = Vo.TryStop();
            MessageBox.Show(messageResult.Message);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtMessage.Clear();
        }
    }
}
