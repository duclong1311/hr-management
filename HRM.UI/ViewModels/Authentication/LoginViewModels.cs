using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Terp.Core.Repositories;
using Terp.Domain.Models;
using Terp.Domain.Utils;
using Terp.UI.Commands;
using Terp.UI.DependencyInjection;
using Terp.UI.Factories;
using Terp.UI.States;
using Terp.UI.Stores;
using Terp.UI.ViewModels.Abstract;

namespace Terp.UI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Privates
        private string _email = "";
        private string _password;
        private string _errorMessage;
        #endregion

        #region Properties
        public bool UserShouldEditValueNow
        {
            get
            {
                return true;
            }
        }
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

        public LoginViewModel(NavigationStore navigationStore, IAuthenticator authenticator,IViewModelFactory viewModelFactory)
        {
            _navigationStore = navigationStore;
            _authenticator = authenticator;
            _viewModelFactory = viewModelFactory;
            LoginCommand = new LoginCommand(this, _authenticator, _navigationStore, _viewModelFactory);
        }
    }
}
