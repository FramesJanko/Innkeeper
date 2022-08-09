using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InnkeeperAuthAPI.Models;


namespace InnkeeperAuthAPI.Controllers
{
    public class CharactersController : ApiController
    {
        List<Character> characters = new List<Character>();

        public CharactersController()
        {
            characters.Add(new Character { Id = 1, Name = "Jordan Tutreller", DndClass = "Paladin", Level = 1, Race = "Human" });
            characters.Add(new Character { Id = 2, Name = "Clair Underwood", DndClass = "Ranger", Level = 1, Race = "Halfling" });
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            return Ok(characters);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
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