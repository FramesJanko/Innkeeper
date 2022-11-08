using InnkeeperAuthAPI.Library.DataAccess;
using InnkeeperAuthAPI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InnkeeperAuthAPI.Controllers
{
    public class StatsController : ApiController
    {
        // GET: api/Stats
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Stats/5
        public List<StatsModel> Get(string id)
        {
            StatsData data = new StatsData();
            return data.GetStatsForUser(id);
        }

        // POST: api/Stats
        public void Post(StatsModel stats)
        {
            StatsData data = new StatsData();
            data.PostStats(stats);
        }

        // PUT: api/Stats/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Stats/5
        public void Delete(int id)
        {
        }
    }
}
