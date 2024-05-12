using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using System.Windows.Input;
using OfficeOpenXml;
using System.IO;
using HRM.UI.Factories;
using HRM.UI.Stores;
using Microsoft.Win32;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using DocumentFormat.OpenXml.Bibliography;
using HRM.UI.States.Users;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.EntityFrameworkCore;
using HRM.UI.Commands;

namespace HRM.UI.ViewModels
{
    public class ImportTrackingViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IViewModelFactory _viewModelFactory;

        private readonly MainContentStore _mainContentStore;

        private readonly IUserStore _userStore;

        private readonly ChildContentStore _childContentStore;

        private IRepository<BangCongNhanSu> _bangCongNhanSuRepository;

        private IRepository<BangCong> _bangCongRepository;
        public ICommand AddCommand { get; set; }
        public ICommand ImportDataCommand { get; set; }
        public ICommand ClearDataCommand { get; set; }
        public ICommand CaculationCommand { get; set; }
        public ICommand AddBangCongCommand { get; set; }

        private ObservableCollection<BangCongNhanSu> _list = new ObservableCollection<BangCongNhanSu>();
        public ObservableCollection<BangCongNhanSu> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<BangCongNhanSu> _bangCongNhanSuDataList = new ObservableCollection<BangCongNhanSu>();
        public ObservableCollection<BangCongNhanSu> BangCongNhanSuDataList
        {
            get { return _bangCongNhanSuDataList; }
            set
            {
                _bangCongNhanSuDataList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<BangCongNhanSu> _listData = new ObservableCollection<BangCongNhanSu>();
        public ObservableCollection<BangCongNhanSu> ListData
        {
            get => _listData;
            set
            {
                _listData = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<BangCong> _bangCongDataList = new ObservableCollection<BangCong>();
        public ObservableCollection<BangCong> BangCongDataList
        {
            get { return _bangCongDataList; }
            set
            {
                _bangCongDataList = value;
                OnPropertyChanged();
            }
        }

        #region
        private string _maNhanVien { get; set; }
        public string MaNhanVien { get { return _maNhanVien; } set { _maNhanVien = value; OnPropertyChanged(); } }
        private string _hoTen { get; set; }
        public string HoTen { get { return _hoTen; } set { _hoTen = value; OnPropertyChanged(); } }
        private int _nam { get; set; }
        public int Nam { get { return _nam; } set { _nam = value; OnPropertyChanged(); } }
        private int _thang { get; set; }
        public int Thang { get { return _thang; } set { _thang = value; OnPropertyChanged(); } }
        private int _tongSoNgayCong { get; set; }
        public int TongSoNgayCong { get { return _tongSoNgayCong; } set { _tongSoNgayCong = value; OnPropertyChanged(); } }
        private int _tongSoNgayCongCN { get; set; }
        public int TongSoNgayCongCN { get { return _tongSoNgayCongCN; } set { _tongSoNgayCongCN = value; OnPropertyChanged(); } }
        private int _tongSoNgayCongNgayLe { get; set; }
        public int TongSoNgayCongNgayLe { get { return _tongSoNgayCongNgayLe; } set { _tongSoNgayCongNgayLe = value; OnPropertyChanged(); } }
        private float _diMuonVeSom { get; set; }
        public float DiMuonVeSom { get { return _diMuonVeSom; } set { _diMuonVeSom = value; OnPropertyChanged(); } }
        private double _tongTimeOT { get; set; }
        public double TongTimeOT { get { return _tongTimeOT; } set { _tongTimeOT = value; OnPropertyChanged(); } }
        private int _nNgayNghiPhep { get; set; }
        public int NgayNghiPhep { get { return _nNgayNghiPhep; } set { _nNgayNghiPhep = value; OnPropertyChanged(); } }
        #endregion

        public ObservableCollection<int> ThangTrongNam { get; set; }
        public ObservableCollection<int> NamTuTruocDenGio { get; set; }
        private void LoadComboBoxData()
        {
            ThangTrongNam = new ObservableCollection<int>();
            for (int thang = 1; thang <= 12; thang++)
            {
                ThangTrongNam.Add(thang);
            }

            NamTuTruocDenGio = new ObservableCollection<int>();
            for (int nam = 2012; nam <= 2024; nam++)
            {
                NamTuTruocDenGio.Add(nam);
            }
        }

        public string OpenFilePath()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return null;
        }
        public void ImportExcelData(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++)
                {

                    BangCongNhanSu dataModel = new BangCongNhanSu();
                    List<string> MaNhanVienList = new List<string>();
                    List<string> HoTenList = new List<string>();

                    for (int col = 1; col <= 2; col++)
                    {
                        string value = worksheet.Cells[row, col].Value?.ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (col == 1)
                            {
                                MaNhanVienList.Add(value);
                            }
                            else if (col == 2)
                            {
                                HoTenList.Add(value);
                            }
                        }
                    }

                    dataModel.MaNhanVien = string.Join(", ", MaNhanVienList);
                    dataModel.HoTen = string.Join(", ", HoTenList);

                    DateTime ngayChamCong;
                    if (DateTime.TryParse(worksheet.Cells[row, 3].Value?.ToString(), out ngayChamCong))
                    {
                        dataModel.Ngay = ngayChamCong;
                    }

                    DateTime gioVao;
                    if (DateTime.TryParse(worksheet.Cells[row, 4].Value?.ToString(), out gioVao))
                    {
                        dataModel.GioVao = gioVao;
                    }

                    DateTime gioRa;
                    if (DateTime.TryParse(worksheet.Cells[row, 5].Value?.ToString(), out gioRa))
                    {
                        dataModel.GioRa = gioRa;
                    }

                    BangCongNhanSuDataList.Add(dataModel);
                }
            }
        }
        public void CalculationTrackingData()
        {
            ListData = new ObservableCollection<BangCongNhanSu>(_bangCongNhanSuRepository.AsQueryable().Where(x => x.Thang == Thang && x.Nam == Nam).ToList());

            Dictionary<string, int> employeeCount = new Dictionary<string, int>();

            foreach (var bangCong in ListData)
            {
                string maNhanVien = bangCong.MaNhanVien;

                if (employeeCount.ContainsKey(maNhanVien))
                {
                    employeeCount[maNhanVien]++;
                }
                else
                {
                    employeeCount[maNhanVien] = 1;
                }
            }

            foreach (var kvp in employeeCount)
            {
                Console.WriteLine($"Mã nhân viên {kvp.Key} xuất hiện {kvp.Value} lần");
            }

            BangCong dataModel = new BangCong();
        }
        public bool CanImportExcelData()
        {
            if (BangCongNhanSuDataList.Count != 0)
                return false;
            return true;
        }
        public bool CanSaveData()
        {
            if (Thang == 0 || Nam == 0 || BangCongNhanSuDataList.Count == 0)
                return false;
            return true;
        }
        public bool CanSaveData2()
        {
            if (Thang == 0 || Nam == 0 || BangCongDataList.Count == 0)
                return false;
            return true;
        }
        public bool CanClearData()
        {
            if (Thang == 0 || Nam == 0 || BangCongNhanSuDataList.Count == 0)
                return false;
            return true;
        }
        public bool CanCalculationData()
        {
            if (Thang == 0 || Nam == 0 || BangCongNhanSuDataList.Count == 0)
                return false;
            return true;
        }
        public bool CanAddBangCongData()
        {
            return true;
        }

        public ImportTrackingViewModel(IViewModelFactory viewModelFactory, MainContentStore mainContentStore, IRepository<BangCongNhanSu> BangCongNhanSuRepository, IUnitOfWork unitOfWork)
        {
            _bangCongNhanSuRepository = BangCongNhanSuRepository;
            _unitOfWork = unitOfWork;
            LoadComboBoxData();

            ImportDataCommand = new Commands.RelayCommand<object>(p => CanImportExcelData(), p => ImportExcelData(OpenFilePath().ToString()));

            AddCommand = new Commands.RelayCommand<object>((p) => CanSaveData(),
                async (p) =>
                {
                    await _unitOfWork.BeginTransactionAsync();

                    try
                    {
                        //bool exists = await _bangCongRepository.IsBangCongExistsAsync(Thang, Nam);
                        bool exists = await _bangCongNhanSuRepository.AsQueryable().AnyAsync(bc => bc.Thang == Thang && bc.Nam == Nam);

                        if (exists)
                        {
                            await _unitOfWork.RollbackAsync();
                            MessageBox.Show("Bảng công cho tháng " + Thang + ", năm " + Nam + " đã tồn tại.");
                            return;
                        }

                        foreach (var bangCong in BangCongNhanSuDataList)
                        {
                            bangCong.Thang = Thang;
                            bangCong.Nam = Nam;
                            await _bangCongNhanSuRepository.AddAsync(bangCong);

                        }

                        await _unitOfWork.CommitAsync();
                        MessageBox.Show("Thêm thành công");
                    }
                    catch (Exception ex)
                    {
                        await _unitOfWork.RollbackAsync();
                        MessageBox.Show("Lỗi hệ thống: " + ex.Message);
                    }
                });

            AddBangCongCommand = new Commands.RelayCommand<object>((p) => CanSaveData2(),
                async (p) =>
                {
                    await _unitOfWork.BeginTransactionAsync();

                    try
                    {
                        bool exists = await _bangCongRepository.AsQueryable().AnyAsync(bc => bc.Thang == Thang && bc.Nam == Nam);

                        if (exists)
                        {
                            await _unitOfWork.RollbackAsync();
                            MessageBox.Show("Bảng công cho tháng " + Thang + ", năm " + Nam + " đã tồn tại.");
                            return;
                        }

                        foreach (var bangCong in BangCongDataList)
                        {
                            bangCong.Thang = Thang;
                            bangCong.Nam = Nam;
                            await _bangCongRepository.AddAsync(bangCong);

                        }

                        await _unitOfWork.CommitAsync();
                        MessageBox.Show("Thêm thành công");
                    }
                    catch (Exception ex)
                    {
                        await _unitOfWork.RollbackAsync();
                        MessageBox.Show("Lỗi hệ thống: " + ex.Message);
                    }
                });

            ClearDataCommand = new Commands.RelayCommand<object>(p => CanClearData(), 
            p =>
            {
                MessageBoxResult result = MessageBox.Show("Xóa dữ liệu để nhập bảng công mới?", "Xác nhận xóa dữ liệu", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                     BangCongNhanSuDataList.Clear();
                }
            });

            CaculationCommand = new Commands.RelayCommand<object>(p => CanCalculationData(),
                p =>
                {
                    CalculationTrackingData();
                });
        }
    }
}
