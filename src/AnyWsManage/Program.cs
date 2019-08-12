using System;
using System.Windows.Forms;
using AnyWs.Helpers;

namespace AnyWsManage
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            AppInit.SetupLog();
            if (!AppInit.IsRunAsAdmin())
            {
                var message = string.Format("{0}{1}{2}", "服务的安装、卸载需要管理员身份！", Environment.NewLine,
                    "请尝试使用右键，然后以管理员身份运行此程序！");
                MessageBox.Show(message);
                return;
            }

            Application.Run(new ServiceManageForm());
        }
    }
}
