using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Terp.UI.States;
using Terp.UI.States.Users;
using Terp.UI.Stores;

namespace Terp.UI.HostBuilders
{
    public static  class AddStoresHostBuilder
    {
        public static IHostBuilder AddStores (this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<MainContentStore>();
                services.AddSingleton<IAuthenticator,Authenticator>();
                services.AddSingleton<IUserStore,UserStore>();
            });
            return host;
        }
    }
}
