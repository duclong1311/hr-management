 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DocumentFormat.OpenXml.Math;
using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using HRM.UI.Commands;

namespace HRM.UI.ViewModels
{
    public class FamilyInforViewModel : BaseViewModel
    {
        private IUnitOfWork _unitOfWork;

        private IRepository<QuanHeGiaDinh> _quanHeGiaDinhRepository;
        public ICommand AddCommand { get; set; }
        public ObservableCollection<string> TinhThanhData { get; set; }
        public ObservableCollection<string> MoiQuanHeData { get; set; }

        private string _moiQuanHe;
        public string MoiQuanHe
        {
            get { return _moiQuanHe; }
            set { _moiQuanHe = value; OnPropertyChanged(); }
        }

        private string _hoVaTen;
        public string HoVaTen
        {
            get { return _hoVaTen; }
            set { _hoVaTen = value; OnPropertyChanged(); }
        }

        private DateOnly _namSinh;
        public DateOnly NamSinh
        {
            get { return _namSinh; }
            set { _namSinh = value; OnPropertyChanged(); }
        }

        private string _queQuan;
        public string TinhThanh
        {
            get { return _queQuan; }
            set { _queQuan = value; OnPropertyChanged(); }
        }

        private string _ngheNghiep;
        public string NgheNghiep
        {
            get { return _ngheNghiep; }
            set { _ngheNghiep = value; OnPropertyChanged(); }
        }

        private string _donViCongTac;
        public string DonViCongTac
        {
            get { return _donViCongTac; }
            set { _donViCongTac = value; OnPropertyChanged(); }
        }

        private string _noiO;
        public string NoiO
        {
            get { return _noiO; }
            set { _noiO = value; OnPropertyChanged(); }
        }

        private string _chucVu;
        public string ChucVu
        {
            get { return _chucVu; }
            set { _chucVu = value; OnPropertyChanged(); }
        }
        private void LoadComboBoxData()
        {
            TinhThanhData = new ObservableCollection<string>();
            TinhThanhData.Add("Hà Nội");
            TinhThanhData.Add("Hồ Chí Minh");
            TinhThanhData.Add("Đà Nẵng");

            MoiQuanHeData = new ObservableCollection<string>();
            MoiQuanHeData.Add("Cha");
            MoiQuanHeData.Add("Mẹ");
            MoiQuanHeData.Add("Con");

        }
        public FamilyInforViewModel(IUnitOfWork unitOfWork, IRepository<QuanHeGiaDinh> quanHeGiaDinhRepository)
        {
            _unitOfWork = unitOfWork;
            _quanHeGiaDinhRepository = quanHeGiaDinhRepository;

            LoadComboBoxData();

            AddCommand = new RelayCommand<object>((p) =>
            {

                return true;
            }, async (p) =>

            {
                var QuanHeGiaDinh = new QuanHeGiaDinh()
                {
                    MoiQuanHe = MoiQuanHe,
                    HoVaTen = HoVaTen,
                    NamSinh = NamSinh,
                    NoiO = NoiO,
                    NgheNghiep = NgheNghiep,
                    QueQuan = TinhThanh,
                    DonViCongTac = DonViCongTac,
                    ChucVu = ChucVu,
                };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    QuanHeGiaDinh = await _quanHeGiaDinhRepository.AddAsync(QuanHeGiaDinh);
                    if (QuanHeGiaDinh != null)
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
        //HoTenAnhChi = hoTenAnhChi;
        //    HoTenAnhChi = hoTenAnhChi;
        //    NamSinh = namSinh;
        //    NamSinh = namSinh;
        //    NgheNghiep = ngheNghiep;
        //    NgheNghiep = ngheNghiep;
        //    CoQuanCongTac = coQuanCongTac;
        //    CoQuanCongTac = coQuanCongTac;
}

