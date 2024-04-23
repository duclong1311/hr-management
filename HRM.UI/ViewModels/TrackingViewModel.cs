using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using System.Windows.Input;

namespace HRM.UI.ViewModels
{
    public class TrackingViewModel : BaseViewModel
    {
        private IUnitOfWork _unitOfWork;

        private IRepository<BangCong> _bangCongRepository;
        public ICommand AddCommand { get; set; }
    }
}
