using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.UI.Factories;
using Terp.UI.Stores;
using Terp.UI.ViewModels;
using Terp.UI.ViewModels.Abstract;


namespace Terp.UI.HostBuilders
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
                services.AddScoped<CurrencyViewModel>();
                services.AddSingleton<MainContentViewModel>();
                services.AddTransient<UnitViewModel>();
                services.AddScoped<ProviderViewModel>();
                services.AddTransient<UnitViewModel>();
                services.AddTransient<AlternativeProductInformationsViewModel>();
                services.AddTransient<ProductViewModel>();
                services.AddTransient<PriceInformationViewModel>();
                services.AddTransient<SearchPriceInformationViewModel>();


                services.AddScoped<CreateViewModel<LoginViewModel>>(service => () => service.GetRequiredService<LoginViewModel>());
                services.AddScoped<CreateViewModel<RegisterViewModel>>(service => () => service.GetRequiredService<RegisterViewModel>());
                services.AddScoped<CreateViewModel<CurrencyViewModel>>(service => () => service.GetRequiredService<CurrencyViewModel>());
                services.AddSingleton<CreateViewModel<MainContentViewModel>>(service => () => service.GetRequiredService<MainContentViewModel>());
                services.AddTransient<CreateViewModel<UnitViewModel>>(service => () => service.GetRequiredService<UnitViewModel>());
                services.AddScoped<CreateViewModel<ProviderViewModel>>(services => () => services.GetRequiredService<ProviderViewModel>());
                services.AddTransient<CreateViewModel<UnitViewModel>>(service => () => service.GetRequiredService<UnitViewModel>());
                services.AddTransient<CreateViewModel<AlternativeProductInformationsViewModel>>(service => () => service.GetRequiredService<AlternativeProductInformationsViewModel>());
                services.AddTransient<CreateViewModel<ProductViewModel>>(service => () => service.GetRequiredService<ProductViewModel>());
                services.AddTransient<CreateViewModel<PriceInformationViewModel>>(service => () => service.GetRequiredService<PriceInformationViewModel>());
                services.AddTransient<CreateViewModel<SearchPriceInformationViewModel>>(service => () => service.GetRequiredService<SearchPriceInformationViewModel>());

                services.AddSingleton<IViewModelFactory,ViewModelFactory>();
            });
            return host;
        }
    }
}
