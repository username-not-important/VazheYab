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
using VazheYab.Controls.Models;
using VazheYab.LevelData;

namespace VazheYab
{
    public partial class LevelsPage : PhoneApplicationPage
    {
        private GameDataSettingsFactory _factory;

        public LevelsPage()
        {
            InitializeComponent();
            Switcher.Opacity = 0;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string game = "";
            if (NavigationContext.QueryString.TryGetValue("game", out game))
            {
                _factory = new GameDataSettingsFactory(game);
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            Switcher.Opacity = 0;

            Storyboard startAnim = Resources["StartAnimation"] as Storyboard;
            startAnim.Begin();

            PopulateLevels();
        }

        private void PopulateLevels()
        {
            if (_factory == null)
                return;

            var settings = _factory.GetLevelsCleared();

            int levelsCleared = Int32.Parse(settings.Value);
            int levelsCount = _factory.GetLevelsCount();
            int levelsToShow = Math.Min(levelsCleared + 1, levelsCount);

            LevelModel[] levels = new LevelModel[levelsCount];
            for (int i = 1; i <= levelsCount; i++)
                levels[i - 1] = new LevelModel(i, i > levelsToShow);

            _LevelList.ItemsSource = null;
            _LevelList.ItemsSource = levels;
        }

        private void _LevelList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_LevelList.SelectedItem == null)
                return;

            var level = _LevelList.SelectedItem as LevelModel;

            if (level.IsLocked)
                return;

            NavigationService.Navigate(new Uri(@"/Pages/Games/" + _factory.GameType + "GamePage.xaml?Level=" + level.Number, UriKind.Relative));
        }
    }
}