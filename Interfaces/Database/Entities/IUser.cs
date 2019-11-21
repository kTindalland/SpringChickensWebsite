using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Database.Entities
{
    public interface IUser : IEntity
    {
        string UserName { get; set; }

        string Hash { get; set; }

        string Salt { get; set; }

        string Email { get; set; }

        bool AdminRights { get; set; }
    }
}
