using HRM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.UI.Services.Authentication
{
    public interface IAuthenticationSevice
    {
        Task<User> Login(string email, string password);
        Task<ERegistrationResult> Register(string email, string username, string password, string confirmPassword);
    }
}
