using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.EFCore;

namespace HRM.DbMigrator
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _lifeTime;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IHostApplicationLifetime lifeTime)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _lifeTime = lifeTime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetService<HRMDbContext>();
            context.Database.Migrate();
            _lifeTime.StopApplication();
        }
    }
}
