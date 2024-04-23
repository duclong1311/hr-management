using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using HRM.UI.Factories;
using HRM.UI.Stores;

namespace HRM.UI.ViewModels
{
    public class DisciplineViewModel : BaseViewModel
    {
        private ObservableCollection<KyLuat> _list = new ObservableCollection<KyLuat>();
        public ObservableCollection<KyLuat> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }
        private IUnitOfWork _unitOfWork;
        private IRepository<KyLuat> _kyLuatRespository;
        public ICommand AddCommand { get; set; }
        public ObservableCollection<string> CapKyLuatData { get; set; }

        private string _capKyLuat;
        public string CapKyLuat
        {
            get { return _capKyLuat; }
            set
            {
                _capKyLuat = value;
                OnPropertyChanged();
            }
        }

        private string _thamQuyenKyLuat;
        public string ThamQuyenKyLuat
        {
            get { return _thamQuyenKyLuat; }
            set
            {
                _thamQuyenKyLuat = value;
                OnPropertyChanged();
            }
        }

        private DateOnly _thoiGianBanHanh;
        public DateOnly ThoiGianBanHanh
        {
            get { return _thoiGianBanHanh; }
            set
            {
                _thoiGianBanHanh = value;
                OnPropertyChanged();
            }
        }

        private string _noiDung;
        public string NoiDung
        {
            get { return _noiDung; }
            set
            {
                _noiDung = value;
                OnPropertyChanged();
            }
        }

        private string _soQuyetDinh;
        public string SoQuyetDinh
        {
            get { return _soQuyetDinh; }
            set
            {
                _soQuyetDinh = value;
                OnPropertyChanged();
            }
        }
        private void LoadComboBoxData()
        {
            CapKyLuatData = new ObservableCollection<string>();
            CapKyLuatData.Add("Cấp 1");
            CapKyLuatData.Add("Cấp 2");
            CapKyLuatData.Add("Cấp 2");
        }
        private readonly IViewModelFactory _viewModelFactory;
        private readonly MainContentStore _mainContentStore;
        public DisciplineViewModel(IViewModelFactory viewModelFactory, MainContentStore mainContentStore, IRepository<KyLuat> KyLuatRepository, IUnitOfWork unitOfWork)
        {
            _viewModelFactory = viewModelFactory;
            _mainContentStore = mainContentStore;
            _kyLuatRespository = KyLuatRepository;
            _unitOfWork = unitOfWork;
            LoadComboBoxData();
            LoadData();

            AddCommand = new Commands.RelayCommand<object>((p) =>
            {
                return true;
            }, async (p) =>
            {
                var KyLuat = new KyLuat()
                {
                    CapKyLuat = CapKyLuat,
                    ThamQuyen = ThamQuyenKyLuat,
                    ThoiGianBanHanh = ThoiGianBanHanh,
                    NoiDung = NoiDung,
                    SoQuyetDinh = SoQuyetDinh,
                };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    KyLuat = await _kyLuatRespository.AddAsync(KyLuat);
                    await _unitOfWork.CommitAsync();
                    LoadData();
                    if (KyLuat != null)
                    {
                        MessageBox.Show("Thêm thành công");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi hệ thống");
                    }

                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });
        }
        private void LoadData()
        {
            List = new ObservableCollection<KyLuat>(_kyLuatRespository.AsQueryable().ToList());
            /*            if (!String.IsNullOrWhiteSpace(Filter))
                        {
                            List = new ObservableCollection<NhanSu>(_repository.AsQueryable().Where(x => x.MaNhanVien.Contains(Filter) || x.HoTen.Contains(Filter)).ToList());
                        }*/
        }
    }
}
