using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using HRM.UI.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HRM.UI.ViewModels
{
    public class PostionStaffViewModel : BaseViewModel
    {
        private ObservableCollection<NhanSu> _listNhanSu = new ObservableCollection<NhanSu>();
        public ObservableCollection<NhanSu> ListNhanSu { get => _listNhanSu; set { _listNhanSu = value; OnPropertyChanged(); } }
        private ObservableCollection<ChucVu> _listChucVu = new ObservableCollection<ChucVu>();
        public ObservableCollection<ChucVu> ListChucVu { get => _listChucVu; set { _listChucVu = value; OnPropertyChanged(); } }
        private ObservableCollection<NhanSuChucVu> _listNhanSuChucVu = new ObservableCollection<NhanSuChucVu>();
        public ObservableCollection<NhanSuChucVu> ListNhanSuChucVu { get => _listNhanSuChucVu; set { _listNhanSuChucVu = value; OnPropertyChanged(); } }
        private string _phuCapChucVu;

        public string PhuCapChucVu
        {
            get { return _phuCapChucVu; }
            set { _phuCapChucVu = value; OnPropertyChanged(); }
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
        private NhanSuChucVu _selectedItem;

        public NhanSuChucVu SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }
        private NhanSu _selectedNhanSu;

        public NhanSu SelectedNhanSu
        {
            get { return _selectedNhanSu; }
            set { _selectedNhanSu = value; OnPropertyChanged(); }
        }
        private ChucVu _selectedChucVu;

        public ChucVu SelectedChucVu
        {
            get { return _selectedChucVu; }
            set { _selectedChucVu = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand PositionStaffCommand { get; set; }

        private IRepository<ChucVu> _chucVuRepository;
        private IRepository<NhanSu> _nhanSuRepository;
        private IRepository<NhanSuChucVu> _nhanSuChucVuRepository;
        private IUnitOfWork _unitOfWork;
        private void LoadCombobox()
        {
            ListNhanSu = new ObservableCollection<NhanSu>(_nhanSuRepository.AsQueryable().ToList());
            SelectedNhanSu = ListNhanSu.FirstOrDefault();

            ListChucVu = new ObservableCollection<ChucVu>(_chucVuRepository.AsQueryable().ToList());
            SelectedChucVu = ListChucVu.FirstOrDefault();
        }
        private void LoadData()
        {
            ListNhanSuChucVu = new ObservableCollection<NhanSuChucVu>(_nhanSuChucVuRepository.AsQueryable().ToList());
        }
        public string GetPhuCapChucVu()
        {
            string tenChucVu = SelectedChucVu.TenChucVu;

            string PhuCapChucVu = "0"; 

            switch (tenChucVu)
            {
                case "Nhân viên":
                    PhuCapChucVu = "500000"; 
                    break;
                case "Quản lý":
                    PhuCapChucVu = "2000000";
                    break;
                case "Trưởng phòng":
                    PhuCapChucVu = "5000000";
                    break;
                case "Trưởng bộ phận":
                    PhuCapChucVu = "7000000";
                    break;
                case "Giám đốc":
                    PhuCapChucVu = "20000000";
                    break;
                case "Phó giám đốc":
                    PhuCapChucVu = "15000000";
                    break;
                default:
                    break;
            }

            return PhuCapChucVu;
        }

        public PostionStaffViewModel(IRepository<ChucVu> chucVuRepository, IRepository<NhanSu> nhanSuRepository, IRepository<NhanSuChucVu> nhanSuChucVuRepository, IUnitOfWork unitOfWork)
        {
            _chucVuRepository = chucVuRepository;
            _nhanSuRepository = nhanSuRepository;
            _unitOfWork = unitOfWork;
            _nhanSuChucVuRepository = nhanSuChucVuRepository;

            LoadData();
            LoadCombobox();


            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, async (p) =>
            {
                var nhanSuChucVu = new NhanSuChucVu()
                {
                    NhanSuId = SelectedNhanSu.Id,
                    ChucVuId = SelectedChucVu.Id,
                    PhuCapChucVu = Double.Parse(GetPhuCapChucVu()),
                    NgayBatDau = NgayBatDau,
                    NgayKetThuc = NgayKetThuc,
                };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    nhanSuChucVu = await _nhanSuChucVuRepository.AddAsync(nhanSuChucVu);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });
        }
    }
}
