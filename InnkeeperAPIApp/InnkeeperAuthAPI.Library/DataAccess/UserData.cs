using InnkeeperAuthAPI.Library.Internal.DataAccess;
using InnkeeperAuthAPI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnkeeperAuthAPI.Library.DataAccess
{
    public class UserData
    {
        public List<UserModel> GetUserById(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { Id = Id };

            var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, "azureInnkeeperData");

            return output;
        }

        public List<UserModel> GetUserByNameAndPassword(string Name, string Password)
        {
            SqlDataAccess sql = new SqlDataAccess();

            string passwordHash = hashstring(Password);
            
            var p = new { username = Name, passwordhash = passwordHash };

            var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookupByNamePassword", p, "azureInnkeeperData");

            return output;
        }

        public void CreateUser(UserModel user)
        {
            //@Id NVARCHAR(128),
            //@Username NVARCHAR(50),
            //@PasswordHash NVARCHAR(128),
            //@FirstName NVARCHAR(50),
            //@LastName NVARCHAR(50)
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { Id = hashstring(user.Username), Username = user.Username, PasswordHash = hashstring(user.Password), FirstName = user.FirstName, LastName = user.LastName };

            sql.SaveData("dbo.spRegisterUser", p, "azureInnkeeperData");
        }

        private string hashstring(string stringToHash)
        {
            string alphabetString = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-=!@#$%^&*()_+";
            char[] alphabet = alphabetString.ToCharArray();
            char[] resultHash = stringToHash.ToCharArray();
            string key = "TIistheBest12#";
            for(int i = 0; i < resultHash.Length; i++)
            {
                if (alphabet.Contains(resultHash[i]))
                {
                    int index = Array.IndexOf(alphabet, resultHash[i]);
                    resultHash[i] = alphabet[(index + Array.IndexOf(alphabet, key[i % key.Length])) % alphabet.Length];
                }
                
            }
            string result = string.Concat(resultHash);
            return result;
        }
    }
}
