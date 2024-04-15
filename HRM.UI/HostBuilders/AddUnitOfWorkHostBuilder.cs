using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using HRM.Core.UnitOfWorks;
using HRM.Core.Repositories;


namespace HRM.UI.HostBuilders
{
    public static class AddUnitOfWorkHostBuilder
    {
        public static IHostBuilder AddUnitOfWork(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddScoped<IUnitOfWork, UnitOfWork>();
                services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            });
            return host;
        }
    }
}
