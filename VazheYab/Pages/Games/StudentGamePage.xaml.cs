using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using VazheYab.LevelData;
using VazheYab.Logic;
using VazheYab.ProgressData;

namespace VazheYab
{
    public partial class StudentGamePage : PhoneApplicationPage
    {
        private StudentGame game;
        private int currentLevel;
        private bool inGame;

        public StudentGamePage()
        {
            InitializeComponent();

            _TimerControl.UILoaded += (o, args) =>
            {
                _TimerControl.Start();

                _GameStartSound.Play();
            };

            _TimerControl.TimerFinished += _TimerControl_TimerFinished;

            _FlipBoard.SelectedWordChanged += _FlipBoard_SelectedWordChanged;
            _FlipBoard.WordSubmitted += _FlipBoard_WordSubmitted;
        }

        private void _TimerControl_TimerFinished(object sender, EventArgs e)
        {
            game_GameEnd(this, game.Score);
        }

        private void StartLevel(int level)
        {
            inGame = true;

            var showGameStoryboard = Resources["ShowGameStoryboard"] as Storyboard;
            showGameStoryboard.Begin();

            currentLevel = level;

            var resource = Application.GetResourceStream(new Uri(@"VazheYab;component/LevelData/StudentgameLevels/level" + level + ".xml", UriKind.Relative));
            var reader = new StreamReader(resource.Stream, Encoding.Unicode);
            string str = reader.ReadToEnd();

            StudentgameData data = StudentgameData.Deserialize(str);

            game = new StudentGame(data);
            game.GoalWordChanged += game_GoalWordChanged;
            game.GameEnd += game_GameEnd;

            _TimerControl.LoadUI(game.TotalTime);
            _ScoreControl.Score = 0;
            _WordControl.Text = "";

            try
            {
                _FlipBoard.Populate(data.GetPersianBoardArray(), data.GetEnglishBoardArray());

                game.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void game_GameEnd(object sender, int score)
        {
            _TextResultScore.Text = score.ToString() + "/" + game.MaxScore;

            _ButtonRetry.Visibility = Visibility.Collapsed;
            _ButtonNext.Visibility = Visibility.Collapsed;

            ProgressSettingsDataContext db = App.DB;
            var levelsClearedSetting = GetLevelsCleared();

            if (score >= game.PassingScore)
            {
                _TextMessage.Text = "آفرین";

                if (currentLevel != StudentgameData.LevelsCount)
                    _ButtonNext.Visibility = Visibility.Visible;
                else
                    _TextMessage.Text += "\n" + "این واپسین گام بود";

                if (score != game.MaxScore)
                {
                    _ButtonRetry.Visibility = Visibility.Visible;
                }


                if (Int32.Parse(levelsClearedSetting.Value) < currentLevel)
                {
                    levelsClearedSetting.Value = currentLevel.ToString();
                    db.SubmitChanges();
                }
            }
            else
            {
                _TextMessage.Text = "برایندتان بس نیست";
                _ButtonRetry.Visibility = Visibility.Visible;

                if (currentLevel != StudentgameData.LevelsCount && Int32.Parse(levelsClearedSetting.Value) >= currentLevel)
                    _ButtonNext.Visibility = Visibility.Visible;
            }

            Storyboard showResultsStoryboard = Resources["ShowResultsStoryboard"] as Storyboard;
            showResultsStoryboard.Begin();
        }

        private ProgressSettings GetLevelsCleared()
        {
            ProgressSettingsDataContext db = App.DB;

            var levelsClearedSetting = (from ProgressSettings ps in db.Items
                                        where ps.Key == "Studentgame_LevelsCleared"
                                        select ps).First();

            return levelsClearedSetting;
        }

        private void game_GoalWordChanged(object sender, WordLanguage language)
        {
            _TextGoal.Text = game.GoalWord;

            _FlipBoard.Flip(language);

            if (language == WordLanguage.English)
                _TextGoal.FontFamily = new FontFamily("/VazheYab;component/Fonts/Fonts.zip#Segoe UI Light");
            else
                _TextGoal.FontFamily = new FontFamily("/VazheYab;component/Fonts/Fonts.zip#MRT_Poster_15");
        }

        void _FlipBoard_WordSubmitted(object sender, string word)
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

            _ScoreControl.Score = game.Score;
        }

        void _FlipBoard_SelectedWordChanged(object sender, EventArgs e)
        {
            _WordControl.Text = _FlipBoard.SelectedWord;
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

        private void _ButtonRetry_Click(object sender, RoutedEventArgs e)
        {
            StartLevel(currentLevel);
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
                throw;
            }
        }

    }
}