using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using System.Windows.Input;
using HRM.UI.Commands;
using System.Windows;
using System.Collections.ObjectModel;
using HRM.UI.Factories;
using HRM.UI.Stores;
using Microsoft.EntityFrameworkCore;
using HRM.UI.States.Users;

namespace HRM.UI.ViewModels
{
    public class WorkProcessViewModel : BaseViewModel
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
            get { return _tuNgayDenNgay;}
            set {  _tuNgayDenNgay = value; OnPropertyChanged(); }
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

        public WorkProcessViewModel(IUserStore userStore, IViewModelFactory viewModelFactory, MainContentStore mainContentStore, IRepository<QuaTrinhCongTac> quaTrinhCongTacRepository, IUnitOfWork unitOfWork, ChildContentStore childContentStore)
        {
            _viewModelFactory = viewModelFactory;
            _mainContentStore = mainContentStore;
            _quaTrinhCongTacRepository = quaTrinhCongTacRepository;
            _childContentStore = childContentStore;
            _unitOfWork = unitOfWork;
            _userStore = userStore;
            LoadData();

            AddCommand = new RelayCommand<object>((p) =>
            {
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
                    var quaTrinhCongTac = await _quaTrinhCongTacRepository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    await _quaTrinhCongTacRepository.DeleteAsync(quaTrinhCongTac);
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
        public void LoadData()
        {
            List = new ObservableCollection<QuaTrinhCongTac>(_quaTrinhCongTacRepository.AsQueryable().Where(x => x.MaNhanVien == _userStore.CurrentNhanSu.MaNhanVien).ToList());
        }
    }
}
