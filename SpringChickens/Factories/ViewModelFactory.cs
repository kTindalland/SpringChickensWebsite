using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Factories;
using Interfaces.ViewModels;
using Interfaces.Services;

namespace SpringChickens.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly ICredentialHoldingService _credService;

        public ViewModelFactory(ICredentialHoldingService credService)
        {
            _credService = credService;
        }

        public T Resolve<T>() where T : IBaseViewModel
        {
            var result = (IBaseViewModel)Activator.CreateInstance(typeof(T));

            result.Layout_SignedIn = _credService.LoggedIn;
            result.Layout_Username = result.Layout_SignedIn ? _credService.Username : null;

            return (T)result;
        }
    }
}
