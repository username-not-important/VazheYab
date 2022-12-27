using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VazheYab.Logic;

namespace VazheYab.LevelData
{
    public class HardcoregameData
    {
        public static int LevelsCount { get; private set; }
        static HardcoregameData()
        {
            LevelsCount = 2;
        }

        public List<string[]> Board { get; set; }
        public List<string> ExpectedWords { get; set; }

        public int MaxScore { get; set; }
        public int PassingScore { get; set; }

        public TimeSpanClass TimeLimit { get; set; }

        public HardcoregameData()
        {
            
        }

        public string[,] GetBoardArray()
        {
            string[,] board = new string[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    board[i, j] = Board[i][j];
                }
            }

            return board;
        }

        public string Serialize()
        {
            return TextSerializer.XmlSerializeToString(this);
        }

        public static HardcoregameData Deserialize(string data)
        {
            return TextSerializer.XmlDeserializeFromString<HardcoregameData>(data);
        }
    }
}
