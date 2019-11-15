using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Services
{
    public interface IUsernameFilterPredicate
    {
        bool Validate(string username, out string errormsg);
    }
}
