using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DocumentFormat.OpenXml.Wordprocessing;
using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using HRM.UI.Factories;
using HRM.UI.States.Users;
using HRM.UI.Stores;
using Microsoft.EntityFrameworkCore;

namespace HRM.UI.ViewModels
{
    public class ListTrackingTableViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly MainContentStore _mainContentStore;
        private IRepository<BangCong> _bangCongRepository;
        private string _maNhanVien { get; set; }
        public string MaNhanVien { get { return _maNhanVien; } set { _maNhanVien = value; OnPropertyChanged(); } }
        private string _hoTen { get; set; }
        public string HoTen { get { return _hoTen; } set { _hoTen = value; OnPropertyChanged(); } }
        private int _nam { get; set; }
        public int Nam { get { return _nam; } set { _nam = value; OnPropertyChanged(); } }
        private int _thang { get; set; }
        public int Thang { get { return _thang; } set { _thang = value; OnPropertyChanged(); } }
        private int _tongSoNgayCong { get; set; }
        public int TongSoNgayCong { get { return _tongSoNgayCong; } set { _tongSoNgayCong = value; OnPropertyChanged(); } }
        private int _tongSoNgayCongCN { get; set; }
        public int TongSoNgayCongCN { get { return _tongSoNgayCongCN; } set { _tongSoNgayCongCN = value; OnPropertyChanged(); } }
        private int _tongSoNgayCongNgayLe { get; set; }
        public int TongSoNgayCongNgayLe { get { return _tongSoNgayCongNgayLe; } set { _tongSoNgayCongNgayLe = value; OnPropertyChanged(); } }
        private float _diMuonVeSom { get; set; }
        public float DiMuonVeSom { get { return _diMuonVeSom; } set { _diMuonVeSom = value; OnPropertyChanged(); } }
        private double _tongTimeOT { get; set; }
        public double TongTimeOT { get { return _tongTimeOT; } set { _tongTimeOT = value; OnPropertyChanged(); } }
        private int _nNgayNghiPhep { get; set; }
        public int NgayNghiPhep { get { return _nNgayNghiPhep; } set { _nNgayNghiPhep = value; OnPropertyChanged(); } }

        private ObservableCollection<BangCong> _bangCongDataList = new ObservableCollection<BangCong>();
        public ObservableCollection<BangCong> BangCongDataList { get { return _bangCongDataList; } set { _bangCongDataList = value; OnPropertyChanged(); } }
        public ICommand FindCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        // Combobox 
        public ObservableCollection<int> ThangTrongNam { get; set; }
        public ObservableCollection<int> NamTuTruocDenGio { get; set; }
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
        // can excute Command
        public bool CanDeleteCommand()
        {
            if (BangCongDataList.Count == 0)
                return false;
            return true;
        }
        public bool CanFindCommand()
        {
            if (Thang == 0 || Nam == 0)
                return false;
            return true;
        }
        // Load data
        private void LoadData(int thang, int nam)
        {
            BangCongDataList = new ObservableCollection<BangCong>(
                _bangCongRepository.AsQueryable()
                    .Where(bc => bc.Thang == thang && bc.Nam == nam)
                    .ToList()
            );
        }
        //Main constructer
        public ListTrackingTableViewModel(IViewModelFactory viewModelFactory, MainContentStore mainContentStore, IRepository<BangCong> BangCongRepository, IUnitOfWork unitOfWork)
        {
            _bangCongRepository = BangCongRepository;
            _unitOfWork = unitOfWork;
            LoadComboBoxData();

            FindCommand = new Commands.RelayCommand<object>((p) => CanFindCommand(),
                async (p) =>
                {
                    await _unitOfWork.BeginTransactionAsync();

                    try
                    {
                        BangCong existingBangCong = await _bangCongRepository.AsQueryable().FirstOrDefaultAsync(bc => bc.Thang == Thang && bc.Nam == Nam);

                        if (existingBangCong == null)
                        {
                            await _unitOfWork.RollbackAsync();
                            MessageBox.Show($"Bảng công cho tháng {Thang}, năm {Nam} chưa tồn tại.");
                            return;
                        }
                        else
                        {
                            BangCongDataList.Clear();
                            LoadData(Thang, Nam); 
                        }

                        await _unitOfWork.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        // Nếu có lỗi, rollback transaction và hiển thị thông báo lỗi
                        await _unitOfWork.RollbackAsync();
                        MessageBox.Show("Lỗi hệ thống: " + ex.Message);
                    }
                }
            );
            DeleteCommand = new Commands.RelayCommand<object> ((p) => CanDeleteCommand(),
                async (p) =>
                {
                    var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa bảng công cho tháng {Thang}, năm {Nam}?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        await _unitOfWork.BeginTransactionAsync();

                        try
                        {
                            foreach (var bangCong in BangCongDataList)
                            {
                                await _bangCongRepository.DeleteAsync(bangCong);
                            }

                            await _unitOfWork.CommitAsync();
                            MessageBox.Show("Xóa thành công");
                            BangCongDataList.Clear();
                        }
                        catch (Exception ex)
                        {
                            await _unitOfWork.RollbackAsync();
                            MessageBox.Show("Lỗi hệ thống: " + ex.Message);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            );
        }
        
    }
}
