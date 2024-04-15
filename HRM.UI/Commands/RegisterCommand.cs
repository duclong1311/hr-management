using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HRM.UI.Factories;
using HRM.UI.Services;
using HRM.UI.Services.Authentication;
using HRM.UI.States;
using HRM.UI.States.Authenticator;
using HRM.UI.Stores;
using HRM.UI.ViewModels;

namespace HRM.UI.Commands
{
    public class RegisterCommand : AsyncCommandBase
    {
        private readonly RegisterViewModel _registerViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly NavigationStore _navigationStore;
        private readonly IViewModelFactory _viewModelFactory;

        public RegisterCommand(RegisterViewModel registerViewModel, IAuthenticator authenticator, NavigationStore navigationStore, IViewModelFactory viewModelFactory)
        {
            _registerViewModel = registerViewModel;
            _authenticator = authenticator;
            _navigationStore = navigationStore;
            _viewModelFactory = viewModelFactory;

            _registerViewModel.PropertyChanged += RegisterViewModel_PropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return _registerViewModel.CanRegister && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                ERegistrationResult RegistrationResult = await _authenticator.Register(_registerViewModel.Email,
                    _registerViewModel.UserName,
                    _registerViewModel.Password,
                    _registerViewModel.ConfirmPassword);

                switch (RegistrationResult)
                {
                    case ERegistrationResult.Success:
                        _navigationStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Login);
                        break;
                    case ERegistrationResult.InvalidPassword:
                        MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự","Fail", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                    case ERegistrationResult.PasswordDoNotMatch:
                        MessageBox.Show("Password và ConfirmPassword không trùng khớp", "Fail", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                    case ERegistrationResult.EmailAlreadyExist:
                        MessageBox.Show("Email đã tồn tại", "Fail", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void RegisterViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(RegisterViewModel.CanRegister))
                {
                    OnCanExecuteChanged();
                }
            }
        }
}
