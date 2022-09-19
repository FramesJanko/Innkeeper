using InnkeeperWPFUserInterface.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InnkeeperWPFUserInterface.Library.API
{
    public class StatsEndpoint : IStatsEndpoint
    {
        private IAPIHelper _apiHelper;

        public StatsEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<StatsModel>> GetStatsForUser()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync($"api/Stats/{_apiHelper.LoggedInUser.Id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<StatsModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
