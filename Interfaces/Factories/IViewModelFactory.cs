using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.ViewModels;

namespace Interfaces.Factories
{
    public interface IViewModelFactory
    {
        T Resolve<T>() where T : IBaseViewModel;
    }
}
