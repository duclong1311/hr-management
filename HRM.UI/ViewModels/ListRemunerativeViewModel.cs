using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using HRM.UI.Factories;
using HRM.UI.States.Users;
using HRM.UI.Stores;
using System.Windows.Input;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace HRM.UI.ViewModels
{
    public class ListRemunerativeViewModel : BaseViewModel
    {
        private ObservableCollection<KhenThuongKyLuat> _list = new ObservableCollection<KhenThuongKyLuat>();
        public ObservableCollection<KhenThuongKyLuat> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<NhanSu> _listNhanSu = new ObservableCollection<NhanSu>();
        public ObservableCollection<NhanSu> ListNhanSu { get => _listNhanSu; set { _listNhanSu = value; OnPropertyChanged(); } }

        private IUnitOfWork _unitOfWork;
        private IRepository<NhanSu> _nhanSuRepository;

        private IRepository<KhenThuongKyLuat> _khenThuongKyLuatRespository;
        public ICommand AddCommand { get; set; }
        public ObservableCollection<string> CapKhenThuongData { get; set; }

        private string _capKhenThuong;
        public string CapKhenThuongKyLuat
        {
            get { return _capKhenThuong; }
            set
            {
                _capKhenThuong = value;
                OnPropertyChanged();
            }
        }

        private string _tenHinhThuc;
        public string TenHinhThuc
        {
            get { return _tenHinhThuc; }
            set
            {
                _tenHinhThuc = value;
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

        private DateTime _ngayQuyetDinh = DateTime.Today;
        public DateTime NgayQuyetDinh
        {
            get { return _ngayQuyetDinh; }
            set
            {
                _ngayQuyetDinh = value;
                OnPropertyChanged();
            }
        }

        public void CheckNullDateTimeValue()
        {
            if (NgayQuyetDinh == DateTime.MinValue)
            {
                NgayQuyetDinh = new DateTime(1900, 1, 1);
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
        private string _maNhanVien { get; set; }
        public string MaNhanVien { get { return _maNhanVien; } set { _maNhanVien = value; OnPropertyChanged(); } }
        

        private NhanSu _selectedNhanSu;

        public NhanSu SelectedNhanSu
        {
            get { return _selectedNhanSu; }
            set { _selectedNhanSu = value; OnPropertyChanged(); }
        }

        private string _hoTen { get; set; }
        public string HoTen { get { return _hoTen; } set { _hoTen = value; OnPropertyChanged(); } }

        private KhenThuongKyLuat _selectedItem;
        public KhenThuongKyLuat SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    CapKhenThuongKyLuat = SelectedItem.CapKhenThuongKyLuat;
                    TenHinhThuc = SelectedItem.TenHinhThuc;
                    NoiDung = SelectedItem.NoiDung;
                    NgayQuyetDinh = (DateTime)SelectedItem.NgayQuyetDinh;
                    SoQuyetDinh = SelectedItem.SoQuyetDinh;
                }
                OnPropertyChanged();
            }
        }
        private void LoadComboBoxData()
        {
            CapKhenThuongData = new ObservableCollection<string>();
            CapKhenThuongData.Add("Cá nhân");
            CapKhenThuongData.Add("Đội, nhóm");
            CapKhenThuongData.Add("Bộ phận");
            CapKhenThuongData.Add("Công ty");

            ListNhanSu = new ObservableCollection<NhanSu>(_nhanSuRepository.AsQueryable().ToList());
            SelectedNhanSu = ListNhanSu.FirstOrDefault();
        }
        private readonly IViewModelFactory _viewModelFactory;
        private readonly MainContentStore _mainContentStore;
        private readonly IUserStore _userStore;
        private readonly ChildContentStore _childContentStore;


        public ListRemunerativeViewModel(IUserStore userStore, IViewModelFactory viewModelFactory, MainContentStore mainContentStore, IRepository<KhenThuongKyLuat> khenThuongKyLuatRepository, IUnitOfWork unitOfWork, ChildContentStore childContentStore, IRepository<NhanSu> nhanSuRepository)
        {
            _viewModelFactory = viewModelFactory;
            _mainContentStore = mainContentStore;
            _nhanSuRepository = nhanSuRepository;
            _khenThuongKyLuatRespository = khenThuongKyLuatRepository;
            _unitOfWork = unitOfWork;
            _childContentStore = childContentStore;
            _userStore = userStore;

            LoadComboBoxData();
            //CheckNullDateTimeValue();
            Load();

            AddCommand = new Commands.RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenHinhThuc))
                    return false;
                return true;
            }, async (p) =>
            {
                var KhenThuongKyLuat = new KhenThuongKyLuat()
                {
                    MaNhanVien = SelectedNhanSu.MaNhanVien,
                    NhanSuId = SelectedNhanSu.Id,
                    CapKhenThuongKyLuat = CapKhenThuongKyLuat,
                    TenHinhThuc = TenHinhThuc,
                    NoiDung = NoiDung,
                    NgayQuyetDinh = NgayQuyetDinh,
                    SoQuyetDinh = SoQuyetDinh,
                };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    KhenThuongKyLuat = await _khenThuongKyLuatRespository.AddAsync(KhenThuongKyLuat);
                    await _unitOfWork.CommitAsync();
                    Load();
                    if (KhenThuongKyLuat != null)
                    {
                        MessageBox.Show("Thêm thành công");

                        TenHinhThuc = string.Empty;
                        NoiDung = string.Empty;
                        NgayQuyetDinh = DateTime.Now;
                        SoQuyetDinh = string.Empty;

                        Load();
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
        public void Load()
        {
            List = new ObservableCollection<KhenThuongKyLuat>(_khenThuongKyLuatRespository.AsQueryable().ToList());
            //List = new ObservableCollection<KhenThuongKyLuat>(_khenThuongKyLuatRespository.AsQueryable().Include(x => x.NhanSu).ToList());
        }
        //public void LoadHoTen()
        //{
        //    HoTen = new ObservableCollection<KhenThuongKyLuat>(_khenThuongKyLuatRespository.AsQueryable().ToList());
        //}
    }
}
