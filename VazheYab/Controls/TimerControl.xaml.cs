using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace VazheYab.Controls
{


    public partial class TimerControl : UserControl
    {
        private enum TimerState
        {
            Unloaded, Paused, Running, Finished
        }

        public TimeSpan TotalTime { get; private set; }
        public double TimeLeft
        {
            get
            {
                return TotalTime.TotalSeconds;
            }
        }

        private TimerState _state;
        private DispatcherTimer _timer;


        public TimerControl()
        {
            InitializeComponent();

            _state = TimerState.Unloaded;
            _timer = new DispatcherTimer();
            _timer.Tick += TimerOnTick;
            _timer.Interval = new TimeSpan(0, 0, 1);
            
            _TextTime.Opacity = 0;
            Clock.Opacity = 0;

        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            TotalTime = TotalTime.Subtract(new TimeSpan(0, 0, 1));

            if (TimeLeft == 0)
            {
                _timer.Stop();
                _state = TimerState.Finished;

                OnTimerFinished(new EventArgs());
            }

            UpdateText();
        }

        private void UpdateText()
        {
            _TextTime.Text = TotalTime.ToString(@"mm\:ss");
        }

        public void LoadUI(TimeSpan totalTime)
        {
            TotalTime = totalTime;
            UpdateText();

            Storyboard startupStoryboard = Resources["StartupStoryboard"] as Storyboard;
            if (startupStoryboard == null) return;

            startupStoryboard.Completed += (sender, args) =>
            {
                _state = TimerState.Paused;
                OnUILoaded(new EventArgs());
            };

            startupStoryboard.Begin();
        }

        public void Start()
        {
            _state = TimerState.Running;
            _timer.Start();
        }

        public void Pause()
        {
            _state = TimerState.Paused;
            _timer.Stop();
        }
        
        #region Events

        public event EventHandler UILoaded;
        protected virtual void OnUILoaded(EventArgs e)
        {
            if (UILoaded != null)
                UILoaded(this, e);
        }

        public event EventHandler TimerFinished;
        protected virtual void OnTimerFinished(EventArgs e)
        {
            if (TimerFinished != null)
                TimerFinished(this, e);
        }

        #endregion

    }
}
