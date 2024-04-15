using HRM.UI.Factories;
using HRM.UI.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HRM.UI.ViewModels
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
