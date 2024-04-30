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

namespace HRM.UI.ViewModels
{
    public class TrackingViewModel : BaseViewModel
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<BangCong> _bangCongRepository;
        private ObservableCollection<BangCong> _bangCongDataList;
        public ObservableCollection<BangCong> BangCongDataList
        {
            get { return _bangCongDataList; }
            set
            {
                _bangCongDataList = value;
                OnPropertyChanged();
            }
        }
        public ICommand ImportDataCommand { get; set; }
        public ICommand AddCommand { get; set; }
        private int _tongSoNgayCong { get; set; }
        public int TongSoNgayCong { get { return _tongSoNgayCong; } set { value = _tongSoNgayCong; OnPropertyChanged(); } }
        private int _tongSoNgayCongCN { get; set; }
        public int TongSoNgayCongCN { get { return _tongSoNgayCongCN; } set { value = _tongSoNgayCongCN; OnPropertyChanged(); } }
        private int _tongSoNgayCongNgayLe { get; set; }
        public int TongSoNgayCongNgayLe { get { return _tongSoNgayCongNgayLe; } set { value = _tongSoNgayCongNgayLe; OnPropertyChanged(); } }
        private float _diMuonVeSom { get; set; }
        public float DiMuonVeSom { get { return _diMuonVeSom; } set { value = _diMuonVeSom; OnPropertyChanged(); } }
        private double _tongTimeOT { get; set; }
        public double TongTimeOT { get { return _tongTimeOT; } set { value = _tongTimeOT; OnPropertyChanged(); } }
        private int _nNgayNghiPhep { get; set; }
        public int NgayNghiPhep { get { return _nNgayNghiPhep; } set { value = _nNgayNghiPhep; OnPropertyChanged(); } }


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
                    BangCong dataModel = new BangCong();
                    List<string> MaNhanVienList = new List<string>();
                    List<string> HoTenList = new List<string>();
                    for (int col = 7; col <= 8; col++)
                    {
                        string value = worksheet.Cells[row, col].Value?.ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (col == 7)
                            {
                                MaNhanVienList.Add(value);
                            }
                            else if (col == 8)
                            {
                                HoTenList.Add(value);
                            }
                        }
                    }
                    //dataModel.NhanSu = string.Join(", ", MaNhanVienList);
                    //dataModel.HoTen = string.Join(", ", HoTenList);

                    int TongSoNgayCong;
                    if (int.TryParse(worksheet.Cells[row, 3].Value?.ToString(), out TongSoNgayCong))
                    {
                        dataModel.TongSoNgayCong = TongSoNgayCong;
                    }

                    int TongSoNgayCongCN;
                    if (int.TryParse(worksheet.Cells[row, 4].Value?.ToString(), out TongSoNgayCongCN))
                    {
                        dataModel.TongSoNgayCongCN = TongSoNgayCongCN;
                    }

                    int TongSoNgayCongNgayLe;
                    if (int.TryParse(worksheet.Cells[row, 5].Value?.ToString(), out TongSoNgayCongNgayLe))
                    {
                        dataModel.TongSoNgayCongNgayLe = TongSoNgayCongNgayLe;
                    }

                    float DiMuonVeSom;
                    if (float.TryParse(worksheet.Cells[row, 6].Value?.ToString(), out DiMuonVeSom))
                    {
                        dataModel.DiMuonVeSom = DiMuonVeSom;
                    }

                    double SoGioTangCa;
                    if (double.TryParse(worksheet.Cells[row, 7].Value?.ToString(), out SoGioTangCa))
                    {
                        dataModel.TongTimeOT = SoGioTangCa;
                    }

                    int NgayNghiPhep;
                    if (int.TryParse(worksheet.Cells[row, 8].Value?.ToString(), out NgayNghiPhep))
                    {
                        dataModel.NgayNghiPhep = NgayNghiPhep;
                    }

                    BangCongDataList.Add(dataModel);
                }
            }
        }
        public bool CanImportExcelData()
        {
            return true;
        }

        public TrackingViewModel(IRepository<BangCong> BangCongRepository, IUnitOfWork unitOfWork)
        {
            _bangCongRepository = BangCongRepository;
            _unitOfWork = unitOfWork;

            BangCongDataList = new ObservableCollection<BangCong>();

            ImportDataCommand = new Commands.RelayCommand<object>(p => CanImportExcelData(), p => ImportExcelData(OpenFilePath().ToString()));

            AddCommand = new Commands.RelayCommand<object>((p) =>
            {
                //if (string.IsNullOrEmpty(HoVaTen))
                //    return false;
                return true;
            }, async (p) =>

            {
                var BangCong = new BangCong()
                {
                    TongSoNgayCong = TongSoNgayCong,
                    TongSoNgayCongCN = TongSoNgayCongCN,
                    TongSoNgayCongNgayLe = TongSoNgayCongNgayLe,
                    DiMuonVeSom = DiMuonVeSom,
                    NgayNghiPhep = NgayNghiPhep,
                    TongTimeOT = TongTimeOT,
                };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    BangCong = await _bangCongRepository.AddAsync(BangCong);
                    await _unitOfWork.CommitAsync();
                    //LoadData();
                    if (BangCong != null)
                    {
                        MessageBox.Show("Thêm thành công");
                        //LoadData();
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
        //private void LoadData()
        //{
        //    List = new ObservableCollection<QuanHeGiaDinh>(BangCong.AsQueryable().ToList());
        //    /*            if (!String.IsNullOrWhiteSpace(Filter))
        //                {
        //                    List = new ObservableCollection<NhanSu>(_repository.AsQueryable().Where(x => x.MaNhanVien.Contains(Filter) || x.HoTen.Contains(Filter)).ToList());
        //                }*/
        //}
    }
}
