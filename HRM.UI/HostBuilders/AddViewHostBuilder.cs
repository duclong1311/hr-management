using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Terp.UI.Views;

namespace Terp.UI.HostBuilders
{
    public static class AddViewHostBuilder
    {
        public static IHostBuilder AddViews(this IHostBuilder host) 
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<MainWindow>();
            });
            return host;
        }
    }
}
