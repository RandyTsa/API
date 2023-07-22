using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pazzo.API.Extentions;
using Pazzo.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pazzo.UnitTest
{
    [TestClass]
    public class BaseTest
    {
        public ServiceProvider provider;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            var config = Configuration();
            var services = new ServiceCollection();
            services.AddFeatureServices();
            services.AddDbContext<PazzoContext>(options => options.UseSqlServer("data source=(localdb)\\MSSQLLocalDB;initial catalog=pazzo;integrated security=True;MultipleActiveResultSets=True"));

            services.AddSingleton(config);

            this.provider = services.BuildServiceProvider();
        }

        public static IConfiguration Configuration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(Path.GetFullPath("appsettings.json"))
                .Build();
            return config;
        }
    }
}