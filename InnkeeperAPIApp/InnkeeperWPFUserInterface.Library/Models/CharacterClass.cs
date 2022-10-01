using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnkeeperWPFUserInterface.Library.Models
{
    public class CharacterClass
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string savingthrow1;

        public string SavingThrow1
        {
            get { return savingthrow1; }
            set { savingthrow1 = value; }
        }
        private string savingthrow2;

        public string SavingThrow2
        {
            get { return savingthrow2; }
            set { savingthrow2 = value; }
        }

        public CharacterClass(string className)
        {
            Name = className;
            switch (className)
            {
                case "Artificer":
                    SavingThrow1 = "ConstitutionSave";
                    SavingThrow2 = "IntelligenceSave";
                    break;
                case "Bard":
                    SavingThrow1 = "DexteritySave";
                    SavingThrow2 = "CharismaSave";
                    break;
                case "Barbarian":
                    SavingThrow1 = "StrengthSave";
                    SavingThrow2 = "ConstitutionSave";
                    break;
                case "Cleric":
                    SavingThrow1 = "WisdomSave";
                    SavingThrow2 = "CharismaSave";
                    break;
                case "Druid":
                    SavingThrow1 = "WisdomSave";
                    SavingThrow2 = "IntelligenceSave";
                    break;
                case "Fighter":
                    SavingThrow1 = "StrengthSave";
                    SavingThrow2 = "ConstitutionSave";
                    break;
                case "Monk":
                    SavingThrow1 = "Strengthave";
                    SavingThrow2 = "DexteritySave";
                    break;
                case "Paladin":
                    SavingThrow1 = "WisdomSave";
                    SavingThrow2 = "CharismaSave";
                    break;
                case "Ranger":
                    SavingThrow1 = "StrengthSave";
                    SavingThrow2 = "DexteritySave";
                    break;
                case "Rogue":
                    SavingThrow1 = "DexteritySave";
                    SavingThrow2 = "IntelligenceSave";
                    break;
                case "Sorcerer":
                    SavingThrow1 = "ConstitutionSave";
                    SavingThrow2 = "CharismaSave";
                    break;
                case "Warlock":
                    SavingThrow1 = "WisdomSave";
                    SavingThrow2 = "CharismaSave";
                    break;
                case "Wizard":
                    SavingThrow1 = "WisdomSave";
                    SavingThrow2 = "IntelligenceSave";
                    break;
            }
        
        }
    }
}
