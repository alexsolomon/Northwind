using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Lib.Business;
using Northwind.Lib.Business.MapperProfiles;
using Northwind.Lib.Repository;
using Northwind.Lib.Repository.Models;

namespace Northwind.Lib.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static void ConfigureLibService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NorthwindDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Northwind"))
            );

            services.AddAutoMapper(typeof(OrderMapper));

            services.AddScoped<ICustomerBusiness, CustomerBusiness>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<IOrderBusiness, OrderBusiness>();
            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}