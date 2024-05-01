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
using System.Windows;
using HRM.UI.States.Users;
using HRM.UI.Factories;
using HRM.UI.Stores;

namespace HRM.UI.ViewModels
{
    public class RemunerativeViewModel : BaseViewModel
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
        private IUnitOfWork _unitOfWork;

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
        }
        private readonly IViewModelFactory _viewModelFactory;
        private readonly MainContentStore _mainContentStore;
        private readonly IUserStore _userStore;
        private readonly ChildContentStore _childContentStore;


        public RemunerativeViewModel(IUserStore userStore, IViewModelFactory viewModelFactory, MainContentStore mainContentStore, IRepository<KhenThuongKyLuat> khenThuongKyLuatRepository, IUnitOfWork unitOfWork, ChildContentStore childContentStore)
        {
            _viewModelFactory = viewModelFactory;
            _mainContentStore = mainContentStore;
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
                    MaNhanVien = userStore.CurrentNhanSu.MaNhanVien,
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
            List = new ObservableCollection<KhenThuongKyLuat>(_khenThuongKyLuatRespository.AsQueryable().Where(x => x.MaNhanVien == _userStore.CurrentNhanSu.MaNhanVien).ToList());
            /*            if (!String.IsNullOrWhiteSpace(Filter))
                        {
                            List = new ObservableCollection<NhanSu>(_repository.AsQueryable().Where(x => x.MaNhanVien.Contains(Filter) || x.HoTen.Contains(Filter)).ToList());
                        }*/
        }
    }
}
