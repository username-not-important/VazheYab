using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VazheYab.ProgressData;

namespace VazheYab.LevelData
{
    public class GameDataSettingsFactory
    {
        public string GameType { get; private set; }

        public GameDataSettingsFactory(string gameType)
        {
            GameType = gameType;
        }

        public int GetLevelsCount()
        {
            if (GameType == "Finder")
                return FindergameData.LevelsCount;
            else if (GameType == "Context")
                return ContextgameData.LevelsCount;
            else if (GameType == "Mastermind")
                return MastermindgameData.LevelsCount;
            else if (GameType == "Student")
                return StudentgameData.LevelsCount;
            else if (GameType == "Hardcore")
                return HardcoregameData.LevelsCount;
            else
                return 0;
        }

        public ProgressSettings GetLevelsCleared()
        {
            ProgressSettingsDataContext db = App.DB;

            var rows = from ProgressSettings ps in db.Items
                       where ps.Key == GameType + "game_LevelsCleared"
                       select ps;

            if (rows.Count() == 0)
                return null;

            return rows.First();
        }

    }
}
