using Caliburn.Micro;
using InnkeeperWPFUserInterface.Library.API;
using InnkeeperWPFUserInterface.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnkeeperWPFUserInterface.ViewModels
{
    public class HomeViewModel : Screen
    {
		private IAPIHelper _apiHelper;
        private ICharacterEndpoint _characterEndpoint;
        private IStatsEndpoint _statsEndpoint;
        private BindingList<CharacterModel> _characterList;
        private CharacterModel _selectedCharacter;
        private BindingList<StatsModel> _statsList;
        private StatsModel _selectedStats;
        private int _proficiencyBonus;
        private string _addName;
        private int _addLevel;
        private string _addRace;
        private string _addClass;
        private int _addAC;
        private int _addHP;

        public BindingList<CharacterModel> CharacterList
		{
			get { return _characterList; }
			set 
			{
				_characterList = value; 
				NotifyOfPropertyChange(() => CharacterList);
			}
		}

        public BindingList<StatsModel> StatsList
        {
            get { return _statsList; }
            set 
            { 
                _statsList = value;
                NotifyOfPropertyChange(() => StatsList); 
            }
        }

        public CharacterModel SelectedCharacter
        {
            get 
            { 
                return _selectedCharacter; 
            }
            set
            {
                _selectedCharacter = value;

                for (int i = 0; i < StatsList.Count; i++)
                {
                    if(SelectedCharacter != null && StatsList[i].Id == SelectedCharacter.StatsId)
                        SelectedStats = StatsList[i];
                }

                NotifyOfPropertyChange(() => SelectedCharacter);
                NotifyOfPropertyChange(() => IsViewCharVisible);
                NotifyOfPropertyChange(() => ViewName);
                NotifyOfPropertyChange(() => ViewRace);
                NotifyOfPropertyChange(() => ViewLevel);
                NotifyOfPropertyChange(() => ViewClass);
                
                Console.WriteLine("Selected a character");
            }
        }
        public StatsModel SelectedStats
        {
            get
            {
                return _selectedStats;
            }
            set
            {
                _selectedStats = value;

                
                NotifyOfPropertyChange(() => ViewHP);
                NotifyOfPropertyChange(() => ViewAC);
                NotifyOfPropertyChange(() => ViewSpeed);
                NotifyOfPropertyChange(() => ViewStatStr);
                NotifyOfPropertyChange(() => ViewStatDex);
                NotifyOfPropertyChange(() => ViewStatCon);
                NotifyOfPropertyChange(() => ViewStatInt);
                NotifyOfPropertyChange(() => ViewStatWis);
                NotifyOfPropertyChange(() => ViewStatCha);
                NotifyOfPropertyChange(() => ViewStatStrSave);
                NotifyOfPropertyChange(() => ViewStatDexSave);
                NotifyOfPropertyChange(() => ViewStatConSave);
                NotifyOfPropertyChange(() => ViewStatIntSave);
                NotifyOfPropertyChange(() => ViewStatWisSave);
                NotifyOfPropertyChange(() => ViewStatChaSave);

            }
        }
        public int ProficiencyBonus
        {
            get
            {
                return _proficiencyBonus;
            }
            set 
            { 
                _proficiencyBonus = value;

                NotifyOfPropertyChange(() => ProficiencyBonus);
            }
        }
        public HomeViewModel(ICharacterEndpoint characterEndpoint, IStatsEndpoint statsEndpoint, IAPIHelper apiHelper)
        {
            _characterEndpoint = characterEndpoint;
            _statsEndpoint = statsEndpoint;
            _apiHelper = apiHelper;
        }

        protected async override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadCharacters();
        }

        public async Task LoadCharacters()
        {

            var characterList = await _characterEndpoint.GetAll();
            var statsList = await _statsEndpoint.GetStatsForUser();
            
            CharacterList = new BindingList<CharacterModel>(characterList);
            StatsList = new BindingList<StatsModel>(statsList);
            

        }

		public bool CanAddCharacter
		{
			get
			{
				bool output = true;
				return output;
			}
		}
		public void AddCharacter()
		{
            SelectedCharacter = null;

		}

        public bool IsAddCharVisible
        {
            get
            {
                bool output = false;
                if (SelectedCharacter == null)
                    output = true;

                return output;
            }
        }

        public bool IsViewCharVisible
        {
            get
            {
                bool output = false;
                if (SelectedCharacter != null)
                {
                    output = true;
                }
                return output;
            }

        }

        public bool CanEditCharacter
        {
            get
            {
                bool output = true;
                //check to see if a character is selected
                return output;
            }
        }
        public void EditCharacter()
        {

        }
        public bool CanDeleteCharacter
        {
            get
            {
                bool output = true;
                //check to see if a character is selected
                return output;
            }
        }
        public void DeleteCharacter()
        {

        }
        public async Task AddButton()
        {
            CharacterModel character = new CharacterModel();
            character.Name = AddName;
            character.CharacterClass = AddClass;
            character.Race = AddRace;
            character.Level = AddLevel;
            character.UserId = _apiHelper.LoggedInUser.Id;
            await _characterEndpoint.PostCharacter(character);
        }
        public string ViewName
        {
            get 
            {
                string viewName = SelectedCharacter != null ? SelectedCharacter.Name : "";
                return viewName;
            }
        }
        public string ViewLevel
        {
            get
            {
                string viewLevel = SelectedCharacter != null ? SelectedCharacter.Level.ToString() : "";
                return viewLevel;
            }
        }
        public string ViewRace
        {
            get
            {
                string viewRace = SelectedCharacter != null ? SelectedCharacter.Race : "";
                return viewRace;
            }
        }
        public string ViewClass
        {
            get
            {
                string viewClass = SelectedCharacter != null ? $"Level {SelectedCharacter.CharacterClass}" : "";
                return viewClass;
            }
        }
        public string ViewHP
        {
            get
            {
                
                string viewHP = SelectedCharacter != null ? $"Hit Points: {SelectedStats.Health}" : "";
                return viewHP;
            }
        }
        public string ViewAC
        {
            get
            {
                string viewAC = SelectedCharacter != null ? $"Armor Class: {SelectedStats.ArmorClass}" : "";
                return viewAC;
            }
        }
        public string ViewSpeed
        {
            get
            {
                string viewClass = SelectedCharacter != null ? $"Speed: {SelectedStats.Speed} ft." : "";
                return viewClass;
            }
        }
        public string ViewStatStr
        {
            get
            {
                string viewStatStr = SelectedCharacter != null ? $"Str: {SelectedStats.Strength}" : "";
                return viewStatStr;
            }
        }
        public string ViewStatDex
        {
            get
            {
                string viewStatDex = SelectedCharacter != null ? $"Dex: {SelectedStats.Dexterity}" : "";
                return viewStatDex;
            }
        }
        public string ViewStatCon
        {
            get
            {
                string viewStatCon = SelectedCharacter != null ? $"Con: {SelectedStats.Constitution}" : "";
                return viewStatCon;
            }
        }
        public string ViewStatInt
        {
            get
            {
                string viewStatInt = SelectedCharacter != null ? $"Int: {SelectedStats.Intelligence}" : "";
                return viewStatInt;
            }
        }
        public string ViewStatWis
        {
            get
            {
                string viewStatWis = SelectedCharacter != null ? $"Wis: {SelectedStats.Wisdom}" : "";
                return viewStatWis;
            }
        }
        public string ViewStatCha
        {
            get
            {
                string viewStatCha = SelectedCharacter != null ? $"Cha: {SelectedStats.Charisma}" : "";
                return viewStatCha;
            }
        }
        public string ViewStatStrSave
        {
            get
            {
                float strSave;
                float strSaveBeforeRounding = SelectedStats != null ? (SelectedStats.Strength - 10) / 2f : 0;
                bool isPositive = strSaveBeforeRounding > 0;
                if (isPositive)
                {
                    strSave = (int)Math.Floor(Math.Abs(strSaveBeforeRounding));
                }
                else
                {
                    strSave = (int)Math.Ceiling(Math.Abs(strSaveBeforeRounding));
                }
                if ("StrengthSave" == SelectedCharacter?.CharacterClassName.SavingThrow1 || "StrengthSave" == SelectedCharacter?.CharacterClassName.SavingThrow2)
                    strSave += SelectedCharacter.ProficiencyBonus;
                string strSaveSign = (strSave >= 0) ? "+ " : "- ";
                string strSaveModifier = SelectedCharacter != null ? $"{strSave}" : "";
                string viewStatStrSave = strSaveSign + strSaveModifier;
                return viewStatStrSave;
            }
        }
        public string ViewStatDexSave
        {
            get
            {
                float save;
                float saveBeforeRounding = SelectedStats != null ? (SelectedStats.Dexterity - 10) / 2f : 0;
                bool isPositive = saveBeforeRounding > 0;
                if (isPositive)
                {
                    save = (int)Math.Floor(Math.Abs(saveBeforeRounding));
                }
                else
                {
                    save = (int)Math.Ceiling(Math.Abs(saveBeforeRounding));
                }
                if ("DexteritySave" == SelectedCharacter?.CharacterClassName.SavingThrow1 || "DexteritySave" == SelectedCharacter?.CharacterClassName.SavingThrow2)
                    save += SelectedCharacter.ProficiencyBonus;
                string saveSign = (save >= 0) ? "+ " : "- ";
                string saveModifier = SelectedCharacter != null ? $"{save}" : "";
                string viewStatDexSave = saveSign + saveModifier;
                
                return viewStatDexSave;
            }
        }
        public string ViewStatConSave
        {
            get
            {
                float save;
                float saveBeforeRounding = SelectedStats != null ? (SelectedStats.Constitution - 10) / 2f : 0;
                bool isPositive = saveBeforeRounding > 0;
                if (isPositive)
                {
                    save = (int)Math.Floor(Math.Abs(saveBeforeRounding));
                }
                else
                {
                    save = (int)Math.Ceiling(Math.Abs(saveBeforeRounding));
                }
                if ("ConstitutionSave" == SelectedCharacter?.CharacterClassName.SavingThrow1 || "ConstitutionSave" == SelectedCharacter?.CharacterClassName.SavingThrow2)
                    save += SelectedCharacter.ProficiencyBonus;
                string saveSign = (save >= 0) ? "+ " : "- ";
                string saveModifier = SelectedCharacter != null ? $"{save}" : "";
                string viewStatConSave = saveSign + saveModifier;
                return viewStatConSave;
            }
        }
        public string ViewStatIntSave
        {
            get
            {
                float save;
                float saveBeforeRounding = SelectedStats != null ? (SelectedStats.Intelligence - 10) / 2f : 0;
                bool isPositive = saveBeforeRounding > 0;
                if (isPositive)
                {
                    save = (int)Math.Floor(Math.Abs(saveBeforeRounding));
                }
                else
                {
                    save = (int)Math.Ceiling(Math.Abs(saveBeforeRounding));
                }
                if ("IntelligenceSave" == SelectedCharacter?.CharacterClassName.SavingThrow1 || "IntelligenceSave" == SelectedCharacter?.CharacterClassName.SavingThrow2)
                    save += SelectedCharacter.ProficiencyBonus;
                string saveSign = (save >= 0) ? "+ " : "- ";
                string saveModifier = SelectedCharacter != null ? $"{save}" : "";
                string viewStatIntSave = saveSign + saveModifier;
                return viewStatIntSave;
            }
        }
        public string ViewStatWisSave
        {
            get
            {
                float save;
                float saveBeforeRounding = SelectedStats != null ? (SelectedStats.Wisdom - 10) / 2f : 0;
                bool isPositive = saveBeforeRounding > 0;
                if (isPositive)
                {
                    save = (int)Math.Floor(Math.Abs(saveBeforeRounding));
                }
                else
                {
                    save = (int)Math.Ceiling(Math.Abs(saveBeforeRounding));
                }
                if ("WisdomSave" == SelectedCharacter?.CharacterClassName.SavingThrow1 || "WisdomSave" == SelectedCharacter?.CharacterClassName.SavingThrow2)
                    save += SelectedCharacter.ProficiencyBonus;
                string saveSign = (save >= 0) ? "+ " : "- ";
                string saveModifier = SelectedCharacter != null ? $"{save}" : "";
                string viewStatWisSave = saveSign + saveModifier;
                return viewStatWisSave;
            }
        }
        public string ViewStatChaSave
        {
            get
            {
                float save;
                float saveBeforeRounding = SelectedStats != null ? (SelectedStats.Charisma - 10) / 2f : 0;
                bool isPositive = saveBeforeRounding > 0;
                if (isPositive)
                {
                    save = (int)Math.Floor(Math.Abs(saveBeforeRounding));
                }
                else
                {
                    save = (int)Math.Ceiling(Math.Abs(saveBeforeRounding));
                }
                if ("CharismaSave" == SelectedCharacter?.CharacterClassName.SavingThrow1 || "CharismaSave" == SelectedCharacter?.CharacterClassName.SavingThrow2)
                    save += SelectedCharacter.ProficiencyBonus;
                string saveSign = (save >= 0) ? "+ " : "- ";
                string saveModifier = SelectedCharacter != null ? $"{save}" : "";
                string viewStatChaSave = saveSign + saveModifier;
                return viewStatChaSave;
            }
        }
        public string AddName
        {
            get
            {
                return _addName;
            }
            set
            {
                if(value.Length > 0 || value.Length <= 50)
                    _addName = value;
                NotifyOfPropertyChange(() => _addName);
            }
        }
        public int AddLevel
        {
            get
            {
                return _addLevel;
            }
            set
            {
                _addLevel = value;
                NotifyOfPropertyChange(() => _addLevel);
            }
        }
        public string AddRace
        {
            get
            {
                return _addRace;
            }
            set
            {
                if (value.Length > 0 || value.Length <= 50)
                    _addRace = value;
                NotifyOfPropertyChange(() => _addRace);
            }
        }
        public string AddClass
        {
            get
            {
                return _addClass;
            }
            set
            {
                if (value.Length > 0 || value.Length <= 50)
                    _addClass = value;
                NotifyOfPropertyChange(() => _addClass);
            }
        }
        public int AddAC
        {
            get
            {
                return _addAC;
            }
            set
            {
                _addAC = value;
                NotifyOfPropertyChange(() => _addAC);
            }
        }
        public int AddHP
        {
            get
            {
                return _addHP;
            }
            set
            {
                _addHP = value;
                NotifyOfPropertyChange(() => _addHP);
            }
        }

    }
}
