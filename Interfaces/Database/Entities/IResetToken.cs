using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Database.Entities
{
    public interface IResetToken : IEntity
    {
        int UserId { get; set; }
        string TokenString { get; set; }
        DateTime ExpiryTime { get; set; }
        bool IsDead { get; set; }
    }
}
