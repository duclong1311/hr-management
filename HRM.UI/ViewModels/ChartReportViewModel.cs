using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts.Wpf;
using LiveCharts;
using DocumentFormat.OpenXml.Wordprocessing;
using HRM.Domain.Models;
using HRM.Core.Repositories;
using System.Windows;
using HRM.Core.UnitOfWorks;
using System.Windows.Input;
using HRM.UI.Commands;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Presentation;

namespace HRM.UI.ViewModels
{
    public class ChartReportViewModel : BaseViewModel
    {
        private readonly IRepository<BangCong> _repositoryBangCong;

        private readonly IUnitOfWork _unitOfWork;
        public ICommand CalculateCommand { get; set; }

        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get { return _seriesCollection; }
            set
            {
                _seriesCollection = value;
                OnPropertyChanged("SeriesCollection");
            }
        }
        //private SeriesCollection _seriesCollection_column;
        //public SeriesCollection SeriesCollection_column
        //{
        //    get { return _seriesCollection_column; }
        //    set
        //    {
        //        _seriesCollection_column = value;
        //        OnPropertyChanged("SeriesCollection");
        //    }
        //}
        private ObservableCollection<BangCong> _listBangCong;
        public ObservableCollection<BangCong> ListBangCong
        {
            get => _listBangCong;
            set
            {
                _listBangCong = value;
                OnPropertyChanged();
            }
        }
        //private int _nam { get; set; }
        //public int Nam { get { return _nam; } set { _nam = value; OnPropertyChanged(); } }
        //private int _thang { get; set; }
        //public int Thang { get { return _thang; } set { _thang = value; OnPropertyChanged(); } }
        public ObservableCollection<int> Months { get; set; }
        public ObservableCollection<int> Years { get; set; }
        public ObservableCollection<string> Types { get; set; }
        public ObservableCollection<string> ChartTypes { get; set; }
        private int _selectedMonth;
        private int _selectedYear;
        private string _selectedType;
        private string _selectedChartType;
        public int SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                _selectedMonth = value;
                OnPropertyChanged(nameof(SelectedMonth));
            }
        }
        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                OnPropertyChanged(nameof(SelectedYear));
            }
        }
        public string SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
            }
        }
        public string SelectedChartType
        {
            get => _selectedChartType;
            set
            {
                _selectedChartType = value;
                OnPropertyChanged(nameof(SelectedChartType));
                UpdateChartVisibility();
            }
        }
        public bool IsPieChartVisible => SelectedChartType == "Biểu đồ tròn";
        public bool IsCartesianChartVisible => SelectedChartType == "Biểu đồ cột";
        private void UpdateChartVisibility()
        {
            OnPropertyChanged(nameof(IsPieChartVisible));
            OnPropertyChanged(nameof(IsCartesianChartVisible));
        }
        public ObservableCollection<string> Labels { get; set; }

        private Func<double, string> _yFormatter;
        public Func<double, string> YFormatter
        {
            get => _yFormatter;
            set => SetProperty(ref _yFormatter, value);
        }

        public double? TotalWorkingTime; // tổng số giờ đi làm các ngày trong tháng
        public double? TotalWorkingTime_Sunday; // tổng số giờ đi làm ngày chủ nhật
        public double? TotalUnworkingTime; // tổng số giờ đi làm về sớm
        public double? TotalOTTime; // tổng số giờ làm thêm
        private double TotalDefaultWorkingTime(int Thang, int Staff_number)
        {
            double total = 0;
            if (Thang == 1 || Thang == 3 || Thang == 5 || Thang == 7 || Thang == 8 || Thang == 10 || Thang == 12)
            {
                total = 27 * 8 * Staff_number;
            }
            else if (Thang == 2)
            {
                total = 24 * 8 * Staff_number;
            }
            else
            {
                total = 26 * 8 * Staff_number;
            }
            return total;
        }
        private void ThongKe(int Month, int Year, string Type)
        {
            TotalWorkingTime = 0;
            TotalWorkingTime_Sunday = 0;
            TotalUnworkingTime = 0;
            TotalOTTime = 0;

            if (Type.Equals("Giờ làm việc"))
            {
                ListBangCong = new ObservableCollection<BangCong>(_repositoryBangCong
                .AsQueryable()
                .Where(x => x.Thang == Month && x.Nam == Year)
                .ToList());

                if (ListBangCong.Count == 0)
                {
                    MessageBox.Show($"Bảng công tháng {Month}, năm {Year} không tồn tại.");
                    return;
                }

                for (int i = 0; i < ListBangCong.Count; i++)
                {
                    TotalWorkingTime += ListBangCong[i].TongSoNgayCong * 8;
                    TotalWorkingTime_Sunday += ListBangCong[i].TongSoNgayCongCN * 8;
                    TotalUnworkingTime += ListBangCong[i].DiMuonVeSom;
                    TotalOTTime += ListBangCong[i].TongTimeOT;
                }

                SeriesCollection = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "Giờ làm việc",
                        Values = new ChartValues<double> { TotalWorkingTime ?? 0 },
                        DataLabels = true
                    },
                    new PieSeries
                    {
                        Title = "Giờ làm việc CN",
                        Values = new ChartValues<double> { TotalWorkingTime_Sunday ?? 0 },
                        DataLabels = true
                    },
                    new PieSeries
                    {
                        Title = "Giờ đi sớm",
                        Values = new ChartValues<double> { TotalUnworkingTime ?? 0 },
                        DataLabels = true
                    },
                    new PieSeries
                    {
                        Title = "Giờ làm thêm",
                        Values = new ChartValues<double> { TotalOTTime ?? 0 },
                        DataLabels = true
                    }
                };
            }
            OnPropertyChanged(nameof(SeriesCollection));
            OnPropertyChanged(nameof(Labels));
            OnPropertyChanged(nameof(YFormatter));
        }
        private void LoadComboBoxData()
        {
            Months = new ObservableCollection<int>(Enumerable.Range(1, 12));
            Years = new ObservableCollection<int>(Enumerable.Range(2000, DateTime.Now.Year - 1999));
            Types = new ObservableCollection<string> { "Giờ làm việc", "Tiền lương" };
            ChartTypes = new ObservableCollection<string> { "Biểu đồ tròn", "Biểu đồ cột" };
        }

        public ChartReportViewModel(IRepository<BangCong> repositoryBangCong, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repositoryBangCong = repositoryBangCong;

            LoadComboBoxData();

            SelectedMonth = 1;
            SelectedYear = 2012;
            //SelectedType = "Giờ làm việc";

            CalculateCommand = new RelayCommand<object>(p =>
            {
                return true;
            }, async (p) =>
            {
                try
                {
                    ThongKe(SelectedMonth, SelectedYear, SelectedType);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
            //ThongKe(SelectedMonth, SelectedYear);

            // Update the SeriesCollection

        }
    }
}
