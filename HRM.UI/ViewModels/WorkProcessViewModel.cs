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
using System.Collections.ObjectModel;
using HRM.UI.Factories;
using HRM.UI.Stores;

namespace HRM.UI.ViewModels
{
    public class WorkProcessViewModel : BaseViewModel
    {
        private ObservableCollection<QuaTrinhCongTac> _list = new ObservableCollection<QuaTrinhCongTac>();
        public ObservableCollection<QuaTrinhCongTac> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }
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
        private readonly IViewModelFactory _viewModelFactory;
        private readonly MainContentStore _mainContentStore;
        public WorkProcessViewModel(IViewModelFactory viewModelFactory, MainContentStore mainContentStore, IRepository<QuaTrinhCongTac> quaTrinhCongTacRepository, IUnitOfWork unitOfWork)
        {
            _viewModelFactory = viewModelFactory;
            _mainContentStore = mainContentStore;
            _quaTrinhCongTacRepository = quaTrinhCongTacRepository;
            _unitOfWork = unitOfWork;
            LoadData();

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
                    LoadData();
                    if (QuaTrinhCongTac != null)
                    {
                        MessageBox.Show("Thêm thành công");
                        LoadData();

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
        private void LoadData()
        {
            List = new ObservableCollection<QuaTrinhCongTac>(_quaTrinhCongTacRepository.AsQueryable().ToList());
            /*            if (!String.IsNullOrWhiteSpace(Filter))
                        {
                            List = new ObservableCollection<NhanSu>(_repository.AsQueryable().Where(x => x.MaNhanVien.Contains(Filter) || x.HoTen.Contains(Filter)).ToList());
                        }*/
        }
    }
}
