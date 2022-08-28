using Caliburn.Micro;
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
		private BindingList<string> _characterList;

		public BindingList<string> CharacterList
		{
			get { return _characterList; }
			set 
			{
				_characterList = value; 
				NotifyOfPropertyChange(() => CharacterList);
			}
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
