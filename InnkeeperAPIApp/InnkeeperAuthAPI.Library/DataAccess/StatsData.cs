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
        public void PostStats(StatsModel statsModel)
        {
            using (SqlDataAccess sql = new SqlDataAccess())
            {
                try
                {
                    sql.StartTransaction("azureInnkeeperData");


                    sql.SaveDataInTransaction("dbo.spInsertStats", new { str = statsModel.Strength, dex = statsModel.Dexterity, con = statsModel.Constitution, @int = statsModel.Intelligence, wis = statsModel.Wisdom, cha = statsModel.Charisma, hp = statsModel.Health, ac = statsModel.ArmorClass, speed = statsModel.Speed, UserId = statsModel.UserId});
                    int statsId = sql.LoadDataInTransaction<int, dynamic>("spLookupStatsId", new { statsModel.UserId, statsModel.CreatedDate }).FirstOrDefault();

                    sql.SaveDataInTransaction("dbo.spInsertCharacterStatsId", new { statsId });
                    sql.CommitTransaction();
                    
                }
                catch
                {
                    sql.RollbackTransaction();
                    throw;
                }
            }
        }
    }
}
