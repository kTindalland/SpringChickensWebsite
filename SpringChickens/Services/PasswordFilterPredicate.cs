using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Services;

namespace SpringChickens.Services
{
    public class PasswordFilterPredicate : IPasswordFilterPredicate
    {
        public bool Validate(string password, out string errormsg)
        {
            // Check length is 6 or more.
            if (password.Length < 6)
            {
                errormsg = "Password needs to be 6\n characters or longer.";
                return false;
            }


            // Check there is at least one capital letter
            if (!password.Any(c => char.IsUpper(c)))
            {
                errormsg = "Password must have a capital letter.";
                return false;
            }

            // Check there is at least one lower case letter
            if (!password.Any(c => char.IsLower(c)))
            {
                errormsg = "Password must have a lower case letter.";
                return false;
            }

            // Check there is at least one number
            if (!password.Any(c => char.IsDigit(c)))
            {
                errormsg = "Password must have a number.";
                return false;
            }

            errormsg = "";
            return true;
        }
    }
}
