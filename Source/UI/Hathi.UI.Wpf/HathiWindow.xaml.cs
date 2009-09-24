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
using System.Windows.Forms.Integration;
using Hathi.UI.Winform;
using System.Threading;
using Hathi.Classes;
using System.Globalization;
using System.Windows.Forms;
using Hathi.eDonkey.InterfaceGateway;

namespace Hathi.UI.Wpf
{    /// <summary>
    /// The main window for Hathi application
    /// </summary>
    public partial class HathiWindow : Window
    {
        #region private members
        private Controls.Servers mServerControl;
        private CkernelGateway mKernelGateway;
        private bool mKernelLoaded;
        private string mLink;
        private InterfacePreferences mPreferences;
        private CInterfaceGateway mRemoteGateway;
        private Mutex mMutex; //used to mantain a reference to the mutex to ensure it is not released
        internal static Config mConfigPreferences;
        public static Classes.Globalization Globalization;

        private string m_LastChatMessage;
        #endregion

        #region Construction
        public HathiWindow()
        {
            InitializeComponent();
        }
        public HathiWindow(string elink, Mutex in_Mutex)
        {
            mMutex = in_Mutex;
            mLink = elink;
            mRemoteGateway = null;
            InitializeComponent();
            Initialize();
        }


        public HathiWindow(string elink, Mutex in_Mutex, CInterfaceGateway remoteGateway)
        {
            mMutex = in_Mutex;
            mLink = elink;
            mRemoteGateway = remoteGateway;
            InitializeComponent();
            Initialize();
        }
        #endregion
        #region Migrated methods
        private void Initialize()
        {
            // Load the default skin.
            Grid mainGrid = this.Content as Grid;
            System.Windows.Controls.MenuItem item = mainGrid.ContextMenu.Items[0] as System.Windows.Controls.MenuItem;
            this.ApplySkinFromMenuItem(item);

            mConfigPreferences = new Config(AppDomain.CurrentDomain.BaseDirectory, "configInterface.xml", "0.01", "HathiInterface");
            mConfigPreferences.PropertyChanged += new Config.PropertyEventHandler(m_OnPropertyChanged);
            mConfigPreferences.PropertyDefaults += new Config.PropertyDefaultHandler(OnGetDefaultProperty);
            mConfigPreferences.LoadProperties();
            Globalization = new Classes.Globalization(AppDomain.CurrentDomain.BaseDirectory
                + System.IO.Path.DirectorySeparatorChar + "language", "interface_", "xml", mConfigPreferences.GetString("Language"));
            m_Globalize();
            mKernelLoaded = false;
            //toolTipMain.SetToolTip(HathiButtonConnect, Globalization["LBL_CONNDISC"]);
            //toolTipMain.SetToolTip(buttonOptions, Globalization["LBL_OPTIONS"]);
            //taskbarNotifier1 = new TaskbarNotifier();
            //taskbarNotifier1.SetBackgroundBitmap(new Bitmap(GetType(), "Client.Resources.Classic.PopUpSkin.bmp"), Color.FromArgb(255, 0, 0));
            //taskbarNotifier1.SetCloseBitmap(new Bitmap(typeof(HathiForm), "Client.Resources.Classic.close.bmp"), Color.FromArgb(255, 0, 255), new Point(127, 8));
            //taskbarNotifier1.TitleRectangle = new Rectangle(50, 65, 100, 70);
            //taskbarNotifier1.ContentRectangle = new Rectangle(8, 75, 133, 100);
            //taskbarNotifier1.NormalContentColor = Color.White;
            ////taskbarNotifier1.TitleClick+=new EventHandler(TitleClick);
            //taskbarNotifier1.ContentClick += new EventHandler(m_BallonClicked);
            ////taskbarNotifier1.CloseClick+=new EventHandler(CloseClick);
            //Skin.CurrentSkin = preferences.GetString("Skin", "default");
            //m_ApplySkin();
            //graphics.AutoGraphicScale = preferences.GetBool("AutoGraphicScale", false);
            //graphics.GraphicScale = preferences.GetInt("GraphicScale", 1);
            //FDownloads = new FormDownloads();
            //FDownloads.TopLevel = false;
            //panelContent.Controls.Add(FDownloads);
            //FDownloads.Dock = DockStyle.Fill;
            //FDownloads.ApplySkin();
            //FUploads = new FormUploads();
            //FUploads.TopLevel = false;
            //FUploads.Dock = DockStyle.Fill;
            //panelContent.Controls.Add(FUploads);
            //FUploads.Dock = DockStyle.Fill;
            //FUploads.ApplySkin();
            //FServers = new FormServers();
            //FServers.TopLevel = false;
            //FServers.Dock = DockStyle.Fill;
            //panelContent.Controls.Add(FServers);
            //FServers.Dock = DockStyle.Fill;
            //FServers.labelmsg = this.labelStatusMsg;
            //FServers.ApplySkin();
            //FSearchs = new FormSearch();
            //FSearchs.TopLevel = false;
            //FSearchs.Dock = DockStyle.Fill;
            //panelContent.Controls.Add(FSearchs);
            //FSearchs.Dock = DockStyle.Fill;
            //FSearchs.ApplySkin();
            //FFriends = new FormFriends();
            //FFriends.TopLevel = false;
            //FFriends.Dock = DockStyle.Fill;
            //panelContent.Controls.Add(FFriends);
            //FFriends.Dock = DockStyle.Fill;
            //FFriends.ApplySkin();
            StartKernel();
        }

        private void m_Globalize()
        {
            DownloadMenu.Tag = Globalization["LBL_DOWNLOADS"];
            UploadMenu.Tag = Globalization["LBL_UPLOADS"];
            ServersMenu.Tag = Globalization["LBL_SERVERS"];
            SearchMenu.Tag = Globalization["LBL_SEARCH"];
            FriendsMenu.Tag = Globalization["LBL_FRIENDS"];
            AboutMenu.Tag = Globalization["LBL_ABOUT"];
            //labelStatusAvgDOSpeed.Text = Globalization["LBL_AVGDO"];
            //labelStatusDOSpeed.Text = Globalization["LBL_DO"];
            //labelStatusUPSpeed.Text = Globalization["LBL_UP"];
            //labelSessionDO.Text = Globalization["LBL_DOWNLOADED"] + ":";
            //labelSessionUP.Text = Globalization["LBL_UPLOADED"] + ":";
            //graphics.strSeconds = Globalization["LBL_SECONDS"];
            //graphics.strMinutes = Globalization["LBL_MINUTES"];
            //graphics.strHours = Globalization["LBL_HOURS"];
            //graphics.strAuto = Globalization["LBL_AUTOMATIC"];

            //contextMenuAbout.MenuItems[0].Text = Globalization["LBL_LPHANTWEB"];
            //contextMenuAbout.MenuItems[1].Text = Globalization["LBL_HELP"] + "/" + Globalization["LBL_SUPPORT"];
            //contextMenuNotifyIcon.MenuItems[0].Text = Globalization["LBL_OPEN"];
            //contextMenuNotifyIcon.MenuItems[1].Text = Globalization["LBL_QUIT"];
            //contextMenuNotifyIcon.MenuItems[2].Text = Globalization["LBL_OPTIONS"];
        }

        internal void OnGetDefaultProperty(object sender, PropertyDefaultArgs e)
        {
            switch (e.Key)
            {
                case "MinimizeToTray":
                    e.Value = true;
                    break;
                case "TaskBarNotifier":
                    e.Value = true;
                    break;
                case "PreviewPlayer":
                    e.Value = "";
                    break;
                case "StartupConnectionSelect":
                    e.Value = false;
                    break;
                case "Language":
                    e.Value = CultureInfo.CurrentCulture.Name;
                    break;
                case "eLinkFromBrowserStopped":
                    e.Value = false;
                    break;
                case "FilterShowCount":
                    e.Value = true;
                    break;
                case "FilterShowTotalSizes":
                    e.Value = true;
                    break;
                case "AutoSort":
                    e.Value = false;
                    break;
            }
        }

        private void Connect()
        {
            mServerControl.Initialize(mKernelGateway);
            //FServers.Connect(krnGateway);
            //FDownloads.Connect(krnGateway);
            //FUploads.Connect(krnGateway);
            //FSearchs.Connect(krnGateway);
            //FFriends.Connect(krnGateway);
            //m_Preferences = krnGateway.GetConfig();
            //graphics.SetMinValue(0);
            //if (m_Preferences.maxDownloadRate > m_Preferences.maxUploadRate)
            //    graphics.SetMaxValue((int)m_Preferences.maxDownloadRate);
            //else
            //    graphics.SetMaxValue((int)m_Preferences.maxUploadRate);
            //activeForm = FDownloads;
            mKernelGateway.OnRefreshStatus += new RefreshEvent(m_InvokeOnRefreshStatus);
            mKernelGateway.OnNewChatMessage += new SourceEvent(m_InvokeOnNewChatMessage);
            mKernelGateway.OnStartChatSession += new ClientEvent(m_InvokeOnStartChatSession);
            mKernelGateway.OnAddingFriend += new ClientEvent(m_InvokeOnAddingFriend);
            mKernelGateway.OnDeleteFriend += new ClientEvent(m_InvokeOnDeleteFriend);
        }

        private void m_AddingFriend()
        {
            //FFriends.LoadFriendsList();
            //if (!btnFriends.Checked)
            //{
            //    btnFriends.Checked = true;
            //}
        }
        private void m_DeleteFriend()
        {
            //FFriends.LoadFriendsList();
            //if (!btnFriends.Checked)
            //{
            //    btnFriends.Checked = true;
            //}
        }
        private void m_InvokeOnRefreshStatus(CkernelGateway in_krnGateway)
        {
            try
            {
                this.Dispatcher.Invoke(new RefreshEvent(m_OnRefreshStatus), new object[] { in_krnGateway });
            }
            catch { }
        }

        private void m_OnRefreshStatus(CkernelGateway in_krnGateway)
        {
            GlobalStatus status = mKernelGateway.GetGlobalStatus();
            //byte IconNumber;
            //            labelStatusUPSpeed.Text = Globalization["LBL_UP"] + status.UploadSpeed.ToString("###0.#");
            //            labelStatusDOSpeed.Text = Globalization["LBL_DO"] + status.DowloadSpeed.ToString("###0.#");
            //            labelStatusAvgDOSpeed.Text = Globalization["LBL_AVGDO"] + status.AvgDownSpeed.ToString();
            //            labelSessionUP.Text = Globalization["LBL_UPLOADED"] + ":" + HathiListView.SizeToString(status.SessionUpload);
            //            labelSessionDO.Text = Globalization["LBL_DOWNLOADED"] + ":" + HathiListView.SizeToString(status.SessionDownload);
            //            notifyIcon1.Text = labelStatusUPSpeed.Text + " " + labelStatusDOSpeed.Text + " " + labelStatusAvgDOSpeed.Text;
            //            this.Text = String.Format("Hathi v{0} ( {1} )", in_krnGateway.Version, notifyIcon1.Text);
            //#if DEBUG
            //        labelStatusServer.Text="Cnx: "+ status.ActiveConnections.ToString()+" ";
            //        labelStatusServer.Text+=status.ServerName;
            //#else
            //            labelStatusServer.Text = status.ServerName;
            //#endif
            //            toolTipMain.SetToolTip(labelStatusServer, Globalization["LBL_USERS"] + ":" + status.ServerUsers + " - " + Globalization["LBL_FILES"] + ":" + status.ServerFiles.ToString() + " - ID:" + status.UserID.ToString());
            //            graphics.AddValue(status.DowloadSpeed, status.UploadSpeed);
            //            if (status.UserID == 0)
            //                IconNumber = 1;
            //            else
            //            {
            //                if (status.IsHighID) IconNumber = 0;
            //                else IconNumber = 2;
            //            }
            //            if (m_LastIcon != IconNumber)
            //            {
            //                m_UpdateStatusIcon(imageListIcons.Images[IconNumber]);
            //                m_LastIcon = IconNumber;
            //            }
            //            if (notifyIcon1.Visible)
            //            {
            //                m_DrawNotifyIcon(imageListIcons.Images[IconNumber], status.DowloadSpeed / m_Preferences.maxDownloadRate, status.UploadSpeed / m_Preferences.maxUploadRate);
            //            }
            //            if (status.UserID != 0)
            //                HathiButtonConnect.ImageList = this.imageListServerDiscon;
            //            else
            //                HathiButtonConnect.ImageList = this.imageListServerCon;
        }

        private void m_InvokeOnNewChatMessage(InterfaceClient client, string message)
        {
            m_LastChatMessage = client.Name + ":" + message;
            MethodInvoker mi = new MethodInvoker(this.m_StartCharSession);
            this.Dispatcher.BeginInvoke(mi);
        }
        private void m_InvokeOnStartChatSession(InterfaceClient client)
        {
            m_LastChatMessage = client.Name;
            MethodInvoker mi = new MethodInvoker(this.m_StartCharSession);
            this.Dispatcher.BeginInvoke(mi);
        }
        private void m_InvokeOnAddingFriend(InterfaceClient client)
        {
            m_LastChatMessage = client.Name;
            MethodInvoker mi = new MethodInvoker(this.m_AddingFriend);
            this.Dispatcher.BeginInvoke(mi);
        }
        private void m_InvokeOnDeleteFriend(InterfaceClient client)
        {
            MethodInvoker mi = new MethodInvoker(this.m_DeleteFriend);
            this.Dispatcher.BeginInvoke(mi);
        }

        private void StartKernel()
        {
            if (!mKernelLoaded)
            {
                mServerControl = new Hathi.UI.Wpf.Controls.Servers();

                ContentWindow.Child = mServerControl;
                mKernelLoaded = true;
                if (mRemoteGateway == null)
                    mKernelGateway = new CkernelGateway();
                else
                    mKernelGateway = new CkernelGateway(mRemoteGateway);
                Connect();
                if (mLink != null) mKernelGateway.DownloadElink(mLink, false);
            }
        }
        private void m_StartCharSession()
        {
            //if (!btnFriends.Checked)
            //{
            //    btnFriends.Checked = true;
            //    if ((preferences.GetBool("MinimizeToTray", true)) && (WindowState == FormWindowState.Minimized))
            //    {
            //        if (preferences.GetBool("TaskBarNotifier", true))
            //        {
            //            /*taskbarNotifier1.CloseClickable=true;
            //            taskbarNotifier1.TitleClickable=false;
            //            taskbarNotifier1.ContentClickable=true;
            //            taskbarNotifier1.EnableSelectionRectangle=true;
            //            taskbarNotifier1.KeepVisibleOnMousOver=true;
            //            taskbarNotifier1.ReShowOnMouseOver=true;
            //            taskbarNotifier1.Show("",Globalization["MSG_NEWCHAT"]+m_LastChatMessage,500,3000,500);
            //            */
            //            Invoke(new TextEvent(m_TaskBarNotifier1Show), new object[] { Globalization["MSG_NEWCHAT"] + m_LastChatMessage });
            //        }
            //    }
            //    else
            //    {
            //        Win32.SetForegroundWindow(Handle);
            //    }
            //}
        }
        #endregion

        #region Migrated event handlers
        /// <summary>
        /// Event handler for preferences class if any property was changed.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">changed property arguments</param>
        private void m_OnPropertyChanged(object sender, PropertyEventArgs e)
        {
            //TO DO
        }
        #endregion
        void OnContextMenuItemClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.MenuItem item = e.OriginalSource as System.Windows.Controls.MenuItem;

            // Update the checked state of the menu items.
            Grid mainGrid = this.Content as Grid;
            foreach (System.Windows.Controls.MenuItem mi in mainGrid.ContextMenu.Items)
                mi.IsChecked = mi == item;

            // Load the selected skin.
            this.ApplySkinFromMenuItem(item);
        }

        void ApplySkinFromMenuItem(System.Windows.Controls.MenuItem item)
        {
            // Get a relative path to the ResourceDictionary which
            // contains the selected skin.
            string skinDictPath = item.Tag as string;
            Uri skinDictUri = new Uri(skinDictPath, UriKind.Relative);

            // Tell the Application to load the skin resources.
            App app = System.Windows.Application.Current as App;
            app.ApplySkin(skinDictUri);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            mKernelGateway.NotRefresh = true;
            Thread.Sleep(1000);
            //FDownloads.SaveListsSettings();
            //FUploads.SaveListsSettings();
            //FServers.SaveListsSettings();
            //FSearchs.SaveListsSettings();
            //mConfigPreferences.SetProperty("AutoGraphicScale", graphics.AutoGraphicScale);
            //mConfigPreferences.SetProperty("GraphicScale", graphics.GraphicScale);
            //if (this.WindowState != FormWindowState.Minimized)
            //{
            //    int margin = 0;
            //    if (this.WindowState == FormWindowState.Maximized)
            //        margin = 1;
            //    preferences.SetProperty("WindowWidth", this.Width - margin * 2);
            //    preferences.SetProperty("WindowHeight", this.Height - margin * 2);
            //    preferences.SetProperty("WindowLocationX", this.Left + margin);
            //    preferences.SetProperty("WindowLocationY", this.Top + margin);
            //}
            //preferences.SaveProperties();
            mKernelGateway.OnRefreshStatus -= new RefreshEvent(m_InvokeOnRefreshStatus);
            mKernelGateway.OnNewChatMessage -= new SourceEvent(m_InvokeOnNewChatMessage);
            mKernelGateway.OnStartChatSession -= new ClientEvent(m_InvokeOnStartChatSession);
            mKernelGateway.OnAddingFriend -= new ClientEvent(m_InvokeOnAddingFriend);
            mKernelGateway.OnDeleteFriend -= new ClientEvent(m_InvokeOnDeleteFriend);
            mKernelGateway.CloseKernel();
            System.Windows.Application.Current.Shutdown();
        }

        private void ServersMenu_Click(object sender, RoutedEventArgs e)
        {
            if (ContentWindow.Child == null)
                ContentWindow.Child = mServerControl;
        }
        private void OtherMenu_Click(object sender, RoutedEventArgs e)
        {
            ContentWindow.Child = null;
        }
    }


}
