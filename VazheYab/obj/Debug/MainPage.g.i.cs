﻿#pragma checksum "C:\Users\MyDear\Documents\Visual Studio 2013\Projects\VazheYab\VazheYab\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "866B544F9462D7BB45C6155C93A7A501"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
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
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Media.Animation.Storyboard StartupStoryboard;
        
        internal System.Windows.Media.Animation.Storyboard SwitchStoryboard;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid patternBG;
        
        internal System.Windows.Controls.Grid Switcher;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.Grid grid;
        
        internal System.Windows.Controls.Image image;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Button _ButtonStart;
        
        internal System.Windows.Controls.Button _ButtonHelp;
        
        internal System.Windows.Controls.Button _ButtonAbout;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/VazheYab;component/MainPage.xaml", System.UriKind.Relative));
            this.StartupStoryboard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("StartupStoryboard")));
            this.SwitchStoryboard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("SwitchStoryboard")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.patternBG = ((System.Windows.Controls.Grid)(this.FindName("patternBG")));
            this.Switcher = ((System.Windows.Controls.Grid)(this.FindName("Switcher")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.grid = ((System.Windows.Controls.Grid)(this.FindName("grid")));
            this.image = ((System.Windows.Controls.Image)(this.FindName("image")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this._ButtonStart = ((System.Windows.Controls.Button)(this.FindName("_ButtonStart")));
            this._ButtonHelp = ((System.Windows.Controls.Button)(this.FindName("_ButtonHelp")));
            this._ButtonAbout = ((System.Windows.Controls.Button)(this.FindName("_ButtonAbout")));
        }
    }
}

