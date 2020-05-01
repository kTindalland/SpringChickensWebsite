using Interfaces.Database;
using Interfaces.Database.Entities;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpringChickens.Services
{
    public class SearchUsersService : ISearchUsersService
    {
        public List<IUser> SearchUsers(List<IUser> users, string searchTerm)
        {
            Regex searchRegex = new Regex(searchTerm.ToLower());

            var matchingRecords = users.Where(r => searchRegex.IsMatch(r.UserName.ToLower())).ToList();

            return matchingRecords;
        }
    }
}
