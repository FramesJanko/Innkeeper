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
        private CharacterModel _editedCharacter;
        private BindingList<StatsModel> _statsList;
        private StatsModel _selectedStats;
        private int _proficiencyBonus;
        private string _addName;
        private int _addLevel;
        private string _addRace;
        private string _addClass;
        private int _addAC;
        private int _addHP;
        private int _addSpeed;
        private int _addStatStr;
        private int _addStatDex;
        private int _addStatCon;
        private int _addStatInt;
        private int _addStatWis;
        private int _addStatCha;

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
                NotifyOfPropertyChange(() => CanEditCharacter);
                NotifyOfPropertyChange(() => CanRetireCharacter);
                NotifyOfPropertyChange(() => ViewName);
                NotifyOfPropertyChange(() => ViewRace);
                NotifyOfPropertyChange(() => ViewLevel);
                NotifyOfPropertyChange(() => ViewClass);
                
                Console.WriteLine("Selected a character");
            }
        }
        public CharacterModel EditedCharacter
        {
            get
            {
                return _editedCharacter;
            }
            set
            {
                _editedCharacter = value;

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
            EditedCharacter = null;

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
                bool output = false;
                //check to see if a character is selected
                if (SelectedCharacter != null)
                {
                    output = true;
                }
                return output;
            }
        }
        public void EditCharacter()
        {
            AddName = SelectedCharacter.Name;
            AddClass = SelectedCharacter.CharacterClass;
            AddRace = SelectedCharacter.Race;
            AddLevel = SelectedCharacter.Level;
            AddStatStr = SelectedStats.Strength;
            AddStatDex = SelectedStats.Dexterity;
            AddStatCon = SelectedStats.Constitution;
            AddStatInt = SelectedStats.Intelligence;
            AddStatWis = SelectedStats.Wisdom;
            AddStatCha = SelectedStats.Charisma;
            AddSpeed = SelectedStats.Speed;
            AddAC = SelectedStats.ArmorClass;
            AddHP = SelectedStats.Health;
            EditedCharacter = SelectedCharacter;
            SelectedCharacter = null;

            NotifyOfPropertyChange(() => AddName);
            NotifyOfPropertyChange(() => AddClass);
            NotifyOfPropertyChange(() => AddRace);
            NotifyOfPropertyChange(() => AddLevel);
            NotifyOfPropertyChange(() => AddStatStr);
            NotifyOfPropertyChange(() => AddStatDex);
            NotifyOfPropertyChange(() => AddStatCon);
            NotifyOfPropertyChange(() => AddStatInt);
            NotifyOfPropertyChange(() => AddStatWis);
            NotifyOfPropertyChange(() => AddStatCha);
            NotifyOfPropertyChange(() => AddSpeed);
            NotifyOfPropertyChange(() => AddAC);
            NotifyOfPropertyChange(() => AddHP);
        }
        public bool CanRetireCharacter
        {
            get
            {
                return SelectedCharacter != null;
            }
        }
        public async void RetireCharacter()
        {
            CombinedCharacterStats combined = new CombinedCharacterStats();
            SelectedCharacter.Retired = true;
            SelectedStats.ModifiedDate = DateTime.Now;
            combined.character = SelectedCharacter;
            combined.stats = SelectedStats;
            CharacterList.Remove(combined.character);
            StatsList.Remove(combined.stats);
            await _characterEndpoint.UpdateCharacter(combined);
        }
        public async Task AddButton()
        {
            if(EditedCharacter == null)
            {
                CharacterModel character = new CharacterModel();
                character.Name = AddName;
                character.CharacterClass = AddClass;
                character.Race = AddRace;
                character.Level = AddLevel;
                character.UserId = _apiHelper.LoggedInUser.Id;
                character.StatsId = StatsList.Count + 1;
                CharacterList.Add(character);
                StatsModel stats = new StatsModel();
                stats.Strength = AddStatStr;
                stats.Dexterity = AddStatDex;
                stats.Constitution = AddStatCon;
                stats.Intelligence = AddStatInt;
                stats.Wisdom = AddStatWis;
                stats.Charisma = AddStatCha;
                stats.Speed = AddSpeed;
                stats.ArmorClass = AddAC;
                stats.Health = AddHP;
                stats.UserId = _apiHelper.LoggedInUser.Id;
                stats.CreatedDate = DateTime.Now;
                stats.ModifiedDate = DateTime.Now;
                stats.Id = StatsList.Count + 1;
                StatsList.Add(stats);
                CombinedCharacterStats combined = new CombinedCharacterStats();
                combined.character = character;
                combined.stats = stats;
                await _characterEndpoint.PostCharacter(combined);
            }
            else if(EditedCharacter != null)
            {
                SelectedCharacter = EditedCharacter;
                EditedCharacter = null;
                SelectedCharacter.Name = AddName;
                SelectedCharacter.CharacterClass = AddClass;
                SelectedCharacter.Race = AddRace;
                SelectedCharacter.Level = AddLevel;
                SelectedCharacter.UserId = _apiHelper.LoggedInUser.Id;
                SelectedStats.Strength = AddStatStr;
                SelectedStats.Dexterity = AddStatDex;
                SelectedStats.Constitution = AddStatCon;
                SelectedStats.Intelligence = AddStatInt;
                SelectedStats.Wisdom = AddStatWis;
                SelectedStats.Charisma = AddStatCha;
                SelectedStats.Speed = AddSpeed;
                SelectedStats.ArmorClass = AddAC;
                SelectedStats.Health = AddHP;
                SelectedStats.UserId = _apiHelper.LoggedInUser.Id;
                SelectedStats.ModifiedDate = DateTime.Now;
                int index = CharacterList.IndexOf(SelectedCharacter);
                CharacterList[index] = SelectedCharacter;
                CombinedCharacterStats combined = new CombinedCharacterStats();
                combined.character = SelectedCharacter;
                combined.stats= SelectedStats;
                await _characterEndpoint.UpdateCharacter(combined);
            }
            
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
                string viewLevel = SelectedCharacter != null ? "Level " + SelectedCharacter.Level.ToString() : "";
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
                string viewClass = SelectedCharacter != null ? $" {SelectedCharacter.CharacterClass}" : "";
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
        public int AddSpeed
        {
            get
            {
                return _addSpeed;
            }
            set
            {
                _addSpeed = value;
                NotifyOfPropertyChange(() => _addSpeed);
            }
        }
        public int AddStatStr
        {
            get
            {
                return _addStatStr;
            }
            set
            {
                _addStatStr = value;
                NotifyOfPropertyChange(() => _addStatStr);
            }
        }
        public int AddStatDex
        {
            get
            {
                return _addStatDex;
            }
            set
            {
                _addStatDex = value;
                NotifyOfPropertyChange(() => _addStatDex);
            }
        }
        public int AddStatCon
        {
            get
            {
                return _addStatCon;
            }
            set
            {
                _addStatCon = value;
                NotifyOfPropertyChange(() => _addStatCon);
            }
        }
        public int AddStatInt
        {
            get
            {
                return _addStatInt;
            }
            set
            {
                _addStatInt = value;
                NotifyOfPropertyChange(() => _addStatInt);
            }
        }
        public int AddStatWis
        {
            get
            {
                return _addStatWis;
            }
            set
            {
                _addStatWis = value;
                NotifyOfPropertyChange(() => _addStatWis);
            }
        }
        public int AddStatCha
        {
            get
            {
                return _addStatCha;
            }
            set
            {
                _addStatCha = value;
                NotifyOfPropertyChange(() => _addStatCha);
            }
        }
    }
}
