using HRM.Domain.Exceptions;
using HRM.UI.Factories;
using HRM.UI.States.Authenticator;
using HRM.UI.Stores;
using HRM.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HRM.UI.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly NavigationStore _navigationStore;
        private readonly IViewModelFactory _viewModelFactory;


        public LoginCommand(LoginViewModel loginViewModel, IAuthenticator authenticator, NavigationStore navigationStore, IViewModelFactory viewModelFactory)
        {
            _loginViewModel = loginViewModel;
            _authenticator = authenticator;
            _navigationStore = navigationStore;
            _viewModelFactory = viewModelFactory;

            _loginViewModel.PropertyChanged += LoginViewModel_PropertyChanged;

        }
        public override bool CanExecute(object parameter)
        {
            return _loginViewModel.CanLogin && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _loginViewModel.ErrorMessage = string.Empty;

            try
            {
                _navigationStore.LoadingVisibility = Visibility.Visible;
                await _authenticator.Login(_loginViewModel.Email, _loginViewModel.Password);
                _navigationStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.MainContent);
            }
            catch (UserNotFoundException)
            {
                _loginViewModel.ErrorMessage = "Username does not exist.";
                MessageBox.Show(_loginViewModel.ErrorMessage);
            }
            catch (InvalidPasswordException)
            {
                _loginViewModel.ErrorMessage = "Incorrect password.";
                MessageBox.Show(_loginViewModel.ErrorMessage);
            }
            catch (Exception)
            {
                _loginViewModel.ErrorMessage = "Login failed.";
                MessageBox.Show(_loginViewModel.ErrorMessage);
            }
            _navigationStore.LoadingVisibility = Visibility.Hidden;

        }


        private void LoginViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginViewModel.CanLogin))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
