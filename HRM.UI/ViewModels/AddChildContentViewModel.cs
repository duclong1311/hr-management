using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HRM.Domain.Models;
using HRM.UI.Commands;
using HRM.UI.Factories;
using HRM.UI.States.Users;
using HRM.UI.Stores;

namespace HRM.UI.ViewModels
{
    public class AddChildContentViewModel : BaseViewModel
    {
        private readonly ChildContentStore _childContentStore;

        private readonly IViewModelFactory _viewModelFactory;

        private readonly IUserStore _userStore;
        public ICommand AddPersonInforCommand { get; set; }
        public ICommand AddFamilyInforCommand { get; set; }
        public ICommand AddTrainingProcessCommand { get; set; }
        public ICommand AddWorkProcessCommand { get; set; }
        public NhanSu CurrentNhanSu => _userStore.CurrentNhanSu;
        public BaseViewModel CurrentViewModel => _childContentStore.CurrentViewModel;
        private void OnCurrenViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        public AddChildContentViewModel(IUserStore userStore, ChildContentStore childContentStore, IViewModelFactory viewModelFactory)
        {
            _userStore = userStore;
            _childContentStore = childContentStore;
            _viewModelFactory = viewModelFactory;
            childContentStore.CurrentViewModelChanged += OnCurrenViewModelChanged;

            AddPersonInforCommand = new RelayCommand<object>(p => true, p =>
            {
                _childContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.AddPersonal);
            });

            AddFamilyInforCommand = new RelayCommand<object>(p => true, p =>
            {
                _childContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.AddFamilyInfor);
            });

            AddTrainingProcessCommand = new RelayCommand<object>(p => true, p =>
            {
                _childContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.AddTrainingProcess);
            });

            AddWorkProcessCommand = new RelayCommand<object>(p => true, p =>
            {
                _childContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.AddWorkProcess);
            });
        }
    }
}
