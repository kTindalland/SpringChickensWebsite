using Interfaces.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Interfaces.Services
{
    public interface ISearchUsersService
    {
        List<IUser> SearchUsers(List<IUser> users, string searchTerm);
    }
}
