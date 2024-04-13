// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HRM.DbMigrator;
using HRM.EFCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<HRMDbContext>(options =>
                options.UseSqlServer(context.Configuration.GetConnectionString("Default")));
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();

