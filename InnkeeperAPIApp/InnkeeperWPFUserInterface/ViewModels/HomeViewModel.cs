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
                NotifyOfPropertyChange(() => SelectedCharacter);
                NotifyOfPropertyChange(() => IsViewCharVisible);
                NotifyOfPropertyChange(() => ViewName);
                NotifyOfPropertyChange(() => ViewRace);
                NotifyOfPropertyChange(() => ViewLevel);
                NotifyOfPropertyChange(() => ViewClass);
                NotifyOfPropertyChange(() => ViewHP);
                NotifyOfPropertyChange(() => ViewAC);
                NotifyOfPropertyChange(() => ViewSpeed);
                Console.WriteLine("Selected a character");
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
                int health = SelectedCharacter != null ? SelectedCharacter.Level * 4 + 8 : 0;
                string viewHP = SelectedCharacter != null ? $"Hit Points: {health}" : "";
                return viewHP;
            }
        }
        public string ViewAC
        {
            get
            {
                string viewAC = SelectedCharacter != null ? "Armor Class: 10" : "";
                return viewAC;
            }
        }
        public string ViewSpeed
        {
            get
            {
                string viewClass = SelectedCharacter != null ? "Speed: 30 ft." : "";
                return viewClass;
            }
        }
    }
}
