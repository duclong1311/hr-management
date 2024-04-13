using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.EFCore;

namespace Terp.UI.HostBuilders
{
    public static class AddDbContextHostBuilder
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host) 
        {
            host.ConfigureServices((context,services) =>
            {
                services.AddDbContext<DbContext,TerpDbContext>(option =>
                {
                    option.UseSqlServer(context.Configuration.GetConnectionString("Default"));
                });
            });
            return host;
        }
    }
}
