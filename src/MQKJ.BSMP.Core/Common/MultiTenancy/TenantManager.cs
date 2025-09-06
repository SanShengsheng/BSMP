using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using MQKJ.BSMP.Authorization.Users;
using MQKJ.BSMP.Editions;

namespace MQKJ.BSMP.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }
    }
}
