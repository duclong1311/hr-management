using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using System.Windows.Input;
using HRM.UI.Commands;
using System.Windows;

namespace HRM.UI.ViewModels
{
    public class WorkProcessViewModel : BaseViewModel
    {
        private IUnitOfWork _unitOfWork;

        private IRepository<QuaTrinhCongTac> _quaTrinhCongTacRepository;
        public ICommand AddCommand { get; set; }

        private string _tuNgayDenNgay;
        public string TuNgayDenNgay
        {
            get { return _tuNgayDenNgay;}
            set {  _tuNgayDenNgay = value; OnPropertyChanged(); }
        }

        private string _noiCongTac;
        public string NoiCongTac
        {
            get { return _noiCongTac; }
            set { _noiCongTac = value; OnPropertyChanged(); }
        }

        private string _chucVu;
        public string ChucVu
        {
            get { return _chucVu; }
            set { _chucVu = value; OnPropertyChanged(); }
        }

        public WorkProcessViewModel(IUnitOfWork unitOfWork, IRepository<QuaTrinhCongTac> quaTrinhCongTacRepository)
        {
            _unitOfWork = unitOfWork;
            _quaTrinhCongTacRepository = quaTrinhCongTacRepository;
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, async (p) =>
            {
                var QuaTrinhCongTac = new QuaTrinhCongTac()
                {
                    TuNgayDenNgay = TuNgayDenNgay,
                    NoiCongTac = NoiCongTac,
                    ChucVu = ChucVu,
                };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    QuaTrinhCongTac = await _quaTrinhCongTacRepository.AddAsync(QuaTrinhCongTac);
                    if (QuaTrinhCongTac != null)
                    {
                        MessageBox.Show("Thêm thành công");
                    }
                    else
                    {
                        MessageBox.Show("Lỗi hệ thống");
                    }
                    await _unitOfWork.CommitAsync();

                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });

           
        }
    }
}
