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
        private readonly CreateViewModel<MainContentViewModel> _createMainContentViewModel;
        private readonly CreateViewModel<ListStaffViewModel> _createListStaffViewModel;
        private readonly CreateViewModel<PersonalInforViewModel> _createPersonalInforViewModel;
        private readonly CreateViewModel<ChildContentViewModel> _createChildContentViewModel;


        public ViewModelFactory(CreateViewModel<LoginViewModel> createLoginViewModel,
            CreateViewModel<RegisterViewModel> createRegisterViewModel,
            CreateViewModel<MainContentViewModel> createMainContentViewModel,
            CreateViewModel<ListStaffViewModel> createListStaffViewModel,
            CreateViewModel<PersonalInforViewModel> createPersonalInforViewModel,
            CreateViewModel<ChildContentViewModel> createChildContentViewModel)

        {
            _createLoginViewModel = createLoginViewModel;
            _createRegisterViewModel = createRegisterViewModel;
            _createMainContentViewModel = createMainContentViewModel;
            _createListStaffViewModel = createListStaffViewModel;
            _createPersonalInforViewModel = createPersonalInforViewModel;
            _createChildContentViewModel = createChildContentViewModel;
        }
        public BaseViewModel CreateViewModel(EViewTypes viewType)
        {
            switch (viewType)
            {
                case EViewTypes.Login:
                    return _createLoginViewModel();
                case EViewTypes.Register:
                    return _createRegisterViewModel();
                case EViewTypes.MainContent:
                    return _createMainContentViewModel();
                case EViewTypes.ListStaff:
                    return _createListStaffViewModel();
                case EViewTypes.PersonalInfor:
                    return _createPersonalInforViewModel();
                case EViewTypes.ChildContent:
                    return _createChildContentViewModel();
                default:
                    throw new ArgumentException();
            }
        }
    }
}

