﻿using System;

namespace InnkeeperWPFUserInterface.Library.Models
{
    public interface ILoggedInUserModel
    {
        DateTime CreatedDate { get; set; }
        string FirstName { get; set; }
        string Id { get; set; }
        string LastName { get; set; }
        string UserName { get; set; }
        string Token { get; set; }
    }
}