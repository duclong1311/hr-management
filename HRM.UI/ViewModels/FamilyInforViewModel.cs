 using System;
using System.Collections.Generic;
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

        private string _hoTenCha;
        public string HoTenCha
        {
            get { return _hoTenCha; }
            set { _hoTenCha = value; OnPropertyChanged(); }
        }

        private DateTime _ngaySinhCha;
        public DateTime NgaySinhCha
        {
            get { return _ngaySinhCha; }
            set { _ngaySinhCha = value; OnPropertyChanged(); }
        }

        private string _ngheNghiepCha;
        public string NgheNghiepCha
        {
            get { return _ngheNghiepCha; }
            set { _ngheNghiepCha = value; OnPropertyChanged(); }
        }

        private string _coQuanCongTacCha;
        public string CoQuanCongTacCha
        {
            get { return _coQuanCongTacCha; }
            set { _coQuanCongTacCha = value; OnPropertyChanged(); }
        }

        private string _choOCha;
        public string ChoOCha
        {
            get { return _choOCha; }
            set { _choOCha = value; OnPropertyChanged(); }
        }

        private string _hoTenMe;
        public string HoTenMe
        {
            get { return _hoTenMe; }
            set { _hoTenMe = value; OnPropertyChanged(); }
        }

        private DateTime _ngaySinhMe;
        public DateTime NgaySinhMe
        {
            get { return _ngaySinhMe; }
            set { _ngaySinhMe = value; OnPropertyChanged(); }
        }

        private string _ngheNghiepMe;
        public string NgheNghiepMe
        {
            get { return _ngheNghiepMe; }
            set { _ngheNghiepMe = value; OnPropertyChanged(); }
        }

        private string _coQuanCongTacMe;
        public string CoQuanCongTacMe
        {
            get { return _coQuanCongTacMe; }
            set { _coQuanCongTacMe = value; OnPropertyChanged(); }
        }

        private string _choOMe;
        public string ChoOMe
        {
            get { return _choOMe; }
            set { _choOMe = value; OnPropertyChanged(); }
        }

        private string _hoTenAnhChi;
        public string HoTenAnhChi
        {
            get { return _hoTenAnhChi; }
            set { _hoTenAnhChi = value; OnPropertyChanged(); }
        }

        private string _namSinh;
        public string NamSinh
        {
            get { return _namSinh; }
            set { _namSinh = value; OnPropertyChanged(); }
        }

        private string _ngheNghiep;
        public string NgheNghiep
        {
            get { return _ngheNghiep; }
            set { _ngheNghiep = value; OnPropertyChanged(); }
        }

        private string _coQuanCongTac;
        public string CoQuanCongTac
        {
            get { return _coQuanCongTac; }
            set { _coQuanCongTac = value; }
        }

        public FamilyInforViewModel(IUnitOfWork unitOfWork, IRepository<QuanHeGiaDinh> quanHeGiaDinhRepository)
        {
            _unitOfWork = unitOfWork;
            _quanHeGiaDinhRepository = quanHeGiaDinhRepository;

            AddCommand = new RelayCommand<object>((p) =>
            {

                return true;
            }, async (p) =>

            {
                var QuanHeGiaDinh = new QuanHeGiaDinh()
                {
                    HoTenCha = HoTenCha,
                    NgaySinhCha = NgaySinhCha,
                    NgheNghiepCha = NgheNghiepCha,
                    CoQuanCongTacCha = CoQuanCongTacCha,
                    ChoOCha = ChoOCha,
                    HoTenMe = HoTenMe,
                    NgaySinhMe = NgaySinhMe,
                    NgheNghiepMe = NgheNghiepMe,
                    CoQuanCongTacMe = CoQuanCongTacMe,
                    ChoOMe = ChoOMe,
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

