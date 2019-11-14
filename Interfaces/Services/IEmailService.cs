using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Services
{
    public interface IEmailService
    {
        void SendEmail(string sendAddress);
    }
}
