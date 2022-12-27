using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows.Media.Animation;
using Microsoft.Phone.Shell;
using VazheYab.ProgressData;

namespace VazheYab.Pages.Settings
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
			
            Storyboard startupStoryboard = Resources["StartupStoryboard"] as Storyboard;
            startupStoryboard.Begin();

            ProgressSettingsDataContext db = App.DB;

            var bgSetting = (from ProgressSettings ps in db.Items
                             where ps.Key == "Settings_BG"
                             select ps).First();

            var dfSetting = (from ProgressSettings ps in db.Items
                             where ps.Key == "Settings_Difficulty"
                             select ps).First();

            foreach (RadioButton r in _BgCollection.Children)
            {
                if (r.Tag.ToString() == bgSetting.Value)
                {
                    r.IsChecked = true;
                    break;
                }
            }

            if (dfSetting.Value == "Easy")
                _Radio_Easy.IsChecked = true;
            else
                _Radio_Difficult.IsChecked = true;
        }

        private void BGRadio_OnClick(object sender, RoutedEventArgs e)
        {
            string newSetting = (sender as RadioButton).Tag.ToString();
            
            ProgressSettingsDataContext db = App.DB;

            var bgSetting = (from ProgressSettings ps in db.Items
                where ps.Key == "Settings_BG"
                select ps).First();

            //ImageBrush bgBrush = App.Current.Resources["AppBGBrush"] as ImageBrush;
            //bgBrush.ImageSource = (ImageSource)new ImageSourceConverter().ConvertFromString("/Images/BG/bg" + newSetting + ".jpg");

            bgSetting.Value = newSetting;

            db.SubmitChanges();
            
        }

        private void _Radio_Difficulty_Checked(object sender, RoutedEventArgs e)
        {
            ProgressSettingsDataContext db = App.DB;

            var dfSetting = (from ProgressSettings ps in db.Items
                             where ps.Key == "Settings_Difficulty"
                             select ps).First();

            if (_Radio_Easy != null)
            dfSetting.Value = _Radio_Easy.IsChecked != null && _Radio_Easy.IsChecked.Value ? "Easy" : "Hard";

            db.SubmitChanges();
        }
    }
}