using HRM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.UI.States.Staff
{
    public interface IStaffStore
    {
        NhanSu CurrentNhanSu { get; set; }

        event Action StateChanged;
    }
}
