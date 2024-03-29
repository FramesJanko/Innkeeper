﻿using InnkeeperWPFUserInterface.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InnkeeperWPFUserInterface.Library.API
{
    public interface ICharacterEndpoint
    {
        Task<List<CharacterModel>> GetAll();
        Task PostCharacter(CombinedCharacterStats character);
        Task UpdateCharacter(CombinedCharacterStats character);
    }
}