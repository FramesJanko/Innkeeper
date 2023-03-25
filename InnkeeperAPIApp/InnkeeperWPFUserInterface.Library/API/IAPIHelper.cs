using InnkeeperWPFUserInterface.Library.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace InnkeeperWPFUserInterface.Library.API
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task<UserLogin> AuthenticateCustom(string username, string password);
        Task RegisterUser(string username, string password, string firstname, string lastname);
        void GetLoggedInUser(UserLogin user);
        HttpClient ApiClient { get; }
        ILoggedInUserModel LoggedInUser { get; }
    }
}