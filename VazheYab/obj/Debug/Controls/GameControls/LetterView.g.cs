﻿#pragma checksum "C:\Users\MyDear\Documents\Visual Studio 2013\Projects\VazheYab\VazheYab\Controls\GameControls\LetterView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8ED1997FB9248D31E0E27068D71CCD2A"
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


namespace VazheYab.Controls.GameControls {
    
    
    public partial class LetterView : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.UserControl LetterViewControl;
        
        internal System.Windows.Media.Animation.Storyboard LoadStoryboard;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.VisualStateGroup HoldVisualStateGroup;
        
        internal System.Windows.VisualState Holded;
        
        internal System.Windows.VisualState Released;
        
        internal System.Windows.Controls.Grid grid;
        
        internal System.Windows.Controls.TextBlock _Text;
        
        internal System.Windows.Controls.TextBlock _Shadow;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/VazheYab;component/Controls/GameControls/LetterView.xaml", System.UriKind.Relative));
            this.LetterViewControl = ((System.Windows.Controls.UserControl)(this.FindName("LetterViewControl")));
            this.LoadStoryboard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("LoadStoryboard")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.HoldVisualStateGroup = ((System.Windows.VisualStateGroup)(this.FindName("HoldVisualStateGroup")));
            this.Holded = ((System.Windows.VisualState)(this.FindName("Holded")));
            this.Released = ((System.Windows.VisualState)(this.FindName("Released")));
            this.grid = ((System.Windows.Controls.Grid)(this.FindName("grid")));
            this._Text = ((System.Windows.Controls.TextBlock)(this.FindName("_Text")));
            this._Shadow = ((System.Windows.Controls.TextBlock)(this.FindName("_Shadow")));
        }
    }
}

