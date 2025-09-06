using Abp;
using Abp.Authorization.Users;
using Abp.Events.Bus;
using Abp.Events.Bus.Entities;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.TestBase;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.Authorization.Users;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.EntityFrameworkCore;
using MQKJ.BSMP.EntityFrameworkCore.Seed;
using MQKJ.BSMP.EntityFrameworkCore.Seed.BonusPoints;
using MQKJ.BSMP.EntityFrameworkCore.Seed.ChineseBaby.FamilyWorships;
using MQKJ.BSMP.EntityFrameworkCore.Seed.Host;
using MQKJ.BSMP.EntityFrameworkCore.Seed.Scenes;
using MQKJ.BSMP.EntityFrameworkCore.Seed.Tenants;
using MQKJ.BSMP.MultiTenancy;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Tests.ChineseBaby.TestData.ProfessionCosts;
using MQKJ.BSMP.Tests.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyEventBuilder = MQKJ.BSMP.Tests.Seed.BabyEventBuilder;
using EventGroupBabyEventBuilder = MQKJ.BSMP.Tests.Seed.EventGroupBabyEventBuilder;
using EventGroupBuilder = MQKJ.BSMP.Tests.Seed.EventGroupBuilder;
using OptionsBuilder = MQKJ.BSMP.Tests.Seed.OptionsBuilder;
using SensitiveWordBuilder = MQKJ.BSMP.Tests.Seed.SensitiveWordBuilder;

namespace MQKJ.BSMP.Tests
{
    public abstract class BSMPTestBase : AbpIntegratedTestBase<BSMPTestModule>
    {
        protected BSMPTestBase()
        {
            void NormalizeDbContext(BSMPDbContext context)
            {
                context.EntityChangeEventHelper = NullEntityChangeEventHelper.Instance;
                context.EventBus = NullEventBus.Instance;
                context.SuppressAutoSetTenantId = true;
            }

            // Seed initial data for host
            AbpSession.TenantId = null;
            UsingDbContext(context =>
            {
                NormalizeDbContext(context);
                new InitialHostDbBuilder(context).Create();
                new DefaultTenantBuilder(context).Create();
            });

            // Seed initial data for default tenant
            AbpSession.TenantId = 1;
            try
            {
                UsingDbContext(context =>
                {
                    NormalizeDbContext(context);
                    new TenantRoleAndUserBuilder(context, 1).Create();
                    //Custome code
                    new RewardConsumeBuilder(context).Create();
                    new ProfessionBuilder(context).Create();
                    new PlayerBuilder(context).Create();
                    new FamilyBuilder(context).Create();
                    new PlayerProfessionBuilder(context).Create();
                    new BabyBuilder(context).Create();
                    new EventGroupBuilder(context).Create();
                    new SensitiveWordBuilder(context).Create();
                    new BabyEventBuilder(context).Create();
                    new EventGroupBabyEventBuilder(context).Create();
                    new OptionsBuilder(context).Create();
                    new BabyEventRecordsBuilder(context).Create();
                    new ProfessionCostBuilder(context).Create();
                    new BabyEndingBuilder(context).Create();
                    new WorshipRewardsBuilder(context).Create();
                    // 道具 end
                    new BabyPropTypeBuilder(context).Create();
                    new BabyPropTermTypeBuilder(context).Create();
                    new BabyPropFeatureTypeBuilder(context).Create();
                    new BabyPropPropertyAwardBuilder(context).Create();
                    new BabyPropBuilder(context).Create();
                    new BabyPropFeatureBuilder(context).Create();
                    new BabyPropTermBuilder(context).Create();
                    new BabyPropPriceBuilder(context).Create();
                    new BabyPropBagBuilder(context).Create();
                    // 道具 end
                    //Custome code end
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
         

            LoginAsDefaultTenantAdmin();
        }

        #region UsingDbContext

        protected IDisposable UsingTenantId(int? tenantId)
        {
            var previousTenantId = AbpSession.TenantId;
            AbpSession.TenantId = tenantId;
            return new DisposeAction(() => AbpSession.TenantId = previousTenantId);
        }

        protected void UsingDbContext(Action<BSMPDbContext> action)
        {
            UsingDbContext(AbpSession.TenantId, action);
        }

        protected Task UsingDbContextAsync(Func<BSMPDbContext, Task> action)
        {
            return UsingDbContextAsync(AbpSession.TenantId, action);
        }

        protected T UsingDbContext<T>(Func<BSMPDbContext, T> func)
        {
            return UsingDbContext(AbpSession.TenantId, func);
        }

        protected Task<T> UsingDbContextAsync<T>(Func<BSMPDbContext, Task<T>> func)
        {
            return UsingDbContextAsync(AbpSession.TenantId, func);
        }

        protected void UsingDbContext(int? tenantId, Action<BSMPDbContext> action)
        {
            using (UsingTenantId(tenantId))
            {
                using (var context = LocalIocManager.Resolve<BSMPDbContext>())
                {
                    action(context);
                    context.SaveChanges();
                }
            }
        }

        protected async Task UsingDbContextAsync(int? tenantId, Func<BSMPDbContext, Task> action)
        {
            using (UsingTenantId(tenantId))
            {
                using (var context = LocalIocManager.Resolve<BSMPDbContext>())
                {
                    await action(context);
                    await context.SaveChangesAsync();
                }
            }
        }

        protected T UsingDbContext<T>(int? tenantId, Func<BSMPDbContext, T> func)
        {
            T result;

            using (UsingTenantId(tenantId))
            {
                using (var context = LocalIocManager.Resolve<BSMPDbContext>())
                {
                    result = func(context);
                    context.SaveChanges();
                }
            }

            return result;
        }

        protected async Task<T> UsingDbContextAsync<T>(int? tenantId, Func<BSMPDbContext, Task<T>> func)
        {
            T result;

            using (UsingTenantId(tenantId))
            {
                using (var context = LocalIocManager.Resolve<BSMPDbContext>())
                {
                    result = await func(context);
                    await context.SaveChangesAsync();
                }
            }

            return result;
        }

        #endregion

        #region Login

        protected void LoginAsHostAdmin()
        {
            LoginAsHost(AbpUserBase.AdminUserName);
        }

        protected void LoginAsDefaultTenantAdmin()
        {
            LoginAsTenant(AbpTenantBase.DefaultTenantName, AbpUserBase.AdminUserName);
        }

        protected void LoginAsHost(string userName)
        {
            AbpSession.TenantId = null;

            var user =
                UsingDbContext(
                    context =>
                        context.Users.FirstOrDefault(u => u.TenantId == AbpSession.TenantId && u.UserName == userName));
            if (user == null)
            {
                throw new Exception("There is no user: " + userName + " for host.");
            }

            AbpSession.UserId = user.Id;
        }

        protected void LoginAsTenant(string tenancyName, string userName)
        {
            var tenant = UsingDbContext(context => context.Tenants.FirstOrDefault(t => t.TenancyName == tenancyName));
            if (tenant == null)
            {
                throw new Exception("There is no tenant: " + tenancyName);
            }

            AbpSession.TenantId = tenant.Id;

            var user =
                UsingDbContext(
                    context =>
                        context.Users.FirstOrDefault(u => u.TenantId == AbpSession.TenantId && u.UserName == userName));
            if (user == null)
            {
                throw new Exception("There is no user: " + userName + " for tenant: " + tenancyName);
            }

            AbpSession.UserId = user.Id;
        }

        #endregion

        /// <summary>
        /// Gets current user if <see cref="IAbpSession.UserId"/> is not null.
        /// Throws exception if it's null.
        /// </summary>
        protected async Task<User> GetCurrentUserAsync()
        {
            var userId = AbpSession.GetUserId();
            return await UsingDbContext(context => context.Users.SingleAsync(u => u.Id == userId));
        }

        /// <summary>
        /// Gets current tenant if <see cref="IAbpSession.TenantId"/> is not null.
        /// Throws exception if there is no current tenant.
        /// </summary>
        protected async Task<Tenant> GetCurrentTenantAsync()
        {
            var tenantId = AbpSession.GetTenantId();
            return await UsingDbContext(context => context.Tenants.SingleAsync(t => t.Id == tenantId));
        }

        #region custom code
        /// <summary>
        /// 获取所有玩家
        /// </summary>
        /// <returns></returns>
        public  async Task<ICollection<Player>> GetAllPlayers()
        {
            return  await UsingDbContext(context => context.Players.ToListAsync());
        }

        public async Task<ICollection<ChineseBabies.Family>> GetAllFamilies()
        {
            return await UsingDbContext(context => context.Families.Include(s=>s.Babies).ToListAsync());
        }

        public async Task<ICollection<ChineseBabies.Baby>> GetAllBabies()
        {
            return await UsingDbContext(context => context.Babies.Include(s=>s.Family).ThenInclude(s=>s.Father).Include(s=>s.Family).ThenInclude(s=>s.Mother).ToListAsync());
        }

        public async Task<ICollection<ChineseBabies.Profession>> GetAllProfessions()
        {
            return await UsingDbContext(context => context.Professions.ToListAsync());
        }
        public async Task<ICollection<ChineseBabies.PlayerProfession>> GetAllPlayerProfessions()
        {
            return await UsingDbContext(context => context.PlayerProfessions
            .Include(x => x.Family).ThenInclude(x => x.Father)
            .Include(x => x.Family).ThenInclude(x => x.Mother)
            .Include(c => c.Profession).ThenInclude(c => c.Costs).ToListAsync());
        }
        #endregion
    }
}
