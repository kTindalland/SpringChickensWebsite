using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Services;

namespace SpringChickens.Services
{
    public class UsernameFilterPredicate : IUsernameFilterPredicate
    {
        public bool Validate(string username, out string errormsg)
        {
            if (username.Length < 6)
            {
                errormsg = "Username needs to be at least 6 characters long.";
                return false;
            }


            errormsg = "";
            return true;
        }
    }
}
