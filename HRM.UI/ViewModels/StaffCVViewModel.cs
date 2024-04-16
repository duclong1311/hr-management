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

namespace HRM.UI.ViewModels
{
    public class StaffCVViewModel : BaseViewModel
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
        public ObservableCollection <BoPhan> ListBoPhan { get => _listBoPhan; set { _listBoPhan = value; OnPropertyChanged(); } }

        private ObservableCollection<ChucVu> _listChucVu = new ObservableCollection<ChucVu>();
        public ObservableCollection<ChucVu> ListChucVu { get => _listChucVu; set { _listChucVu = value; OnPropertyChanged(); } }


        private ObservableCollection<ViTri> _listViTri = new ObservableCollection<ViTri>();
        public ObservableCollection<ViTri> ListViTri { get => _listViTri; set { _listViTri = value; OnPropertyChanged(); } }

     
        private BoPhan _selectedCboBoPhan;
        public BoPhan SeletedCboBoPhan { get { return _selectedCboBoPhan; } set { _selectedCboBoPhan = value; } }

        private ChucVu _selectedCboChucVu;
        public ChucVu SeletedCboChucVu { get { return _selectedCboChucVu; } set { _selectedCboChucVu = value; } }
        private ViTri _selectedCboViTri;
        public ViTri SeletedCboViTri { get { return _selectedCboViTri; } set { _selectedCboViTri = value; } }


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
                    EmailNoiBo = _selectedItem.EmailCongTy;
                    STKNganHang = _selectedItem.STK;
                    SoBHXH = _selectedItem.MaSoBHXH;
                    MaSoThue = _selectedItem.MaSoThue;
                }
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
        private IRepository<NhanSu> _repository;
        private IRepository<BoPhan> _boPhanRepository;
        private IRepository<ChucVu> _chucVuRepository;
        private IRepository<ViTri> _viTriRepository;
        private IUnitOfWork _unitOfWork;

        public NhanSuViewModel(IRepository<NhanSu> repository, IUnitOfWork unitOfWork, IRepository<Unit> repositoryUnit,ILogger logger)
        {
            _repository = repository;
            _unitRepository = repositoryUnit;
            _logger = logger;
            _unitOfWork = unitOfWork;
            LoadCombobox();
            LoadData();
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;
                if (_repository.AsQueryable().Any(x => x.Code == Code))
                    return false;
                if(SelectedCbo == null) return false;
                return true;
            }, async (p) =>
            {
                var NhanSu = new NhanSu() { Name = DisplayName, Code = Code, Description = Description, UnitId = SelectedCbo.Id };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    NhanSu = await _repository.AddAsync(NhanSu);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                    _logger.LogWrite(NhanSu, Defines.EModifyTypes.Added);
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
                if(_repository.AsQueryable().FirstOrDefaultAsync(x => x.Code != SelectedItem.Code && x.Name == SelectedItem.Name) != null)
                {
                    CanReadOnly = true;
                    return true;
                }

                if (!_repository.AsQueryable().Any(x => x.Id == SelectedItem.Id))
                    return false;

                return true;

            }, async (p) =>
            {
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var NhanSu = await _repository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    NhanSu.Name = DisplayName;
                    NhanSu.Code = Code;
                    NhanSu.Description = Description;
                    NhanSu.UnitId = SelectedCbo.Id;
                    await _repository.UpdateAsync(NhanSu);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                    _logger.LogWrite(NhanSu, Defines.EModifyTypes.Updated);
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
                    NhanSu.IsDeleted = true;
                    await _repository.UpdateAsync(NhanSu);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                    _logger.LogWrite(NhanSu, Defines.EModifyTypes.Deleted);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });

            ResetCommand = new RelayCommand<object>((p) =>
            {
                if(String.IsNullOrEmpty(DisplayName) && String.IsNullOrEmpty(Description) && String.IsNullOrEmpty(Code))
                    return false;
                return true;
            }, async (p) =>
            {
                try
                {
                    DisplayName = String.Empty;
                    Code = String.Empty;
                    Description = String.Empty;
                    CanReadOnly = false;
                    SelectedItem = null;
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            }
            );
            LoadData();
        }
        private void LoadCombobox()
        {
            ListBoPhan = new ObservableCollection<BoPhan>(_boPhanRepository.AsQueryable().Where(x => x.TenBoPhan.Contains(FilterBoPhan)).ToList());
            SeletedCboBoPhan = ListBoPhan.FirstOrDefault();

            ListChucVu = new ObservableCollection<ChucVu>(_chucVuRepository.AsQueryable().Where(x => x.TenChucVu.Contains(FilterBoPhan)).ToList());
            SeletedCboChucVu = ListChucVu.FirstOrDefault();

            ListViTri = new ObservableCollection<ViTri>(_vitriRepository.AsQueryable().Where(x => x.TenBoPhan.Contains(FilterBoPhan)).ToList());
            SeletedCboViTri = ListViTri.FirstOrDefault();
        }

        private void LoadData()
        {
            List = new ObservableCollection<NhanSu>(_repository.AsQueryable().Where(x => !x.IsDeleted).Include(p => p.Unit).ToList());
            if (!String.IsNullOrWhiteSpace(Filter))
            {
                List = new ObservableCollection<NhanSu>(_repository.AsQueryable().Where(x => !x.IsDeleted).Where(x => x.Name.Contains(Filter) || x.Code.Contains(Filter)).ToList());
            }
        }
    }
}
