using Microsoft.Extensions.DependencyInjection;
using Pazzo.Interface;
using Pazzo.Repository.Repositories;
using Pazzo.Service;
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
            services.AddScoped<IMemberService, MemberService>();

            services.AddScoped<IMemberRepository, MemberRepository>();
            return services;
        }
    }
}