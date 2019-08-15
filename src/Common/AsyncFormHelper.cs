using System;
using System.Windows.Forms;

namespace Common
{
    public delegate void UpdateMessageDelegate(string value);

    public class AsyncUpdateMessageHelper
    {
        public AsyncUpdateMessageHelper(Control invokeControl, UpdateMessageDelegate updateMessage)
        {
            InvokeControl = invokeControl;
            UpdateMessage = updateMessage;
            AsyncFormEventBus.Register<AsyncFormMessageEvent>(UpdateUi);
            WithPrefix = true;
            AutoAppendLine = true;
        }

        public bool WithPrefix { get; set; }
        public bool AutoAppendLine { get; set; }
        public Control InvokeControl { get; set; }
        public UpdateMessageDelegate UpdateMessage { get; set; }
        
        //此方法支持在自动判断是否在非创建线程中被调用
        private void UpdateUi(AsyncFormMessageEvent messageEvent)
        {
            if (!AsyncFormEventBus.ShouldRaise())
            {
                return;
            }

            string value = string.Format("{0}{1}", messageEvent.Message, AutoAppendLine ? Environment.NewLine : string.Empty);
            if (WithPrefix)
            {
                value = messageEvent.DateTimeEventOccurred.ToString("yyyy-MM-dd HH:mm:ss:fff") + " => " + value;
            }
            if (InvokeControl.InvokeRequired)
            {
                InvokeControl.Invoke(UpdateMessage, value);
            }
            else
            {
                UpdateMessage(value);
            }
        }

        #region for easy use

        public static AsyncUpdateMessageHelper Create(Control invokeControl, UpdateMessageDelegate updateMessage)
        {
            return new AsyncUpdateMessageHelper(invokeControl, updateMessage);
        }

        #endregion
    }
}
