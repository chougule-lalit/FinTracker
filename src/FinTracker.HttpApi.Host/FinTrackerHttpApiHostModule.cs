using FinTracker.Application;
using FinTracker.Domain;
using FinTracker.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace FinTracker.HttpApi.Host
{
    public static class FinTrackerHttpApiHostModule
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Configure all modules
            FinTrackerDomainModule.ConfigureServices(services);
            FinTrackerApplicationModule.ConfigureServices(services);
            FinTrackerHttpApiModule.ConfigureServices(services);

            var connectionString = configuration.GetConnectionString("Default");
            FinTrackerEntityFrameworkCoreModule.ConfigureServices(services, connectionString!);

            // Configure CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            // Configure Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FinTracker API",
                    Version = "v1",
                    Description = "FinTracker API Documentation"
                });

                // Add JWT Authentication to Swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Add Authentication & Authorization
            services.AddAuthentication();
            services.AddAuthorization();

            // Add HttpContextAccessor
            services.AddHttpContextAccessor();
        }

        public static void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinTracker API V1");
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            // Replace UseEndpoints with direct MapControllers call
            app.MapControllers();
            app.MapSwagger();
        }
    }
}
