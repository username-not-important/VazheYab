using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VazheYab.Logic
{
    public delegate void GameEndEventHandler(object sender, int score);

    public enum WordFeedback {Accepted, Rejected, Duplicate};

    public abstract class VazheGame
    {
        public int Score { get; protected set; }
        public int MaxScore { get; protected set; }
        public int PassingScore { get; protected set; }
        
        public VazheGame()
        {

        }

        public abstract WordFeedback Submit(string word);

        public event GameEndEventHandler GameEnd;

        protected virtual void OnGameEnd(int score)
        {
            if (GameEnd != null)
                GameEnd(this, score);
        }
    }
}
