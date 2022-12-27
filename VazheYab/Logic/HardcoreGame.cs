using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VazheYab.LevelData;
using VazheYab.ProgressData;

namespace VazheYab.Logic
{
    public class HardcoreGame : VazheGame
    {
        public TimeSpan TotalTime { get; private set; }

        private List<string> _goalWords;
        public string[] GoalWords
        {
            get { return Range(_goalWords.ToArray(),0,5); }
        }

        private string[] Range(string[] array, int from, int count)
        {
            List<string> result = new List<string>();
            for (int i = from; i < Math.Min(from+count, array.Length); i++)
            {
                result.Add(array[i]);
            }

            return result.ToArray();
        }

        protected string[] AllWords { get; private set; }


        public HardcoreGame(HardcoregameData data)
        {
            AllWords = data.ExpectedWords.ToArray();
            TotalTime = data.TimeLimit.Value;

            ProgressSettingsDataContext db = App.DB;

            var dfSetting = (from ProgressSettings ps in db.Items
                             where ps.Key == "Settings_Difficulty"
                             select ps).First().Value;

            if (dfSetting == "Easy")
                TotalTime = TotalTime.Add(TimeSpan.FromSeconds(20));

            MaxScore = data.MaxScore;
            PassingScore = data.PassingScore;
        }

        public void Start()
        {
            _goalWords = AllWords.ToList();

            Score = 0;
            OnGoalWordChanged(new EventArgs());
        }

        public override WordFeedback Submit(string word)
        {
            bool found = false;
            for (int i=0; i< Math.Min(GoalWords.Length, 5); i++)
                if (GoalWords[i] == word)
                {
                    found = true;
                    break;
                }

            if (found)
            {
                Score++;

                _goalWords.Remove(word);

                if (_goalWords.Count == 0)
                {
                    OnGameEnd(Score);
                    return WordFeedback.Accepted;
                }

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
