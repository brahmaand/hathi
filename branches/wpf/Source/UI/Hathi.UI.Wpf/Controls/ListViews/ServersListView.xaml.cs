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
using Hathi.eDonkey.InterfaceGateway;
using Hathi.Types;
using System.Net;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace Hathi.UI.Wpf.Controls.ListViews
{


    /// <summary>
    /// Interaction logic for ServersListView.xaml
    /// </summary>
    public partial class ServersListView : UserControl
    {
        private CkernelGateway mKernlGateway;

        public ServersListView()
        {
            InitializeComponent();
            this.DataContext = Servers;
        }

        private void ContextMenu_Click(object sender, RoutedEventArgs e)
        {

        }
        public ObservableCollection<InterfaceServer> Servers
        {
            get
            {
                ObservableCollection<InterfaceServer> servers = new ObservableCollection<InterfaceServer>();
                InterfaceServer server = new InterfaceServer();
                server.Name = "Random 1";
                server.IP = "1.2.3.4";
                server.Port = 222;
                server.LastConnection = DateTime.Now;
                server.Users = 1;
                server.Files = 12;
                server.Priority = Constants.ServerPriority.High;
                servers.Add(server);

                server = new InterfaceServer();
                server.Name = "Random 2";
                server.IP = "5.6.7.8";
                server.Port = 123;
                server.LastConnection = DateTime.Now;
                server.Users = 5;
                server.Files = 15;
                server.Priority = Constants.ServerPriority.High;
                servers.Add(server);
                return servers;
            }
        }

        #region Migrated Code

        public void Initilize(CkernelGateway kernelGateway)
        {
            Name = "searchListView";
            mKernlGateway = kernelGateway;
            //mKernlGateway.OnNewServer += new ServerEvent(OnNewServer);
            //mKernlGateway.OnDeleteServer += new IPEvent(OnDeleteServer);
            //mKernlGateway.OnRefreshServers += new RefreshEvent(m_OnRefreshList);

            this.KeyDown += new KeyEventHandler(OnKeyDown);
            Globalize();
            ReloadList();
        }

        private void Globalize()
        {
            GridView view = listViewServers.View as GridView;
            //view.Columns[0].Header = HathiWindow.Globalization["LBL_SERVERNAME"];
            //view.Columns[1].Header = HathiWindow.Globalization["LBL_ADDRESS"];
            //view.Columns[2].Header = HathiWindow.Globalization["LBL_FAILED"];
            //view.Columns[3].Header = HathiWindow.Globalization["LBL_FILES"];
            //view.Columns[4].Header = HathiWindow.Globalization["LBL_USERS"];
            //view.Columns[5].Header = HathiWindow.Globalization["LBL_PRIORITY"];

            MenuItem item = listViewServers.ContextMenu.Items[0] as MenuItem;
            item.Header = HathiWindow.Globalization["LBL_CONNECT"];
            item = listViewServers.ContextMenu.Items[1] as MenuItem;
            item.Header = HathiWindow.Globalization["LBL_DELETE"];
            item = listViewServers.ContextMenu.Items[2] as MenuItem;
            item.Header = HathiWindow.Globalization["LBL_COPYLINK"];
            item = listViewServers.ContextMenu.Items[3] as MenuItem;
            item.Header = HathiWindow.Globalization["LBL_COPYLINK"] + " (HTML)";
            item = listViewServers.ContextMenu.Items[4] as MenuItem;
            item.Header = HathiWindow.Globalization["LBL_PRIORITY"];
            MenuItem subItem = item.Items[0] as MenuItem;
            subItem.Header = HathiWindow.Globalization["LBL_HIGH"];
            subItem = item.Items[1] as MenuItem;
            subItem.Header = HathiWindow.Globalization["LBL_NORMAL"];
            subItem = item.Items[2] as MenuItem;
            subItem.Header = HathiWindow.Globalization["LBL_LOW"];
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            //switch (e.Key)
            //{
            //case Keys.Delete:
            //    m_DeleteServer();
            //    break;
            //case Keys.Enter:
            //    m_OnConnectServer();
            //    break;
            //}
        }

        private void OnDeleteServer(uint ip, ushort port)
        {
            InterfaceServer server;
            foreach (ListViewItem ItemServer in listViewServers.Items)
            {
                server = (InterfaceServer)ItemServer.Tag;
                IPAddress iptodelete = new IPAddress(ip);
                if ((IPAddress.Parse(server.IP).Equals(iptodelete) && (server.Port == port)))
                {
                    listViewServers.Items.Remove(ItemServer);
                    break;
                }
            }
        }

        private void OnCopyLink(object sender, System.EventArgs e)
        {
            if (listViewServers.SelectedItems.Count == 0) return;
            InterfaceServer server = (InterfaceServer)listViewServers.SelectedItems[0];//.Tag;
            Clipboard.SetDataObject("ed2k://|server|" + server.IP + "|" + server.Port.ToString() + "|/");
        }

        private void OnCopyLinkHTML(object sender, System.EventArgs e)
        {
            if (listViewServers.SelectedItems.Count == 0) return;
            InterfaceServer server = (InterfaceServer)listViewServers.SelectedItems[0];
            Clipboard.SetDataObject("<a href=\"ed2k://|server|" + server.IP + "|" + server.Port.ToString() + "|/\">" + server.Name + "</a>");
        }

        private void OnHighPriority(object sender, System.EventArgs e)
        {
            SetPriority(Constants.ServerPriority.High);
        }
        private void OnNormalPriority(object sender, System.EventArgs e)
        {
            SetPriority(Constants.ServerPriority.Normal);
        }
        private void OnLowPriority(object sender, System.EventArgs e)
        {
            SetPriority(Constants.ServerPriority.Low);
        }
        private void SetPriority(Constants.ServerPriority priority)
        {
            if (listViewServers.SelectedItems.Count == 0) return;
            for (int i = 0; i != listViewServers.SelectedItems.Count; i++)
            {
                InterfaceServer itemServer = listViewServers.SelectedItems[i] as InterfaceServer;
                if (itemServer == null) return;

                IPAddress ip = IPAddress.Parse(itemServer.IP);
                mKernlGateway.SetServerPriority(BitConverter.ToUInt32(ip.GetAddressBytes(), 0), itemServer.Port, priority);
                //Data binding should takes care to update the UI.
            }
        }
        private void OnConnectServer()
        {
            if (listViewServers.SelectedItems.Count == 0) return;

            InterfaceServer ItemServer = listViewServers.SelectedItems[0] as InterfaceServer;
            if (ItemServer == null) return;
            if (ItemServer.IP.Length == 0) return;
            IPAddress ip = IPAddress.Parse(ItemServer.IP);
            mKernlGateway.ConnectToServer(BitConverter.ToUInt32(ip.GetAddressBytes(), 0), ItemServer.Port);
        }

        private void OnConnectServer(object sender, System.EventArgs e)
        {
            OnConnectServer();
        }

        private void DeleteServer()
        {
            if (listViewServers.SelectedItems.Count == 0) return;
            try
            {
                int SelectedItem = listViewServers.SelectedItems.Count;
                InterfaceServer[] Items = new InterfaceServer[SelectedItem];
                for (int i = 0; i != SelectedItem; i++)
                {
                    Items[i] = (InterfaceServer)listViewServers.SelectedItems[i];
                }
                for (int i = 0; i != SelectedItem; i++)
                {
                    try
                    {
                        IPAddress ip = IPAddress.Parse(Items[i].IP);
                        mKernlGateway.DeleteServer(BitConverter.ToUInt32(ip.GetAddressBytes(), 0), Items[i].Port);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void DeleteServer(object sender, System.EventArgs e)
        {
            DeleteServer();
        }

        private void ServerToItem(InterfaceServer server, ListViewItem ItemServer)
        {
            //if (server == null) return;
            //if (ItemServer.SubItems[0].Text != server.Name) ItemServer.SubItems[0].Text = server.Name;
            //if (ItemServer.SubItems[1].Text != server.IP + ":" + server.Port.ToString()) ItemServer.SubItems[1].Text = server.IP + ":" + server.Port.ToString();
            //if (ItemServer.SubItems[2].Text != server.FailedConnections.ToString()) ItemServer.SubItems[2].Text = server.FailedConnections.ToString();
            //if (ItemServer.SubItems[3].Text != server.Files.ToString()) ItemServer.SubItems[3].Text = server.Files.ToString();
            //if (ItemServer.SubItems[4].Text != server.Users.ToString()) ItemServer.SubItems[4].Text = server.Users.ToString();
            ////if (ItemServer.SubItems[5].Text!=server.LastConnection.ToShortTimeString()) ItemServer.SubItems[5].Text=server.LastConnection.ToShortTimeString();
            //if (ItemServer.SubItems[5].Text != ServerPriorityToString(server.Priority)) ItemServer.SubItems[5].Text = ServerPriorityToString(server.Priority);
            //ItemServer.Tag = server;
        }

        //public void OnNewServer(InterfaceServer server)
        //{
        //    ListViewItem ItemServer = new ListViewItem(new string[] { "", "", "", "", "", "" });
        //    ItemServer.Tag = server;
        //    ServerToItem(server, ItemServer);
        //    Items.Add(ItemServer);
        //}

        public void UpdateOrAddServer(InterfaceServer server)
        {
            //string ipPort = server.IP + ":" + server.Port.ToString();
            //bool found = false;
            //foreach (ListViewItem ItemServer in this.Items)
            //{
            //    if (ItemServer.SubItems[1].Text == ipPort)
            //    {
            //        ServerToItem(server, ItemServer);
            //        found = true;
            //    }
            //}
            //if (!found)
            //    OnNewServer(server);
        }

        private void m_OnRefreshList(CkernelGateway kernelGateway)
        {
            ReloadList(); //May be we do not need this
        }

        public void ReloadList()
        {
            InterfaceServer[] servers = mKernlGateway.GetServerList();
            //listViewServers.Items.Clear();
            //if (servers == null) return;
            //listViewServers.ItemsSource = Servers;


        }
        private void m_CheckPriority()
        {
            //PriorityMenu.MenuItems[0].Checked = false;
            //PriorityMenu.MenuItems[1].Checked = false;
            //PriorityMenu.MenuItems[2].Checked = false;
            //if (SelectedItems.Count == 0) return;
            //if (SelectedItems.Count > 1) return;
            InterfaceServer server = (InterfaceServer)listViewServers.SelectedItems[0];
            byte index = 1;
            switch (server.Priority)
            {
                case Constants.ServerPriority.High:
                    index = 0;
                    break;
                case Constants.ServerPriority.Normal:
                    index = 1;
                    break;
                case Constants.ServerPriority.Low:
                    index = 2;
                    break;
                default:
                    index = 1;
                    break;
            }
            //PriorityMenu.MenuItems[index].Checked = true;
        }
        #endregion

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            //ContextMenu.MenuItems[ContextMenuServers.MenuItems.IndexOf(ConnectMenu)].DefaultItem = true;
            //m_CheckPriority();
            //if (SelectedItems.Count == 0)
            //{
            //    ContextMenu.MenuItems[ContextMenuServers.MenuItems.IndexOf(ConnectMenu)].Enabled = false;
            //    ContextMenu.MenuItems[ContextMenuServers.MenuItems.IndexOf(DeleteMenu)].Enabled = false;
            //    ContextMenu.MenuItems[ContextMenuServers.MenuItems.IndexOf(PriorityMenu)].Enabled = false;
            //    ContextMenu.MenuItems[ContextMenuServers.MenuItems.IndexOf(CopyLinkMenu)].Enabled = false;
            //    ContextMenu.MenuItems[ContextMenuServers.MenuItems.IndexOf(CopyLinkHTMLMenu)].Enabled = false;
            //    PriorityMenu.MenuItems[PriorityMenu.MenuItems.IndexOf(HighPriorityMenu)].Enabled = false;
            //    PriorityMenu.MenuItems[PriorityMenu.MenuItems.IndexOf(NormalPriorityMenu)].Enabled = false;
            //    PriorityMenu.MenuItems[PriorityMenu.MenuItems.IndexOf(LowPriorityMenu)].Enabled = false;
            //}
            //if (SelectedItems.Count == 1)
            //{
            //    ContextMenu.MenuItems[ContextMenuServers.MenuItems.IndexOf(ConnectMenu)].Enabled = true;
            //    ContextMenu.MenuItems[ContextMenuServers.MenuItems.IndexOf(DeleteMenu)].Enabled = true;
            //    ContextMenu.MenuItems[ContextMenuServers.MenuItems.IndexOf(PriorityMenu)].Enabled = true;
            //    ContextMenu.MenuItems[ContextMenuServers.MenuItems.IndexOf(CopyLinkMenu)].Enabled = true;
            //    ContextMenu.MenuItems[ContextMenuServers.MenuItems.IndexOf(CopyLinkHTMLMenu)].Enabled = true;
            //    PriorityMenu.MenuItems[PriorityMenu.MenuItems.IndexOf(HighPriorityMenu)].Enabled = true;
            //    PriorityMenu.MenuItems[PriorityMenu.MenuItems.IndexOf(NormalPriorityMenu)].Enabled = true;
            //    PriorityMenu.MenuItems[PriorityMenu.MenuItems.IndexOf(LowPriorityMenu)].Enabled = true;
            //}
            //if (SelectedItems.Count > 1)
            //{
            //    ContextMenu.MenuItems[ContextMenuServers.MenuItems.IndexOf(ConnectMenu)].Enabled = false;
            //    ContextMenu.MenuItems[ContextMenuServers.MenuItems.IndexOf(DeleteMenu)].Enabled = true;
            //    ContextMenu.MenuItems[ContextMenuServers.MenuItems.IndexOf(PriorityMenu)].Enabled = true;
            //    ContextMenu.MenuItems[ContextMenuServers.MenuItems.IndexOf(CopyLinkMenu)].Enabled = false;
            //    ContextMenu.MenuItems[ContextMenuServers.MenuItems.IndexOf(CopyLinkHTMLMenu)].Enabled = false;
            //    PriorityMenu.MenuItems[PriorityMenu.MenuItems.IndexOf(HighPriorityMenu)].Enabled = true;
            //    PriorityMenu.MenuItems[PriorityMenu.MenuItems.IndexOf(NormalPriorityMenu)].Enabled = true;
            //    PriorityMenu.MenuItems[PriorityMenu.MenuItems.IndexOf(LowPriorityMenu)].Enabled = true;
            //}
        }
    }
}
