using System;
using Interfaces.ViewModels;

namespace SpringChickens.Models
{
    public class ErrorViewModel : IBaseViewModel 
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public bool Layout_SignedIn { get; set; }
        public string Layout_Username { get; set; }
        public string Layout_Password { get; set; }
        public bool Layout_IsAdmin { get; set; }
    }
}
