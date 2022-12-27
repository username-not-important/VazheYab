using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace VazheYab
{
    public partial class BuddyControl : UserControl
    {
        private DispatcherTimer _timer;
        private bool _duringAnimation;

        private Storyboard[] _randomStoryboards;
        private Storyboard _talkStoryboard;

        public BuddyControl()
        {
            InitializeComponent();

            _randomStoryboards = new Storyboard[4];
            _randomStoryboards[0] = Resources["BlinkStoryboard"] as Storyboard;
            _randomStoryboards[1] = Resources["EyebrowStoryboard"] as Storyboard;
            _randomStoryboards[2] = Resources["LookLeftStoryboard"] as Storyboard;
            _randomStoryboards[3] = Resources["TwinkStoryboard"] as Storyboard;

            _talkStoryboard = Resources["TalkStoryboard"] as Storyboard;

            foreach (var storyboard in _randomStoryboards)
            {
                storyboard.Completed += (sender, args) => _duringAnimation = false;
            }

            _talkStoryboard.Completed += (sender, args) => _duringAnimation = false;

            _duringAnimation = false;

            _timer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 4)};
            _timer.Tick += _timer_Tick;

            _timer.Start();
        }

        public void StartOver()
        {
            _TextTalk.Text = "درود همه ی راهنمایی هارو بگیر تا آغاز کنیم";
            _duringAnimation = false;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (_duringAnimation)
                return;

            int random = (new Random().Next(_randomStoryboards.Length));
            _randomStoryboards[random].Begin();
        }

        public void Talk(string text)
        {
            text = text.Replace("\r", "").Replace("\n", "");

            _duringAnimation = true;

            _talkStoryboard.Begin();

            _TextTalk.Text = text;
        }
    }
}
