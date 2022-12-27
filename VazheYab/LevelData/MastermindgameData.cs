using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VazheYab.LevelData
{
    public class MastermindgameData
    {
        public static int LevelsCount { get; private set; }
        static MastermindgameData()
        {
            LevelsCount = 11;
        }

        public string SecretWord { get; set; }

        public List<string> Board { get; set; }
        public List<string> Hints { get; set; }
        public List<WordReaction> Reactions { get; set; }

        public MastermindgameData()
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

        public static MastermindgameData Deserialize(string data)
        {
            return TextSerializer.XmlDeserializeFromString<MastermindgameData>(data);
        }
    }

    public class WordReaction
    {
        public string Word { get; set; }
        public string Reaction { get; set; }

        public WordReaction()
        {

        }

        public WordReaction(string word, string reaction)
        {
            Word = word;
            Reaction = reaction;
        }
    }
}
