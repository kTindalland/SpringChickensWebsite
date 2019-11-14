using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringChickens.ViewModels
{
    public class SignupViewmodel
    {
        public string ErrorMessage { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
