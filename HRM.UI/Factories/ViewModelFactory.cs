using HRM.UI.Defines;
using HRM.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.UI.Factories
{
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : BaseViewModel;

    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
        private readonly CreateViewModel<RegisterViewModel> _createRegisterViewModel;
        private readonly CreateViewModel<MainContentViewModel> _createMainContentViewModel;
        private readonly CreateViewModel<ListStaffViewModel> _createListStaffViewModel;
        private readonly CreateViewModel<PersonalInforViewModel> _createPersonalInforViewModel;
        private readonly CreateViewModel<FamilyInforViewModel> _createFamilyInforViewModel;
        private readonly CreateViewModel<TrainingProcessViewModel> _createTrainingProcessViewModel;
        private readonly CreateViewModel<WorkProcessViewModel> _createWorkProcessViewModel;
        private readonly CreateViewModel<CVStatusViewModel> _createCVStatusViewModel;
        private readonly CreateViewModel<ChildContentViewModel> _createChildContentViewModel;
        private readonly CreateViewModel<StaffCVViewModel> _createStaffCVViewModel;
        private readonly CreateViewModel<AddCVViewModel> _createAddCVViewModel;


        public ViewModelFactory(CreateViewModel<LoginViewModel> createLoginViewModel,
            CreateViewModel<RegisterViewModel> createRegisterViewModel,
            CreateViewModel<MainContentViewModel> createMainContentViewModel,
            CreateViewModel<ListStaffViewModel> createListStaffViewModel,
            CreateViewModel<PersonalInforViewModel> createPersonalInforViewModel,
            CreateViewModel<FamilyInforViewModel> createFamilyInforViewModel,
            CreateViewModel<TrainingProcessViewModel> createTrainingProcessViewModel,
            CreateViewModel<WorkProcessViewModel> createWorkProcessViewModel,
            CreateViewModel<CVStatusViewModel> createCVStatusViewModel,
            CreateViewModel<ChildContentViewModel> createChildContentViewModel,
            CreateViewModel<StaffCVViewModel> createStaffCVViewModel,
            CreateViewModel<AddCVViewModel> createAddCVViewModel)

        {
            _createLoginViewModel = createLoginViewModel;
            _createRegisterViewModel = createRegisterViewModel;
            _createMainContentViewModel = createMainContentViewModel;
            _createListStaffViewModel = createListStaffViewModel;
            _createPersonalInforViewModel = createPersonalInforViewModel;
            _createFamilyInforViewModel = createFamilyInforViewModel;
            _createTrainingProcessViewModel = createTrainingProcessViewModel;
            _createWorkProcessViewModel = createWorkProcessViewModel;
            _createCVStatusViewModel = createCVStatusViewModel;
            _createChildContentViewModel = createChildContentViewModel;
            _createStaffCVViewModel = createStaffCVViewModel;
            _createAddCVViewModel = createAddCVViewModel;
        }
        public BaseViewModel CreateViewModel(EViewTypes viewType)
        {
            switch (viewType)
            {
                case EViewTypes.Login:
                    return _createLoginViewModel();
                case EViewTypes.Register:
                    return _createRegisterViewModel();
                case EViewTypes.MainContent:
                    return _createMainContentViewModel();
                case EViewTypes.ListStaff:
                    return _createListStaffViewModel();
                case EViewTypes.PersonalInfor:
                    return _createPersonalInforViewModel();
                case EViewTypes.FamilyInfor:
                    return _createFamilyInforViewModel();
                case EViewTypes.TrainingProcess:
                    return _createTrainingProcessViewModel();
                case EViewTypes.WorkProcess:
                    return _createWorkProcessViewModel();
                case EViewTypes.CVStatus:
                    return _createCVStatusViewModel();
                case EViewTypes.ChildContent:
                    return _createChildContentViewModel();
                case EViewTypes.StaffCV:
                    return _createStaffCVViewModel();
                case EViewTypes.AddCV:
                    return _createAddCVViewModel();
                default:
                    throw new ArgumentException();
            }
        }
    }
}

