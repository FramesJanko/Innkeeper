using Caliburn.Micro;
using InnkeeperWPFUserInterface.EventModels;
using InnkeeperWPFUserInterface.Helpers;
using InnkeeperWPFUserInterface.Library.API;
using InnkeeperWPFUserInterface.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnkeeperWPFUserInterface.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName;
        private string _password;
        private IAPIHelper _apiHelper;
        private IEventAggregator _events;

        public LoginViewModel(IAPIHelper apiHelper, IEventAggregator events)
        {
            _apiHelper = apiHelper;
            _events = events;
        }        

        public string UserName
        {
            get { return _userName; }
            set 
            { 
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        private string _message;
        private string _firstName;
        private string _lastName;
        private bool _isRegisterVisible = false;

        public bool IsRegisterVisible
        {
            get { return _isRegisterVisible; }
            set
            {
                _isRegisterVisible = value;
                NotifyOfPropertyChange(() => IsRegisterVisible);
                NotifyOfPropertyChange(() => CanRegister);
            }
        }
        public string Message
        {
            get { return _message; }
            set 
            { 
                _message = value;
                NotifyOfPropertyChange(() => Message);
                NotifyOfPropertyChange(() => IsMessageVisible);
            }
        }

        public bool IsMessageVisible
        {
            get 
            {
                bool output = false;
                if(Message?.Length > 0)
                {
                    output = true;
                }
                return output; 
            }
            
        }


        public string Password
        {
            get { return _password; }
            set 
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => CanRegister);
            }
        }
        
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => CanRegister);
            }
        }

        public bool CanLogIn
        {
            get
            {
                bool output = false;

                if (UserName?.Length > 0 && Password?.Length > 0)
                    output = true;

                return output;
            }
        }

        public bool CanRegister
        {
            get
            {
                bool output = false;
                if (!IsRegisterVisible)
                {
                    output = true;
                    return output;

                }
                else
                {
                    if (UserName?.Length > 0 && Password?.Length > 0 && FirstName?.Length > 0 && LastName?.Length > 0)
                        output = true; 
                    return output;
                }
            }
        }

        public void Cancel()
        {
            UserName = "";
            Password = "";
            FirstName = "";
            LastName = "";
            IsRegisterVisible = false;
        }

        public async Task Register()
        {
            if (!IsRegisterVisible)
            {
                IsRegisterVisible = true;
            }
            else
            {
                await InitiateRegister();
            }
        }

        public async Task LogIn()
        {
            try
            {
                Message = "";
                UserLogin result = await _apiHelper.AuthenticateCustom(UserName, Password);
                //result.Id = "40d85781-3985-4f81-b27f-ae8aac34ae6e";

                //capture more information about the user
                _apiHelper.GetLoggedInUser(result);

                await _events.PublishOnUIThreadAsync(new LogOnEvent());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            
        }

        public async Task InitiateRegister()
        {
            try
            {
                Message = "";
                
                await _apiHelper.RegisterUser(UserName, Password, FirstName, LastName);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
    }
}
