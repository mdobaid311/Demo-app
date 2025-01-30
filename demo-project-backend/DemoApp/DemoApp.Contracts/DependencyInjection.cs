using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Contracts
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddContractsDI(this IServiceCollection services)
        {
            return services;
        }
    }
}
