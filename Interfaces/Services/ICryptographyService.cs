using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Services
{
    public interface ICryptographyService
    {
        string GenerateSalt();
        string Hash(string content);
    }
}
