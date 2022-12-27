using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VazheYab.LevelData;
using VazheYab.ProgressData;

namespace VazheYab.Logic
{
    public class FinderGame : VazheGame
    {
        public TimeSpan TotalTime { get; private set; }

        public string GoalWord
        {
            get { return AllWords[_CurrentWordIndex]; }
        }

        protected int _CurrentWordIndex;
        protected string[] AllWords { get; private set; }


        public FinderGame(FindergameData data)
        {
            AllWords = data.ExpectedWords.ToArray();
            TotalTime = data.TimeLimit.Value;

            ProgressSettingsDataContext db = App.DB;

            var dfSetting = (from ProgressSettings ps in db.Items
                             where ps.Key == "Settings_Difficulty"
                             select ps).First().Value;

            if (dfSetting == "Easy")
                TotalTime = TotalTime.Add(TimeSpan.FromSeconds(10));

            MaxScore = data.MaxScore;
            PassingScore = data.PassingScore;
        }

        public void Start()
        {
            _CurrentWordIndex = 0;
            Score = 0;
            OnGoalWordChanged(new EventArgs());
        }

        public override WordFeedback Submit(string word)
        {
            if (word == GoalWord)
            {
                Score++;

                if (_CurrentWordIndex == AllWords.Length - 1)
                {
                    OnGameEnd(Score);
                    return WordFeedback.Accepted;
                }

                _CurrentWordIndex++;
                OnGoalWordChanged(new EventArgs());


                return WordFeedback.Accepted;
            }
            else
            {
                Score--;

                return WordFeedback.Rejected;
            }
        }

        public event EventHandler GoalWordChanged;
        protected virtual void OnGoalWordChanged(EventArgs e)
        {
            if (GoalWordChanged != null)
                GoalWordChanged(this, e);
        }
    }
}
