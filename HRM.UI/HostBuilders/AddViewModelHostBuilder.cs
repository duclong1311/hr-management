﻿using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using HRM.UI.ViewModels;
using HRM.UI.Factories;
using HRM.UI.Views;


namespace HRM.UI.HostBuilders
{
    public static class AddViewModelHostBuilder
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient<MainViewModel>();
                services.AddTransient<LoginViewModel>();
                services.AddTransient<RegisterViewModel>();
                services.AddTransient<MainContentViewModel>();
                services.AddScoped<PersonalInforViewModel>();
                services.AddScoped<FamilyInforViewModel>();
                services.AddScoped<TrainingProcessViewModel>();
                services.AddScoped<WorkProcessViewModel>();
                services.AddScoped<RemunerativeViewModel>();
                services.AddScoped<ContractViewModel>();
                services.AddScoped<DisciplineViewModel>();
                services.AddScoped<ListStaffViewModel>();
                services.AddScoped<ListContractViewModel>();
                services.AddScoped<ChildContentViewModel>();
                services.AddScoped<AddChildContentViewModel>();
                services.AddScoped<ChildContentView>();
                services.AddScoped<StaffCVViewModel>();
                services.AddScoped<DepartmentViewModel>();
                services.AddScoped<PositionViewModel>();
                services.AddScoped<AddCVViewModel>();
                services.AddScoped<FamilyInforViewModel>();
                services.AddScoped<TrackingViewModel>();
                services.AddScoped<ListTrackingTableViewModel>();
                services.AddScoped<PostionStaffViewModel>();
                services.AddScoped<ListRemunerativeViewModel>();
                services.AddScoped<AdvanceSalaryViewModel>();
                services.AddScoped<SalaryViewModel>();
                services.AddScoped<AddFamilyInforViewModel>();
                services.AddScoped<AddWorkProcessViewModel>();
                services.AddScoped<AddTrainingProcessViewModel>();
                services.AddScoped<AddPersonalInforViewModel>();
                services.AddScoped<ImportTrackingViewModel>();
                services.AddScoped<ChartReportViewModel>();


                services.AddScoped<CreateViewModel<LoginViewModel>>(service => () => service.GetRequiredService<LoginViewModel>());
                services.AddScoped<CreateViewModel<RegisterViewModel>>(service => () => service.GetRequiredService<RegisterViewModel>());
                services.AddScoped<CreateViewModel<MainContentViewModel>>(service => () => service.GetRequiredService<MainContentViewModel>());
                services.AddScoped<CreateViewModel<PersonalInforViewModel>>(service => () => service.GetRequiredService<PersonalInforViewModel>());
                services.AddScoped<CreateViewModel<FamilyInforViewModel>>(service => () => service.GetRequiredService<FamilyInforViewModel>());
                services.AddScoped<CreateViewModel<TrainingProcessViewModel>>(service => () => service.GetRequiredService<TrainingProcessViewModel>());
                services.AddScoped<CreateViewModel<WorkProcessViewModel>>(service => () => service.GetRequiredService<WorkProcessViewModel>());
                services.AddScoped<CreateViewModel<RemunerativeViewModel>>(service => () => service.GetRequiredService<RemunerativeViewModel>());
                services.AddScoped<CreateViewModel<DisciplineViewModel>>(service => () => service.GetRequiredService<DisciplineViewModel>());
                services.AddScoped<CreateViewModel<ListStaffViewModel>>(service => () => service.GetRequiredService<ListStaffViewModel>());
                services.AddScoped<CreateViewModel<ListContractViewModel>>(service => () => service.GetRequiredService<ListContractViewModel>());
                services.AddScoped<CreateViewModel<ChildContentViewModel>>(service => () => service.GetRequiredService<ChildContentViewModel>());
                services.AddScoped<CreateViewModel<StaffCVViewModel>>(service => () => service.GetRequiredService<StaffCVViewModel>());
                services.AddScoped<CreateViewModel<AddCVViewModel>>(service => () => service.GetRequiredService<AddCVViewModel>());
                services.AddScoped<CreateViewModel<DepartmentViewModel>>(service => () => service.GetRequiredService<DepartmentViewModel>());
                services.AddScoped<CreateViewModel<ContractViewModel>>(service => () => service.GetRequiredService<ContractViewModel>());
                services.AddScoped<CreateViewModel<PositionViewModel>>(service => () => service.GetRequiredService<PositionViewModel>());
                services.AddScoped<CreateViewModel<TrackingViewModel>>(service => () => service.GetRequiredService<TrackingViewModel>());
                services.AddScoped<CreateViewModel<ListTrackingTableViewModel>>(service => () => service.GetRequiredService<ListTrackingTableViewModel>());
                services.AddScoped<CreateViewModel<PostionStaffViewModel>>(services => () => services.GetRequiredService<PostionStaffViewModel>());
                services.AddScoped<CreateViewModel<ListRemunerativeViewModel>>(services => () => services.GetRequiredService<ListRemunerativeViewModel>());
                services.AddScoped<CreateViewModel<AdvanceSalaryViewModel>>(service => () => service.GetRequiredService<AdvanceSalaryViewModel>());
                services.AddScoped<CreateViewModel<SalaryViewModel>>(service => () => service.GetRequiredService<SalaryViewModel>());
                services.AddScoped<CreateViewModel<AddChildContentViewModel>>(service => () => service.GetRequiredService<AddChildContentViewModel>());
                services.AddScoped<CreateViewModel<AddFamilyInforViewModel>>(service => () => service.GetRequiredService<AddFamilyInforViewModel>());
                services.AddScoped<CreateViewModel<AddWorkProcessViewModel>>(service => () => service.GetRequiredService<AddWorkProcessViewModel>());
                services.AddScoped<CreateViewModel<AddTrainingProcessViewModel>>(service => () => service.GetRequiredService<AddTrainingProcessViewModel>());
                services.AddScoped<CreateViewModel<AddPersonalInforViewModel>>(service => () => service.GetRequiredService<AddPersonalInforViewModel>());
                services.AddScoped<CreateViewModel<ImportTrackingViewModel>>(service => () => service.GetRequiredService<ImportTrackingViewModel>());
                services.AddScoped<CreateViewModel<ChartReportViewModel>>(service => () => service.GetRequiredService<ChartReportViewModel>());

                services.AddScoped<IViewModelFactory, ViewModelFactory>();
            });
            return host;
        }
    }
}

