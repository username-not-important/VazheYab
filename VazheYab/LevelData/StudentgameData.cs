using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VazheYab.Logic;

namespace VazheYab.LevelData
{
    public class StudentgameData
    {
        public static int LevelsCount { get; private set; }
        static StudentgameData()
        {
            LevelsCount = 11;
        }

        public List<string> PersianBoard { get; set; }
        public List<string> EnglishBoard { get; set; }

        public List<WordPair> ExpectedPairs { get; set; }

        public int MaxScore { get; set; }
        public int PassingScore { get; set; }

        public TimeSpanClass TimeLimit { get; set; }

        public StudentgameData()
        {
            
        }

        public string[,] GetPersianBoardArray()
        {
            string[,] board = new string[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    board[i, j] = PersianBoard[i][j].ToString();
                }
            }

            return board;
        }

        public string[,] GetEnglishBoardArray()
        {
            string[,] board = new string[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    board[i, j] = EnglishBoard[i][j].ToString();
                }
            }

            return board;
        }

        public string Serialize()
        {
            return TextSerializer.XmlSerializeToString(this);
        }

        public static StudentgameData Deserialize(string data)
        {
            return TextSerializer.XmlDeserializeFromString<StudentgameData>(data);
        }
    }

    public class WordPair
    {
        public string English { get; set; }
        public string Persian { get; set; }

        public WordPair()
        {
            
        }

        public WordPair(string english, string persian)
        {
            English = english;
            Persian = persian;
        }
    }
}
