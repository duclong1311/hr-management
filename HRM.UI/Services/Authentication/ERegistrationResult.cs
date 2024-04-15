using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.UI.Services.Authentication
{
    public enum ERegistrationResult
    {
        Success,
        InvalidPassword,
        PasswordDoNotMatch,
        EmailAlreadyExist,
    }
}
