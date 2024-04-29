using HRM.Domain.Models;
using HRM.UI.Commands;
using HRM.UI.Factories;
using HRM.UI.States.Users;
using HRM.UI.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HRM.UI.ViewModels
{
    public class MainContentViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly MainContentStore _mainContentStore;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IUserStore _userStore;

        public ICommand LogoutCommand { get; set; }
        public ICommand StaffListCommand { get; set; }
        public ICommand StaffCVCommand { get; set; }
        public ICommand AddCVCommand { get; set; }
        public ICommand DepartmentCommand { get; set; }
        public ICommand PositionCommand { get; set; }
        public ICommand ContractCommand { get; set; }
        public ICommand TrackingCommand { get; set; }  
        public ICommand RemunerativeCommand { get; set; }
        public ICommand AdvanceSalaryCommand { get; set; }

        public BaseViewModel CurrentViewModel => _mainContentStore.CurrentViewModel;
        public User CurrentUser => _userStore.CurrentUser;
        public MainContentViewModel(NavigationStore navigationStore, MainContentStore mainContentStore, IViewModelFactory viewModelFactory, IUserStore userStore)
        {
            _navigationStore = navigationStore;
            _mainContentStore = mainContentStore;
            _viewModelFactory = viewModelFactory;
            _userStore = userStore;
            _mainContentStore.CurrentViewModelChanged += OnCurrenViewModelChanged;

            

            LogoutCommand = new RelayCommand<object>(p => true, p =>
            {
                _navigationStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Login);
                _mainContentStore.CurrentViewModel = null;
            });
            StaffListCommand = new RelayCommand<object>(p => true, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.ListStaff);
            });
            StaffCVCommand = new RelayCommand<object>(p => true, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.StaffCV);
            });
            DepartmentCommand = new RelayCommand<object>(p => true, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Department);
            });
            PositionCommand = new RelayCommand<object>(p => true, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Position);
            });
            AddCVCommand = new RelayCommand<object>(p => true, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.ChildContent);
            });
            RemunerativeCommand = new RelayCommand<object>(p => true, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Remunerative);
            });
            ContractCommand = new RelayCommand<object>(p => true, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Contract);
            });
            TrackingCommand = new RelayCommand<object>(p => true, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Tracking);
            });

            //Lương 
            AdvanceSalaryCommand = new RelayCommand<object>(p => true, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.AdvanceSalary);
            });
        }

        private void OnCurrenViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}

