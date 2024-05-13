using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.VariantTypes;
using HRM.UI.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using Microsoft.Identity.Client;
using HRM.UI.Commands;
using HRM.UI.Stores;
using HRM.UI.Factories;
using DocumentFormat.OpenXml.ExtendedProperties;

namespace HRM.UI.ViewModels
{
    public class ListStaffViewModel : BaseViewModel
    {
        private ObservableCollection<NhanSu> _list = new ObservableCollection<NhanSu>();
        public ObservableCollection<NhanSu> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<BoPhan> _listBoPhan = new ObservableCollection<BoPhan>();
        public ObservableCollection<BoPhan> ListBoPhan { get => _listBoPhan; set { _listBoPhan = value; OnPropertyChanged(); } }

        private ObservableCollection<ChucVu> _listChucVu = new ObservableCollection<ChucVu>();
        public ObservableCollection<ChucVu> ListChucVu { get => _listChucVu; set { _listChucVu = value; OnPropertyChanged(); } }


        private BoPhan _selectedCboBoPhan;
        public BoPhan SeletedCboBoPhan { get { return _selectedCboBoPhan; } set { _selectedCboBoPhan = value; OnPropertyChanged(); } }

        private ChucVu _selectedCboChucVu;
        public ChucVu SeletedCboChucVu { get { return _selectedCboChucVu; } set { _selectedCboChucVu = value; OnPropertyChanged(); } }

        private NhanSu _selectedItem;
        public NhanSu SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    MaNhanVien = _selectedItem.MaNhanVien;
                    DisplayName = _selectedItem.HoTen;
                    //EmailNoiBo = _selectedItem.EmailCongTy;
                    STKNganHang = _selectedItem.STK;
                    SoBHXH = _selectedItem.MaSoBHXH;
                    MaSoThue = _selectedItem.MaSoThue;
                    SeletedCboBoPhan = SelectedItem.BoPhan;
                    SeletedCboChucVu = SelectedItem.ChucVu;
                }
                OnPropertyChanged();
            }
        }
        private string _filter;
        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                OnPropertyChanged();
                LoadData();
            }
        }

        private string _maNhanVien;
        public string MaNhanVien { get => _maNhanVien; set { _maNhanVien = value; OnPropertyChanged(); } }
        private string _displayName;
        public string DisplayName { get => _displayName; set { _displayName = value; OnPropertyChanged(); } }
        private string _emailNoiBo;
        public string EmailNoiBo { get => _emailNoiBo; set { _emailNoiBo = value; OnPropertyChanged(); } }
        private string _stkNganHang;
        public string STKNganHang { get => _stkNganHang; set { _stkNganHang = value; OnPropertyChanged(); } }
        private string _soBHXH;
        public string SoBHXH { get => _soBHXH; set { _soBHXH = value; OnPropertyChanged(); } }
        private string _maSoThue;
        public string MaSoThue { get => _maSoThue; set { _maSoThue = value; OnPropertyChanged(); } }
        private DateTime _ngayVaoLam = DateTime.Now;
        public DateTime NgayVaoLam
        {
            get { return _ngayVaoLam; }
            set
            {
                _ngayVaoLam = value.Date;
                OnPropertyChanged();
            }
        }

        private bool _canReadOnly = false;
        public bool CanReadOnly { get => _canReadOnly; set { _canReadOnly = value; OnPropertyChanged(); } }
        private string _filterBoPhan = "";
        public string FilterBoPhan { get => _filterBoPhan; set { _filterBoPhan = value; OnPropertyChanged(); LoadCombobox(); } }
        private string _filterChucVu = "";
        public string FilterChucVu { get => _filterChucVu; set { _filterChucVu = value; OnPropertyChanged(); LoadCombobox(); } }
        private string _filterViTri = "";
        public string FilterViTri { get => _filterViTri; set { _filterViTri = value; OnPropertyChanged(); LoadCombobox(); } }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand StaffViewCommand { get; set; }

        private IRepository<NhanSu> _repository;
        private IRepository<BoPhan> _boPhanRepository;
        private IRepository<ChucVu> _chucVuRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly MainContentStore _mainContentStore;

        public ListStaffViewModel(IViewModelFactory viewModelFactory, MainContentStore mainContentStore, IRepository<NhanSu> repository, IUnitOfWork unitOfWork, IRepository<BoPhan> repositoryBoPhan, IRepository<ChucVu> repositoryChucVu)
        {
            _viewModelFactory = viewModelFactory;
            _mainContentStore = mainContentStore;
            _repository = repository;
            _boPhanRepository = repositoryBoPhan;
            _chucVuRepository = repositoryChucVu;
            _unitOfWork = unitOfWork;

            LoadCombobox();
            LoadData();
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;
                if (_repository.AsQueryable().Any(x => x.MaNhanVien == MaNhanVien))
                    return false;
                if (SeletedCboBoPhan == null) return false;
                return true;
            }, async (p) =>
            {
                var NhanSu = new NhanSu()
                {
                    HoTen = DisplayName,
                    MaNhanVien = MaNhanVien,
                    STK = STKNganHang,
                    //EmailCongTy = EmailNoiBo,
                    MaSoBHXH = SoBHXH,
                    MaSoThue = MaSoThue,
                    BoPhanId = SeletedCboBoPhan.Id,
                    ChucVuId = SeletedCboChucVu.Id,
                };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    NhanSu = await _repository.AddAsync(NhanSu);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });
            UpdateCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                if (!_repository.AsQueryable().Any(x => x.MaNhanVien == SelectedItem.MaNhanVien))
                    return false;

                return true;

            }, async (p) =>
            {
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var NhanSu = await _repository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    NhanSu.HoTen = DisplayName;
                    NhanSu.MaNhanVien = MaNhanVien;
                    NhanSu.STK = STKNganHang;
                    //NhanSu.EmailCongTy = EmailNoiBo;
                    NhanSu.MaSoBHXH = SoBHXH;
                    NhanSu.MaSoThue = MaSoThue;
                    NhanSu.BoPhanId = SeletedCboBoPhan.Id;
                    NhanSu.ChucVuId = SeletedCboChucVu.Id;
                    await _repository.UpdateAsync(NhanSu);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                return true;

            }, async (p) =>
            {
                if (!_repository.AsQueryable().Any(x => x.Id == SelectedItem.Id))
                    return;

                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var NhanSu = await _repository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    await _repository.UpdateAsync(NhanSu);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });

            //DeleteCommand = new RelayCommand<object>((p) =>
            //{
            //    if (SelectedItem == null)
            //        return false;
            //    return true;
            //}, async (p) =>
            //{
            //    await _unitOfWork.BeginTransactionAsync();
            //    try
            //    {
            //        var NhanSu = await _repository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
            //        await _repository.DeleteAsync(NhanSu);
            //        await _unitOfWork.CommitAsync();
            //        LoadData();


            //    }
            //    catch (Exception ex)
            //    {
            //        await _unitOfWork.RollbackAsync();
            //    }
            //}
            //);
        }
        private void LoadCombobox()
        {
            ListBoPhan = new ObservableCollection<BoPhan>(_boPhanRepository.AsQueryable().Where(x => x.TenBoPhan.Contains(FilterBoPhan)).ToList());
            SeletedCboBoPhan = ListBoPhan.FirstOrDefault();

            ListChucVu = new ObservableCollection<ChucVu>(_chucVuRepository.AsQueryable().Where(x => x.TenChucVu.Contains(FilterBoPhan)).ToList());
            SeletedCboChucVu = ListChucVu.FirstOrDefault();
        }

        private void LoadData()
        {
            List = new ObservableCollection<NhanSu>(_repository.AsQueryable().Include(x => x.BoPhan).ToList());
            /*            if (!String.IsNullOrWhiteSpace(Filter))
                        {
                            List = new ObservableCollection<NhanSu>(_repository.AsQueryable().Where(x => x.MaNhanVien.Contains(Filter) || x.HoTen.Contains(Filter)).ToList());
                        }*/
        }
    }
}
