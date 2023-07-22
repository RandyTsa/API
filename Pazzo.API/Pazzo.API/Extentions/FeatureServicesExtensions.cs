using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pazzo.API.Extentions
{
    public static class FeatureServicesExtensions
    {
        public static IServiceCollection AddFeatureServices(this IServiceCollection services)
        {
            return services;
        }
    }
}