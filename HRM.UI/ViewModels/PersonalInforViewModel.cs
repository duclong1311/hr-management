﻿using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using HRM.UI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HRM.UI.ViewModels
{
    public class PersonalInforViewModel : BaseViewModel
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<HoSo> _hosoRepository;
        public ICommand AddCommand { get; set; }
        private string _hoTen;

        public string HoTen
        {
            get { return _hoTen; }
            set { _hoTen = value; }
        }
        private DateTime _ngaySinh;

        public DateTime NgaySinh
        {
            get { return _ngaySinh; }
            set { _ngaySinh = value; }
        }
        private string _nguyenQuan;

        public string NguyenQuan
        {
            get { return _nguyenQuan; }
            set { _nguyenQuan = value; }
        }
        private string _danToc;

        public string DanToc
        {
            get { return _danToc; }
            set { _danToc = value; }
        }
        private string _cccd;

        public string CCCD
        {
            get { return _cccd; }
            set { _cccd = value; }
        }
        private string _trinhDoVanHoa;

        public string TrinhDoVanHoa
        {
            get { return _trinhDoVanHoa; }
            set { _trinhDoVanHoa = value; }
        }
        private string _ketNapDoan;

        public string KetNapDoan
        {
            get { return _ketNapDoan; }
            set { _ketNapDoan = value; }
        }
        private string _ketNapDang;

        public string KetNapDang
        {
            get { return _ketNapDang; }
            set { _ketNapDang = value; }
        }
        private string _khenThuong;

        public string KhenThuong
        {
            get { return _khenThuong; }
            set { _khenThuong = value; }
        }
        private string _soThich;

        public string SoThich
        {
            get { return _soThich; }
            set { _soThich = value; }
        }
        private string _gioiTinh;

        public string GioiTinh
        {
            get { return _gioiTinh; }
            set { _gioiTinh = value; }
        }
        private string _noiSinh;

        public string NoiSinh
        {
            get { return _noiSinh; }
            set { _noiSinh = value; }
        }
        private string _tonGiao;

        public string TonGiao
        {
            get { return _tonGiao; }
            set { _tonGiao = value; }
        }
        private DateTime _capNgay;

        public DateTime CapNgay
        {
            get { return _capNgay; }
            set { _capNgay = value; }
        }
        private string _noiKetNapDoan;

        public string NoiKetNapDoan
        {
            get { return _noiKetNapDoan; }
            set { _noiKetNapDoan = value; }
        }
        private string _noiKetNapDang;

        public string NoiKetNapDang
        {
            get { return _noiKetNapDang; }
            set { _noiKetNapDang = value; }
        }
        public PersonalInforViewModel(IRepository<HoSo> hosoRepository, IUnitOfWork unitOfWork)
        {
            _hosoRepository = hosoRepository;
            _unitOfWork = unitOfWork;
            AddCommand = new RelayCommand<object>((p) =>
            {

                return true;
            }, async (p) =>
            {
                var HoSo = new HoSo()
                {
                    HoTen = HoTen,
                    SinhNgay = NgaySinh,
                    NguyenQuan = NguyenQuan,
                    DanToc = DanToc,
                    NoiSinh =NoiSinh,
                    CCCD = CCCD,
                    TonGiao = TonGiao,
                    CapNgay = CapNgay,
                    TrinhDoVanHoa = TrinhDoVanHoa,
                    KetNapDang = KetNapDang,
                    KetNapDoan = KetNapDoan,
                    NoiketNapDang = NoiKetNapDang,
                    NoiKetNapDoan = NoiKetNapDoan,
                    KhenThuong = KhenThuong,
                    SoThich = SoThich,
                };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    HoSo = await _hosoRepository.AddAsync(HoSo);
                    if(HoSo !=null)
                    {
                        MessageBox.Show("Thêm thành công");
                    } else
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

