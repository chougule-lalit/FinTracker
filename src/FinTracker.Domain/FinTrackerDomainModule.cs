using FinTracker.Domain.Shared;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Domain
{
    public class FinTrackerDomainModule
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            FinTrackerDomainSharedModule.ConfigureServices(services);

            // Configure domain services
            services.AddScoped<ICurrentUser, CurrentUser>();
        }
    }
}
