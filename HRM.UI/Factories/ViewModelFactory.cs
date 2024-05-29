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
        private readonly CreateViewModel<AddChildContentViewModel> _createAddChildContentViewModel;
        private readonly CreateViewModel<AddTrainingProcessViewModel> _createAddTrainingProcessViewModel;
        private readonly CreateViewModel<AddWorkProcessViewModel> _createAddWorkProcessViewModel;
        private readonly CreateViewModel<AddFamilyInforViewModel> _createAddFamilyInforViewModel;
        private readonly CreateViewModel<AddPersonalInforViewModel> _createAddPersonalInforViewModel;
        private readonly CreateViewModel<ImportTrackingViewModel> _createImportTrackingViewModel;
        private readonly CreateViewModel<ChartReportViewModel> _createChartReportViewModel;



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
            CreateViewModel<AddChildContentViewModel> createAddChildContentViewModel,
            CreateViewModel<AddFamilyInforViewModel> createAddFamilyInforViewModel,
            CreateViewModel<AddTrainingProcessViewModel> createAddTrainingProcessViewModel,
            CreateViewModel<AddWorkProcessViewModel> createAddWorkProcessViewModel,
            CreateViewModel<SalaryViewModel> createSalaryViewModel,
            CreateViewModel<AddPersonalInforViewModel> createAddPersonalInforViewModel,
            CreateViewModel<ChartReportViewModel> createChartReportViewModel,
            CreateViewModel<ImportTrackingViewModel> createImportTrackingViewModel)

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
            _createAddChildContentViewModel = createAddChildContentViewModel;
            _createAddFamilyInforViewModel = createAddFamilyInforViewModel;
            _createAddTrainingProcessViewModel = createAddTrainingProcessViewModel;
            _createAddWorkProcessViewModel = createAddWorkProcessViewModel;
            _createAddPersonalInforViewModel = createAddPersonalInforViewModel;
            _createAddPersonalInforViewModel = createAddPersonalInforViewModel;
            _createImportTrackingViewModel = createImportTrackingViewModel;
            _createChartReportViewModel = createChartReportViewModel;
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
                case EViewTypes.AddChildContent:
                    return _createAddChildContentViewModel();
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
                case EViewTypes.AddFamilyInfor:
                    return _createAddFamilyInforViewModel();
                case EViewTypes.AddTrainingProcess:
                    return _createAddTrainingProcessViewModel();
                case EViewTypes.AddWorkProcess:
                    return _createAddWorkProcessViewModel();
                case EViewTypes.AddPersonal:
                    return _createAddPersonalInforViewModel();
                case EViewTypes.Salary:
                    return _createSalaryViewModel();
                case EViewTypes.ImportTracking:
                    return _createImportTrackingViewModel();
                case EViewTypes.ChartReport:
                    return _createChartReportViewModel();
                default:
                    throw new ArgumentException();
            }
        }
    }
}

