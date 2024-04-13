using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Domain.Models;
using Terp.UI.Services;
using Terp.UI.States.Users;

namespace Terp.UI.States
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationSevice _authenticationSevice;
        private readonly IUserStore _userStore;

        public Authenticator(IAuthenticationSevice authenticationSevice , IUserStore userStore) 
        {
            _authenticationSevice = authenticationSevice;
            _userStore = userStore;
        }
        public User CurrentUser
        {
            get
            {
                return _userStore.CurrentUser;
            }
            set
            {
                _userStore.CurrentUser = value;
                StateChanged?.Invoke();
            }
        }

        public bool IsLoggedIn => CurrentUser != null;

        public event Action StateChanged;

        public async Task Login(string username, string password)
        {
            CurrentUser = await _authenticationSevice.Login(username, password);
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        public async Task<ERegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            return await _authenticationSevice.Register(email,username, password, confirmPassword);
        }
    }
}
