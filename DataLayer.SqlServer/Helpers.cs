using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SqlServer
{
    public static class Helpers
    {
        public static IServiceCollection RegisterDAOs(this IServiceCollection services) 
        {
            return services
                .AddScoped<IRegistryDao, RegistryDao>()
                .AddScoped<IVerbalDao, VerbalDao>()
                .AddScoped<IViolationDao, ViolationDao>()
                ;
        }
    }
}
