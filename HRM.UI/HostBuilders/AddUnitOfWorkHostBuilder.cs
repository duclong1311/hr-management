using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Core.Repositories;
using Terp.Core.UnitOfWorks;
using Terp.Domain.Models;

namespace Terp.UI.HostBuilders
{
    public static class AddUnitOfWorkHostBuilder
    {
        public static IHostBuilder AddUnitOfWork(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddScoped<IUnitOfWork , UnitOfWork>();
                services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            });
            return host;
        }
    }
}
