using InnkeeperWPFUserInterface.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InnkeeperWPFUserInterface.Library.API
{
    public interface IStatsEndpoint
    {
        Task<List<StatsModel>> GetStatsForUser();
        Task PostStats(StatsModel statsModel);
    }
}