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

namespace VazheYab.Pages.Help
{
    public partial class HelpFrame : PhoneApplicationPage
    {
        public HelpFrame()
        {
            InitializeComponent();

            Storyboard startupStoryboard = Resources["StartupStoryboard"] as Storyboard;
            startupStoryboard.Begin();
        }

        private void Buttons_Click(object sender, RoutedEventArgs e)
        {
            Storyboard goHelp = Resources["GoHelp"] as Storyboard;
            goHelp.Begin();
        }

        private void _CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard closeHelp = Resources["CloseHelp"] as Storyboard;
            closeHelp.Begin();
        }
    }
}