using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Terp.EFCore;
using Terp.UI.Stores;
using Terp.UI.ViewModels;
using Terp.UI.Views;

namespace Terp.UI.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplicationViews(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContext, TerpDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });
            
            services.AddScoped<CurrencyView>();
            
            services.AddScoped<MainWindow>();
            services.AddScoped<MainViewModel>();
            services.AddScoped<LoginView>();
            services.AddScoped<LoginViewModel>();
            services.AddScoped<CurrencyViewModel>();
            services.AddSingleton<NavigationStore>();
            services.AddSingleton<MainContentStore>();
            services.AddScoped<RegisterView>();
            services.AddScoped<RegisterViewModel>();
            services.AddScoped<MainContentView>();
            services.AddScoped<MainContentViewModel>();
            services.AddScoped<UnitViewModel>();
            services.AddScoped<UnitView>();
        }
    }
}
