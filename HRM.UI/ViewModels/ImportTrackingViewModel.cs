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
        public ICommand AddCommand { get; set; }
        public ICommand ImportDataCommand { get; set; }

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
        private string _maNhanVien { get; set; }
        public string MaNhanVien { get { return _maNhanVien; } set { _maNhanVien = value; OnPropertyChanged(); } }
        private string _hoTen { get; set; }
        public string HoTen { get { return _hoTen; } set { _hoTen = value; OnPropertyChanged(); } }
        private int _nam { get; set; }
        public int Nam { get { return _nam; } set { _nam = value; OnPropertyChanged(); } }
        private int _thang { get; set; }
        public int Thang { get { return _thang; } set { _thang = value; OnPropertyChanged(); } }
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
        public bool CanImportExcelData()
        {
            return true;
        }
        public bool CanSaveData()
        {
            if (Thang == 0 || Nam == 0 || BangCongNhanSuDataList.Count == 0)
                return false;
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
                        BangCongNhanSuDataList.Clear();
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
