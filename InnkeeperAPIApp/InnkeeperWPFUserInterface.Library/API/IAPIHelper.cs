using InnkeeperWPFUserInterface.Library.Models;
using System.Threading.Tasks;

namespace InnkeeperWPFUserInterface.Library.API
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task GetLoggedInUser(string token);
    }
}