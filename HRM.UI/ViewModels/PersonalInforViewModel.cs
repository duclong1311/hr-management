using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using HRM.UI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Runtime.CompilerServices;
using HRM.UI.Stores;
using HRM.UI.Factories;

namespace HRM.UI.ViewModels
{
    public class PersonalInforViewModel : BaseViewModel
    {
        private IUnitOfWork _unitOfWork;

        private IRepository<NhanSu> _hosoRepository;
        private IRepository<NhanSu> _repository;
        private IRepository<BoPhan> _boPhanRepository;
        private IRepository<ChucVu> _chucVuRepository;
        private readonly IViewModelFactory _viewModelFactory;
        public ICommand AddCommand { get; set; }
        public ICommand UploadImageCommand { get; set; }
        public ICommand SelectGenderCommand { get; set; }
        public ICommand FamilyInforCommand { get; set; }
        public ObservableCollection<string> EthnicityData { get; set; }

        private ObservableCollection<BoPhan> _listBoPhan;
        public ObservableCollection<BoPhan> ListBoPhan 
        {
            get => _listBoPhan; 
            set 
            { 
                _listBoPhan = value; OnPropertyChanged(); 
            } 
        }

        private ObservableCollection<ChucVu> _listChucVu;
        public ObservableCollection<ChucVu> ListChucVu 
        {
            get => _listChucVu; 
            set 
            {
                _listChucVu = value; OnPropertyChanged(); 
            } 
        }
        private string _filterBoPhan = "";
        public string FilterBoPhan { get => _filterBoPhan; set { _filterBoPhan = value; OnPropertyChanged(); LoadComboBoxData(); } }
        private string _filterChucVu = "";
        public string FilterChucVu { get => _filterChucVu; set { _filterChucVu = value; OnPropertyChanged(); LoadComboBoxData(); } }

        private BoPhan _selectedCboBoPhan;
        public BoPhan SeletedCboBoPhan { get { return _selectedCboBoPhan; } set { _selectedCboBoPhan = value; OnPropertyChanged(); } }

        private ChucVu _selectedCboChucVu;
        public ChucVu SeletedCboChucVu { get { return _selectedCboChucVu; } set { _selectedCboChucVu = value; OnPropertyChanged(); } }

        private string _hoTen;
        public string HoTen
        {
            get { return _hoTen; }
            set { _hoTen = value; OnPropertyChanged(); }
        }
        private DateTime _ngaySinh;

        public DateTime NgaySinh
        {
            get { return _ngaySinh; }
            set { _ngaySinh = value; OnPropertyChanged(); }
        }
        private string _nguyenQuan;

        public string NguyenQuan
        {
            get { return _nguyenQuan; }
            set { _nguyenQuan = value; OnPropertyChanged(); }
        }
        private string _danToc;

        public string DanToc
        {
            get { return _danToc; }
            set { _danToc = value; OnPropertyChanged(); }
        }
        private string _cccd;

        public string CCCD
        {
            get { return _cccd; }
            set { _cccd = value; OnPropertyChanged(); }
        }
        private string _trinhDoVanHoa;

        public string TrinhDoVanHoa
        {
            get { return _trinhDoVanHoa; }
            set { _trinhDoVanHoa = value; OnPropertyChanged(); }
        }
        private string _ketNapDang;

        public string KetNapDang
        {
            get { return _ketNapDang; }
            set { _ketNapDang = value; OnPropertyChanged(); }
        }
        private string _khenThuong;

        public string KhenThuong
        {
            get { return _khenThuong; }
            set { _khenThuong = value; OnPropertyChanged(); }
        }
        private string _soThich;

        public string SoThich
        {
            get { return _soThich; }
            set { _soThich = value; OnPropertyChanged(); }
        }
        private bool _gioiTinh;

        public bool GioiTinh
        {
            get { return _gioiTinh; }
            set { _gioiTinh = value; OnPropertyChanged(); }
        }
        private string _noiSinh;

        public string NoiSinh
        {
            get { return _noiSinh; }
            set { _noiSinh = value; OnPropertyChanged(); }
        }
        private string _tonGiao;

        public string TonGiao
        {
            get { return _tonGiao; }
            set { _tonGiao = value; OnPropertyChanged(); }
        }
        private DateTime _capNgay;

        public DateTime CapNgay
        {
            get { return _capNgay; }
            set { _capNgay = value; OnPropertyChanged(); }
        }
        private string _noiKetNapDoan;

        public string NoiKetNapDoan
        {
            get { return _noiKetNapDoan; }
            set { _noiKetNapDoan = value; OnPropertyChanged(); }
        }
        private string _noiKetNapDang;

        public string NoiKetNapDang
        {
            get { return _noiKetNapDang; }
            set { _noiKetNapDang = value; OnPropertyChanged(); }
        }
        private string _maNhanVien;

        public string MaNhanVien
        {
            get { return _maNhanVien; }
            set { _maNhanVien = value; OnPropertyChanged(); }
        }
        private string _sTK;
        public string STK
        {
            get
            {
                return _sTK;
            }
            set { _sTK = value; OnPropertyChanged(); }
        }

        private string _maSoBHXH;
        public string MaSoBHXH
        {
            get
            {
                return _maSoBHXH;
            }
            set { _maSoBHXH = value; OnPropertyChanged(); }
        }

        private string _maSoThue;
        public string MaSoThue
        {
            get
            {
                return _maSoThue;
            }
            set { _maSoThue = value; OnPropertyChanged(); }
        }

        //Image
        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }
        public void UploadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif)|*.jpg; *.jpeg; *.png; *.gif";
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;

                try
                {
                    BitmapImage bitmapImage = new BitmapImage(new Uri(selectedImagePath));

                    ImageSource = bitmapImage;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading image: " + ex.Message);
                }
            }
        }
        public bool CanUploadImage()
        {
            return true;
        }
        //Combobox 
        private void LoadComboBoxData()
        {
            ListBoPhan = new ObservableCollection<BoPhan>();

            ListChucVu = new ObservableCollection<ChucVu>();

            EthnicityData = new ObservableCollection<string>();
            EthnicityData.Add("Kinh");
            EthnicityData.Add("Mông");
            EthnicityData.Add("Tày");
        }

        //Handle radio button function
        private void SelectGender(object gender)
        {
            if (gender.ToString() == "Male")
            {
                GioiTinh = true; // Nam
            }
            else if (gender.ToString() == "Female")
            {
                GioiTinh = false; // Nữ
            }
        }

        public PersonalInforViewModel(IRepository<NhanSu> hosoRepository, IUnitOfWork unitOfWork, ChildContentStore childContentStore, IViewModelFactory viewModelFactory)
        {
            _hosoRepository = hosoRepository;
            _unitOfWork = unitOfWork;
            _viewModelFactory = viewModelFactory;

            FamilyInforCommand = new Commands.RelayCommand<object>(p => true, p =>
            {
                childContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.FamilyInfor);
            });
            //Load combobox
            LoadComboBoxData();

            //Handle radio button
            GioiTinh = true;
            SelectGenderCommand = new CommunityToolkit.Mvvm.Input.RelayCommand<object>(SelectGender);

            //Upload ảnh
            UploadImageCommand = new Commands.RelayCommand<object>(p => CanUploadImage(), p => UploadImage());

            // Thêm thông tin bản thân vào database
            AddCommand = new Commands.RelayCommand<object>((p) =>
            {

                return true;
            }, async (p) =>
            {
                var NhanSu = new NhanSu()
                {
                    MaNhanVien = MaNhanVien,
                    Anh = ImageSource.ToString(),
                    HoTen = HoTen,
                    GioiTinh = GioiTinh,
                    NgaySinh = NgaySinh,
                    NguyenQuan = NguyenQuan,
                    DanToc = DanToc,
                    CCCD = CCCD,
                    CapNgay = CapNgay,
                    TonGiao = TonGiao,
                    KetNapDang = KetNapDang,
                    NoiketNapDang = NoiKetNapDang,
                    SoThich = SoThich,
                    STK = STK, 
                    MaSoBHXH = MaSoBHXH,
                    MaSoThue = MaSoThue,
                    //BoPhanId = SeletedCboBoPhan.Id,
                    //ChucVuId = SeletedCboChucVu.Id,
                };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    NhanSu = await _hosoRepository.AddAsync(NhanSu);
                    if (NhanSu != null)
                    {
                        MessageBox.Show("Thêm thành công");
                    }
                    else
                    {
                        MessageBox.Show("Lỗi hệ thống");
                    }
                    await _unitOfWork.CommitAsync();
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });


        }
    }
}

