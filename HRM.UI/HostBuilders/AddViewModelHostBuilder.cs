using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using HRM.UI.ViewModels;
using HRM.UI.Factories;


namespace HRM.UI.HostBuilders
{
    public static class AddViewModelHostBuilder
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<MainViewModel>();
                services.AddScoped<LoginViewModel>();
                services.AddScoped<RegisterViewModel>();
                services.AddScoped<MainContentViewModel>();
                //Child content view
                services.AddScoped<PersonalInforViewModel>();
                services.AddScoped<FamilyInforViewModel>();
                services.AddScoped<TrainingProcessViewModel>();
                services.AddScoped<WorkProcessViewModel>();
                services.AddScoped<RemunerativeViewModel>();
                services.AddScoped<DisciplineViewModel>();
                services.AddScoped<ListStaffViewModel>();
                services.AddScoped<ChildContentViewModel>();
                services.AddScoped<StaffCVViewModel>();
                services.AddScoped<AddCVViewModel>();
                services.AddScoped<TrackingViewModel>();
                
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
                services.AddScoped<CreateViewModel<ChildContentViewModel>>(service => () => service.GetRequiredService<ChildContentViewModel>());
                services.AddScoped<CreateViewModel<StaffCVViewModel>>(service => () => service.GetRequiredService<StaffCVViewModel>());
                services.AddScoped<CreateViewModel<AddCVViewModel>>(service => () => service.GetRequiredService<AddCVViewModel>());
                services.AddScoped<CreateViewModel<TrackingViewModel>>(service => () => service.GetRequiredService<TrackingViewModel>());
                services.AddScoped<IViewModelFactory, ViewModelFactory>();
            });
            return host;
        }
    }
}

