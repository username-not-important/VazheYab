using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VazheYab.ProgressData
{
    public class ProgressSettingsDataContext : DataContext
    {
        public Table<ProgressSettings> Items;

        public ProgressSettingsDataContext(string connectionString) : base(connectionString)
        {

        }
    }
}
