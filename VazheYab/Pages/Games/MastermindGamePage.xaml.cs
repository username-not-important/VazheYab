using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using VazheYab.LevelData;
using VazheYab.Logic;
using VazheYab.ProgressData;

namespace VazheYab.Pages.Games
{
    public partial class MastermindGamePage : PhoneApplicationPage
    {
        private MastermindGame game;
        private int currentLevel;
        private bool inGame;

        private int currentIndex;

        public MastermindGamePage()
        {
            InitializeComponent();

            _Board.SelectedWordChanged += _Board_SelectedWordChanged;
            _Board.WordSubmitted += _Board_WordSubmitted;
        }

        private void StartLevel(int level)
        {
            inGame = true;

            var showGameStoryboard = Resources["ShowGameStoryboard"] as Storyboard;
            showGameStoryboard.Begin();

            currentLevel = level;
            currentIndex = -1;

            var resource = Application.GetResourceStream(new Uri(@"VazheYab;component/LevelData/MastermindGameLevels/level" + level + ".xml", UriKind.Relative));
            var reader = new StreamReader(resource.Stream, Encoding.Unicode);
            string str = reader.ReadToEnd();

            MastermindgameData data = MastermindgameData.Deserialize(str);

            game = new MastermindGame(data);
            game.ReactionRequired += game_ReactionRequired;
            game.GameEnd += game_GameEnd;

            _WordControl.Text = "";
            _BuddyControl.StartOver();

            _GridHint.Opacity = 1;

            UpdateHintButton();

            try
            {
                _Board.Populate(data.GetBoardArray(), WordLanguage.Persian);

                game.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        void UpdateHintButton()
        {
            _TextGetHint.Text = "راهنمایی " + "(" + game.HintsLeft + ")";
        }

        void game_ReactionRequired(object sender, string reaction)
        {
            _BuddyControl.Talk(reaction);
        }

        private void _Board_WordSubmitted(object sender, string word)
        {
            var result = game.Submit(word);

            if (result == WordFeedback.Accepted)
            {
                _CorrectWordSound.Play();
                _WordControl.AcceptWord();
            }
            else
            {
                _WrongWordSound.Play();
                _WordControl.RejectWord();

            }
        }

        private void _Board_SelectedWordChanged(object sender, EventArgs e)
        {
            _WordControl.Text = _Board.SelectedWord;
        }

        private void _ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            currentLevel++;
            StartLevel(currentLevel);
        }

        private void _ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.GoBack();
            }
            catch (Exception)
            {
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ResultPanel.Visibility = Visibility.Collapsed;

            if (inGame)
                return;

            string level = "";
            if (NavigationContext.QueryString.TryGetValue("Level", out level))
            {
                StartLevel(Int32.Parse(level));
            }
        }

        private void _ButtonHint_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (_GridHint.Opacity < 1)
                return;

            string hint = game.Hint();

            _BuddyControl.Talk(hint);

            if (game.HintsLeft == 0)
                _GridHint.Opacity = 0.5;

            UpdateHintButton();
        }

        private ProgressSettings GetLevelsCleared()
        {
            ProgressSettingsDataContext db = App.DB;

            var levelsClearedSetting = (from ProgressSettings ps in db.Items
                                        where ps.Key == "MastermindGame_LevelsCleared"
                                        select ps).First();

            return levelsClearedSetting;
        }

        private void game_GameEnd(object sender, int score)
        {
            _TextResultScore.Text = score + "/" + game.MaxScore;

            _ButtonNext.Visibility = Visibility.Collapsed;

            ProgressSettingsDataContext db = App.DB;
            var levelsClearedSetting = GetLevelsCleared();

            _TextMessage.Text = "آفرین";

            if (currentLevel != MastermindgameData.LevelsCount)
                _ButtonNext.Visibility = Visibility.Visible;
            else
                _TextMessage.Text += "\n" + "این واپسین گام بود";

            if (Int32.Parse(levelsClearedSetting.Value) < currentLevel)
            {
                levelsClearedSetting.Value = currentLevel.ToString();
                db.SubmitChanges();
            }


            if (currentLevel != MastermindgameData.LevelsCount && Int32.Parse(levelsClearedSetting.Value) >= currentLevel)
                _ButtonNext.Visibility = Visibility.Visible;

            Storyboard showResultsStoryboard = Resources["ShowResultsStoryboard"] as Storyboard;
            showResultsStoryboard.Begin();

        }
    }
}