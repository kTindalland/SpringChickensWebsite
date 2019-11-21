using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.Database.Entities;

namespace Database.Models
{
    internal class User : IUser
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Hash { get; set; }

        public string Salt { get; set; }

        public string Email { get; set; }
        
        public bool AdminRights { get; set; }
    }
}
