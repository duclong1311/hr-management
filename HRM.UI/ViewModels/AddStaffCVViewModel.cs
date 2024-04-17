using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using DocumentFormat.OpenXml.Math;
using HRM.Core.UnitOfWorks;
using HRM.UI.Factories;
using HRM.UI.Stores;

namespace HRM.UI.ViewModels
{
    public class AddStaffCVViewModel : BaseViewModel
    {
        private IUnitOfWork _unitOfWork;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly MainContentStore _mainContentStore;
        public ICommand AddStaffCVCommand { get; set; }
        //public AddStaffCVViewModel(IViewModelFactory viewModelFactory, MainContentStore mainContentStore)
        //{
        //    _viewModelFactory = viewModelFactory;
        //    _mainContentStore = mainContentStore;
        //    AddStaffCVCommand = new RelayCommand<object>(p => true, p =>
        //    {
        //        mainContentStore.CurrentViewModel = viewModelFactory.CreateViewModel(Defines.EViewTypes.AddStaffCV);
        //    });
        //}
    }
}
