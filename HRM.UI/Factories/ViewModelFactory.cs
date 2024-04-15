using HRM.UI.Defines;
using HRM.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.UI.Factories
{
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : BaseViewModel;

    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
        private readonly CreateViewModel<RegisterViewModel> _createRegisterViewModel;


        public ViewModelFactory(CreateViewModel<LoginViewModel> createLoginViewModel,
            CreateViewModel<RegisterViewModel> createRegisterViewModel)
    
        {
            _createLoginViewModel = createLoginViewModel;
            _createRegisterViewModel = createRegisterViewModel;

        }
        public BaseViewModel CreateViewModel(EViewTypes viewType)
        {
            switch (viewType)
            {
                case EViewTypes.Login:
                    return _createLoginViewModel();
                case EViewTypes.Register:
                    return _createRegisterViewModel();
                default:
                    throw new ArgumentException();
            }
        }
    }
}

