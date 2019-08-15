using System;
using System.Windows.Forms;

namespace WinApp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppInit.SetupLog();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new ServiceManageForm());
            Application.Run(new AsyncMessageForm());
        }
    }
}
