using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Hathi.Classes;
using System.Windows;
using Hathi.UI.Winform;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Hathi.UI.Wpf
{
    class Startup
    {
        [STAThread]

        static void Main(string[] args)
        {
            CMain main = new CMain(args);
        }

    }

    public class CMain
    {
        private static Mutex mMutex;
        private static string mElink;
        private static Config mPreferences;
        public CMain(string[] args)
        {
            mPreferences = new Config(AppDomain.CurrentDomain.BaseDirectory, "configInterface.xml", "0.01", "HathiInterface");
            mPreferences.LoadProperties();
            if (args.Length > 0)
                mElink = args[0];

            mMutex = new Mutex(true, InterfaceConstants.GUID);
            //if (noInstanceRuning)
            if (mMutex.WaitOne(0, true))
            {
                App app = new App();
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnAppDomainException);
                if (mPreferences.GetBool("StartupLocal", true))
                    app.MainWindow = new HathiWindow(mElink, mMutex);
                else if (mPreferences.GetBool("StartupRemote", false))
                {
                    string remoteIP = mPreferences.GetString("RemoteIP", "");
                    int remotePort = mPreferences.GetInt("RemotePort", 0);
                    string remotePass = mPreferences.GetString("RemotePassword", "");
                    if ((remotePort > 0) && (remoteIP.Length > 0) && (remotePass.Length > 0))
                    {
                        CedonkeyCRemote cRemote = new CedonkeyCRemote();
                        if (cRemote.Connect(remoteIP, remotePass, remotePort))

                            app.MainWindow = new HathiWindow(mElink, mMutex, cRemote.remoteInterface);
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Can not connect with the host specified in preferences", "Hathi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            app.MainWindow = new HathiWindow(mElink, mMutex);
                        }
                    }
                    else
                        app.MainWindow = new HathiWindow(mElink, mMutex);

                }
                else
                    app.MainWindow = new HathiWindow(mElink, mMutex);

                app.MainWindow.Show();
                app.Run();

            }
            else
            {
                NotifyToMainInstance();
            }
        }
        private static void NotifyToMainInstance()
        {
            Win32.EnumWindows(m_EnumProc, 0);
        }
        private static Win32.EnumWindowsProc m_EnumProc = new Win32.EnumWindowsProc(m_EnumWindows);
        public static int m_EnumWindows(IntPtr hwnd, int lParam)
        {
            if (Win32.GetProp(hwnd, InterfaceConstants.GUID) == 1)
            {
                if (mElink == null) mElink = "";
                byte[] lpStr = Encoding.Default.GetBytes(mElink);
                IntPtr lpB = Marshal.AllocHGlobal(lpStr.Length);
                Marshal.Copy(lpStr, 0, lpB, lpStr.Length);
                Win32.COPYDATASTRUCT stMsg;
                stMsg.dwData = 0;
                stMsg.cbData = lpStr.Length;
                stMsg.lpData = lpB.ToInt32();
                IntPtr lpCD = Marshal.AllocHGlobal(Marshal.SizeOf(stMsg));
                Marshal.StructureToPtr(stMsg, lpCD, false);
                Win32.SendMessage(hwnd, Win32.WM_COPYDATA, hwnd, lpCD);
                Marshal.FreeHGlobal(lpCD);
                Marshal.FreeHGlobal(lpB);
                return 0;
            }
            else
            {
                return 1;
            }
        }

        // thanks to rss bandit developers
        private void OnAppDomainException(object sender, UnhandledExceptionEventArgs e)
        {
            // this seems to be the only place to "handle" the
            // System.NullReferenceException: Object reference not set to aninstance of an object.
            // at System.Net.OSSOCK.WSAGetOverlappedResult(IntPtrsocketHandle, IntPtr overlapped, UInt32& bytesTransferred, Boolean wait,IntPtr ignored)
            // at System.Net.Sockets.OverlappedAsyncResult.CompletionPortCallback(UInt32 errorCode, UInt32 numBytes,NativeOverlapped* nativeOverlapped)
            // that occurs on some systems running behind a NAT/Router/Dialer network connection.
            // See also the discussions here:
            //http://groups.google.com/groups?hl=de&ie=UTF-8&oe=UTF-8&q=WSAGetOverlappedResult+%22Object+reference+not+set%22&sa=N&tab=wg&lr=
            //http://groups.google.com/groups?hl=de&lr=&ie=UTF-8&oe=UTF-8&threadm=7P-cnbOVWf_pEtKiXTWc-g%40speakeasy.net&rnum=4&prev=/groups%3Fhl%3Dde%26ie%3DUTF-8%26oe%3DUTF-8%26q%3DWSAGetOverlappedResult%2B%2522Object%2Breference%2Bnot%2Bset%2522%26sa%3DN%26tab%3Dwg%26lr%3D
            //http://groups.google.com/groups?hl=de&lr=&ie=UTF-8&oe=UTF-8&threadm=3fd6eba3.432257543%40news.microsoft.com&rnum=3&prev=/groups%3Fhl%3Dde%26ie%3DUTF-8%26oe%3DUTF-8%26q%3DWSAGetOverlappedResult%2B%2522Object%2Breference%2Bnot%2Bset%2522%26sa%3DN%26tab%3Dwg%26lr%3D
            if (e.ExceptionObject is NullReferenceException)
            {
                string message = ((Exception)e.ExceptionObject).ToString();
                if (message.IndexOf("WSAGetOverlappedResult") >= 0 && message.IndexOf("CompletionPortCallback") >= 0)
                    Debug.WriteLine("Unhandled exception ignored: " + message);
                return; // ignore. See comment above :-(
            }
        }
    }
}
