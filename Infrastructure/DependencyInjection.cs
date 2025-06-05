using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Infrastructure.Repositories;
using Domain.Entities;
using Application.Repositories;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPooledDbContextFactory<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Database")));

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            return services;
        }

    }
}
