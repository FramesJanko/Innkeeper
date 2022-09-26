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
		private ICharacterEndpoint _characterEndpoint;
        private IStatsEndpoint _statsEndpoint;
        private BindingList<CharacterModel> _characterList;
        private CharacterModel _selectedCharacter;
        private BindingList<StatsModel> _statsList;
        private StatsModel _selectedStats;
        private int _proficiencyBonus;
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
                    if(StatsList[i].Id == SelectedCharacter.StatsId)
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
        public HomeViewModel(ICharacterEndpoint characterEndpoint, IStatsEndpoint statsEndpoint)
        {
            _characterEndpoint = characterEndpoint;
            _statsEndpoint = statsEndpoint;
            
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
                string strSaveSign = isPositive ? "+ " : "- ";
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
                string saveSign = isPositive ? "+ " : "- ";
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
                string saveSign = isPositive ? "+ " : "- ";
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
                string saveSign = isPositive ? "+ " : "- ";
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
                string saveSign = isPositive ? "+ " : "- ";
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
                string saveSign = isPositive ? "+ " : "- ";
                string saveModifier = SelectedCharacter != null ? $"{save}" : "";
                string viewStatChaSave = saveSign + saveModifier;
                return viewStatChaSave;
            }
        }
    }
}
