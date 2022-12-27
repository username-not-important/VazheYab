using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using VazheYab.Logic;

namespace VazheYab.Controls.GameControls
{

    public partial class LetterView : UserControl
    {
        public static readonly DependencyProperty TextProperty;
        public static readonly DependencyProperty IsOnHoldProperty;

        static LetterView()
        {
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(LetterView),
                new PropertyMetadata("ج"));

            IsOnHoldProperty = DependencyProperty.Register("IsOnHold", typeof(bool), typeof(LetterView),
                new PropertyMetadata(false, IsOnHoldChangedCallback));
        }

        private static void IsOnHoldChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue)
                return;

            VisualStateManager.GoToState(sender as LetterView, (bool)e.NewValue == true ? "Holded" : "Released", true);
        }

        public LetterView(WordLanguage language)
        {
            InitializeComponent();

            VisualStateManager.GoToState(this, language == WordLanguage.English ? "English" : "Persian", false);
        }

        public LetterView(int delay, WordLanguage language)
            : this(language)
        {
            _delay = delay;
        }

        public string Text
        {
            get { return this.GetValue(TextProperty).ToString(); }
            set { this.SetValue(TextProperty, value); }
        }

        public bool IsOnHold
        {
            get { return (bool)this.GetValue(IsOnHoldProperty); }
            set { this.SetValue(IsOnHoldProperty, value); }
        }

        private int _delay;
        private void LetterViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard loadStoryboard = Resources["LoadStoryboard"] as Storyboard;

            if (loadStoryboard != null)
            {
                if (_delay != 0)
                    loadStoryboard.BeginTime = System.TimeSpan.FromMilliseconds(_delay);

                loadStoryboard.Begin();
            }

        }
    }
}
