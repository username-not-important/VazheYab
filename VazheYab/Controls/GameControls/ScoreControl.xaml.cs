using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace VazheYab
{
    public partial class ScoreControl : UserControl
    {
        private static readonly DependencyProperty ScoreProperty;

        static ScoreControl()
        {
            ScoreProperty = DependencyProperty.Register("Score", typeof(int), typeof(ScoreControl), new PropertyMetadata(0, PropertyCallback));
        }

        private static void PropertyCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue)
                return;

            ScoreControl sender = d as ScoreControl;

            Storyboard pt1 = sender.Resources["ScoreUpdatePt1Storyboard"] as Storyboard;
            pt1.Completed += (S, E) =>
            {
                sender._TextScore.Text = e.NewValue.ToString();

                Storyboard pt2 = ((int)e.NewValue > (int)e.OldValue) ? sender.Resources["ScoreUpdatePt2UpStoryboard"] as Storyboard :
                                                                       sender.Resources["ScoreUpdatePt2DownStoryboard"] as Storyboard;

                pt2.Begin();
            };

            pt1.Begin();
        }

        public ScoreControl()
        {
            InitializeComponent();
        }

        public int Score
        {
            get { return (int)GetValue(ScoreProperty); }
            set
            {
                SetValue(ScoreProperty, value);
            }
        }
    }
}
