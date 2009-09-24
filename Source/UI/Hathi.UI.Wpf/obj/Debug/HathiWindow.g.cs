﻿#pragma checksum "..\..\HathiWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B776E0B0F72942552CE4CF94286CB527"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3074
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Hathi.UI.Wpf {
    
    
    /// <summary>
    /// HathiWindow
    /// </summary>
    public partial class HathiWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\HathiWindow.xaml"
        internal System.Windows.Controls.DockPanel Dockpanel1;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\HathiWindow.xaml"
        internal System.Windows.Controls.Border HeaderBorder;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\HathiWindow.xaml"
        internal System.Windows.Controls.DockPanel Header;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\HathiWindow.xaml"
        internal System.Windows.Controls.Border MenuBorder;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\HathiWindow.xaml"
        internal System.Windows.Controls.MenuItem ServersMenu;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\HathiWindow.xaml"
        internal System.Windows.Controls.MenuItem FriendsMenu;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\HathiWindow.xaml"
        internal System.Windows.Controls.MenuItem DownloadMenu;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\HathiWindow.xaml"
        internal System.Windows.Controls.MenuItem UploadMenu;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\HathiWindow.xaml"
        internal System.Windows.Controls.MenuItem SearchMenu;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\HathiWindow.xaml"
        internal System.Windows.Controls.MenuItem OptionsMenu;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\HathiWindow.xaml"
        internal System.Windows.Controls.MenuItem AboutMenu;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\HathiWindow.xaml"
        internal System.Windows.Controls.Border ContentWindow;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Hathi.UI.Wpf;component/hathiwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\HathiWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 6 "..\..\HathiWindow.xaml"
            ((Hathi.UI.Wpf.HathiWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Dockpanel1 = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 3:
            this.HeaderBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 4:
            this.Header = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 5:
            this.MenuBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 6:
            this.ServersMenu = ((System.Windows.Controls.MenuItem)(target));
            
            #line 16 "..\..\HathiWindow.xaml"
            this.ServersMenu.Click += new System.Windows.RoutedEventHandler(this.ServersMenu_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.FriendsMenu = ((System.Windows.Controls.MenuItem)(target));
            
            #line 17 "..\..\HathiWindow.xaml"
            this.FriendsMenu.Click += new System.Windows.RoutedEventHandler(this.OtherMenu_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.DownloadMenu = ((System.Windows.Controls.MenuItem)(target));
            
            #line 18 "..\..\HathiWindow.xaml"
            this.DownloadMenu.Click += new System.Windows.RoutedEventHandler(this.OtherMenu_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.UploadMenu = ((System.Windows.Controls.MenuItem)(target));
            
            #line 19 "..\..\HathiWindow.xaml"
            this.UploadMenu.Click += new System.Windows.RoutedEventHandler(this.OtherMenu_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.SearchMenu = ((System.Windows.Controls.MenuItem)(target));
            
            #line 20 "..\..\HathiWindow.xaml"
            this.SearchMenu.Click += new System.Windows.RoutedEventHandler(this.OtherMenu_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.OptionsMenu = ((System.Windows.Controls.MenuItem)(target));
            
            #line 21 "..\..\HathiWindow.xaml"
            this.OptionsMenu.Click += new System.Windows.RoutedEventHandler(this.OtherMenu_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.AboutMenu = ((System.Windows.Controls.MenuItem)(target));
            
            #line 22 "..\..\HathiWindow.xaml"
            this.AboutMenu.Click += new System.Windows.RoutedEventHandler(this.OtherMenu_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.ContentWindow = ((System.Windows.Controls.Border)(target));
            return;
            case 14:
            
            #line 29 "..\..\HathiWindow.xaml"
            ((System.Windows.Controls.ContextMenu)(target)).AddHandler(System.Windows.Controls.MenuItem.ClickEvent, new System.Windows.RoutedEventHandler(this.OnContextMenuItemClick));
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}