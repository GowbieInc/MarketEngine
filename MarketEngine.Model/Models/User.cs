using System;
using System.Collections.Generic;
using System.Text;

namespace MarketEngine.Model.Models
{
    public class User
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}
