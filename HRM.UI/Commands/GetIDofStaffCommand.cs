using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.UI.Factories;
using HRM.UI.States.Authenticator;
using HRM.UI.Stores;
using HRM.UI.ViewModels;

namespace HRM.UI.Commands
{
    class GetIDofStaffCommand : AsyncCommandBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly AddPersonalInforViewModel _personalInforViewModel;
        private readonly MainContentStore _mainContentStore;
        private readonly IViewModelFactory _viewModelFactory;
        public GetIDofStaffCommand(IAuthenticator authenticator, AddPersonalInforViewModel personalInforViewModel, MainContentStore mainContentStore, IViewModelFactory viewModelFactory)
        {
            _authenticator = authenticator;
            _personalInforViewModel = personalInforViewModel;
            _mainContentStore = mainContentStore;
            _viewModelFactory = viewModelFactory;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            await _authenticator.LoginNhanSu(_personalInforViewModel.MaNhanVien);
            
            _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.AddChildContent);
            //_mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.AddFamilyInfor);
        }
    }
}
