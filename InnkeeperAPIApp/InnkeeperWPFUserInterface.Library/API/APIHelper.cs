using InnkeeperWPFUserInterface.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InnkeeperWPFUserInterface.Library.API
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient _apiClient;
        private ILoggedInUserModel _loggedInUser;

        public APIHelper(ILoggedInUserModel loggedInUser)
        {
            InitializeClient();
            _loggedInUser = loggedInUser;

        }

        public ILoggedInUserModel LoggedInUser
        {
            get { return _loggedInUser; }
        }

        public HttpClient ApiClient 
        {
            get 
            {
                return _apiClient; 
            }
        }

        private void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];

            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)

            });

            using (HttpResponseMessage response = await _apiClient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<UserLogin> AuthenticateCustom(string username, string password)
        {
            UserLogin user = new UserLogin();
            user.Username = username;
            user.Password = password;

            using (HttpResponseMessage response = await _apiClient.PostAsJsonAsync("/api/User/Authenticate", user))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content?.ReadAsAsync<UserLogin>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task RegisterUser(string username, string password, string firstname, string lastname)
        {
            UserLogin user = new UserLogin();
            user.Username = username;
            user.Password = password;
            user.FirstName = firstname;
            user.LastName = lastname;

            using (HttpResponseMessage response = await _apiClient.PostAsJsonAsync("/api/User/Register", user))
            {
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public void GetLoggedInUser(UserLogin user)
        {
            //_apiClient.DefaultRequestHeaders.Clear();
            //_apiClient.DefaultRequestHeaders.Accept.Clear();
            //_apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            //using (HttpResponseMessage response = await _apiClient.GetAsync("api/User"))
            //{
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var result = await response.Content.ReadAsAsync<LoggedInUserModel>();
            //        _loggedInUser.CreatedDate = result.CreatedDate;
            //        _loggedInUser.FirstName = result.FirstName;
            //        _loggedInUser.LastName = result.LastName;
            //        _loggedInUser.Id = result.Id;
            //        _loggedInUser.Token = token;
            //    }
            //    else
            //    {
            //        throw new Exception(response.ReasonPhrase);
            //    }
            //}

            
            _loggedInUser.CreatedDate = user.CreatedDate;
            _loggedInUser.FirstName = user.FirstName;
            _loggedInUser.LastName = user.LastName;
            _loggedInUser.UserName = user.Username;
            _loggedInUser.Id = user.Id;
        }
    }
}
