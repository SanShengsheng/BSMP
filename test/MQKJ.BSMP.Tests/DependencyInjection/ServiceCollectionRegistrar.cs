using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Castle.MicroKernel.Registration;
using Castle.Windsor.MsDependencyInjection;
using Abp.Dependency;
using MQKJ.BSMP.EntityFrameworkCore;
using MQKJ.BSMP.Identity;
using Microsoft.Extensions.Caching.Distributed;
using JCSoft.WX.Framework.Api;
using MQKJ.BSMP.MiniappServices;
using JCSoft.Core.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using MQKJ.BSMP.QCloud;

namespace MQKJ.BSMP.Tests.DependencyInjection
{
    public static class ServiceCollectionRegistrar
    {
        public static void Register(IIocManager iocManager)
        {
            var services = new ServiceCollection();

            IdentityRegistrar.Register(services);

            services.AddEntityFrameworkInMemoryDatabase();

            services.AddSingleton<IDistributedCache, MemoryDistributedCache>();

            services.AddSingleton<IApiClient,DefaultApiClient>();

            services.AddSingleton<IMiniappService, MiniappService>();

            services.AddSingleton<IHttpFactory, HttpFactory>();

            services.AddSingleton<IHostingEnvironment, HostingEnvironment>();

            services.AddScoped<IQCloudApiClient, QCloudApiClient>();

            services.AddWXFramework();
            services.AddHttpClientService();

            var serviceProvider = WindsorRegistrationHelper.CreateServiceProvider(iocManager.IocContainer, services);

            var builder = new DbContextOptionsBuilder<BSMPDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString()).UseInternalServiceProvider(serviceProvider);

            iocManager.IocContainer.Register(
                Component
                    .For<DbContextOptions<BSMPDbContext>>()
                    .Instance(builder.Options)
                    .LifestyleSingleton()
            );
        }
    }
}
