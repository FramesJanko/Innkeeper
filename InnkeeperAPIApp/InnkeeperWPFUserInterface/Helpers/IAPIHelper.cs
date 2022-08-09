using InnkeeperWPFUserInterface.Models;
using System.Threading.Tasks;

namespace InnkeeperWPFUserInterface.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}