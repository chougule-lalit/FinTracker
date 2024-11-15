using FinTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrutor;

namespace FinTracker.EntityFrameworkCore
{
    public class FinTrackerEntityFrameworkCoreModule
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            FinTrackerDomainModule.ConfigureServices(services);

            //services.AddDbContext<FinTrackerDbContext>(options =>
            //    options.UseNpgsql(connectionString));

            // Register repositories
            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }
    }
}
