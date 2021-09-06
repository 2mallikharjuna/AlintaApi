using System;
using System.IO;
using System.Reflection;
using AlintaApi.Repositories;
using AlintaApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AlintaApi.Extensions
{
    /// <summary>
    /// Dependency injector of all services
    /// </summary>
    public static class DependencyInjector
    {
        /// <summary>
        /// Extension method to add the dependencies
        /// </summary>
        /// <param name="services">Base services</param>
        /// <param name="configuration">Base configuration</param>
        /// <returns></returns>
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<IConfiguration>(configuration);
            services.AddSingleton(configuration);
            services.InjectDependencies(configuration);
            return services;
        }
        
        /// <summary>
        /// Create a dependency injection
        /// </summary>
        /// <param name="services">Base services</param>
        /// <param name="configuration"></param>
        public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();           
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            //sql connection
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(configuration["ConnectionStrings:Customers"]));

            // Register the Swagger generator, defining 1 or more Swagger documents
            // Enable middleware to serve swagger - ui(HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Alinta Api", Version = "v1" });

                // Get/Create xml comments path
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlFile);

                // Set xml path
                options.IncludeXmlComments(xmlPath);
            });
        }
    }
}
