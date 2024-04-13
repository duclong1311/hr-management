using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terp.UI.Services
{
    public enum ERegistrationResult
    {
        Success,
        InvalidPassword,
        PasswordDoNotMatch,
        EmailAlreadyExist,
    }
}
