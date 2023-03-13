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

        public void PostCharacter(CombinedCharacterStats characterModel)
        {
            using (SqlDataAccess sql = new SqlDataAccess())
            {
                try
                {
                    sql.StartTransaction("azureInnkeeperData");
                    
                    
                    sql.SaveDataInTransaction("dbo.spInsertCharacter", new { name = characterModel.Character.Name, characterClass = characterModel.Character.CharacterClass, race = characterModel.Character.Race, level = characterModel.Character.Level, UserId = characterModel.Character.UserId});
                    sql.SaveDataInTransaction("dbo.spInsertStats", new { str = characterModel.Stats.Strength, dex = characterModel.Stats.Dexterity, con = characterModel.Stats.Constitution, @int = characterModel.Stats.Intelligence, wis = characterModel.Stats.Wisdom, cha = characterModel.Stats.Charisma, hp = characterModel.Stats.Health, ac = characterModel.Stats.ArmorClass, speed = characterModel.Stats.Speed, UserId = characterModel.Stats.UserId, CreatedDate = characterModel.Stats.CreatedDate });
                    int statsId = sql.LoadDataInTransaction<int, dynamic>("dbo.spStats_LookupId", new { UserId = characterModel.Stats.UserId, CreatedDate = characterModel.Stats.CreatedDate }).FirstOrDefault();
                    int characterId = sql.LoadDataInTransaction<int, dynamic>("dbo.spCharacter_LookupId", new { UserId = characterModel.Character.UserId, Name = characterModel.Character.Name }).FirstOrDefault();
                    sql.SaveDataInTransaction("dbo.spInsertCharacterStatsId", new { statsId, characterId });
                    //sql.CommitTransaction();
                }
                catch
                {
                    sql.RollbackTransaction();
                    throw;
                }
            }
        }
        public void UpdateCharacter(CombinedCharacterStats characterModel)
        {
            using (SqlDataAccess sql = new SqlDataAccess())
            {
                try
                {
                    sql.StartTransaction("azureInnkeeperData");
                    sql.SaveDataInTransaction("dbo.spCharacter_Update", new { Id = characterModel.Character.Id, Name = characterModel.Character.Name, CharacterClass = characterModel.Character.CharacterClass, race = characterModel.Character.Race, level = characterModel.Character.Level, ModifiedDate = characterModel.Stats.ModifiedDate });
                    sql.SaveDataInTransaction("dbo.spStats_Update", new { Id = characterModel.Stats.Id, Strength = characterModel.Stats.Strength, Dexterity = characterModel.Stats.Dexterity, Constitution = characterModel.Stats.Constitution, Intelligence = characterModel.Stats.Intelligence, Wisdom = characterModel.Stats.Wisdom, Charisma = characterModel.Stats.Charisma, Health = characterModel.Stats.Health, ArmorClass = characterModel.Stats.ArmorClass, Speed = characterModel.Stats.Speed, Skills = characterModel.Stats.Skills, ModifiedDate = characterModel.Stats.ModifiedDate });
                    //sql.CommitTransaction();
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
