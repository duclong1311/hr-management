using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using HRM.UI.Factories;
using HRM.UI.States.Users;
using HRM.UI.Stores;
using Microsoft.EntityFrameworkCore;

namespace HRM.UI.ViewModels
{
    public class ContractViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly MainContentStore _mainContentStore;
        private readonly IUserStore _userStore;
        private readonly ChildContentStore _childContentStore;
        private IRepository<HopDong> _hopDongRepository;
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }

        private ObservableCollection<HopDong> _list = new ObservableCollection<HopDong>();
        public ObservableCollection<HopDong> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<NhanSu> _listNhanVien = new ObservableCollection<NhanSu>();
        public ObservableCollection<NhanSu> ListNhanVien
        {
            get { return _listNhanVien; }
            set
            {
                _listNhanVien = value;
                OnPropertyChanged(nameof(ListNhanVien));
            }
        }
        private string _maNhanVien { get; set; }
        public string MaNhanVien
        {
            get { return _maNhanVien; }
            set { _maNhanVien = value; OnPropertyChanged(); }
        }
        private string _soHopDong { get; set; }
        public string SoHopDong
        {
            get { return _soHopDong; }
            set { _soHopDong = value; OnPropertyChanged(); }
        }
        private DateTime _ngayBatDau = DateTime.Now;
        public DateTime NgayBatDau
        {
            get { return _ngayBatDau; }
            set { _ngayBatDau = value; OnPropertyChanged(); }
        }
        private DateTime _ngayKetThuc = DateTime.Now;
        public DateTime NgayKetThuc
        {
            get { return _ngayKetThuc; }
            set { _ngayKetThuc = value; OnPropertyChanged(); }
        }
        private string _loaiHopDong { get; set; }
        public string LoaiHopDong
        {
            get { return _loaiHopDong; }
            set { _loaiHopDong = value; OnPropertyChanged(); }
        }
        private double _luongCoBan { get; set; }
        public double LuongCoBan
        {
            get { return _luongCoBan; }
            set { _luongCoBan = value; OnPropertyChanged(); }
        }
        private float _heSoLuong { get; set; }
        public float HeSoLuong
        {
            get { return _heSoLuong; }
            set { _heSoLuong = value; OnPropertyChanged(); }
        }
        private HopDong _selectedItem;
        public HopDong SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    SoHopDong = SelectedItem.SoHopDong;
                    LoaiHopDong = SelectedItem.LoaiHopDong;
                    NgayBatDau = (DateTime)SelectedItem.NgayBatDau;
                    NgayKetThuc = (DateTime)SelectedItem.NgayKetThuc;
                    LuongCoBan = (double)SelectedItem.LuongCoBan;
                    HeSoLuong = (float)SelectedItem.HeSoLuong;
                }
                OnPropertyChanged();
            }
        }
        public ICommand RemunativeCommand { get; set; }
        public ContractViewModel(IUserStore userStore, IViewModelFactory viewModelFactory, MainContentStore mainContentStore, IRepository<HopDong> hopDongRepository, IUnitOfWork unitOfWork, ChildContentStore childContentStore)
        {
            _viewModelFactory = viewModelFactory;
            _mainContentStore = mainContentStore;
            _hopDongRepository = hopDongRepository;
            _unitOfWork = unitOfWork;
            _childContentStore = childContentStore;
            _userStore = userStore;

            Load();

            RemunativeCommand = new Commands.RelayCommand<object>(p => true, p =>
            {
                _childContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Remunerative);
            });

            AddCommand = new Commands.RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(LoaiHopDong))
                    return false;
                return true;
            }, async (p) =>
            {
                var HopDong = new HopDong()
                {
                    MaNhanVien = userStore.CurrentNhanSu.MaNhanVien,
                    SoHopDong = SoHopDong,
                    LoaiHopDong = LoaiHopDong,
                    NgayBatDau = NgayBatDau,
                    NgayKetThuc = NgayKetThuc,
                    LuongCoBan = LuongCoBan,
                    HeSoLuong = HeSoLuong,
                };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    HopDong = await _hopDongRepository.AddAsync(HopDong);
                    await _unitOfWork.CommitAsync();
                    Load();
                    if (HopDong != null)
                    {
                        MessageBox.Show("Thêm thành công");

                        SoHopDong = string.Empty;
                        LoaiHopDong = string.Empty;
                        NgayBatDau = DateTime.Now;
                        NgayKetThuc = DateTime.Now;
                        LuongCoBan = 0;
                        HeSoLuong = 0;

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
                            var hopdong = await _hopDongRepository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);

                            if (hopdong != null)
                            {
                                await _hopDongRepository.DeleteAsync(hopdong);
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
        }
        public void Load()
        {
            List = new ObservableCollection<HopDong>(_hopDongRepository.AsQueryable().Where(x => x.MaNhanVien == _userStore.CurrentNhanSu.MaNhanVien).ToList());
            /*            if (!String.IsNullOrWhiteSpace(Filter))
                        {
                            List = new ObservableCollection<NhanSu>(_repository.AsQueryable().Where(x => x.MaNhanVien.Contains(Filter) || x.HoTen.Contains(Filter)).ToList());
                        }*/
        }
    }
}
