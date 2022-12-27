using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using VazheYab.LevelData;

namespace VazheYab.Logic
{
    public class ContextGame : VazheGame
    {
        public int HintsLeft { get; protected set; }
        public int CurrentWordIndex
        {
            get { return GoalIndexes[_CurrentGoalIndex]; }
        }

        protected InlineText[] AllWords { get; private set; }

        protected int _CurrentGoalIndex;
        protected List<int> GoalIndexes { get; set; }

        protected string GoalWord
        {
            get { return AllWords[GoalIndexes[_CurrentGoalIndex]].Replacement; }
        }

        public ContextGame(ContextgameData data)
        {
            AllWords = data.Context.ToArray();

            GoalIndexes = new List<int>();
            for (int i = 0; i < AllWords.Length; i++)
            {
                if (AllWords[i].IsForeign)
                    GoalIndexes.Add(i);
            }

            MaxScore = data.MaxScore;
            PassingScore = data.PassingScore;
            HintsLeft = data.Hints;
        }

        public void Start()
        {
            _CurrentGoalIndex = 0;
            Score = 0;
            OnGoalWordChanged(new EventArgs());
        }

        public override WordFeedback Submit(string word)
        {
            if (word == GoalWord)
            {
                Score += HintsLeft + 1;

                if (_CurrentGoalIndex == GoalIndexes.Count - 1)
                {
                    OnGameEnd(Score);
                    return WordFeedback.Accepted;
                }

                _CurrentGoalIndex++;
                OnGoalWordChanged(new EventArgs());

                return WordFeedback.Accepted;
            }
            else
            {
                return WordFeedback.Rejected;
            }

        }

        public string Hint()
        {
            if (HintsLeft == 0)
                return "";

            HintsLeft--;

            return AllWords[CurrentWordIndex].Hint;
        }

        public List<Inline> GetInlines()
        {
            var inlines = new List<Inline>();
            foreach (var word in AllWords)
            {
                Run r = new Run();
                r.Text = word.Text;
                
                if (word.IsForeign)
                    r.TextDecorations = System.Windows.TextDecorations.Underline;

                inlines.Add(r);
            }

            return inlines;
        }

        public event EventHandler GoalWordChanged;
        protected virtual void OnGoalWordChanged(EventArgs e)
        {
            if (GoalWordChanged != null)
                GoalWordChanged(this, e);
        }
    }
}
