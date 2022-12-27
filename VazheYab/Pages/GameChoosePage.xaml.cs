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
    public partial class GameChoosePage : PhoneApplicationPage
    {
        public GameChoosePage()
        {
            InitializeComponent();
            Switcher.Opacity = 0;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard startAnim = Resources["StartAnimation"] as Storyboard;
            startAnim.Begin();
        }


        private void _Tile_Clicked(object sender, RoutedEventArgs e)
        {
            TileControl t = (sender as TileControl);
            if (t == null)
                return;

            string game = t.Tag.ToString();

            NavigationService.Navigate(new Uri(@"/Pages/LevelsPage.xaml?game=" + game, UriKind.Relative));
        }
    }
}