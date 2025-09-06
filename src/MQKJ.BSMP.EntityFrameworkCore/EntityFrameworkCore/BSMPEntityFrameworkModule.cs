using Abp.Dapper;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using MQKJ.BSMP.EntityFrameworkCore.Seed;
using System.Collections.Generic;
using System.Reflection;
using DapperExtensions;
namespace MQKJ.BSMP.EntityFrameworkCore
{
    [DependsOn(
        typeof(BSMPCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule),
        typeof(AbpDapperModule))]
    public class BSMPEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<BSMPDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        BSMPDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        BSMPDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BSMPEntityFrameworkModule).GetAssembly());
            DapperExtensions.DapperExtensions.SetMappingAssemblies(new List<Assembly> { typeof(BSMPEntityFrameworkModule).GetAssembly() });
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
