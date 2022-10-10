using InnkeeperWPFUserInterface.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InnkeeperWPFUserInterface.Library.API
{
    public class CharacterEndpoint : ICharacterEndpoint
    {
        private IAPIHelper _apiHelper;

        public CharacterEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<CharacterModel>> GetAll()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/Characters"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<CharacterModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<CharacterModel> Post(CharacterModel characterModel)
        {
            return characterModel;
        }
    }
}
