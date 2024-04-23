using HRM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HRM.UI.States.Users
{
    public class UserStore : IUserStore
    {
        private User _currentUser;
        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                StateChanged?.Invoke();
            }
        }
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
                StateChanged1?.Invoke();
            }
        }

        public event Action StateChanged;
        public event Action StateChanged1;
    }
}
