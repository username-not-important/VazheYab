using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VazheYab.LevelData
{
    public class ContextgameData
    {
        public static int LevelsCount { get; private set; }
        static ContextgameData()
        {
            LevelsCount = 20;
        }

        public List<string> Board { get; set; }
        public List<InlineText> Context { get; set; }

        public int MaxScore { get; set; }
        public int PassingScore { get; set; }

        public int Hints { get; set; }

        public ContextgameData ()
	    {

    	}

        public string[,] GetBoardArray()
        {
            string[,] board = new string[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    board[i, j] = Board[i][j].ToString();
                }
            }

            return board;
        }

        public string Serialize()
        {
            return TextSerializer.XmlSerializeToString(this);
        }

        public static ContextgameData Deserialize(string data)
        {
            return TextSerializer.XmlDeserializeFromString<ContextgameData>(data);
        }
    }

    public class InlineText
    {
        public string Text { get; set; }
        public bool IsForeign { get; set; }
        public string Replacement { get; set; }
        public string Hint { get; set; }

        public InlineText()
        {

        }
    }
}
