using Microsoft.AspNetCore.Hosting;
using DemoApp.Application;
using DemoApp.Infrastructure;

namespace MyApp.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiDI(this IServiceCollection services,WebApplicationBuilder builder)
        {
            services.AddInfrastructureDI(builder.Configuration);
            services.AddApplicationDI();
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
            return services;
        }
    }
}