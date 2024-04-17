using HRM.Core.Repositories;
using HRM.Domain.Models;
using HRM.UI.Commands;
using HRM.UI.Factories;
using HRM.UI.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HRM.UI.ViewModels
{
    public class StaffCVViewModel : BaseViewModel
    {
        private ObservableCollection<NhanSu> _list;
        public ObservableCollection<NhanSu> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }
        private NhanSu _selectedItem;
        public NhanSu SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }
        private string _filter;
        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                OnPropertyChanged();
                LoadData();
            }
        }
        private readonly IRepository<NhanSu> _repository;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly MainContentStore _mainContentStore;
        public ICommand StaffViewCommand { get; set; }
        public ICommand AddCVCommand { get; set; }
        public StaffCVViewModel(IViewModelFactory viewModelFactory, MainContentStore mainContentStore, IRepository<NhanSu> repository)
        {
            _repository = repository;
            _viewModelFactory = viewModelFactory;
            _mainContentStore = mainContentStore;
            StaffViewCommand = new RelayCommand<object>(p => true, p =>
            {
                mainContentStore.CurrentViewModel = viewModelFactory.CreateViewModel(Defines.EViewTypes.ChildContent);
            });
            AddCVCommand = new RelayCommand<object>(p => true, p => 
            {
                mainContentStore.CurrentViewModel = viewModelFactory.CreateViewModel(Defines.EViewTypes.ChildContent);
            });
            LoadData();

        }
        private void LoadData()
        {
            List = new ObservableCollection<NhanSu>(_repository.AsQueryable().Include(x => x.BoPhan).Include(x => x.ChucVu).Include(x => x.ViTri).ToList());
            if (!String.IsNullOrWhiteSpace(Filter))
            {
                List = new ObservableCollection<NhanSu>(_repository.AsQueryable().Where(x => x.MaNhanVien.Contains(Filter) || x.HoTen.Contains(Filter)).ToList());
            }
        }
    }
}
