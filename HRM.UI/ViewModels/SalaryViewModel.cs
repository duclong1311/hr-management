using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using HRM.UI.Commands;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PageSize = iTextSharp.text.PageSize;
using Font = iTextSharp.text.Font;
using System.Windows.Forms;
using Paragraph = iTextSharp.text.Paragraph;
using MessageBox = System.Windows.MessageBox;
using System.Globalization;
using DocumentFormat.OpenXml.ExtendedProperties;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.IO.Packaging;

namespace HRM.UI.ViewModels
{
    public class SalaryViewModel : BaseViewModel
    {
        public class LuongNhanVien : BaseViewModel
        {
            public string MaNhanVien { get; set; }
            public string HoTen { get; set; }
            public string ChucVu { get; set; }//
            public double? LuongCoBan { get; set; }//
            public int TongSoNgayCong { get; set; }//
            public double TienCongThuong { get; set; }
            public double LuongThucTe { get; set; }//
            public int NgayCongChuNhat { get; set; }//
            public double TienCongCN { get; set; }
            public double SoGioOT { get; set; }//
            public double PhuCapAnTrua { get; set; }//
            public double PhuCapDiLai { get; set; }//
            public double PhuCap { get; set; }
            public double TongLuong { get; set; }//
            public double BaoHiemXaHoi { get; set; }//
            public double BaoHiemYTe { get; set; }//
            public double SoGioDiMuonVeSom { get; set; }//
            public double TienCongNgayLe { get; set; }
            public double TienTangCa { get; set; }
            public double TienNgayNghiPhep { get; set; }
            public double TienDiSomVeMuon { get; set; }
            public double TienBaoHiem { get; set; }
            public double UngLuong { get; set; }
            public double TongKhauTru { get; set; }//
            public double ThucNhan { get; set; }
            public double ThueTNCN { get; set; }

            //StringFormat={}{0:N0}, ConverterCulture='vi-VN'
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
        private void LoadComboBoxData()
        {
            ThangTrongNam = new ObservableCollection<int>();
            for (int thang = 1; thang <= 12; thang++)
            {
                ThangTrongNam.Add(thang);
            }

            NamTuTruocDenGio = new ObservableCollection<int>();
            for (int nam = 2012; nam <= DateTime.Now.Year; nam++)
            {
                NamTuTruocDenGio.Add(nam);
            }
        }

        public string GetTenChucVu(double phucap)
        {
            string TenChucVu = "";

            switch (phucap)
            {
                case 500000:
                    TenChucVu = "Nhân viên";
                    break;
                case 2000000:
                    TenChucVu = "Quản lý";
                    break;
                case 5000000:
                    TenChucVu = "Trưởng phòng";
                    break;
                case 7000000:
                    TenChucVu = "Trưởng bộ phận";
                    break;
                case 20000000:
                    TenChucVu = "Giám đốc";
                    break;
                case 15000000:
                    TenChucVu = "Phó giám đốc";
                    break;
                default:
                    break;
            }

            return TenChucVu;
        }
        private double TinhThueTNCN(double LuongThucNhan)
        {
            // 1. Xác định thu nhập chịu thuế
            double ThuNhapChiuThue = LuongThucNhan;

            // 2. Áp dụng mức thuế suất
            double Thue = 0;
            if (ThuNhapChiuThue <= 0)
            {
                Thue = 0;
            }
            else if (ThuNhapChiuThue <= 5000000)
            {
                Thue = ThuNhapChiuThue * 5 / 100;
            }
            else if (ThuNhapChiuThue <= 10000000)
            {
                Thue = 250000 + (ThuNhapChiuThue - 5000000) * 10 / 100;
            }
            else if (ThuNhapChiuThue <= 18000000)
            {
                Thue = 750000 + (ThuNhapChiuThue - 10000000) * 15 / 100;
            }
            else if (ThuNhapChiuThue <= 32000000)
            {
                Thue = 1950000 + (ThuNhapChiuThue - 18000000) * 20 / 100;
            }
            else if (ThuNhapChiuThue <= 52000000)
            {
                Thue = 4750000 + (ThuNhapChiuThue - 32000000) * 25 / 100;
            }
            else if (ThuNhapChiuThue <= 80000000)
            {
                Thue = 9750000 + (ThuNhapChiuThue - 52000000) * 30 / 100;
            }
            else if (ThuNhapChiuThue >= 80000000)
            {
                Thue = 18150000 + (ThuNhapChiuThue - 80000000) * 35 / 100;
            }

            // 3. Trả về số thuế TNCN
            return Thue;
        }

        private double TinhGiamTruGiaCanh(string MoiQuanHe)
        {
            double GiamTruGiaCanh = 11000000;

            switch (MoiQuanHe)
            {
                case "Vợ":
                    GiamTruGiaCanh += 4400000;
                    break;
                case "Chồng":
                    GiamTruGiaCanh += 4400000;
                    break;
                case "Con":
                    GiamTruGiaCanh += 4400000;
                    break;
                default:
                    break;
            }

            return GiamTruGiaCanh;
        }

        private void TinhLuong()
        {
            ListBangCong = new ObservableCollection<BangCong>(_repositoryBangCong.AsQueryable().Where(x => x.Thang == Thang && x.Nam == Nam).ToList());

            if (ListBangCong.Count == 0)
            {
                MessageBox.Show($"Bảng công tháng {Thang}, năm {Nam} không tồn tại.");
                return;
            }

            for (int i = 0; i < ListBangCong.Count; i++)
            {
                var maNhanVien = ListBangCong[i].MaNhanVien;

                var hopDong = _repositoryHopDong.AsQueryable()
                        .OrderByDescending(x => x.NgayBatDau)
                        .FirstOrDefault(x => x.MaNhanVien == maNhanVien);

                var danhSachQuanHeGiaDinh = _repositoryQuanHeGiaDinh.AsQueryable()
                         .Where(x => x.MaNhanVien == maNhanVien)
                         .Select(x => x.MoiQuanHe)
                         .ToList();

                double GiamTruGiaCanh = 0;

                foreach (var quanHe in danhSachQuanHeGiaDinh)
                {
                     GiamTruGiaCanh = TinhGiamTruGiaCanh(quanHe);
                }
                

                if (hopDong == null)
                {
                    MessageBox.Show($"Nhân viên \"{ListBangCong[i].HoTen}\" không có hợp đồng.");
                    continue;
                }

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

                double phucap = _repositoryNhanSuChucVu
                    .AsQueryable()
                    .Include(x => x.NhanSu)
                    .Where(x => x.NhanSu.MaNhanVien == ListBangCong[i].MaNhanVien)
                    .OrderByDescending(x => x.PhuCapChucVu)
                    .FirstOrDefault().PhuCapChucVu;

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
                    ChucVu = GetTenChucVu(phucap),
                    LuongCoBan = (double)luongcoban,
                    TongSoNgayCong = (int)(ListBangCong[i].TongSoNgayCong),
                    TienCongThuong = (double)(luongngay * ListBangCong[i].TongSoNgayCong),
                    NgayCongChuNhat = (int)(ListBangCong[i].TongSoNgayCongCN),
                    TienCongCN = (double)(luongngay * ListBangCong[i].TongSoNgayCongCN * 2),
                    //TienCongNgayLe = (double)(luongngay * ListBangCong[i].TongSoNgayCongNgayLe * 3),
                    SoGioOT = (double)(ListBangCong[i].TongTimeOT),
                    TienTangCa = (double)(luonggio * ListBangCong[i].TongTimeOT * 1.5),
                    PhuCapAnTrua = 1250000,
                    PhuCapDiLai = 500000,
                    PhuCap = phucap,
                    BaoHiemXaHoi = (double)(luongcoban * 0.08),
                    BaoHiemYTe = (double)(luongcoban * 0.015),
                    SoGioDiMuonVeSom = (double)(ListBangCong[i].DiMuonVeSom),
                    TienDiSomVeMuon = (double)(luonggio * ListBangCong[i].DiMuonVeSom),
                    TienNgayNghiPhep = (double)(luongngay * ListBangCong[i].NgayNghiPhep),
                    TienBaoHiem = (double)(luongcoban * 10.5 / 100),
                    UngLuong = (double)ungluong
                };
                luongNhanVien.TongKhauTru = (double)(luongNhanVien.BaoHiemYTe + luongNhanVien.BaoHiemXaHoi
                    + luongNhanVien.TienDiSomVeMuon + luongNhanVien.UngLuong);

                luongNhanVien.TongLuong = (double)(luongNhanVien.TienCongThuong + luongNhanVien.TienCongCN
                    + luongNhanVien.TienTangCa + luongNhanVien.PhuCapAnTrua + luongNhanVien.PhuCapDiLai
                    + luongNhanVien.PhuCap);

                luongNhanVien.ThucNhan = (double)(luongNhanVien.TongLuong - luongNhanVien.TongKhauTru);

                luongNhanVien.ThueTNCN = TinhThueTNCN((luongNhanVien.ThucNhan - GiamTruGiaCanh));
                //luongNhanVien.ThucNhan = (double)(luongNhanVien.TienCongThuong + luongNhanVien.TienCongCN + luongNhanVien.TienCongNgayLe - luongNhanVien.UngLuong
                //    + luongNhanVien.TienTangCa - luongNhanVien.TienDiSomVeMuon + luongNhanVien.TienNgayNghiPhep - luongNhanVien.TienBaoHiem - luongNhanVien.TienBaoHiem);
                ListLuongNhanVien.Add(luongNhanVien);
            }
        }

        private void ExportListViewToPdf()
        {

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string filePath = Path.Combine(desktopPath, $"Salary_Report_{Thang}_{Nam}.pdf");

            if (File.Exists(filePath))
            {
                // Nếu tệp đã tồn tại, hiển thị thông báo và kết thúc hàm
                MessageBox.Show($"Bảng lương đã tồn tại tại đường dẫn \"{filePath}\". Vui lòng xóa hoặc đổi tên trước khi xuất lại.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Thông tin tháng và năm
            string monthYearString = $"BẢNG LƯƠNG THÁNG {Thang} NĂM {Nam}";

            string companyName = "CÔNG TY TNHH TOP ENGINEERING VINA";
            string companyAddress = "Địa chỉ: Lô C1, Khu công nghiệp Bá Thiện II, Xã Thiện Kế, Huyện Bình Xuyên, Tỉnh Vĩnh Phúc, Việt Nam";
            string companyTaxCode = "Mã số thuế: 2500641492";

            string bieuNgu1 = "CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM";
            string bieuNgu2 = "Độc lập - Tự do - Hạnh phúc";
            string pattern = "--------------";

            string voucherMaker = "Người lập biểu";
            string Manager = "GIÁM ĐỐC";
            string datetime = $"Hà nội, ngày {DateTime.Now.Day} tháng {DateTime.Now.Month} năm {DateTime.Now.Year}";
            string sign_manager = "(Ký, ghi rõ họ tên và đóng dấu)";
            string sign_maker = "(Ký, ghi rõ họ tên)";

            string fontPath = "C:\\Users\\Admin\\Desktop\\DoAn\\HRM\\HRM.UI\\ViewModels\\Font\\times.ttf"; // Thay đổi đường dẫn tới font tiếng Việt
            //string fontPath = "C:\\Users\\Admin\\Documents\\GitHub\\HRM\\HRM.UI\\ViewModels\\Font\\times.ttf"; // Thay đổi đường dẫn tới font tiếng Việt

            // Đăng ký font Unicode tiếng Việt cho iTextSharp
            FontFactory.Register(fontPath, "MyVietnameseFont");

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var document = new iTextSharp.text.Document(PageSize.A4.Rotate(), 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                document.Open();

                // Font settings
                BaseFont vietnameseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                Font headerFont = new Font(vietnameseFont, 8, Font.BOLD);
                Font title = new Font(vietnameseFont, 8, Font.NORMAL);
                Font table_title_font = new Font(vietnameseFont, 8, Font.NORMAL);
                Font table_footer_font = new Font(vietnameseFont, 8, Font.ITALIC);
                Font bodyFont = new Font(vietnameseFont, 7, Font.NORMAL);

                // Tạo bảng với hai cột
                PdfPTable table_title = new PdfPTable(2);
                table_title.WidthPercentage = 100;

                // Tạo ô bên trái với thông tin công ty
                PdfPCell leftCell = new PdfPCell();
                leftCell.Border = PdfPCell.NO_BORDER;
                leftCell.VerticalAlignment = Element.ALIGN_TOP;

                // Thêm thông tin công ty vào ô bên trái
                leftCell.AddElement(new Paragraph(companyName, title));
                leftCell.AddElement(new Paragraph(companyTaxCode, title));
                leftCell.AddElement(new Paragraph(companyAddress, title));

                // Tạo ô bên phải với các dòng khác
                PdfPCell rightCell = new PdfPCell();
                rightCell.Border = PdfPCell.NO_BORDER;
                rightCell.VerticalAlignment = Element.ALIGN_TOP;

                // Thêm các dòng vào ô bên phải
                rightCell.AddElement(new Paragraph(bieuNgu1, title) { Alignment = Element.ALIGN_CENTER });
                rightCell.AddElement(new Paragraph(bieuNgu2, title) { Alignment = Element.ALIGN_CENTER });
                rightCell.AddElement(new Paragraph(pattern, title) { Alignment = Element.ALIGN_CENTER });

                // Thêm các ô vào bảng
                table_title.AddCell(leftCell);
                table_title.AddCell(rightCell);

                // Thêm bảng vào tài liệu
                document.Add(table_title);

                document.Add(new Paragraph("\n"));

                // Thêm dòng chữ "Bảng lương tháng... năm..."
                var header = new Paragraph(monthYearString, headerFont);
                header.Alignment = Element.ALIGN_CENTER;
                document.Add(header);

                document.Add(new Paragraph("\n")); // Thêm một dòng trống

                // Tạo một bảng PDF với số cột tương ứng với số cột của ListView
                PdfPTable table = new PdfPTable(21); // Số cột của ListView

                // Thêm tiêu đề cột vào bảng PDF
                string[] headers = { "Mã nhân viên", "Họ và tên", "Chức vụ", "Lương cơ bản", "Ngày công thực tế", "Lương thực tế",
                             "Ngày công chủ nhật", "Thành tiền", "Số giờ tăng ca", "Thành tiền", "Phụ cấp ăn trưa",
                             "Phụ cấp đi lại", "Phụ cấp chức vụ", "Tổng lương", "BHXH", "BHYT", "Đi muộn về sớm",
                             "Thành tiền", "Ứng lương", "Tổng khấu trừ", "Thực nhận" };

                foreach (string headerText in headers)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(headerText, table_title_font))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        MinimumHeight = 30f,
                        BackgroundColor = new BaseColor(204, 204, 204) // Màu nền xám nhạt
                    };
                    table.AddCell(cell);
                }

                // Thêm các hàng dữ liệu vào bảng PDF
                foreach (LuongNhanVien luong in ListLuongNhanVien)
                {
                    // Định dạng dữ liệu số theo tiền tệ của Việt Nam và loại bỏ phần thập phân
                    string formattedLuongCoBan = $"{luong.LuongCoBan:N0}";
                    string formattedLuongThucTe = $"{luong.LuongThucTe:N0}";
                    string formattedTienCongCN = $"{luong.TienCongCN:N0}";
                    string formattedTienTangCa = $"{luong.TienTangCa:N0}";
                    string formattedPhuCapAnTrua = $"{luong.PhuCapAnTrua:N0}";
                    string formattedPhuCapDiLai = $"{luong.PhuCapDiLai:N0}";
                    string formattedPhuCap = $"{luong.PhuCap:N0}";
                    string formattedTongLuong = $"{luong.TongLuong:N0}";
                    string formattedBaoHiemXaHoi = $"{luong.BaoHiemXaHoi:N0}";
                    string formattedBaoHiemYTe = $"{luong.BaoHiemYTe:N0}";
                    string formattedTienDiSomVeMuon = $"{luong.TienDiSomVeMuon:N0}";
                    string formattedUngLuong = $"{luong.UngLuong:N0}";
                    string formattedTongKhauTru = $"{luong.TongKhauTru:N0}";
                    string formattedThucNhan = $"{luong.ThucNhan:N0}";

                    // Thêm từng ô dữ liệu vào bảng PDF
                    table.AddCell(CreateCell(luong.MaNhanVien.ToString(), bodyFont));
                    table.AddCell(CreateCell(luong.HoTen, bodyFont));
                    table.AddCell(CreateCell(luong.ChucVu, bodyFont));
                    table.AddCell(CreateCell(formattedLuongCoBan, bodyFont));
                    table.AddCell(CreateCell(luong.TongSoNgayCong.ToString(), bodyFont));
                    table.AddCell(CreateCell(formattedLuongThucTe, bodyFont));
                    table.AddCell(CreateCell(luong.NgayCongChuNhat.ToString(), bodyFont));
                    table.AddCell(CreateCell(formattedTienCongCN, bodyFont));
                    table.AddCell(CreateCell(luong.SoGioOT.ToString(), bodyFont));
                    table.AddCell(CreateCell(formattedTienTangCa, bodyFont));
                    table.AddCell(CreateCell(formattedPhuCapAnTrua, bodyFont));
                    table.AddCell(CreateCell(formattedPhuCapDiLai, bodyFont));
                    table.AddCell(CreateCell(formattedPhuCap, bodyFont));
                    table.AddCell(CreateCell(formattedTongLuong, bodyFont));
                    table.AddCell(CreateCell(formattedBaoHiemXaHoi, bodyFont));
                    table.AddCell(CreateCell(formattedBaoHiemYTe, bodyFont));
                    table.AddCell(CreateCell(luong.SoGioDiMuonVeSom.ToString(), bodyFont));
                    table.AddCell(CreateCell(formattedTienDiSomVeMuon, bodyFont));
                    table.AddCell(CreateCell(formattedUngLuong, bodyFont));
                    table.AddCell(CreateCell(formattedTongKhauTru, bodyFont));
                    PdfPCell thucNhanCell = CreateCell(formattedThucNhan, bodyFont);
                    thucNhanCell.BackgroundColor = new BaseColor(255, 255, 153); // Màu vàng
                    table.AddCell(thucNhanCell);
                }

                // Tự động điều chỉnh kích thước cột theo nội dung
                table.WidthPercentage = 100;
                document.Add(table);

                document.Add(new Paragraph("\n")); // Thêm một dòng trống

                // Tạo bảng với hai cột
                PdfPTable table_footer = new PdfPTable(2);
                table_footer.WidthPercentage = 100;

                // Đặt tỷ lệ độ rộng các cột
                float[] widths = new float[] { 0.3f, 0.3f };
                table_footer.SetWidths(widths);

                // Tạo ô cho "Người lập biểu"
                PdfPCell leftCell_ft = new PdfPCell();
                leftCell_ft.Border = PdfPCell.NO_BORDER;
                leftCell_ft.HorizontalAlignment = Element.ALIGN_CENTER;

                leftCell_ft.AddElement(new Paragraph(" ", headerFont) { Alignment = Element.ALIGN_CENTER });
                leftCell_ft.AddElement(new Paragraph(voucherMaker, headerFont) { Alignment = Element.ALIGN_CENTER });
                leftCell_ft.AddElement(new Paragraph(sign_maker, table_footer_font) { Alignment = Element.ALIGN_CENTER });

                // Tạo ô cho "Giám đốc"
                PdfPCell rightCell_ft = new PdfPCell();
                rightCell_ft.Border = PdfPCell.NO_BORDER;
                rightCell_ft.HorizontalAlignment = Element.ALIGN_TOP;

                rightCell_ft.AddElement(new Paragraph(datetime, table_footer_font) { Alignment = Element.ALIGN_CENTER });
                rightCell_ft.AddElement(new Paragraph(Manager, headerFont) { Alignment = Element.ALIGN_CENTER });
                rightCell_ft.AddElement(new Paragraph(sign_manager, table_footer_font) { Alignment = Element.ALIGN_CENTER });


                // Thêm các ô vào bảng
                table_footer.AddCell(leftCell_ft);
                table_footer.AddCell(rightCell_ft);

                // Thêm bảng vào tài liệu
                document.Add(table_footer);

                //Paragraph maker = new Paragraph(voucherMaker, table_title_font);
                //maker.Alignment = Element.ALIGN_UNDEFINED;
                //document.Add(maker);

                //Paragraph ceo = new Paragraph(Manager, table_title_font);
                //ceo.Alignment = Element.ALIGN_UNDEFINED;
                //document.Add(ceo);

                document.Close();
                writer.Close();
            }

            MessageBox.Show("File PDF đã được tạo thành công trên Desktop!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            OpenPdf(filePath);
        }

        private PdfPCell CreateCell(string text, Font font)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = 40f
            };
            return cell;
        }

        private void OpenPdf(string filePath)
        {
            try
            {
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở file PDF: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportListViewToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "Salary_Report.xlsx");

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets["Bảng Lương"];
                if (worksheet != null)
                {
                    MessageBox.Show($"Bảng lương đã tồn tại tại đường dẫn \"{filePath}\". Vui lòng xóa hoặc đổi tên trước khi xuất lại.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                worksheet = package.Workbook.Worksheets.Add("Bảng Lương");

                // Thông tin tháng và năm
                string monthYearString = $"BẢNG LƯƠNG THÁNG {Thang} NĂM {Nam}";

                // Tiêu đề cột
                string[] headers = { "Mã nhân viên", "Họ và tên", "Chức vụ", "Lương cơ bản", "Ngày công thực tế", "Lương thực tế",
                              "Ngày công chủ nhật", "Thành tiền", "Số giờ tăng ca", "Thành tiền", "Phụ cấp ăn trưa",
                              "Phụ cấp đi lại", "Phụ cấp chức vụ", "Tổng lương", "BHXH", "BHYT", "Đi muộn về sớm",
                              "Thành tiền", "Ứng lương", "Tổng khấu trừ", "Thực nhận" };

                // Set tiêu đề và format
                worksheet.Cells["A1"].Value = monthYearString;
                worksheet.Cells["A1"].Style.Font.Bold = true;
                worksheet.Cells["A1:U1"].Merge = true;
                worksheet.Cells["A1:U1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Set header
                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[2, i + 1].Value = headers[i];
                    worksheet.Cells[2, i + 1].Style.Font.Bold = true;
                    worksheet.Cells[2, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[2, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    worksheet.Cells[2, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // Dữ liệu
                int row = 3;
                foreach (LuongNhanVien luong in ListLuongNhanVien)
                {
                    worksheet.Cells[row, 1].Value = luong.MaNhanVien;
                    worksheet.Cells[row, 2].Value = luong.HoTen;
                    worksheet.Cells[row, 3].Value = luong.ChucVu;
                    worksheet.Cells[row, 4].Value = luong.LuongCoBan;
                    worksheet.Cells[row, 5].Value = luong.TongSoNgayCong;
                    worksheet.Cells[row, 6].Value = luong.LuongThucTe;
                    worksheet.Cells[row, 7].Value = luong.NgayCongChuNhat;
                    worksheet.Cells[row, 8].Value = luong.TienCongCN;
                    worksheet.Cells[row, 9].Value = luong.SoGioOT;
                    worksheet.Cells[row, 10].Value = luong.TienTangCa;
                    worksheet.Cells[row, 11].Value = luong.PhuCapAnTrua;
                    worksheet.Cells[row, 12].Value = luong.PhuCapDiLai;
                    worksheet.Cells[row, 13].Value = luong.PhuCap;
                    worksheet.Cells[row, 14].Value = luong.TongLuong;
                    worksheet.Cells[row, 15].Value = luong.BaoHiemXaHoi;
                    worksheet.Cells[row, 16].Value = luong.BaoHiemYTe;
                    worksheet.Cells[row, 17].Value = luong.SoGioDiMuonVeSom;
                    worksheet.Cells[row, 18].Value = luong.TienDiSomVeMuon;
                    worksheet.Cells[row, 19].Value = luong.UngLuong;
                    worksheet.Cells[row, 20].Value = luong.TongKhauTru;
                    worksheet.Cells[row, 21].Value = luong.ThucNhan;

                    row++;
                }

                // Auto fit các cột
                worksheet.Cells.AutoFitColumns();

                // Save file
                package.Save();

                MessageBox.Show("File Excel đã được tạo thành công trên Desktop!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            //OpenExcel(filePath);
        }

        private readonly IRepository<BangCong> _repositoryBangCong;
        private readonly IRepository<HopDong> _repositoryHopDong;
        private readonly IRepository<NhanSuChucVu> _repositoryNhanSuChucVu;
        private readonly IRepository<ChucVu> _repositoryChucVu;
        private readonly IRepository<UngLuong> _repositoryUngLuong;
        private readonly IRepository<QuanHeGiaDinh> _repositoryQuanHeGiaDinh;
        private readonly IUnitOfWork _unitOfWork;
        public ICommand CalculateCommand { get; set; }
        public ICommand ExportPDFCommand { get; set; }
        public ICommand ExportExcelCommand { get; set; }
        public SalaryViewModel(IRepository<UngLuong> repositoryUngLuong, IRepository<ChucVu> repositoryChucVu, IRepository<NhanSuChucVu> repositoryNhanSuChucVu, IRepository<HopDong> repositoryHopDong, IRepository<BangCong> repositoryBangCong, IRepository<QuanHeGiaDinh> repositoryQuanHeGiaDinh, IUnitOfWork unitOfWork)
        {
            _repositoryHopDong = repositoryHopDong;
            _repositoryBangCong = repositoryBangCong;
            _repositoryNhanSuChucVu = repositoryNhanSuChucVu;
            _repositoryChucVu = repositoryChucVu;
            _repositoryUngLuong = repositoryUngLuong;
            _repositoryQuanHeGiaDinh = repositoryQuanHeGiaDinh;
            _unitOfWork = unitOfWork;
            LoadComboBoxData();

            CalculateCommand = new RelayCommand<object>(p =>
            {
                if (Thang == 0 || Nam == 0)
                    return false;
                else
                    return true;
            }, async (p) =>
            {

                try
                {
                    ListLuongNhanVien.Clear();
                    TinhLuong();
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            ExportPDFCommand = new RelayCommand<object>(p =>
            {
                if (ListLuongNhanVien.Count == 0)
                    return false;
                else
                    return true;
            }, async (p) =>
            {
                try
                {
                    ExportListViewToPdf();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            ExportExcelCommand = new RelayCommand<object>(p =>
            {
                if (ListLuongNhanVien.Count == 0)
                    return false;
                else
                    return true;
            }, async (p) =>
            {
                try
                {
                    ExportListViewToExcel();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }
    }
}
