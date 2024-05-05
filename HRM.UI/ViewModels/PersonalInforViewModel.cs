using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using HRM.UI.Stores;
using HRM.UI.Factories;
using System.IO;
using HRM.UI.States.Users;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;

namespace HRM.UI.ViewModels
{
    public class PersonalInforViewModel : BaseViewModel
    {
        private IUnitOfWork _unitOfWork;

        private IRepository<NhanSu> _hosoRepository;
        private IRepository<BoPhan> _boPhanRepository;
        private IUserStore _userStore;
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

        public bool check;

        private string _hoTen;
        public string HoTen
        {
            get { return _hoTen; }
            set
            {
                _hoTen = value;
                if (!ValidationHoTen(HoTen) && !HoTen.IsNullOrEmpty())
                {
                    ErrorHoTen = "Họ và tên chưa hợp lệ";
                    check = false;
                }
                else
                {
                    ErrorHoTen = "";
                    check = true;
                }
                OnPropertyChanged(nameof(HoTen));
            }
        }
        private DateTime _ngaySinh;

        public DateTime NgaySinh
        {
            get { return _ngaySinh; }
            set { _ngaySinh = value; OnPropertyChanged(nameof(NgaySinh)); }
        }
        private string _nguyenQuan;

        public string NguyenQuan
        {
            get { return _nguyenQuan; }
            set
            {
                _nguyenQuan = value;
                if (!ValidationHoTen(NguyenQuan) && !NguyenQuan.IsNullOrEmpty())
                {
                    check = false;
                    ErrorNguyenQuan = "Nguyên quán chưa hợp lệ!";
                }
                else
                {
                    check = true;
                    ErrorNguyenQuan = "";
                }
                OnPropertyChanged(nameof(NguyenQuan));
            }
        }
        private string _danToc;

        public string DanToc
        {
            get { return _danToc; }
            set { _danToc = value; OnPropertyChanged(nameof(DanToc)); }
        }
        private string _cccd;

        public string CCCD
        {
            get { return _cccd; }
            set
            {
                _cccd = value;
                if (!ValidationCCCD(CCCD) && !CCCD.IsNullOrEmpty())
                {
                    check = false;
                    ErrorCCCD = "Số CCCD gồm 12 chữ số!";
                }
                else
                {
                    check = true;
                    ErrorCCCD = "";
                }
                OnPropertyChanged(nameof(CCCD));
            }
        }
        private string _trinhDoVanHoa;

        public string TrinhDoVanHoa
        {
            get { return _trinhDoVanHoa; }
            set { _trinhDoVanHoa = value; OnPropertyChanged(nameof(TrinhDoVanHoa)); }
        }
        private string _ketNapDang;

        public string KetNapDang
        {
            get { return _ketNapDang; }
            set
            {
                _ketNapDang = value;
                //if (KetNapDang.Equals("Đã kết nạp") == false)
                //{
                //    check = false;
                //    ErrorKetNapDang = "'Đã kết nạp' hoặc 'Chưa kết nạp'";
                //}
                //else if (KetNapDang.Equals("Chưa kết nạp") == false)
                //{
                //    check = false;
                //    ErrorKetNapDang = "'Đã kết nạp' hoặc 'Chưa kết nạp'";
                //}
                //else
                //{
                //    check = true;
                //    ErrorKetNapDang = "";
                //}
                OnPropertyChanged(nameof(KetNapDang));
            }
        }
        private string _khenThuong;

        public string KhenThuong
        {
            get { return _khenThuong; }
            set { _khenThuong = value; OnPropertyChanged(nameof(KhenThuong)); }
        }
        private string _soThich;

        public string SoThich
        {
            get { return _soThich; }
            set
            {
                _soThich = value;
                if (!ValidationHoTen(SoThich) && !SoThich.IsNullOrEmpty())
                {
                    check = false;
                    ErrorSoThich = "Sở thích chưa hợp lệ!";
                }
                else
                {
                    check = true;
                    ErrorSoThich = "";
                }
                OnPropertyChanged(nameof(SoThich));
            }
        }
        private bool _gioiTinh;

        public bool GioiTinh
        {
            get { return _gioiTinh; }
            set { _gioiTinh = value; OnPropertyChanged(nameof(GioiTinh)); }
        }
        private string _noiSinh;

        public string NoiSinh
        {
            get { return _noiSinh; }
            set { _noiSinh = value; OnPropertyChanged(nameof(NoiSinh)); }
        }
        private string _tonGiao;

        public string TonGiao
        {
            get { return _tonGiao; }
            set
            {
                _tonGiao = value;
                OnPropertyChanged(nameof(TonGiao));
            }
        }
        private DateTime _capNgay = DateTime.Now;

        public DateTime CapNgay
        {
            get { return _capNgay; }
            set { _capNgay = value; OnPropertyChanged(nameof(CapNgay)); }
        }
        private string _noiKetNapDoan;

        public string NoiKetNapDoan
        {
            get { return _noiKetNapDoan; }
            set { _noiKetNapDoan = value; OnPropertyChanged(nameof(NoiKetNapDoan)); }
        }
        private string _noiKetNapDang;

        public string NoiKetNapDang
        {
            get { return _noiKetNapDang; }
            set
            {
                _noiKetNapDang = value;
                if (!ValidationHoTen(NoiKetNapDang) && !NoiKetNapDang.IsNullOrEmpty())
                {
                    check = false;
                    ErrorNoiKetNapDang = "Nơi kết nạp đảng chưa hợp lệ chưa hợp lệ!";
                }
                else
                {
                    check = true;
                    ErrorNoiKetNapDang = "";
                }
                OnPropertyChanged(nameof(NoiKetNapDang));
            }
        }
        private string _maNhanVien;

        public string MaNhanVien
        {
            get { return _maNhanVien; }
            set
            {
                _maNhanVien = value;
                if (!IsAlphaNumeric(MaNhanVien) && !MaNhanVien.IsNullOrEmpty())
                {
                    //check = false;
                    ErrorMaNhanVien = "Mã nhân viên chưa hợp lệ!";
                }
                else
                {
                    ErrorMaNhanVien = "";
                    //check = true;
                }
                OnPropertyChanged(nameof(MaNhanVien));
            }
        }
        private string _sTK;
        public string STK
        {
            get
            {
                return _sTK;
            }
            set
            {
                _sTK = value;
                if (!ValidationSTK(STK) && !STK.IsNullOrEmpty())
                {
                    check = false;
                    ErrorSTK = "Số tài khoản chưa hợp lệ!";
                }
                else
                {
                    check = true;
                    ErrorSTK = "";
                }
                OnPropertyChanged(nameof(STK));
            }
        }

        private string _maSoBHXH;
        public string MaSoBHXH
        {
            get
            {
                return _maSoBHXH;
            }
            set
            {
                _maSoBHXH = value;
                if (!ValidationMaSoBHXH(MaSoBHXH) && !MaSoBHXH.IsNullOrEmpty())
                {
                    check = false;
                    ErrorMaSoBHXH = "Mã số BHXH gồm 10 chữ số!";
                }
                else
                {
                    check = true;
                    ErrorMaSoBHXH = "";
                }
                OnPropertyChanged(nameof(MaSoBHXH));
            }
        }

        private string _maSoThue;
        public string MaSoThue
        {
            get
            {
                return _maSoThue;
            }
            set
            {
                _maSoThue = value;
                if (!ValidationMaSoThue(MaSoThue) && !MaSoThue.IsNullOrEmpty())
                {
                    //check = false;
                    ErrorMaSoThue = "Mã số thuế gồm 10 hoặc 13 chữ số!"; 
                }
                else
                {
                    //check = true;
                    ErrorMaSoThue = "";
                }
                OnPropertyChanged(nameof(MaSoThue));
            }
        }

        //Image
        private string _imageSource;
        public string ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }


        //Combobox 
        private void LoadComboBoxData()
        {
            ListBoPhan = new ObservableCollection<BoPhan>(_boPhanRepository.AsQueryable().ToList());
            SeletedCboBoPhan = ListBoPhan.FirstOrDefault();

            EthnicityData = new ObservableCollection<string>();
            EthnicityData.Add("Kinh");
            EthnicityData.Add("Tày");
            EthnicityData.Add("Thái");
            EthnicityData.Add("Mường");
            EthnicityData.Add("H'Mông");
            EthnicityData.Add("Dao");
            EthnicityData.Add("Khơ Mú");
            EthnicityData.Add("Nùng");
            EthnicityData.Add("Gia Rai");
            EthnicityData.Add("Ê Đê (Ê Đêh)");
            EthnicityData.Add("Ba Na");
            EthnicityData.Add("Xơ Đăng");
            EthnicityData.Add("Chăm");
            EthnicityData.Add("Sán Chay");
            EthnicityData.Add("Cơ Ho");
            EthnicityData.Add("Chu Ru");
            EthnicityData.Add("Giáy");
            EthnicityData.Add("Lào");
            EthnicityData.Add("Lự");
            EthnicityData.Add("M'Nông");
            EthnicityData.Add("Xtiêng");
            EthnicityData.Add("Bru - Vân Kiều");
            EthnicityData.Add("Cơ Tu");
            EthnicityData.Add("Ta Ôi");
            EthnicityData.Add("Co");
            EthnicityData.Add("Ro Mam");
            EthnicityData.Add("Khơ Me");
            EthnicityData.Add("Hrê");
            EthnicityData.Add("Ra Glai");
            EthnicityData.Add("Xinh Mun");
            EthnicityData.Add("Cho Ro");
            EthnicityData.Add("Hà Nhì");
            EthnicityData.Add("La Chí");
            EthnicityData.Add("La Ha");
            EthnicityData.Add("Pu Péo");
            EthnicityData.Add("Chứt");
            EthnicityData.Add("Lô Lô");
            EthnicityData.Add("Mảng");
            EthnicityData.Add("Pà Thẻn");
            EthnicityData.Add("Cờ Lao");
            EthnicityData.Add("Bố Y");
            EthnicityData.Add("La Hu");
            EthnicityData.Add("Lự");
            EthnicityData.Add("Ngái");
            EthnicityData.Add("Pák Tu");
            EthnicityData.Add("Chơ Ro");
            EthnicityData.Add("Xinh Mun");
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



        #region Validation
        private string _errorMaNhanVien = "";
        public string ErrorMaNhanVien { get { return _errorMaNhanVien; } set { _errorMaNhanVien = value; OnPropertyChanged(nameof(ErrorMaNhanVien)); } }

        private string _errorHoTen = " ";
        public string ErrorHoTen { get { return _errorHoTen; } set { _errorHoTen = value; OnPropertyChanged(nameof(ErrorHoTen)); } }

        private string _errorNS;
        public string ErrorNS { get { return _errorNS; } set { _errorNS = value; OnPropertyChanged(nameof(ErrorNS)); } }

        private string _errorNguyenQuan;
        public string ErrorNguyenQuan { get { return _errorNguyenQuan; } set { _errorNguyenQuan = value; OnPropertyChanged(nameof(ErrorNguyenQuan)); } }

        private string _errorCCCD;
        public string ErrorCCCD { get { return _errorCCCD; } set { _errorCCCD = value; OnPropertyChanged(nameof(ErrorCCCD)); } }

        private string _errorKetNapDang;
        public string ErrorKetNapDang { get { return _errorKetNapDang; } set { _errorKetNapDang = value; OnPropertyChanged(nameof(ErrorKetNapDang)); } }

        private string _errorSoThich;
        public string ErrorSoThich { get { return _errorSoThich; } set { _errorSoThich = value; OnPropertyChanged(nameof(ErrorSoThich)); } }

        private string _errorSTK;
        public string ErrorSTK { get { return _errorSTK; } set { _errorSTK = value; OnPropertyChanged(nameof(ErrorSTK)); } }

        private string _errorMaSoBHXH;
        public string ErrorMaSoBHXH { get { return _errorMaSoBHXH; } set { _errorMaSoBHXH = value; OnPropertyChanged(nameof(ErrorMaSoBHXH)); } }

        private string _errorMaSoThue;
        public string ErrorMaSoThue { get { return _errorMaSoThue; } set { _errorMaSoThue = value; OnPropertyChanged(nameof(ErrorMaSoThue)); } }

        private string _errorTonGiao;
        public string ErrorTonGiao { get { return _errorTonGiao; } set { _errorTonGiao = value; OnPropertyChanged(nameof(ErrorTonGiao)); } }

        private string _errorCapNgay;
        public string ErrorCapNgay { get { return _errorCapNgay; } set { _errorCapNgay = value; OnPropertyChanged(nameof(ErrorCapNgay)); } }

        private string _errorNoiKetNapDang;
        public string ErrorNoiKetNapDang { get { return _errorNoiKetNapDang; } set { _errorNoiKetNapDang = value; OnPropertyChanged(nameof(ErrorNoiKetNapDang)); } }
        #endregion


        public PersonalInforViewModel(IUserStore userStore, IRepository<BoPhan> bophanRepository, IRepository<NhanSu> hosoRepository, IUnitOfWork unitOfWork, ChildContentStore childContentStore, IViewModelFactory viewModelFactory)
        {
            _userStore = userStore;
            _hosoRepository = hosoRepository;
            _boPhanRepository = bophanRepository;
            _unitOfWork = unitOfWork;
            _viewModelFactory = viewModelFactory;
            LoadComboBoxData();
            LoadData();
            FamilyInforCommand = new Commands.RelayCommand<object>(p => true, p =>
            {
                childContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.FamilyInfor);
            });
            //Load combobox
            //Handle radio button
            GioiTinh = true;
            SelectGenderCommand = new CommunityToolkit.Mvvm.Input.RelayCommand<object>(SelectGender);
            //Upload ảnh
            UploadImageCommand = new Commands.RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg",
                    Title = "Select an image file"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    ImageSource = openFileDialog.FileName;  // Update the UI
                    SaveImageToFolder(openFileDialog.FileName);  // Save the file
                }
            });

            // Thêm thông tin bản thân vào database
            AddCommand = new Commands.RelayCommand<object>((p) =>
            {
                if(check == false)
                {
                    return false;
                }
                return true;
            }, async (p) =>
            {
                if (string.IsNullOrEmpty(MaNhanVien))
                {
                    ErrorMaNhanVien = "Không được để trống mục này";
                    return;
                }
                if (string.IsNullOrEmpty(HoTen))
                {
                    ErrorHoTen = "Không được để trống mục này";
                    return;
                }
                if (string.IsNullOrEmpty(NguyenQuan))
                {
                    ErrorNguyenQuan = "Không được để trống mục này";
                    return;
                }
                if (string.IsNullOrEmpty(CCCD))
                {
                    ErrorCCCD = "Không được để trống mục này";
                    return;
                }
                if (string.IsNullOrEmpty(KetNapDang))
                {
                    ErrorKetNapDang = "Không được để trống mục này";
                    return;
                }
                if (string.IsNullOrEmpty(SoThich))
                {
                    ErrorSoThich = "Không được để trống mục này";
                    return;
                }
                if (string.IsNullOrEmpty(STK))
                {
                    ErrorSTK = "Không được để trống mục này";
                    return;
                }
                if (string.IsNullOrEmpty(MaSoBHXH))
                {
                    ErrorMaSoBHXH = "Không được để trống mục này";
                    return;
                }
                if (string.IsNullOrEmpty(MaSoThue))
                {
                    ErrorMaSoThue = "Không được để trống mục này";
                    return;
                }
                if (string.IsNullOrEmpty(NoiKetNapDang))
                {
                    ErrorNoiKetNapDang = "Không được để trống mục này";
                    return;
                }
                if (_hosoRepository.AsQueryable().Any(x => x.MaNhanVien.Equals(MaNhanVien)))
                {
                    resetError();
                    ErrorMaNhanVien = "Mã nhân viên đã tồn tại";
                    return;
                }

                if (IsAlphaNumeric(MaNhanVien) == false)
                {
                    resetError();
                    ErrorMaNhanVien = "Mã nhân viên không hợp lệ";
                    return;
                }

                if (ValidationHoTen(HoTen) == false)
                {
                    resetError();
                    ErrorHoTen = "Họ và tên không hợp lệ";
                    return;
                }

                if (string.IsNullOrEmpty(NgaySinh.ToString()))
                {
                    resetError();
                    ErrorNS = "Ngày tháng năm sinh không hợp lệ";
                    return;
                }

                if (ValidationHoTen(NguyenQuan) == false)
                {
                    resetError();
                    ErrorNguyenQuan = "Nguyên quán không hợp lệ";
                    return;
                }

                if (ValidationCCCD(CCCD) == false)
                {
                    resetError();
                    ErrorCCCD = "Số CCCD phải là 12 chữ số";
                    return;
                }

                //if (KetNapDang.Contains("Đã kết nạp") == false)
                //{
                //    resetError();
                //    ErrorKetNapDang = "'Đã kết nạp' hoặc 'Chưa kết nạp'";
                //    return;
                //}

                //if (KetNapDang.Contains("Chưa kết nạp") == false)
                //{
                //    resetError();
                //    ErrorKetNapDang = "'Đã kết nạp' hoặc 'Chưa kết nạp'";
                //    return;
                //}

                //if (ValidationHoTen(SoThich))
                //{
                //    resetError();
                //    ErrorSoThich = "Sở thích chưa hợp lệ";
                //    return;
                //}

                //if (ValidationSTK(STK))
                //{
                //    resetError();
                //    ErrorSTK = "Số tài khoản chưa hợp lệ!";
                //    return;
                //}

                //if (ValidationMaSoBHXH(MaSoBHXH))
                //{
                //    resetError();
                //    ErrorMaSoBHXH = "Mã số thuế gồm 10 hoặc 13 chữ số!";
                //    return;
                //}

                //if (ValidationMaSoThue(MaSoThue))
                //{
                //    resetError();
                //    ErrorMaSoThue = "Mã số BHXH gồm 10 chữ số!";
                //    return;
                //}

                //if (ValidationHoTen(NoiKetNapDang))
                //{
                //    resetError();
                //    ErrorNoiKetNapDang = "Nơi kết nạp đảng chưa hợp lệ chưa hợp lệ!";
                //    return;
                //}
                var NhanSu = new NhanSu()
                {
                    MaNhanVien = MaNhanVien,
                    Anh = CoppyLink.ToString(),
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
                    BoPhanId = SeletedCboBoPhan.Id,
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

        public void resetError()
        {
            ErrorHoTen = "";
            ErrorMaNhanVien = "";
            ErrorNguyenQuan = "";
            ErrorNS = "";
            ErrorCCCD = "";
            ErrorKetNapDang = "";
            ErrorSoThich = "";
        }
        public string CoppyLink = "";
        private void SaveImageToFolder(string filePath)
        {
            //string directoryPath = @"D:\PersonInfo\";  // Modify the path as needed
            string directoryPath = @"C:\Users\Admin\Desktop\DoAn\HRM\HRM.UI\ViewModels\PersonImage\";  // Modify the path as needed


            string newFileName = $"Image_{DateTime.Now:yyyyMMddHHmmss}.jpg";  // Tạo tên mới cho file dựa trên thời gian
            string destinationPath = Path.Combine(directoryPath, newFileName);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            File.Copy(filePath, destinationPath, true);  // Lưu file với tên mới
            //CoppyLink = @"D:\PersonInfo\" + newFileName;
            CoppyLink = @"C:\Users\Admin\Desktop\DoAn\HRM\HRM.UI\ViewModels\PersonImage\" + newFileName;

        }
        public void LoadData()
        {
            if (_userStore.CurrentNhanSu != null)
            {
                NhanSu NhanSu = _hosoRepository.AsQueryable().FirstOrDefault(x => x.MaNhanVien == _userStore.CurrentNhanSu.MaNhanVien);
                MaNhanVien = NhanSu.MaNhanVien;
                ImageSource = NhanSu.Anh;
                HoTen = NhanSu.HoTen;
                NgaySinh = (DateTime)NhanSu.NgaySinh;
                NguyenQuan = NhanSu.NguyenQuan;
                DanToc = NhanSu.DanToc;
                CCCD = NhanSu.CCCD;
                KetNapDang = NhanSu.KetNapDang;
                SoThich = NhanSu.SoThich;
                STK = NhanSu.STK;
                MaSoBHXH = NhanSu.MaSoBHXH;
                MaSoThue = NhanSu.MaSoThue;
                TonGiao = NhanSu.TonGiao;
                CapNgay = NhanSu.CapNgay;
                NoiKetNapDang = NhanSu.NoiketNapDang;
                SeletedCboBoPhan.Id = (int)NhanSu.BoPhanId;
            }
            else
            {
                MaNhanVien = "";
                ImageSource = "";
                HoTen = "";
                NguyenQuan = "";
                DanToc = "";
                CCCD = "";
                KetNapDang = "";
                SoThich = "";
                STK = "";
                MaSoBHXH = "";
                MaSoThue = "";
                TonGiao = "";
                NoiKetNapDang = "";
            }
        }

        static bool IsAlphaNumeric(string input)
        {

            // Mẫu regex để kiểm tra chỉ chứa chữ cái và chữ số
            string pattern = @"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d]{5,10}$";

            // Kiểm tra xem chuỗi đầu vào có khớp với mẫu regex không
            return Regex.IsMatch(input, pattern);
        }

        static bool ValidationHoTen(string input)
        {
            // Kiểm tra chuỗi input không được null và không chỉ gồm khoảng trắng
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            // Kiểm tra chuỗi input chỉ chứa các ký tự chữ cái và dấu cách
            if (!Regex.IsMatch(input, @"^[a-zA-ZÀ-Ỹà-ỹ ]+$"))
            {
                return false;
            }

            // Kiểm tra xem chuỗi input có ít nhất một khoảng trắng để phân tách giữa họ và tên
            if (!input.Contains(" "))
            {
                return false;
            }

            return true;
        }

        static bool ValidationCCCD(string input)
        {
            string pattern = @"^\d{12}$";

            // Kiểm tra xem chuỗi đầu vào có khớp với mẫu regex không
            return Regex.IsMatch(input, pattern);
        }

        static bool ValidationMaSoBHXH(string input)
        {
            string pattern = @"^\d{10}$";

            // Kiểm tra xem chuỗi đầu vào có khớp với mẫu regex không
            return Regex.IsMatch(input, pattern);
        }

        static bool ValidationMaSoThue(string input)
        {
            string pattern = @"^\d{10}|\d{13}$";

            // Kiểm tra xem chuỗi đầu vào có khớp với mẫu regex không
            return Regex.IsMatch(input, pattern);
        }

        static bool ValidationSTK(string input)
        {
            string pattern = @"[0-9]{4,18}$";

            // Kiểm tra xem chuỗi đầu vào có khớp với mẫu regex không
            return Regex.IsMatch(input, pattern);
        }

    }
}

