using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
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
    public class RegisterViewModel : BaseViewModel
    {
        private readonly IRepository<User> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly NavigationStore _navigationStore;
        private readonly IAuthenticator _authenticator;
        private string _email;
        private string _userName;
        private string _password;
        private string _confirmPassword;

        #region Properties
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanRegister));
            }
        }
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanRegister));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanRegister));
            }
        }
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanRegister));
            }
        }
        public bool CanRegister => !string.IsNullOrEmpty(Email) &&
            !string.IsNullOrEmpty(UserName) &&
            !string.IsNullOrEmpty(Password) &&
            !string.IsNullOrEmpty(ConfirmPassword);
        #endregion

        public ICommand RegisterCommand { get; set; }
        public ICommand LoginViewCommand { get; set; }

        public RegisterViewModel(IRepository<User> repository, NavigationStore navigationStore, IAuthenticator authenticator, IViewModelFactory viewModelFactory)
        {
            _repository = repository;
            _viewModelFactory = viewModelFactory;
            _navigationStore = navigationStore;
            _authenticator = authenticator;
            LoginViewCommand = new RelayCommand<object>(p => true, p =>
            {
                _navigationStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Login);
            });

            RegisterCommand = new RegisterCommand(this, _authenticator, _navigationStore, _viewModelFactory);
        }
    }
}

