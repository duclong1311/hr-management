using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using HRM.UI.Commands;
using HRM.UI.Factories;
using HRM.UI.Stores;
using Microsoft.EntityFrameworkCore;

namespace HRM.UI.ViewModels
{
    public class DepartmentViewModel : BaseViewModel
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly MainContentStore _mainContentStore;
        private IUnitOfWork _unitOfWork;
        private IRepository<BoPhan> _boPhanRepository;
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private string _tenBoPhan;
        public string TenBoPhan
        {
            get { return _tenBoPhan; }
            set
            {
                _tenBoPhan = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BoPhan> _list = new ObservableCollection<BoPhan>();
        public ObservableCollection<BoPhan> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }
        private BoPhan _selectedItem;
        public BoPhan SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    TenBoPhan = SelectedItem.TenBoPhan;
                }
                OnPropertyChanged();
            }
        }

        private void LoadData()
        {
            List = new ObservableCollection<BoPhan>(_boPhanRepository.AsQueryable().ToList());
            /*            if (!String.IsNullOrWhiteSpace(Filter))
                        {
                            List = new ObservableCollection<NhanSu>(_repository.AsQueryable().Where(x => x.MaNhanVien.Contains(Filter) || x.HoTen.Contains(Filter)).ToList());
                        }*/
        }

        public DepartmentViewModel(IViewModelFactory viewModelFactory, MainContentStore mainContentStore, IRepository<BoPhan> BoPhanRepository, IUnitOfWork unitOfWork)
        {
            _viewModelFactory = viewModelFactory;
            _mainContentStore = mainContentStore;
            _boPhanRepository = BoPhanRepository;
            _unitOfWork = unitOfWork;

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, async (p) =>

            {
                var BoPhan = new BoPhan()
                {
                    TenBoPhan = TenBoPhan
                };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    BoPhan = await _boPhanRepository.AddAsync(BoPhan);
                    await _unitOfWork.CommitAsync();
                    LoadData();
                    if (BoPhan != null)
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
                    var QuanHeGiaDinh = await _boPhanRepository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    await _boPhanRepository.DeleteAsync(QuanHeGiaDinh);
                    await _unitOfWork.CommitAsync();
                    LoadData();
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            }
            );

            LoadData();
        }

    }
}
