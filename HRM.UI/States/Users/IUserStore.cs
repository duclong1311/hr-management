using HRM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Models;
namespace HRM.UI.States.Users
{
    public interface IUserStore
    {
        User CurrentUser { get; set; }
        NhanSu CurrentNhanSu { get; set; }

        event Action StateChanged;
        event Action StateChanged1;
    }
}
