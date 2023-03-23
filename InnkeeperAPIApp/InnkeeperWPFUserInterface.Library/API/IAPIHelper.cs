using InnkeeperWPFUserInterface.Library.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace InnkeeperWPFUserInterface.Library.API
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task<AuthenticatedUser> AuthenticateCustom(string username, string password);
        Task GetLoggedInUser(string token);
        HttpClient ApiClient { get; }
        ILoggedInUserModel LoggedInUser { get; }
    }
}