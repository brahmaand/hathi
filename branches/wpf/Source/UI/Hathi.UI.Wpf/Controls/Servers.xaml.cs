using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hathi.UI.Winform;
using Hathi.Types;
using Hathi.eDonkey.InterfaceGateway;

namespace Hathi.UI.Wpf.Controls
{


    /// <summary>
    /// Interaction logic for Servers.xaml
    /// </summary>
    public partial class Servers : UserControl
    {
        private CkernelGateway mKernelGateway;
        private delegate void LogDelegate(object sender, Constants.Log importance, string strMsg);
        private LogDelegate logDelegate;
        public Servers()
        {
            InitializeComponent();

        }

        public void Initialize(CkernelGateway kernelGateway)
        {
            mKernelGateway = kernelGateway;
            logDelegate = new LogDelegate(Log);
            mKernelGateway.OnLogMessage += new LogEvent(OnLog);
            mServersListviewControl.Initilize(mKernelGateway);

            Globalize();
        }

        public void OnLog(Constants.Log importance, string strMsg)
        {
            this.Dispatcher.BeginInvoke(logDelegate, new object[] { this, importance, strMsg });
        }

        private void Log(object sender, Constants.Log importance, string strMsg)
        {
            //if (importance == Constants.Log.Notify) labelmsg.Text = strMsg;
            string newline = DateTime.Now.ToShortTimeString() + " " + strMsg + "\n";
            lock (richTextBoxLog)
            {
                richTextBoxLog.AppendText(newline);
            }
        }

        private void Globalize()
        {
            //contextMenu1.MenuItems[0].Text=HathiForm.Globalization["LBL_CLEARLOG"];
            //labelPort.Text=HathiForm.Globalization["LBL_PORT"]+":";
            //buttonAddServer.Text=HathiForm.Globalization["LBL_ADDSERVER"];
            //buttonDownloadServerList.Text=HathiForm.Globalization["LBL_DOWNLOAD_SERVERLIST"];
        }

        private void buttonAddServer_Click(object sender, RoutedEventArgs e)
        {
            if ((textBoxIP.Text.Length > 0) && (textBoxPort.Text.Length > 0))
            {
                mKernelGateway.DownloadElink("ed2k://|server|" + textBoxIP.Text + "|" + textBoxPort.Text + "|/", false);
            }
        }

        private void buttonDownloadServerList_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxServerMetUri.Text.StartsWith("http"))
                mKernelGateway.DownloadServerList(textBoxServerMetUri.Text);
        }

    }
}
