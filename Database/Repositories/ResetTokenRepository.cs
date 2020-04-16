using Database.Models;
using Interfaces.Database.Entities;
using Interfaces.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class ResetTokenRepository : IResetTokenRepository
    {
        private readonly DbContext _context;

        private DatabaseContext Context { get => (DatabaseContext)_context; }


        public ResetTokenRepository(DbContext context)
        {
            _context = context;
        }

        public bool Add(IResetToken entity)
        {
            Context.ResetTokens.Add((ResetToken)entity);
            Context.SaveChangesAsync();
            return true;
        }

        public void CheckToken(IResetToken token, out bool isDead)
        {
            // Validate it exists
            if (!Context.ResetTokens.Any(r => r.Id == token.Id))
            {
                isDead = true;
                return;
            }

            // Get record
            var record = Context.ResetTokens.First(r => r.Id == token.Id);


            // Check expiry date
            if (record.ExpiryTime < DateTime.Now)
            {
                record.IsDead = true;
                Context.SaveChangesAsync();
            }

            // Get dead status
            isDead = record.IsDead;
            return;
        }

        public void CleanTable()
        {
            var deadRecords = Context.ResetTokens.Where(r => r.IsDead);

            Context.ResetTokens.RemoveRange(deadRecords);
            Context.SaveChangesAsync();
        }

        public bool GenerateToken(IUser user, string tokenString, DateTime expiryTime)
        {
            var newToken = new ResetToken()
            {
                UserId = user.Id,
                TokenString = tokenString,
                ExpiryTime = expiryTime,
                IsDead = false
            };

            return Add(newToken);
        }

        public IResetToken GetToken(string tokenString, out bool realToken)
        {
            realToken = Context.ResetTokens.Any(r => r.TokenString == tokenString);

            IResetToken token;
            if (!realToken)
            {
                token = default;
            }
            else
            {
                token = Context.ResetTokens.First(r => r.TokenString == tokenString);
            }


            return token;
        }

        public void KillToken(IResetToken token)
        {
            if (Context.ResetTokens.Any(r => r.Id == token.Id))
            {
                Context.ResetTokens.Remove((ResetToken)token);
                Context.SaveChangesAsync();
            }
        }
    }
}
