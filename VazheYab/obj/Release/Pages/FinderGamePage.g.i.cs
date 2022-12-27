﻿#pragma checksum "C:\Users\MyDear\Documents\Visual Studio 2013\Projects\VazheYab\VazheYab\Pages\FinderGamePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B4C58B77A24464F496CA1DE827A5BC96"
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
using VazheYab;
using VazheYab.Controls;
using VazheYab.Controls.GameControls;


namespace VazheYab.Pages {
    
    
    public partial class FinderGamePage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Media.Animation.Storyboard ShowResultsStoryboard;
        
        internal System.Windows.Media.Animation.Storyboard ShowGameStoryboard;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.MediaElement _CorrectWordSound;
        
        internal System.Windows.Controls.MediaElement _GameStartSound;
        
        internal System.Windows.Controls.MediaElement _WrongWordSound;
        
        internal System.Windows.Controls.Grid patternBG;
        
        internal System.Windows.Controls.Grid grid;
        
        internal System.Windows.Controls.Image logoSmall;
        
        internal System.Windows.Controls.TextBlock _Title;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal VazheYab.Controls.TimerControl _TimerControl;
        
        internal VazheYab.ScoreControl _ScoreControl;
        
        internal VazheYab.Controls.GameControls.WordView _WordControl;
        
        internal System.Windows.Controls.TextBlock _TextGoal;
        
        internal System.Windows.Controls.Grid _GameGrid;
        
        internal VazheYab.Controls.GameControls.BoardView _Board;
        
        internal System.Windows.Controls.Grid ResultPanel;
        
        internal System.Windows.Controls.TextBlock _TextResultScore;
        
        internal System.Windows.Controls.TextBlock _TextMessage;
        
        internal System.Windows.Controls.Button _ButtonRetry;
        
        internal System.Windows.Controls.Button _ButtonNext;
        
        internal System.Windows.Controls.Button _ButtonBack;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/VazheYab;component/Pages/FinderGamePage.xaml", System.UriKind.Relative));
            this.ShowResultsStoryboard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("ShowResultsStoryboard")));
            this.ShowGameStoryboard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("ShowGameStoryboard")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this._CorrectWordSound = ((System.Windows.Controls.MediaElement)(this.FindName("_CorrectWordSound")));
            this._GameStartSound = ((System.Windows.Controls.MediaElement)(this.FindName("_GameStartSound")));
            this._WrongWordSound = ((System.Windows.Controls.MediaElement)(this.FindName("_WrongWordSound")));
            this.patternBG = ((System.Windows.Controls.Grid)(this.FindName("patternBG")));
            this.grid = ((System.Windows.Controls.Grid)(this.FindName("grid")));
            this.logoSmall = ((System.Windows.Controls.Image)(this.FindName("logoSmall")));
            this._Title = ((System.Windows.Controls.TextBlock)(this.FindName("_Title")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this._TimerControl = ((VazheYab.Controls.TimerControl)(this.FindName("_TimerControl")));
            this._ScoreControl = ((VazheYab.ScoreControl)(this.FindName("_ScoreControl")));
            this._WordControl = ((VazheYab.Controls.GameControls.WordView)(this.FindName("_WordControl")));
            this._TextGoal = ((System.Windows.Controls.TextBlock)(this.FindName("_TextGoal")));
            this._GameGrid = ((System.Windows.Controls.Grid)(this.FindName("_GameGrid")));
            this._Board = ((VazheYab.Controls.GameControls.BoardView)(this.FindName("_Board")));
            this.ResultPanel = ((System.Windows.Controls.Grid)(this.FindName("ResultPanel")));
            this._TextResultScore = ((System.Windows.Controls.TextBlock)(this.FindName("_TextResultScore")));
            this._TextMessage = ((System.Windows.Controls.TextBlock)(this.FindName("_TextMessage")));
            this._ButtonRetry = ((System.Windows.Controls.Button)(this.FindName("_ButtonRetry")));
            this._ButtonNext = ((System.Windows.Controls.Button)(this.FindName("_ButtonNext")));
            this._ButtonBack = ((System.Windows.Controls.Button)(this.FindName("_ButtonBack")));
        }
    }
}
