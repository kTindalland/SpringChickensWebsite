using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.ViewModels
{
    public interface IBaseViewModel
    {
        bool Layout_SignedIn { get; set; }
        string Layout_Username { get; set; }
        string Layout_Password { get; set; }
        bool Layout_IsAdmin { get; set; }
    }
}
