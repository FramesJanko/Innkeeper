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
        private BindingList<CharacterModel> _characterList;

		public BindingList<CharacterModel> CharacterList
		{
			get { return _characterList; }
			set 
			{
				_characterList = value; 
				NotifyOfPropertyChange(() => CharacterList);
			}
		}

        public HomeViewModel(ICharacterEndpoint characterEndpoint)
        {
            _characterEndpoint = characterEndpoint;
            
        }

        protected async override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadCharacters();
        }

        public async Task LoadCharacters()
        {

            var characterList = await _characterEndpoint.GetAll();
            CharacterList = new BindingList<CharacterModel>(characterList);
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
    }
}
