using System.Configuration;
using System.Data;
using System.Windows;
using HRM.UI.ViewModels;
using HRM.UI.Views;
using Microsoft.Extensions.Hosting;
using HRM.UI.HostBuilders;
using Microsoft.Extensions.DependencyInjection;

namespace HRM.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        public App()
        {
            //var builder = new ConfigurationBuilder()
            // .SetBasePath(Directory.GetCurrentDirectory())
            // .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            //var services = new ServiceCollection();
            //services.RegisterUnitOfWork();
            //services.AddApplicationViews(builder.Build());
            //var serviceProvider = services.BuildServiceProvider();
            //IoC.Register(serviceProvider);

            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddViews()
                .AddViewModels()
                .AddUnitOfWork()
                .AddStores()
                .AddDbContext()
                .AddServices();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Window mainWd = _host.Services.GetRequiredService<MainWindow>();
            mainWd.DataContext = _host.Services.GetRequiredService<MainViewModel>();
            mainWd.Show();
        }
    }

}

