using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace VazheYab.Controls.GameControls
{
    public partial class WordView : UserControl
    {
        public static readonly DependencyProperty TextProperty;

        static WordView()
        {
            TextProperty = DependencyProperty.Register("Text", typeof (string), typeof (WordView), new PropertyMetadata(default(string)));
        }

        public WordView()
        {
            InitializeComponent();
        }

        public void AcceptWord()
        {
            Storyboard toCenterStoryboard = Resources["FlareGoCenterStoryboard"] as Storyboard;
            if (toCenterStoryboard == null) return;

            toCenterStoryboard.Completed += (sender, args) =>
            {
                Storyboard toEndStoryboard = Resources["FlareGoToEndStoryboard"] as Storyboard;
                if (toEndStoryboard == null) return;

                toEndStoryboard.Begin();

                this.Text = "";

            };

            toCenterStoryboard.Begin();
        }

        public void RejectWord()
        {
            Storyboard rejectStoryboard = Resources["RejectStoryboard"] as Storyboard;
            if (rejectStoryboard == null) return;

            rejectStoryboard.Completed += (sender, args) =>
            {
                this.Text = "";
            };

            rejectStoryboard.Begin();
        }

        public void RejectDuplicateWord()
        {
            Storyboard rejectDuplicateStoryboard = Resources["RejectDuplicateStoryboard"] as Storyboard;
            if (rejectDuplicateStoryboard == null) return;

            rejectDuplicateStoryboard.Completed += (sender, args) =>
            {
                this.Text = "";
            };

            rejectDuplicateStoryboard.Begin();
        }

        public void AddCharacter(string Char)
        {
            Text += Char;
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

    }
}
