#pragma checksum "C:\Users\MyDear\Documents\Visual Studio 2013\Projects\VazheYab\VazheYab\Controls\TileControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D3CF0371DEEE5E91E16E95D9DD87166A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace VazheYab {
    
    
    public partial class TileControl : System.Windows.Controls.Primitives.ButtonBase {
        
        internal System.Windows.Controls.Primitives.ButtonBase tileControl;
        
        internal System.Windows.Media.Animation.Storyboard TileAnimation;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock _Title;
        
        internal System.Windows.Controls.Grid Desc;
        
        internal System.Windows.Controls.TextBlock _Tooltip;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/VazheYab;component/Controls/TileControl.xaml", System.UriKind.Relative));
            this.tileControl = ((System.Windows.Controls.Primitives.ButtonBase)(this.FindName("tileControl")));
            this.TileAnimation = ((System.Windows.Media.Animation.Storyboard)(this.FindName("TileAnimation")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this._Title = ((System.Windows.Controls.TextBlock)(this.FindName("_Title")));
            this.Desc = ((System.Windows.Controls.Grid)(this.FindName("Desc")));
            this._Tooltip = ((System.Windows.Controls.TextBlock)(this.FindName("_Tooltip")));
        }
    }
}

