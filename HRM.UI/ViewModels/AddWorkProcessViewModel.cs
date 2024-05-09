using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using HRM.UI.Factories;
using HRM.UI.States.Users;
using HRM.UI.Stores;
using System.Windows.Input;
using HRM.UI.Commands;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace HRM.UI.ViewModels
{
    public class AddWorkProcessViewModel : BaseViewModel
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly MainContentStore _mainContentStore;
        private readonly ChildContentStore _childContentStore;
        private readonly IUserStore _userStore;

        private ObservableCollection<QuaTrinhCongTac> _list = new ObservableCollection<QuaTrinhCongTac>();
        public ObservableCollection<QuaTrinhCongTac> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }

        private IUnitOfWork _unitOfWork;

        private IRepository<QuaTrinhCongTac> _quaTrinhCongTacRepository;

        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private string _tuNgayDenNgay;
        public string TuNgayDenNgay
        {
            get { return _tuNgayDenNgay; }
            set { _tuNgayDenNgay = value; OnPropertyChanged(); }
        }

        private string _noiCongTac;
        public string NoiCongTac
        {
            get { return _noiCongTac; }
            set { _noiCongTac = value; OnPropertyChanged(); }
        }

        private string _chucVu;
        public string ChucVu
        {
            get { return _chucVu; }
            set { _chucVu = value; OnPropertyChanged(); }
        }
        private QuaTrinhCongTac _selectedItem;
        public QuaTrinhCongTac SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    TuNgayDenNgay = SelectedItem.TuNgayDenNgay;
                    NoiCongTac = SelectedItem.NoiCongTac;
                    ChucVu = SelectedItem.ChucVu;
                }
                OnPropertyChanged();
            }
        }

        public void LoadData()
        {
            if (_userStore.CurrentNhanSu == null || string.IsNullOrEmpty(_userStore.CurrentNhanSu.MaNhanVien))
            {
                MessageBox.Show("Không có mã nhân viên để truy vấn quá trình công tác.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            List = new ObservableCollection<QuaTrinhCongTac>(_quaTrinhCongTacRepository
                .AsQueryable()
                .Where(x => x.MaNhanVien == _userStore.CurrentNhanSu.MaNhanVien)
                .ToList());
        }

        public AddWorkProcessViewModel(IUserStore userStore, IViewModelFactory viewModelFactory, MainContentStore mainContentStore, IRepository<QuaTrinhCongTac> quaTrinhCongTacRepository, IUnitOfWork unitOfWork, ChildContentStore childContentStore)
        {
            _viewModelFactory = viewModelFactory;
            _mainContentStore = mainContentStore;
            _quaTrinhCongTacRepository = quaTrinhCongTacRepository;
            _childContentStore = childContentStore;
            _unitOfWork = unitOfWork;
            _userStore = userStore;

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NoiCongTac))
                    return false;

                //if (_userStore.CurrentNhanSu?.MaNhanVien == null)
                //{
                //    MessageBox.Show("Chưa nhập mã nhân viên.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    return false;
                //}

                return true;
            }, async (p) =>
            {
                var QuaTrinhCongTac = new QuaTrinhCongTac()
                {
                    MaNhanVien = userStore.CurrentNhanSu.MaNhanVien,
                    TuNgayDenNgay = TuNgayDenNgay,
                    NoiCongTac = NoiCongTac,
                    ChucVu = ChucVu,
                };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    QuaTrinhCongTac = await _quaTrinhCongTacRepository.AddAsync(QuaTrinhCongTac);
                    await _unitOfWork.CommitAsync();
                    LoadData();
                    if (QuaTrinhCongTac != null)
                    {
                        MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi hệ thống", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            DeleteCommand = new RelayCommand<object>((p) =>
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
                            var quaTrinhCongTac = await _quaTrinhCongTacRepository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);

                            if (quaTrinhCongTac != null)
                            {
                                await _quaTrinhCongTacRepository.DeleteAsync(quaTrinhCongTac);
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

    }
}
