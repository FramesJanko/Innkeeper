using InnkeeperAuthAPI.Library.Internal.DataAccess;
using InnkeeperAuthAPI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnkeeperAuthAPI.Library.DataAccess
{
    public class CharacterData
    {
        public List<CharacterModel> GetAllCharacters()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<CharacterModel, dynamic>("dbo.spGetAllCharacters", new { }, "azureInnkeeperData");

            return output;
        }
    }
}
