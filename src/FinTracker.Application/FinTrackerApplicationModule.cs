using FinTracker.Application.Contracts;
using FinTracker.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrutor;

namespace FinTracker.Application
{
    public class FinTrackerApplicationModule
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            FinTrackerDomainModule.ConfigureServices(services);
            FinTrackerApplicationContractsModule.ConfigureServices(services);

            services.AddAutoMapper(typeof(FinTrackerApplicationModule));

            // Register application services
            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }
    }
}
