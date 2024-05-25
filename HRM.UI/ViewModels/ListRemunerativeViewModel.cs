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
using HRM.UI.Commands;

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
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ObservableCollection<string> CapKhenThuongData { get; set; }
        public ObservableCollection<string> TenHinhThucData { get; set; }

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
        private string _filter;
        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                OnPropertyChanged();
                Load();
            }
        }

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

            TenHinhThucData = new ObservableCollection<string>();
            TenHinhThucData.Add("Thưởng tiền mặt");
            TenHinhThucData.Add("Trao giấy khen, bằng khen");
            TenHinhThucData.Add("Khiển trách bằng lời nói");
            TenHinhThucData.Add("Khiển trách bằng văn bản");
            TenHinhThucData.Add("Sa thải");

            ListNhanSu = new ObservableCollection<NhanSu>(_nhanSuRepository.AsQueryable().ToList());
            SelectedNhanSu = ListNhanSu.FirstOrDefault();
        }
        private readonly IViewModelFactory _viewModelFactory;
        private readonly MainContentStore _mainContentStore;
        private readonly IUserStore _userStore;
        private readonly ChildContentStore _childContentStore;
        public void Load()
        {
            if (string.IsNullOrWhiteSpace(Filter))
            {
                List = new ObservableCollection<KhenThuongKyLuat>(_khenThuongKyLuatRespository.AsQueryable().ToList());
            }
            else
            {
                List = new ObservableCollection<KhenThuongKyLuat>(_khenThuongKyLuatRespository.AsQueryable()
                    .Where(x => x.NhanSu.MaNhanVien.Contains(Filter) || x.NhanSu.HoTen.Contains(Filter))
                    .ToList());
            }
        }
        //public void LoadHoTen(string maNhanVien)
        //{
        //    var nhanVien = _nhanSuRepository.AsQueryable()
        //                                     .FirstOrDefault(x => x.MaNhanVien == maNhanVien);

        //    HoTen = nhanVien.HoTen;
        //}

        public ListRemunerativeViewModel(IUserStore userStore, IViewModelFactory viewModelFactory, MainContentStore mainContentStore, IRepository<KhenThuongKyLuat> khenThuongKyLuatRepository, IUnitOfWork unitOfWork, ChildContentStore childContentStore, IRepository<NhanSu> nhanSuRepository)
        {
            _viewModelFactory = viewModelFactory;
            _mainContentStore = mainContentStore;
            _nhanSuRepository = nhanSuRepository;
            _khenThuongKyLuatRespository = khenThuongKyLuatRepository;
            _unitOfWork = unitOfWork;
            _childContentStore = childContentStore;
            _userStore = userStore;

            //LoadHoTen("TEV001");
            LoadComboBoxData();
            //CheckNullDateTimeValue();
            Load();

            AddCommand = new Commands.RelayCommand<object>((p) =>
            {
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

            DeleteCommand = new Commands.RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, async (p) =>
            {
                if (SelectedItem != null)
                {
                    MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận xóa", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.OK)
                    {
                        await _unitOfWork.BeginTransactionAsync();

                        try
                        {
                            var khenThuongKyLuat = await _khenThuongKyLuatRespository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);

                            if (khenThuongKyLuat != null)
                            {
                                await _khenThuongKyLuatRespository.DeleteAsync(khenThuongKyLuat);
                                await _unitOfWork.CommitAsync();
                                Load();
                            }
                        }
                        catch (Exception ex)
                        {
                            await _unitOfWork.RollbackAsync();
                            MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            });

            UpdateCommand = new Commands.RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, async (p) =>
            {
                await _unitOfWork.BeginTransactionAsync();

                try
                {
                    var khenThuongKyLuat = await _khenThuongKyLuatRespository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);

                    if (khenThuongKyLuat != null)
                    {

                        if (khenThuongKyLuat.MaNhanVien != SelectedNhanSu.MaNhanVien)
                        {
                            MessageBox.Show("Không được phép sửa mã nhân viên.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            await _unitOfWork.RollbackAsync();
                            return;
                        }
                        // Cập nhật các thuộc tính từ SelectedItem
                        khenThuongKyLuat.CapKhenThuongKyLuat = CapKhenThuongKyLuat;
                        khenThuongKyLuat.TenHinhThuc = TenHinhThuc;
                        khenThuongKyLuat.NoiDung = NoiDung;
                        khenThuongKyLuat.NgayQuyetDinh = NgayQuyetDinh;
                        khenThuongKyLuat.SoQuyetDinh = SoQuyetDinh;

                        await _khenThuongKyLuatRespository.UpdateAsync(khenThuongKyLuat);
                        await _unitOfWork.CommitAsync();
                        Load();

                        MessageBox.Show("Dữ liệu đã được cập nhật thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }


    }
}
