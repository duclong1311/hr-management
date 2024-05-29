using HRM.Domain.Models;
using HRM.UI.Commands;
using HRM.UI.Factories;
using HRM.UI.Models;
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
        public ICommand ListContractCommand { get; set; }
        public ICommand TrackingCommand { get; set; }
        public ICommand ListTrackingTableCommand { get; set; }
        public ICommand ImportTrackingCommand { get; set; }
        public ICommand RemunerativeCommand { get; set; }
        public ICommand DisciplineCommand { get; set; }
        public ICommand AdvanceSalaryCommand { get; set; }
        public ICommand PositionStaffCommand { get; set; }
        public ICommand ListRemunerativeCommand { get; set; }
        public ICommand SalaryCommand { get; set; }
        public ICommand AddStaffViewCommand { get; set; }

        public BaseViewModel CurrentViewModel => _mainContentStore.CurrentViewModel;
        public User CurrentUser => _userStore.CurrentUser;
        private LoggedInUser _user = new LoggedInUser();
        public LoggedInUser CurrentUserRole { get => _user; set { _user = value; OnPropertyChanged(nameof(CurrentUserRole)); } }
        public MainContentViewModel(NavigationStore navigationStore, MainContentStore mainContentStore, IViewModelFactory viewModelFactory, IUserStore userStore)
        {
            _navigationStore = navigationStore;
            _mainContentStore = mainContentStore;
            _viewModelFactory = viewModelFactory;
            _userStore = userStore;
            _mainContentStore.CurrentViewModelChanged += OnCurrenViewModelChanged;


            CurrentUserRole.User = CurrentUser;
            LogoutCommand = new RelayCommand<object>(p => true, p =>
            {
                _userStore.CurrentUser = null;
                _mainContentStore.CurrentViewModel = null;

                _navigationStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Login);
            });
            StaffListCommand = new RelayCommand<object>(p => CurrentUserRole.IsAdmin || CurrentUserRole.IsManager, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.ListStaff);
            });
            AddStaffViewCommand = new RelayCommand<object>(p => CurrentUserRole.IsAdmin, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.AddChildContent);
            });
            StaffCVCommand = new RelayCommand<object>(p => CurrentUserRole.IsAdmin, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.StaffCV);
            });
            DepartmentCommand = new RelayCommand<object>(p => CurrentUserRole.IsAdmin || CurrentUserRole.IsManager, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Department);
            });
            PositionCommand = new RelayCommand<object>(p => CurrentUserRole.IsAdmin, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Position);
            });
            AddCVCommand = new RelayCommand<object>(p => CurrentUserRole.IsAdmin, p =>
            {
                _userStore.CurrentNhanSu = null;
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.PersonalInfor);
            });
            RemunerativeCommand = new RelayCommand<object>(p => CurrentUserRole.IsAdmin, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Remunerative);
            });
            DisciplineCommand = new RelayCommand<object>(p => CurrentUserRole.IsAdmin, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Discipline);
            });
            ContractCommand = new RelayCommand<object>(p => CurrentUserRole.IsAdmin, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Contract);
            });
            ListContractCommand = new RelayCommand<object>(p => CurrentUserRole.IsAdmin, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.ListContract);
            });
            TrackingCommand = new RelayCommand<object>(p => CurrentUserRole.IsAdmin, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Tracking);
            });
            ListTrackingTableCommand = new RelayCommand<object>(p => CurrentUserRole.IsAdmin, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.ListTracking);
            });
            PositionStaffCommand = new RelayCommand<object>(p => CurrentUserRole.IsAdmin, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.PositionStaff);
            });
            ListRemunerativeCommand = new RelayCommand<object>(p => CurrentUserRole.IsAdmin, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.ListRemunerative);
            });

            ImportTrackingCommand = new RelayCommand<object>(p => CurrentUserRole.IsAdmin, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.ImportTracking);
            });

            //Lương 
            AdvanceSalaryCommand = new RelayCommand<object>(p => CurrentUserRole.IsAdmin, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.AdvanceSalary);
            });
            SalaryCommand = new RelayCommand<object>(p => CurrentUserRole.IsAdmin, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Salary);
            });
        }

        private void OnCurrenViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}

