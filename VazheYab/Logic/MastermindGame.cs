using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VazheYab.LevelData;

namespace VazheYab.Logic
{
    public delegate void ReactionRequiredEventHandler(object sender, string reaction);

    public class MastermindGame : VazheGame
    {
        protected int _CurrentHintIndex;
        protected string[] AllHints { get; private set; }
        protected Dictionary<string,string> WordReactions { get; private set; } 

        protected string SecretWord { get; private set; }

        public int HintsLeft
        {
            get { return AllHints.Length - _CurrentHintIndex; }
        }

        public MastermindGame(MastermindgameData data)
        {
            AllHints = data.Hints.ToArray();
            MaxScore = data.Hints.Count;
            SecretWord = data.SecretWord;

            WordReactions = new Dictionary<string, string>();
            foreach (WordReaction r in data.Reactions)
            {
                WordReactions.Add(r.Word, r.Reaction);
            }
        }

        public void Start()
        {
            _CurrentHintIndex = 0;
            Score = MaxScore;
        }

        public override WordFeedback Submit(string word)
        {
            if (word == SecretWord)
            {
                Score = Math.Min(HintsLeft+1,AllHints.Length);

                OnGameEnd(Score);
                return WordFeedback.Accepted;
            }
            else
            {
                if (WordReactions.ContainsKey(word))
                    OnReactionRequired(WordReactions[word]);
                else
                {
                    Random r = new Random();
                    double chance = r.NextDouble();

                    if (chance > 0.7)
                    {
                        OnReactionRequired("نه " + word + " درست نیست");
                    }
                }

                return WordFeedback.Rejected;
            }
        }

        public string Hint()
        {
            if (HintsLeft == 0)
                return "";

            return AllHints[_CurrentHintIndex++];
        }

        public event ReactionRequiredEventHandler ReactionRequired;
        protected virtual void OnReactionRequired(string reaction)
        {
            if (ReactionRequired != null)
                ReactionRequired(this, reaction);
        }
    }
}
