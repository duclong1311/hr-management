using System.Windows;
using Terp.UI.DependencyInjection;
using Terp.UI.Factories;
using Terp.UI.Stores;
using Terp.UI.ViewModels.Abstract;

namespace Terp.UI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly IViewModelFactory _viewModelFactory;

        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        public Visibility LoadingVisibility => _navigationStore.LoadingVisibility;
        public MainViewModel(NavigationStore navigationStore, IViewModelFactory viewModelFactory)
        {
            _navigationStore = navigationStore;
            _viewModelFactory = viewModelFactory;
            _navigationStore.CurrentViewModelChanged += OnCurrenViewModelChanged;
            _navigationStore.LoadingVisibilityChanged += OnLoadingVisibilityChanged;
            _navigationStore.LoadingVisibility = Visibility.Hidden;
            if (_navigationStore.CurrentViewModel == null)
            {
                _navigationStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Login);
            }
        }

        private void OnCurrenViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void OnLoadingVisibilityChanged()
        {
            OnPropertyChanged(nameof(LoadingVisibility));
        }
    }
}
