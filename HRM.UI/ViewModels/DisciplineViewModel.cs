using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using HRM.UI.Factories;
using HRM.UI.Stores;
using Microsoft.EntityFrameworkCore;

namespace HRM.UI.ViewModels
{
    public class DisciplineViewModel : BaseViewModel
    {
        private ObservableCollection<DanhMucKhenThuongKyLuat> _list = new ObservableCollection<DanhMucKhenThuongKyLuat>();
        public ObservableCollection<DanhMucKhenThuongKyLuat> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }
        private DanhMucKhenThuongKyLuat _selectedItem;
        public DanhMucKhenThuongKyLuat SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    HinhThuc = _selectedItem.HinhThucKhenThuongKyLuat;
                    Cap = _selectedItem.CapKhenThuongKyLuat;
                }
                OnPropertyChanged();
            }
        }
        private string _hinhThuc { get; set; }
        private string _cap { get; set; }
        public string HinhThuc { get => _hinhThuc; set { _hinhThuc = value; OnPropertyChanged(); } }
        public string Cap { get => _cap; set { _cap = value; OnPropertyChanged(); } }

        private IUnitOfWork _unitOfWork;
        private IRepository<DanhMucKhenThuongKyLuat> _danhMucKhenThuongKyLuatRespository;
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public DisciplineViewModel(IRepository<DanhMucKhenThuongKyLuat> DanhMucKhenThuongKyLuatRepository, IUnitOfWork unitOfWork)
        {
            _danhMucKhenThuongKyLuatRespository = DanhMucKhenThuongKyLuatRepository;
            _unitOfWork = unitOfWork;
            LoadData();

            AddCommand = new Commands.RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(HinhThuc))
                    return false;
                if (_danhMucKhenThuongKyLuatRespository
                .AsQueryable()
                .Any(x => x.HinhThucKhenThuongKyLuat == HinhThuc) && _danhMucKhenThuongKyLuatRespository
                .AsQueryable()
                .Any(x => x.CapKhenThuongKyLuat == Cap))
                    return false;
                return true;
            }, async (p) =>
            {
                var danhMucKhenThuongKyLuat = new DanhMucKhenThuongKyLuat()
                {
                    CapKhenThuongKyLuat = Cap,
                    HinhThucKhenThuongKyLuat = HinhThuc
                };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    danhMucKhenThuongKyLuat = await _danhMucKhenThuongKyLuatRespository
                    .AddAsync(danhMucKhenThuongKyLuat);
                    await _unitOfWork.CommitAsync();
                    LoadData();
                    if (danhMucKhenThuongKyLuat != null)
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

            UpdateCommand = new Commands.RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                if (!_danhMucKhenThuongKyLuatRespository
                .AsQueryable()
                .Any(x => x.Id == SelectedItem.Id))
                    return false;
                return true;

            }, async (p) =>
            {
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var unit = await _danhMucKhenThuongKyLuatRespository
                    .AsQueryable()
                    .FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    unit.CapKhenThuongKyLuat = Cap;
                    unit.HinhThucKhenThuongKyLuat = HinhThuc;
                    await _danhMucKhenThuongKyLuatRespository.UpdateAsync(unit);
                    await _unitOfWork.CommitAsync();

                    LoadData();
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });

            DeleteCommand = new Commands.RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, async (p) =>
            {
                if (SelectedItem != null)
                {
                    MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận xóa", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        await _unitOfWork.BeginTransactionAsync();

                        try
                        {
                            var danhmuckhenThuongKyLuat = await _danhMucKhenThuongKyLuatRespository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);

                            if (danhmuckhenThuongKyLuat != null)
                            {
                                await _danhMucKhenThuongKyLuatRespository.DeleteAsync(danhmuckhenThuongKyLuat);
                                await _unitOfWork.CommitAsync();
                                LoadData();
                            }
                        }
                        catch (Exception ex)
                        {
                            await _unitOfWork.RollbackAsync();
                            MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            });
        }
        private void LoadData()
        {
            List = new ObservableCollection<DanhMucKhenThuongKyLuat>(_danhMucKhenThuongKyLuatRespository.AsQueryable().ToList());
        }
    }
}
