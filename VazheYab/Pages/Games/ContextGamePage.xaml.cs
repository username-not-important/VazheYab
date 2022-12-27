using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using VazheYab.LevelData;
using VazheYab.Logic;
using VazheYab.ProgressData;

namespace VazheYab.Pages.Games
{
    public partial class ContextGamePage : PhoneApplicationPage
    {
        private ContextGame game;
        private int currentLevel;
        private bool inGame;

        private int currentIndex;

        public ContextGamePage()
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

            var resource = Application.GetResourceStream(new Uri(@"VazheYab;component/LevelData/ContextgameLevels/level" + level + ".xml", UriKind.Relative));
            var reader = new StreamReader(resource.Stream, Encoding.Unicode);
            string str = reader.ReadToEnd();

            ContextgameData data = ContextgameData.Deserialize(str);

            game = new ContextGame(data);
            game.GoalWordChanged += game_GoalWordChanged;
            game.GameEnd += game_GameEnd;

            _ScoreControl.Score = 0;
            _WordControl.Text = "";
            _TextHint.Text = "";

            UpdateHintButton();

            _TextContext.Inlines.Clear();
            foreach (var inline in game.GetInlines())
            {
                (inline as Run).Text = (inline as Run).Text.Insert(0, " ") + "  ";
                _TextContext.Inlines.Add(inline);
            }

            try
            {
                _Board.Populate(data.GetBoardArray(), WordLanguage.Persian);

                game.Start();

                currentIndex = game.CurrentWordIndex;
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

                if (currentLevel != ContextgameData.LevelsCount)
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

                if (currentLevel != ContextgameData.LevelsCount && Int32.Parse(levelsClearedSetting.Value) >= currentLevel)
                    _ButtonNext.Visibility = Visibility.Visible;
            }

            Storyboard showResultsStoryboard = Resources["ShowResultsStoryboard"] as Storyboard;
            showResultsStoryboard.Begin();
            
        }

        private ProgressSettings GetLevelsCleared()
        {
            ProgressSettingsDataContext db = App.DB;

            var levelsClearedSetting = (from ProgressSettings ps in db.Items
                                        where ps.Key == "Contextgame_LevelsCleared"
                                        select ps).First();

            return levelsClearedSetting;
        }

        private void game_GoalWordChanged(object sender, EventArgs e)
        {
            int inlineIndex = game.CurrentWordIndex;

            var inline = _TextContext.Inlines[inlineIndex];
            inline.FontSize += 4;
            inline.Foreground = new SolidColorBrush(Colors.Orange);
        }

        void UpdateHintButton()
        {
            _TextGetHint.Text = "راهنمایی " + "(" + game.HintsLeft + ")";

            if (game.HintsLeft == 0)
                _GridHint.Opacity = 0.5;
        }

        void _Board_WordSubmitted(object sender, string word)
        {
            var result = game.Submit(word);

            if (result == WordFeedback.Accepted)
            {
                _CorrectWordSound.Play();
                _WordControl.AcceptWord();

                if (currentIndex != -1)
                {
                    (_TextContext.Inlines[currentIndex] as Run).FontSize -= 4;
                    (_TextContext.Inlines[currentIndex] as Run).Foreground = new SolidColorBrush(Colors.Green);
                    (_TextContext.Inlines[currentIndex] as Run).Text = word;
                }

                currentIndex = game.CurrentWordIndex;

                _TextHint.Text = "";

                if (game.HintsLeft != 0)
                    _GridHint.Opacity = 1;
            }
            else
            {
                _WrongWordSound.Play();
                _WordControl.RejectWord();

            }

            _ScoreControl.Score = game.Score;
        }

        void _Board_SelectedWordChanged(object sender, EventArgs e)
        {
            _WordControl.Text = _Board.SelectedWord;
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

        private void _ButtonHint_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (_GridHint.Opacity < 1)
                return;

            string hint = game.Hint();

            _TextHint.Text = hint;

            _GridHint.Opacity = 0.5;

            UpdateHintButton();
        }

    }
}