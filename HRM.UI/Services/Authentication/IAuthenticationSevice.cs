using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Domain.Models;

namespace Terp.UI.Services
{
    public interface IAuthenticationSevice
    {
        Task<User> Login(string email, string password);
        Task<ERegistrationResult> Register(string email, string username, string password, string confirmPassword);
    }
}
