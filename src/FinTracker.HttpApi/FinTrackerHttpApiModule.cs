using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.HttpApi
{
    public class FinTrackerHttpApiModule
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddApplicationPart(typeof(FinTrackerHttpApiModule).Assembly);

            // Configure API specific services
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
