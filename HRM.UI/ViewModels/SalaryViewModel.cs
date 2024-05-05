using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using HRM.UI.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HRM.UI.ViewModels
{
    public class SalaryViewModel : BaseViewModel
    {
        public class LuongNhanVien : BaseViewModel
        {
            public string MaNhanVien { get; set; }
            public string HoTen { get; set; }
            public double TienCongThuong { get; set; }
            public double TienCongCN { get; set; }
            public double TienCongNgayLe { get; set; }
            public double TienTangCa { get; set; }
            public double TienNgayNghiPhep { get; set; }
            public double TienDiSomVeMuon { get; set; }
            public double TienBaoHiem { get; set; }
            public double PhuCap { get; set; }
            public double UngLuong {  get; set; }
            public double ThucNhan { get; set; }
        }
        private int _nam { get; set; }
        public int Nam { get { return _nam; } set { _nam = value; OnPropertyChanged(); } }
        private int _thang { get; set; }
        public int Thang { get { return _thang; } set { _thang = value; OnPropertyChanged(); } }
        public ObservableCollection<int> ThangTrongNam { get; set; }
        public ObservableCollection<int> NamTuTruocDenGio { get; set; }
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
        private ObservableCollection<LuongNhanVien> _listLuongNhanVien = new ObservableCollection<LuongNhanVien>();

        public ObservableCollection<LuongNhanVien> ListLuongNhanVien
        {
            get => _listLuongNhanVien;
            set
            {
                _listLuongNhanVien = value;
                OnPropertyChanged();
            }
        }
        private readonly IRepository<BangCong> _repositoryBangCong;
        private readonly IRepository<HopDong> _repositoryHopDong;
        private readonly IRepository<NhanSuChucVu> _repositoryNhanSuChucVu;
        private readonly IRepository<UngLuong> _repositoryUngLuong;
        private readonly IUnitOfWork _unitOfWork;
        public ICommand CalculateCommand { get; set; }
        public SalaryViewModel(IRepository<UngLuong> repositoryUngLuong, IRepository<NhanSuChucVu> repositoryNhanSuChucVu, IRepository<HopDong> repositoryHopDong, IRepository<BangCong> repositoryBangCong, IUnitOfWork unitOfWork)
        {
            LoadComboBoxData();
            _repositoryHopDong = repositoryHopDong;
            _repositoryBangCong = repositoryBangCong;
            _repositoryNhanSuChucVu = repositoryNhanSuChucVu;
            _repositoryUngLuong = repositoryUngLuong;
            _unitOfWork = unitOfWork;
            CalculateCommand = new RelayCommand<object>(p =>
            {
                return true;
            }, p =>
            {
                ListLuongNhanVien.Clear();
                TinhLuong();
            });
        }
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
        private void TinhLuong()
        {
            ListBangCong = new ObservableCollection<BangCong>(_repositoryBangCong.AsQueryable().Where(x => x.Thang == Thang && x.Nam == Nam).ToList());
            for (int i = 0; i < ListBangCong.Count; i++)
            {
                double? luongcoban = _repositoryHopDong.AsQueryable().OrderByDescending(x => x.NgayBatDau).FirstOrDefault(x => x.MaNhanVien == ListBangCong[i].MaNhanVien).LuongCoBan;
                int ngaycongthucte;
                if (Thang == 1 || Thang == 3 || Thang == 5 || Thang == 7 || Thang == 8 || Thang == 10 || Thang == 12)
                {
                    ngaycongthucte = 27;
                }
                else if (Thang == 2)
                {
                    ngaycongthucte = 24;
                }
                else
                {
                    ngaycongthucte = 26;
                }
                double luongngay = (double)(luongcoban / ngaycongthucte);
                double luonggio = (double)(luongngay / 8);
                double phucap = _repositoryNhanSuChucVu.AsQueryable().Include(x => x.NhanSu).Where(x => x.NhanSu.MaNhanVien == ListBangCong[i].MaNhanVien).OrderByDescending(x => x.PhuCapChucVu).FirstOrDefault().PhuCapChucVu;

                double? ungluong = null;

                // Thực hiện truy vấn và lưu trữ kết quả tạm thời
                var ungLuongRecord = _repositoryUngLuong.AsQueryable()
                    .Include(x => x.NhanSu)
                    .Where(x => x.NhanSu.MaNhanVien == ListBangCong[i].MaNhanVien &&
                                x.NgayUngLuong.HasValue &&
                                x.NgayUngLuong.Value.Date.Month == Thang &&
                                x.NgayUngLuong.Value.Date.Year == Nam)
                    .FirstOrDefault();

                // Kiểm tra kết quả trước khi truy cập SoTienUng
                if (ungLuongRecord != null)
                {
                    ungluong = ungLuongRecord.SoTienUng;
                }
                else
                {
                    // Xử lý trường hợp không tìm thấy bản ghi nào, ví dụ: gán ungluong là 0, hiển thị thông báo, v.v.
                    ungluong = 0;  // hoặc bất kỳ giá trị mặc định nào bạn muốn.
                }
                LuongNhanVien luongNhanVien = new LuongNhanVien()
                {
                    HoTen = ListBangCong[i].HoTen,
                    MaNhanVien = ListBangCong[i].MaNhanVien,
                    TienCongThuong = (double)(luongngay * ListBangCong[i].TongSoNgayCong),
                    TienCongCN = (double)(luongngay * ListBangCong[i].TongSoNgayCongCN * 2),
                    TienCongNgayLe = (double)(luongngay * ListBangCong[i].TongSoNgayCongNgayLe * 3),
                    TienTangCa = (double)(luonggio * ListBangCong[i].TongTimeOT * 1.5),
                    TienDiSomVeMuon = (double)(luonggio * ListBangCong[i].DiMuonVeSom),
                    TienNgayNghiPhep = (double)(luongngay * ListBangCong[i].NgayNghiPhep),
                    TienBaoHiem = (double)(luongcoban * 10.5 / 100),
                    PhuCap = phucap,
                    UngLuong = (double)ungluong
                };
                luongNhanVien.ThucNhan = (double)(luongNhanVien.TienCongThuong + luongNhanVien.TienCongCN + luongNhanVien.TienCongNgayLe - luongNhanVien.UngLuong
                    + luongNhanVien.TienTangCa - luongNhanVien.TienDiSomVeMuon + luongNhanVien.TienNgayNghiPhep - luongNhanVien.TienBaoHiem - luongNhanVien.TienBaoHiem);
                ListLuongNhanVien.Add(luongNhanVien);
            }
        }
    }
}
