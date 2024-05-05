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
        private readonly CreateViewModel<ListContractViewModel> _createListContractViewModel;
        private readonly CreateViewModel<PersonalInforViewModel> _createPersonalInforViewModel;
        private readonly CreateViewModel<FamilyInforViewModel> _createFamilyInforViewModel;
        private readonly CreateViewModel<TrainingProcessViewModel> _createTrainingProcessViewModel;
        private readonly CreateViewModel<WorkProcessViewModel> _createWorkProcessViewModel;
        private readonly CreateViewModel<DisciplineViewModel> _createDisciplineViewModel;
        private readonly CreateViewModel<RemunerativeViewModel> _createRemunerativeViewModel;
        private readonly CreateViewModel<ChildContentViewModel> _createChildContentViewModel;
        private readonly CreateViewModel<StaffCVViewModel> _createStaffCVViewModel;
        private readonly CreateViewModel<AddCVViewModel> _createAddCVViewModel;
        private readonly CreateViewModel<DepartmentViewModel> _createDepartmentViewModel;
        private readonly CreateViewModel<PositionViewModel> _createPositionViewModel;
        private readonly CreateViewModel<ContractViewModel> _createContractViewModel;
        private readonly CreateViewModel<TrackingViewModel> _createTrackingViewModel;
        private readonly CreateViewModel<ListTrackingTableViewModel> _createListTrackingTableViewModel;
        private readonly CreateViewModel<AdvanceSalaryViewModel> _createAdvanceSalaryViewModel;
        private readonly CreateViewModel<PostionStaffViewModel> _createPositionStaffViewModel;
        private readonly CreateViewModel<ListRemunerativeViewModel> _createListRemunerativeViewModel;
        private readonly CreateViewModel<SalaryViewModel> _createSalaryViewModel;



        public ViewModelFactory(CreateViewModel<LoginViewModel> createLoginViewModel,
            CreateViewModel<RegisterViewModel> createRegisterViewModel,
            CreateViewModel<MainContentViewModel> createMainContentViewModel,
            CreateViewModel<ListStaffViewModel> createListStaffViewModel,
            CreateViewModel<ListContractViewModel> createListContractViewModel,
            CreateViewModel<PersonalInforViewModel> createPersonalInforViewModel,
            CreateViewModel<FamilyInforViewModel> createFamilyInforViewModel,
            CreateViewModel<TrainingProcessViewModel> createTrainingProcessViewModel,
            CreateViewModel<WorkProcessViewModel> createWorkProcessViewModel,
            CreateViewModel<RemunerativeViewModel> createRemunerativeViewModel,
            CreateViewModel<DisciplineViewModel> createDisciplineViewModel,
            CreateViewModel<ChildContentViewModel> createChildContentViewModel,
            CreateViewModel<StaffCVViewModel> createStaffCVViewModel,
            CreateViewModel<AddCVViewModel> createAddCVViewModel,
            CreateViewModel<DepartmentViewModel> createDepartmentViewModel,
            CreateViewModel<PositionViewModel> createPositionViewModel,
            CreateViewModel<ContractViewModel> createContractViewModel,
            CreateViewModel<TrackingViewModel> createTrackingViewModel,
            CreateViewModel<AdvanceSalaryViewModel> createAdvanceSalaryViewModel,
            CreateViewModel<ListTrackingTableViewModel> createListTrackingTableViewModel,
            CreateViewModel<ListRemunerativeViewModel> createListRemunerativeViewModel,
            CreateViewModel<PostionStaffViewModel> createPositionStaffViewModel,
            CreateViewModel<SalaryViewModel> createSalaryViewModel)

        {
            _createLoginViewModel = createLoginViewModel;
            _createRegisterViewModel = createRegisterViewModel;
            _createMainContentViewModel = createMainContentViewModel;
            _createListStaffViewModel = createListStaffViewModel;
            _createListContractViewModel = createListContractViewModel;
            _createPersonalInforViewModel = createPersonalInforViewModel;
            _createFamilyInforViewModel = createFamilyInforViewModel;
            _createTrainingProcessViewModel = createTrainingProcessViewModel;
            _createWorkProcessViewModel = createWorkProcessViewModel;
            _createRemunerativeViewModel = createRemunerativeViewModel;
            _createDisciplineViewModel = createDisciplineViewModel;
            _createChildContentViewModel = createChildContentViewModel;
            _createStaffCVViewModel = createStaffCVViewModel;
            _createAddCVViewModel = createAddCVViewModel;
            _createDepartmentViewModel = createDepartmentViewModel;
            _createPositionViewModel = createPositionViewModel;
            _createContractViewModel = createContractViewModel;
            _createTrackingViewModel = createTrackingViewModel;
            _createAdvanceSalaryViewModel = createAdvanceSalaryViewModel;
            _createPositionStaffViewModel = createPositionStaffViewModel;
            _createListTrackingTableViewModel = createListTrackingTableViewModel;
            _createListRemunerativeViewModel = createListRemunerativeViewModel;
            _createSalaryViewModel = createSalaryViewModel;
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
                case EViewTypes.ListContract:
                    return _createListContractViewModel();
                case EViewTypes.PersonalInfor:
                    return _createPersonalInforViewModel();
                case EViewTypes.FamilyInfor:
                    return _createFamilyInforViewModel();
                case EViewTypes.TrainingProcess:
                    return _createTrainingProcessViewModel();
                case EViewTypes.WorkProcess:
                    return _createWorkProcessViewModel();
                case EViewTypes.Remunerative:
                    return _createRemunerativeViewModel();
                case EViewTypes.Discipline:
                    return _createDisciplineViewModel();
                case EViewTypes.ChildContent:
                    return _createChildContentViewModel();
                case EViewTypes.StaffCV:
                    return _createStaffCVViewModel();
                case EViewTypes.Department:
                    return _createDepartmentViewModel();
                case EViewTypes.Position:
                    return _createPositionViewModel();
                case EViewTypes.AddCV:
                    return _createAddCVViewModel();
                case EViewTypes.Contract:
                    return _createContractViewModel();
                case EViewTypes.Tracking:
                    return _createTrackingViewModel();
                case EViewTypes.ListTracking:
                    return _createListTrackingTableViewModel();
                case EViewTypes.AdvanceSalary:
                    return _createAdvanceSalaryViewModel();
                case EViewTypes.PositionStaff:
                    return _createPositionStaffViewModel();
                case EViewTypes.ListRemunerative:
                    return _createListRemunerativeViewModel();
                case EViewTypes.Salary:
                    return _createSalaryViewModel();
                default:
                    throw new ArgumentException();
            }
        }
    }
}

