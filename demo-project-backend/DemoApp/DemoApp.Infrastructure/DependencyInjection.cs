using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Application.Common.Interfaces;
using DemoApp.Infrastructure.Common;
using DemoApp.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DemoApp.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using DemoApp.Application.Common.Interfaces.Repositories;
using DemoApp.Infrastructure.Salaries;

namespace DemoApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));

            services.AddSingleton<IDbConnectionFactory, SqlConnectionFactory>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ISalaryRepository,SalaryRepository>();

            return services;
        }
    }
}
