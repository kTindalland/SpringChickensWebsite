﻿using Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringChickens.ViewModels.Reset
{
    public class ChangePasswordViewModel : IBaseViewModel
    {
        public bool Layout_SignedIn { get; set; }
        public string Layout_Username { get; set; }
        public string Layout_Password { get; set; }
        public bool Layout_IsAdmin { get; set; }


        public string NewPassword { get; set; }
        public string ErrorMessage { get; set; }
        public string TokenString { get; set; }
    }
}
