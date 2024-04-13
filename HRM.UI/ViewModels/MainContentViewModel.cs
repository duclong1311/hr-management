using System.Windows.Input;
using Terp.Domain.Models;
using Terp.UI.DependencyInjection;
using Terp.UI.Factories;
using Terp.UI.States.Users;
using Terp.UI.Stores;
using Terp.UI.ViewModels.Abstract;

namespace Terp.UI.ViewModels
{
    public class MainContentViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly MainContentStore _mainContentStore;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IUserStore _userStore;

        public ICommand UnitCommand { get; set; }
        public ICommand CustomerCommand { get; set; }
        public ICommand CurrencyCommand { get; set; }
        public ICommand ProviderCommand { get; set; }
        public ICommand ProductCommand { get; set; }
        public ICommand RoleCommand { get; set; }
        public ICommand ReceiveNoteCommand { get; set; }
        public ICommand DeliveryNoteCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand InventoryCommand { get; set; }
        public ICommand UserCommand { get; set; }
        public ICommand TimeKeepingCommand { get; set; }
        public ICommand PriceInformationCommand {  get; set; }
        public ICommand SearchPriceCommand {  get; set; }
        public BaseViewModel CurrentViewModel => _mainContentStore.CurrentViewModel;
        public User CurrentUser => _userStore.CurrentUser;
        public MainContentViewModel(NavigationStore navigationStore, MainContentStore mainContentStore,IViewModelFactory viewModelFactory,IUserStore userStore)
        {
            _navigationStore = navigationStore;
            _mainContentStore = mainContentStore;
            _viewModelFactory = viewModelFactory;
            _userStore = userStore;
            _mainContentStore.CurrentViewModelChanged += OnCurrenViewModelChanged;

            CurrencyCommand = new RelayCommand<object>(p => true, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Currency);
            });

            UnitCommand = new RelayCommand<object>(p => true, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Unit);
            });

            CustomerCommand = new RelayCommand<object>(p => true, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Customer);
            });
            ProviderCommand = new RelayCommand<object>(p => true, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Provider);
            });

            ProductCommand = new RelayCommand<object>(p => true, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Product);
            });

            LogoutCommand = new RelayCommand<object>(p => true, p =>
            {
                _navigationStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Login);
                _mainContentStore.CurrentViewModel = null;
            });
            PriceInformationCommand = new RelayCommand<object>(p => true, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.PriceInformation);
            });
            SearchPriceCommand = new RelayCommand<object>(p => true, p =>
            {
                _mainContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.SearchPriceInformation);
            });
        }

        private void OnCurrenViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
