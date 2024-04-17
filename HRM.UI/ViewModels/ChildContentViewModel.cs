using HRM.Domain.Models;
using HRM.UI.Commands;
using HRM.UI.Factories;
using HRM.UI.States.Staff;
using HRM.UI.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HRM.UI.ViewModels
{
    public class ChildContentViewModel : BaseViewModel
    {
        private readonly ChildContentStore _childContentStore;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly StaffStore _staffStore;
        public ICommand PersonInforCommand { get; set; }
        public BaseViewModel CurrentViewModel => _childContentStore.CurrentViewModel;
        public NhanSu CurrentNhanSu => _staffStore.CurrentNhanSu;
        public ChildContentViewModel(StaffStore staffStore, ChildContentStore childContentStore, IViewModelFactory viewModelFactory)
        {
            _staffStore = staffStore;
            _childContentStore = childContentStore;
            _viewModelFactory = viewModelFactory;
            childContentStore.CurrentViewModelChanged += OnCurrenViewModelChanged;

            PersonInforCommand = new RelayCommand<object>(p => true, p =>
            {
                _childContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.PersonalInfor);
            });

        }
        private void OnCurrenViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
