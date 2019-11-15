using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Services
{
    public interface IPasswordFilterPredicate
    {
        bool Validate(string password, out string errormsg);
    }
}
