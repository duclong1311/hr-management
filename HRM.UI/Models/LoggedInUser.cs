using HRM.Domain.Models;
using HRM.UI.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.UI.Models
{
    public class LoggedInUser
    {
        public User User { get; set; }
        public bool IsAdmin => User.RoleId == RoleConstants.AdminId;
        public bool IsManager => User.RoleId == RoleConstants.ManagerId;
        public bool IsCompensation => User.RoleId == RoleConstants.CompensationId;
        public bool IsStaff => User.RoleId == RoleConstants.StaffId;
    }
}
