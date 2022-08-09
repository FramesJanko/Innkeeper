using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnkeeperAuthAPI.Models
{
    public class Character
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = "";

        public int Level { get; set; } = 1;

        public string Race { get; set; } = "";

        public string DndClass { get; set; } = "";

    }
}