using HRM.UI.States.Authenticator;
using Microsoft.Extensions.DependencyInjection;
using HRM.UI.States.Users;
using HRM.UI.Stores;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.UI.HostBuilders
{
    public static class AddStoresHostBuilder
    {
        public static IHostBuilder AddStores(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<MainContentStore>();
                services.AddSingleton<ChildContentStore>();
                services.AddSingleton<IAuthenticator, Authenticator>();
                services.AddSingleton<IUserStore, UserStore>();
            });
            return host;
        }
    }
}
