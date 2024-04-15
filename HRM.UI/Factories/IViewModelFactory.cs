using HRM.UI.Defines;
using HRM.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.UI.Factories
{
    public interface IViewModelFactory
    {
        BaseViewModel CreateViewModel(EViewTypes viewType);
    }
}
