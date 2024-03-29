﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InnkeeperAuthAPI.Library.DataAccess;
using InnkeeperAuthAPI.Library.Models;
using InnkeeperAuthAPI.Models;


namespace InnkeeperAuthAPI.Controllers
{
    //[Authorize]
    public class CharactersController : ApiController
    {
        // GET api/<controller>
        public List<CharacterModel> Get()
        {
            CharacterData data = new CharacterData();
            return data.GetAllCharacters();
        }

        // GET api/<controller>/5
        public List<CharacterModel> Get(string id)
        {
            CharacterData data = new CharacterData();
            return data.GetCharactersForUser(id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post(CombinedCharacterStats character)
        {
            CharacterData data = new CharacterData();
            data.PostCharacter(character);
        }

        [Route("api/Characters/Update")]
        [HttpPost]
        public void Update(CombinedCharacterStats character)
        {
            CharacterData data = new CharacterData();
            data.UpdateCharacter(character);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {

        }
    }
}