using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using HRM.UI.Factories;
using HRM.UI.States.Users;
using HRM.UI.Stores;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace HRM.UI.ViewModels
{
    public class AdvanceSalaryViewModel : BaseViewModel
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly MainContentStore _mainContentStore;
        private readonly IUserStore _userStore;
        private IRepository<UngLuong> _ungLuongRepository;
        private IRepository<NhanSu> _nhanSuRepository;
        private IUnitOfWork _unitOfWork;
        private readonly ChildContentStore _childContentStore;
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand CancleCommand { get; set; }
        private string _maNhanVien { get; set; }
        public string MaNhanVien { get { return _maNhanVien; } set { _maNhanVien = value; OnPropertyChanged(); } }
        private string _hoTen { get; set; }
        public string HoTen { get { return _hoTen; } set { _hoTen = value; OnPropertyChanged(); } }
        private double _soTienUng { get; set; }
        public double SoTienUng { get { return _soTienUng; } set { _soTienUng = value; OnPropertyChanged(); } }
        private DateTime _ngayUngLuong { get; set; } = DateTime.Now;
        public DateTime NgayUngLuong { get { return _ngayUngLuong; } set { _ngayUngLuong = value; OnPropertyChanged(); } }
        private string _ghiChu { get; set; }
        public string GhiChu { get { return _ghiChu; } set { _ghiChu = value; OnPropertyChanged(); } }
        private ObservableCollection<UngLuong> _list { get; set; } = new ObservableCollection<UngLuong>();
        public ObservableCollection<UngLuong> List { get { return _list; } set { _list = value; OnPropertyChanged(); } }
        private ObservableCollection<NhanSu> _listCbbNhanVien { get; set; } = new ObservableCollection<NhanSu>();
        public ObservableCollection<NhanSu> ListCbbNhanVien { get { return _listCbbNhanVien; } set { _listCbbNhanVien = value; OnPropertyChanged(); } }

        private NhanSu _selectedNhanVien;
        public NhanSu SelectedNhanVien { get { return _selectedNhanVien; } set { _selectedNhanVien = value; OnPropertyChanged(); } }

        private UngLuong _selectedItem;
        public UngLuong SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    SoTienUng = (double)SelectedItem.SoTienUng;
                    NgayUngLuong = (DateTime)SelectedItem.NgayUngLuong;
                    GhiChu = SelectedItem.GhiChu;
                }
                OnPropertyChanged();
            }
        }
        private void LoadComboboxData()
        {
            ListCbbNhanVien = new ObservableCollection<NhanSu>(_nhanSuRepository.AsQueryable().ToList());
            SelectedNhanVien = ListCbbNhanVien.FirstOrDefault();
        }

        public bool CanAddCommand()
        {
            return true;
        }
        public bool CanDeleteCommand()
        {
            if (SelectedItem == null)
                return false;
            return true;
        }
        public bool CanCancleCommand()
        {
            return true;
        }
        public bool CanUpdateCommand()
        {
            return true;
        }

        public void ConvertMoney()
        {
            CultureInfo cultureInfo = new CultureInfo("vi-VN");
            string formattedCurrency = string.Format(cultureInfo, "{0:N0}", SoTienUng);
        }

        public void LoadData()
        {
            List = new ObservableCollection<UngLuong>(_ungLuongRepository.AsQueryable().Include(x => x.NhanSu).ToList());
        }

        public AdvanceSalaryViewModel(IRepository<NhanSu> nhanSuRepository, IUserStore userStore, IViewModelFactory viewModelFactory, MainContentStore mainContentStore, IRepository<UngLuong> ungLuongRepository, IUnitOfWork unitOfWork, ChildContentStore childContentStore)
        {
            _nhanSuRepository = nhanSuRepository;
            _viewModelFactory = viewModelFactory;
            _mainContentStore = mainContentStore;
            _ungLuongRepository = ungLuongRepository;
            _unitOfWork = unitOfWork;
            _childContentStore = childContentStore;
            _userStore = userStore;

            LoadData();
            LoadComboboxData();

            AddCommand = new Commands.RelayCommand<object>((p) => CanAddCommand(),
            async (p) =>
            {
                var ungluong = new UngLuong()
                {
                    NhanSuId = SelectedNhanVien.Id,
                    SoTienUng = SoTienUng,
                    NgayUngLuong = NgayUngLuong,
                    GhiChu = GhiChu,
                };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    ungluong = await _ungLuongRepository.AddAsync(ungluong);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                    if (ungluong != null)
                    {
                        MessageBox.Show("Thêm thành công");
                        ConvertMoney();
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



            DeleteCommand = new Commands.RelayCommand<object>((p) => CanDeleteCommand(),
            async (p) =>
            {
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var ungluong = await _ungLuongRepository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    await _ungLuongRepository.DeleteAsync(ungluong);
                    await _unitOfWork.CommitAsync();
                    LoadData();
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                    MessageBox.Show("Lỗi hệ thống: " + ex.Message);
                }
            });
        }
    }
}
