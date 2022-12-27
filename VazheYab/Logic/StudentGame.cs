using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VazheYab.LevelData;

namespace VazheYab.Logic
{
    public enum WordLanguage
    {
        Persian,English
    };

    public delegate void GoalWordChangedEventHandler(object sender, WordLanguage language);

    public class StudentGame : VazheGame
    {
        public TimeSpan TotalTime { get; private set; }

        public string GoalWord
        {
            get { return AllWords[_CurrentWordIndex]; }
        }

        protected int _CurrentWordIndex;
        protected string[] AllWords { get; private set; }


        public StudentGame(StudentgameData data)
        {
            AllWords = new string[data.ExpectedPairs.Count*2];
            for (int i = 0; i < data.ExpectedPairs.Count; i++)
            {
                AllWords[2*i] = data.ExpectedPairs[i].Persian;
                AllWords[2*i + 1] = data.ExpectedPairs[i].English;
            }

            TotalTime = data.TimeLimit.Value;

            MaxScore = data.MaxScore;
            PassingScore = data.PassingScore;
        }

        public void Start()
        {
            _CurrentWordIndex = 0;
            Score = 0;
            OnGoalWordChanged(WordLanguage.Persian);
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
                OnGoalWordChanged(_CurrentWordIndex % 2 == 0 ? WordLanguage.Persian : WordLanguage.English);


                return WordFeedback.Accepted;
            }
            else
            {
                Score--;

                return WordFeedback.Rejected;
            }
        }

        public event GoalWordChangedEventHandler GoalWordChanged;
        protected virtual void OnGoalWordChanged(WordLanguage language)
        {
            if (GoalWordChanged != null)
                GoalWordChanged(this, language);
        }
    }
}
