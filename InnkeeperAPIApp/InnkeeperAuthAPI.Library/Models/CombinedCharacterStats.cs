using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnkeeperAuthAPI.Library.Models
{
    public class CombinedCharacterStats
    {
        public CharacterModel Character { get; set; }
        public StatsModel Stats { get; set; }
    }
}
