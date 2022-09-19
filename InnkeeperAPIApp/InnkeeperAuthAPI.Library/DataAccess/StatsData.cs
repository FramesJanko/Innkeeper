using InnkeeperAuthAPI.Library.Internal.DataAccess;
using InnkeeperAuthAPI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnkeeperAuthAPI.Library.DataAccess
{
    public class StatsData
    {
        public List<StatsModel> GetStatsForUser(string UserId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { UserId = UserId };

            var output = sql.LoadData<StatsModel, dynamic>("spGetStatsForUser", p, "azureInnkeeperData");

            return output;
        }
    }
}
