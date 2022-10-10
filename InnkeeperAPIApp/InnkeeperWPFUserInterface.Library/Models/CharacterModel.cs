using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnkeeperWPFUserInterface.Library.Models
{
    public class CharacterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CharacterClass { get; set; }
        public string Race { get; set; }
        public int Level { get; set; }
        public string UserId { get; set; }
        public int StatsId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        private CharacterClass _characterClassName;
        public CharacterClass CharacterClassName 
        {
            get
            {
                _characterClassName = new CharacterClass(CharacterClass);
                return _characterClassName;
            }
            set
            {
                _characterClassName = value;
            }
        }

        private int _proficiencyBonus;

        public int ProficiencyBonus 
        {
            get
            {
                int proficiency = 2;
                _proficiencyBonus = proficiency + ((Level - 1) / 4);
                return _proficiencyBonus;
            }
            set
            {
                _proficiencyBonus = value;
            }
        }

        
    }
}
