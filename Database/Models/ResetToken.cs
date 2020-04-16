using Interfaces.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public class ResetToken : IResetToken
    {
        public int UserId { get; set; }
        public string TokenString { get; set; }
        public DateTime ExpiryTime { get; set; }
        public bool IsDead { get; set; }
        public int Id { get; set; }
    }
}
