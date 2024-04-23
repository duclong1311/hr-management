using HRM.Core.Repositories;
using HRM.Domain.Models;
using HRM.UI.Services.Authentication;
using HRM.UI.States.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HRM.UI.States.Authenticator
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationSevice _authenticationSevice;
        private readonly IUserStore _userStore;
        private readonly IRepository<NhanSu> _nhansuRepository;

        public Authenticator(IRepository<NhanSu> nhansuRepository, IAuthenticationSevice authenticationSevice, IUserStore userStore)
        {
            _nhansuRepository = nhansuRepository;
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
        public NhanSu CurrentNhanSu
        {
            get
            {
                return _userStore.CurrentNhanSu;
            }
            set
            {
                _userStore.CurrentNhanSu = value;
                StateChanged1?.Invoke();
            }
        }

        public bool IsLoggedIn => CurrentUser != null;

        User IAuthenticator.CurrentUser => throw new NotImplementedException();

        public event Action StateChanged;
        public event Action StateChanged1;

        public async Task Login(string username, string password)
        {
            CurrentUser = await _authenticationSevice.Login(username, password);
        }
        public async Task LoginNhanSu(string MaNhanVien)
        {
            CurrentNhanSu = await _nhansuRepository.AsQueryable().FirstOrDefaultAsync(x=> x.MaNhanVien == MaNhanVien);
        }


        public void Logout()
        {
            CurrentUser = null;
            CurrentNhanSu = null;
        }

        public async Task<ERegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            return await _authenticationSevice.Register(email, username, password, confirmPassword);
        }
    }
}