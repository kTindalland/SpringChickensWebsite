using Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringChickens.ViewModels.Admin
{
    public class NewEventViewModel : IBaseViewModel
    {
        public bool Layout_SignedIn { get; set; }
        public string Layout_Username { get; set; }
        public string Layout_Password { get; set; }
        public bool Layout_IsAdmin { get; set; }

        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string ErrorMessage { get; set; }


    }
}
