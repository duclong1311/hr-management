using HRM.Domain.Models;
using HRM.UI.Commands;
using HRM.UI.Factories;
using HRM.UI.States.Staff;
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
    public class ChildContentViewModel : BaseViewModel
    {
        private readonly ChildContentStore _childContentStore;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IUserStore _userStore;
        public ICommand PersonInforCommand { get; set; }
        public ICommand FamilyInforCommand { get; set; }
        public ICommand TrainingProcessCommand { get; set; }
        public ICommand WorkProcessCommand { get; set; }
        public ICommand DisciplineCommand { get; set; }
        public ICommand RemunerativeCommand { get; set; }
        public NhanSu CurrentNhanSu => _userStore.CurrentNhanSu;
        public BaseViewModel CurrentViewModel => _childContentStore.CurrentViewModel;

        public ChildContentViewModel(IUserStore userStore, ChildContentStore childContentStore, IViewModelFactory viewModelFactory)
        {
            //_staffStore = staffStore;
            _userStore = userStore;
            _childContentStore = childContentStore;
            _viewModelFactory = viewModelFactory;
            childContentStore.CurrentViewModelChanged += OnCurrenViewModelChanged;

            PersonInforCommand = new RelayCommand<object>(p => true, p =>
            {
                _childContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.PersonalInfor);
            });
            FamilyInforCommand = new RelayCommand<object>(p => true, p =>
            {
                _childContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.FamilyInfor);
            });
            TrainingProcessCommand = new RelayCommand<object>(p => true, p =>
            {
                _childContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.TrainingProcess);
            });
            WorkProcessCommand = new RelayCommand<object>(p => true, p =>
            {
                _childContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.WorkProcess);
            });
            DisciplineCommand = new RelayCommand<object>(p => true, p =>
            {
                _childContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Discipline);
            });
            RemunerativeCommand = new RelayCommand<object>(p => true, p =>
            {
                _childContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.Remunerative);
            });


        }
        private void OnCurrenViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
