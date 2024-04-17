using HRM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.UI.States.Staff
{
    public class StaffStore : IStaffStore
    {
        private NhanSu _currentNhanSu;
        public NhanSu CurrentNhanSu
        {
            get
            {
                return _currentNhanSu;
            }
            set
            {
                _currentNhanSu = value;
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;
    }
}
