using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using Common.WindowsServices;

namespace WinApp
{
    public partial class AsyncMessageForm : Form
    {
        public AsyncMessageForm()
        {
            InitializeComponent();
        }

        public AsyncUpdateMessageHelper MessageHelper { get; set; }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            MessageHelper = AsyncUpdateMessageHelper.Create(this.txtLogs, value =>
            {
                this.txtLogs.AppendText(value);
            });
        }

        private bool _processing = false;
        private int _messageIndex = 0;
        private void btnStart_Click(object sender, EventArgs e)
        {
            _messageIndex = 0;
            this.txtLogs.Clear();
            _processing = true;
            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    if (!_processing)
                    {
                        break;
                    }
                    _messageIndex++;
                    Thread.Sleep(200);
                    LogHelper.Instance.Trace("message " + _messageIndex);
                }
                _processing = false;
            });
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _processing = false;
            LogHelper.Instance.Trace("Stopped!");
        }
    }
}
