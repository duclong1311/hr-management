using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using System.Windows.Input;
using DocumentFormat.OpenXml.Wordprocessing;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Linq.Expressions;
using HRM.UI.Commands;

namespace HRM.UI.ViewModels
{
    public class TrainingProcessViewModel : BaseViewModel
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<QuaTrinhDaoTao> _quaTrinhDaoTaoRepository;
        public ICommand AddCommand { get; set; }

        private string _tuNgayDenNgay;
        public string TuNgayDenNgay
        {
            get { return _tuNgayDenNgay; }
            set { _tuNgayDenNgay = value; OnPropertyChanged(); }
        }

        private string _noiDaoTao;
        public string NoiDaoTao
        {
            get { return _noiDaoTao; }
            set { _noiDaoTao = value; OnPropertyChanged(); }
        }
        private string _nganhHoc;
        public string NganhHoc
        {
            get { return _nganhHoc; }
            set { _nganhHoc = value; OnPropertyChanged(); }
        }

        private string _hinhThucDaoTao;
        public string HinhThucDaoTao
        {
            get { return _hinhThucDaoTao; }
            set { _hinhThucDaoTao = value; OnPropertyChanged(); }
        }

        private string _vanBangChungChi;
        public string VanBangChungChi
        {
            get { return _vanBangChungChi; }
            set { _vanBangChungChi = value; OnPropertyChanged(); }
        }

        public TrainingProcessViewModel(IRepository<QuaTrinhDaoTao> QuaTrinhRepository, IUnitOfWork unitOfWork)
        {
            _quaTrinhDaoTaoRepository = QuaTrinhRepository;
            _unitOfWork = unitOfWork;

            AddCommand = new Commands.RelayCommand<object>((p) =>
            {

                return true;
            }, async (p) =>
            {
                var QuaTrinhDaoTao = new QuaTrinhDaoTao()
                {
                    TuNgayDenNgay = TuNgayDenNgay,
                    NoiDaoTao = NoiDaoTao,
                    HinhThucDaoTao = HinhThucDaoTao,
                    NganhHoc = NganhHoc,
                    VanBangChungChi = VanBangChungChi,
                };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    QuaTrinhDaoTao = await _quaTrinhDaoTaoRepository.AddAsync(QuaTrinhDaoTao);
                    if (QuaTrinhDaoTao != null)
                        MessageBox.Show("Thêm thành công");
                    else
                        MessageBox.Show("Lỗi hệ thống");
                    await _unitOfWork.CommitAsync();
                }
                catch (Exception e)
                {
                    await _unitOfWork.RollbackAsync();

                }
            });
        }
    }
}
