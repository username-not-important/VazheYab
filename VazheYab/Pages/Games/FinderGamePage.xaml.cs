using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using VazheYab.LevelData;
using VazheYab.Logic;
using VazheYab.ProgressData;

namespace VazheYab.Pages.Games
{
    public partial class FinderGamePage : PhoneApplicationPage
    {
        private FinderGame game;
        private int currentLevel;
        private bool inGame;

        public FinderGamePage()
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

        private void StartLevel(int level)
        {
            inGame = true;

            var showGameStoryboard = Resources["ShowGameStoryboard"] as Storyboard;
            showGameStoryboard.Begin();

            currentLevel = level;

            var resource = Application.GetResourceStream(new Uri(@"VazheYab;component/LevelData/FindergameLevels/level" + level + ".xml", UriKind.Relative));
            var reader = new StreamReader(resource.Stream, Encoding.Unicode);
            string str = reader.ReadToEnd();

            FindergameData data = FindergameData.Deserialize(str);

            game = new FinderGame(data);
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

                if (currentLevel != FindergameData.LevelsCount)
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
                                        where ps.Key == "Findergame_LevelsCleared"
                                        select ps).First();

            return levelsClearedSetting;
        }

        void game_GoalWordChanged(object sender, EventArgs e)
        {
            _TextGoal.Text = game.GoalWord;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

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

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
        }
    }
}