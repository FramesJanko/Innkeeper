using InnkeeperAuthAPI.Library.DataAccess;
using InnkeeperAuthAPI.Library.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InnkeeperAuthAPI.Controllers
{
    //[Authorize]
    public class UserController : ApiController
    {
        // GET: api/User
        [HttpGet]
        public UserModel GetById()
        {
            string id = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();

            return data.GetUserById(id).First();
            
        }

        [Route("api/User/Authenticate")]
        [HttpPost]
        public UserModel GetUserById(UserModel user)
        {
            UserData data = new UserData();

            return data.GetUserByNameAndPassword(user.Username, user.Password).FirstOrDefault();
        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("api/User/Register")]
        [HttpPost]
        public void RegisterUser(UserModel user)
        {
            UserData data = new UserData();

            data.CreateUser(user);
        }

    }
}
