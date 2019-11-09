using ProductManagement.Data.Context;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Business.Interfaces;
using ProductManagement.Data.Repository;
using ProductManagement.Business.Services;

namespace ProductManagement.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ProductManagementDbContext>();
            services.AddScoped<IProductRepository, ProductRepository> ();

            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
