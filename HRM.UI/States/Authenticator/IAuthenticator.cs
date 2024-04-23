using HRM.Domain.Models;
using HRM.UI.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.UI.States.Authenticator
{
    public interface IAuthenticator
    {
        User CurrentUser { get; }
        NhanSu CurrentNhanSu { get; }
        bool IsLoggedIn { get; }
        event Action StateChanged;
        event Action StateChanged1;
        Task<ERegistrationResult> Register(string email, string username, string password, string confirmPassword);
        Task Login(string username, string password);
        Task LoginNhanSu(string MaNhanVien);
        void Logout();
    }
}
