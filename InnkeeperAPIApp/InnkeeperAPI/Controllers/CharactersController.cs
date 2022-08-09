using InnkeeperAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InnkeeperAPI.Controllers
{
    public class CharactersController : ApiController
    {
        List<Character> characters = new List<Character>();

        public CharactersController()
        {
            characters.Add(new Character { Id = 1, Name = "Jordan Tutreller", DndClass = "Paladin", Level = 1, Race = "Human" });
            characters.Add(new Character { Id = 2, Name = "Clair Underwood", DndClass = "Ranger", Level = 1, Race = "Halfling" });
        }
        
        // GET: api/Characters
        public IHttpActionResult Get()
        {
            return Ok(characters);
        }

        // GET: api/Characters/5
        public Character Get(int id)
        {
            return characters.Where(character => character.Id == id).FirstOrDefault();
        }

        // POST: api/Characters
        public void Post(Character chctr)
        {
            characters.Add(chctr);
        }

        // DELETE: api/Characters/5
        public void Delete(int id)
        {
        }
    }
}
