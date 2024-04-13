using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.UI.Services;
using Terp.UI.Services.LogService;

namespace Terp.UI.HostBuilders
{
    public static class AddServicesHostBuidler
    {
        public static IHostBuilder AddServices(this IHostBuilder host) 
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IAuthenticationSevice,AuthenticationService>();
                services.AddSingleton<IExchangeRateCurrencyService,ExchangeRateCurrencyService>();
                services.AddSingleton<ILogger,Logger>();
            });
            return host;
        }
    }
}
