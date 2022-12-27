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
    public partial class HardcoreGamePage : PhoneApplicationPage
    {
        private HardcoreGame game;
        private int currentLevel;
        private bool inGame;

        public HardcoreGamePage()
        {
            InitializeComponent();

            _TimerControl.UILoaded += (o, args) =>
            {
                _TimerControl.Start();

                _GameStartSound.Play();
            };

            _TimerControl.TimerFinished += _TimerControl_TimerFinished;

            _Board.SelectedWordChanged += _board_SelectedWordChanged;
            _Board.WordSubmitted += _board_WordSubmitted;
        }

        void _TimerControl_TimerFinished(object sender, EventArgs e)
        {
            game_GameEnd(this, game.Score);
        }

        void game_GameEnd(object sender, int score)
        {
            _TextResultScore.Text = score.ToString() + "/" + game.MaxScore;

            _ButtonRetry.Visibility = Visibility.Collapsed;
            _ButtonNext.Visibility = Visibility.Collapsed;

            ProgressSettingsDataContext db = App.DB;
            var levelsClearedSetting = GetLevelsCleared();

            if (score >= game.PassingScore)
            {
                _TextMessage.Text = "آفرین";

                if (currentLevel != HardcoregameData.LevelsCount)
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

                if (currentLevel != FindergameData.LevelsCount && Int32.Parse(levelsClearedSetting.Value) >= currentLevel)
                    _ButtonNext.Visibility = Visibility.Visible;
            }

            Storyboard showResultsStoryboard = Resources["ShowResultsStoryboard"] as Storyboard;
            showResultsStoryboard.Begin();
        }

        private ProgressSettings GetLevelsCleared()
        {
            ProgressSettingsDataContext db = App.DB;

            var levelsClearedSetting = (from ProgressSettings ps in db.Items
                                        where ps.Key == "Hardcoregame_LevelsCleared"
                                        select ps).First();

            return levelsClearedSetting;
        }

        private int GWCounter = 0;
        void game_GoalWordChanged(object sender, EventArgs e)
        {
            Style goalWordStyle = Resources["GoalWordStyle"] as Style;
            
            _WordStack.Children.Clear();

            foreach (var word in game.GoalWords)
            {
                TextBlock b = new TextBlock();
                b.Text = word;
                b.Style = goalWordStyle;
                b.Name = "goalW" + GWCounter++;

                _WordStack.Children.Add(b);
            }
        }

        private void StartLevel(int level)
        {
            inGame = true;

            var showGameStoryboard = Resources["ShowGameStoryboard"] as Storyboard;
            showGameStoryboard.Begin();

            currentLevel = level;

            var resource = Application.GetResourceStream(new Uri(@"VazheYab;component/LevelData/HardcoregameLevels/level" + level + ".xml", UriKind.Relative));
            var reader = new StreamReader(resource.Stream, Encoding.Unicode);
            string str = reader.ReadToEnd();

            HardcoregameData data = HardcoregameData.Deserialize(str);

            game = new HardcoreGame(data);
            game.GoalWordChanged += game_GoalWordChanged;
            game.GameEnd += game_GameEnd;

            _TimerControl.LoadUI(game.TotalTime);
            _ScoreControl.Score = 0;
            _WordControl.Text = "";

            _TextMessage.Text = "";

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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (inGame)
                return;

            string level = "";
            if (NavigationContext.QueryString.TryGetValue("Level", out level))
            {
                StartLevel(Int32.Parse(level));
            }
        }

        void _board_WordSubmitted(object sender, string word)
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

        void _board_SelectedWordChanged(object sender, EventArgs e)
        {
            _WordControl.Text = _Board.SelectedWord;
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
            }
        }

    }
}