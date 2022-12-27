using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VazheYab.Controls.Models
{
    public class LevelModel
    {
        public int Number { get; set; }
        public bool IsLocked { get; set; }

        public LevelModel()
        {
            
        }

        public LevelModel(int number, bool isLocked)
        {
            IsLocked = isLocked;
            Number = number;
        }
    }
}
