using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.UI.Defines;
using Terp.UI.ViewModels.Abstract;

namespace Terp.UI.Factories
{
    public interface IViewModelFactory
    {
        BaseViewModel CreateViewModel(EViewTypes viewType);
    }
}
