﻿ using System;
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
using HRM.UI.Factories;
using HRM.UI.Stores;
using Microsoft.EntityFrameworkCore;

namespace HRM.UI.ViewModels
{
    public class FamilyInforViewModel : BaseViewModel
    {
        private ObservableCollection<QuanHeGiaDinh> _list = new ObservableCollection<QuanHeGiaDinh>();
        public ObservableCollection<QuanHeGiaDinh> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }
        private IUnitOfWork _unitOfWork;

        private IRepository<QuanHeGiaDinh> _quanHeGiaDinhRepository;
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ObservableCollection<string> TinhThanhData { get; set; }
        public ObservableCollection<string> MoiQuanHeData { get; set; }

        private QuanHeGiaDinh _selectedItem;
        public QuanHeGiaDinh SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    MoiQuanHe = SelectedItem.MoiQuanHe;
                    HoVaTen = SelectedItem.HoVaTen;
                    NamSinh = SelectedItem.NamSinh;
                    TinhThanh = TinhThanh;
                    NgheNghiep = NgheNghiep;
                    DonViCongTac = DonViCongTac;
                    ChucVu = ChucVu;
                }
                OnPropertyChanged();
            }
        }

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

        private DateTime _bidDate = DateTime.Now;
        public DateTime NamSinh
        {
            get { return _bidDate; }
            set
            {
                _bidDate = value.Date;
                OnPropertyChanged();
            }
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
        private readonly IViewModelFactory _viewModelFactory;
        private readonly MainContentStore _mainContentStore;
        public FamilyInforViewModel(IViewModelFactory viewModelFactory, MainContentStore mainContentStore, IRepository<QuanHeGiaDinh> quanHeGiaDinhRepository, IUnitOfWork unitOfWork)
        {
            _viewModelFactory = viewModelFactory;
            _mainContentStore = mainContentStore;
            _quanHeGiaDinhRepository = quanHeGiaDinhRepository;
            _unitOfWork = unitOfWork;
            LoadComboBoxData();
            LoadData();

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(HoVaTen))
                    return false;
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
                    await _unitOfWork.CommitAsync();
                    LoadData();
                    if (QuanHeGiaDinh != null)
                    {
                        MessageBox.Show("Thêm thành công");
                        LoadData();
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

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, async (p) =>
            {
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var QuanHeGiaDinh = await _quanHeGiaDinhRepository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    await _quanHeGiaDinhRepository.DeleteAsync(QuanHeGiaDinh);
                    await _unitOfWork.CommitAsync();
                    LoadData();


                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            }
            );
        }
        private void LoadData()
        {
            List = new ObservableCollection<QuanHeGiaDinh>(_quanHeGiaDinhRepository.AsQueryable().ToList());
            /*            if (!String.IsNullOrWhiteSpace(Filter))
                        {
                            List = new ObservableCollection<NhanSu>(_repository.AsQueryable().Where(x => x.MaNhanVien.Contains(Filter) || x.HoTen.Contains(Filter)).ToList());
                        }*/
        }
    }
}

