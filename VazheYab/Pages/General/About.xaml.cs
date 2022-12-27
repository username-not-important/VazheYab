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
using Microsoft.Phone.Tasks;

namespace VazheYab
{
    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            InitializeComponent();

            Storyboard startupStoryboard = Resources["StartupStoryboard"] as Storyboard;
            startupStoryboard.Begin();
            
        }

        private void _ButtonReview_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

        private void _ButtonContact_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = @"در مورد برنامه واژه یاب";
            emailComposeTask.Body = "";
            emailComposeTask.To = "ac_ali2582@yahoo.com";

            emailComposeTask.Show();

        }
    }
}