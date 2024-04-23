using HRM.UI.Factories;
using HRM.UI.States.Authenticator;
using HRM.UI.Stores;
using HRM.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.UI.Commands
{
    public class StaffViewCommand : AsyncCommandBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly StaffCVViewModel _staffCVViewModel;
        private readonly MainContentStore _mainContentStore;
        private readonly IViewModelFactory _viewModelFactory;
        public StaffViewCommand(IAuthenticator authenticator, StaffCVViewModel staffCVViewModel, MainContentStore mainContentStore, IViewModelFactory viewModelFactory)
        {
            _authenticator = authenticator;
            _staffCVViewModel = staffCVViewModel;
            _mainContentStore = mainContentStore;
            _viewModelFactory = viewModelFactory;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            await _authenticator.LoginNhanSu(_staffCVViewModel.SelectedItem.MaNhanVien);
            _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.ChildContent);

        }
    }
}
