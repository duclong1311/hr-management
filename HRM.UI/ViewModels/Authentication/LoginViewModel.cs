using HRM.Core.Repositories;
using HRM.Domain.Models;
using HRM.UI.Commands;
using HRM.UI.Factories;
using HRM.UI.States.Authenticator;
using HRM.UI.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HRM.UI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Privates
        private string _email = "1";
        private string _password = "111111";
        private string _errorMessage;
        #endregion

        #region Properties
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanLogin));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanLogin));
            }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
        public bool CanLogin => !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        #endregion

        public ICommand LoginCommand { get; set; }
        public ICommand PasswordResetCommand { get; set; }
        public ICommand RegisterViewCommand
        {
            get
            {
                return new RelayCommand<object>(p => true, p =>
                {
                    _navigationStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Register);
                });
            }
        }
        private readonly NavigationStore _navigationStore;
        private readonly IAuthenticator _authenticator;
        private readonly IRepository<User> _repository;
        private readonly IViewModelFactory _viewModelFactory;

        public LoginViewModel(NavigationStore navigationStore, IAuthenticator authenticator, IViewModelFactory viewModelFactory)
        {
            _navigationStore = navigationStore;
            _authenticator = authenticator;
            _viewModelFactory = viewModelFactory;
            LoginCommand = new LoginCommand(this, _authenticator, _navigationStore, _viewModelFactory);
        }
    }
}

