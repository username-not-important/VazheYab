#pragma checksum "C:\Users\MyDear\Documents\Visual Studio 2013\Projects\VazheYab\VazheYab\Controls\GameControls\FlipBoardView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C3F51061FF638AE25885C2AC82344909"
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
using VazheYab.Controls.GameControls;


namespace VazheYab {
    
    
    public partial class FlipBoardView : System.Windows.Controls.UserControl {
        
        internal System.Windows.Media.Animation.Storyboard GoEngStoryboard;
        
        internal System.Windows.Media.Animation.Storyboard GoPerStoryboard;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal VazheYab.Controls.GameControls.BoardView PersianBoard;
        
        internal VazheYab.Controls.GameControls.BoardView EnglishBoard;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/VazheYab;component/Controls/GameControls/FlipBoardView.xaml", System.UriKind.Relative));
            this.GoEngStoryboard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("GoEngStoryboard")));
            this.GoPerStoryboard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("GoPerStoryboard")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.PersianBoard = ((VazheYab.Controls.GameControls.BoardView)(this.FindName("PersianBoard")));
            this.EnglishBoard = ((VazheYab.Controls.GameControls.BoardView)(this.FindName("EnglishBoard")));
        }
    }
}

