using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using VazheYab.Logic;

namespace VazheYab.LevelData
{
    public class FindergameData
    {
        public static int LevelsCount { get; private set; }
        static FindergameData()
        {
            LevelsCount = 21;
        }

        public List<string> Board { get; set; }
        public List<string> ExpectedWords { get; set; }

        public int MaxScore { get; set; }
        public int PassingScore { get; set; }

        public TimeSpanClass TimeLimit { get; set; }

        public FindergameData()
        {

        }

        public string[,] GetBoardArray()
        {
            string[,] board = new string[4, 4];

            for (int i=0; i<4; i++)
            {
                for (int j=0; j<4; j++)
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

        public static FindergameData Deserialize(string data)
        {
            return TextSerializer.XmlDeserializeFromString<FindergameData>(data);
        }
    }
}
