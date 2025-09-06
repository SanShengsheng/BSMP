using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.MultiTenancy;
using MQKJ.BSMP.Editions;
using MQKJ.BSMP.MultiTenancy;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.Tenants
{
    public class DefaultTenantBuilder
    {
        private readonly BSMPDbContext _context;

        public DefaultTenantBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateDefaultTenant();
        }

        private void CreateDefaultTenant()
        {
            // Default tenant

            var defaultTenant = _context.Tenants.IgnoreQueryFilters().FirstOrDefault(t => t.TenancyName == AbpTenantBase.DefaultTenantName);
            if (defaultTenant == null)
            {
                defaultTenant = new Tenant(AbpTenantBase.DefaultTenantName, AbpTenantBase.DefaultTenantName);

                var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
                if (defaultEdition != null)
                {
                    defaultTenant.EditionId = defaultEdition.Id;
                }

                _context.Tenants.Add(defaultTenant);
                _context.SaveChanges();
            }
            //关系进化
            var relationEvolution = _context.Tenants.IgnoreQueryFilters().FirstOrDefault(t => t.TenancyName == "relationEvolution");
            if (relationEvolution == null)
            {
                relationEvolution = new Tenant("relationEvolution", "关系进化") { WechatAppId = "wx105ed7745f7ebc37", WechatAppSecret = "6746421ecb4d5cdf7c39ac3cf8ee4f7d", IsApplyWechat = true };

                var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == "关系进化");
                if (defaultEdition != null)
                {
                    defaultTenant.EditionId = defaultEdition.Id;
                }

                _context.Tenants.Add(relationEvolution);
                _context.SaveChanges();
            }
            //爬梯
            var partyTenant = _context.Tenants.IgnoreQueryFilters().FirstOrDefault(t => t.TenancyName =="party" );
            if (partyTenant == null)
            {
                partyTenant = new Tenant("party", "爬梯") { WechatAppId = "wx661921458a160424", WechatAppSecret = "25669de1597fa2e7e3b4ad8f587a4962", IsApplyWechat = true };

                var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
                if (defaultEdition != null)
                {
                    defaultTenant.EditionId = defaultEdition.Id;
                }

                _context.Tenants.Add(partyTenant);
                _context.SaveChanges();
            }
            //恋习大冒险
            var adventureTenant = _context.Tenants.IgnoreQueryFilters().FirstOrDefault(t => t.TenancyName == "adventure");
            if (adventureTenant == null)
            {
                adventureTenant = new Tenant("adventure", "恋习大冒险") { WechatAppId= "wxdb7e28315b6f3304", WechatAppSecret= "76fa54075b28bb9d62adfd3733fbaf19" ,IsApplyWechat=true};

                var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
                if (defaultEdition != null)
                {
                    defaultTenant.EditionId = defaultEdition.Id;
                }

                _context.Tenants.Add(adventureTenant);
                _context.SaveChanges();
            }

            //恋爱名片
            var loveCardTenant = _context.Tenants.IgnoreQueryFilters().FirstOrDefault(t => t.TenancyName == "loveCard");
            if (loveCardTenant == null)
            {
                loveCardTenant = new Tenant("loveCard", "恋爱名片") { WechatAppId = "wxda043bcc7266c732", WechatAppSecret = "56bf4dd2514fa5f31fcc0cf73ba8b4d7", IsApplyWechat = true };

                var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
                if (defaultEdition != null)
                {
                    defaultTenant.EditionId = defaultEdition.Id;
                }

                _context.Tenants.Add(loveCardTenant);
                _context.SaveChanges();
            }

            //中国式养娃
            var chineseBabyTenant = _context.Tenants.IgnoreQueryFilters().FirstOrDefault(t => t.TenancyName == "chineseBaby");
            if (chineseBabyTenant == null)
            {
                chineseBabyTenant = new Tenant("chineseBaby", "中国式养娃") { WechatAppId = "wx7db4778518a1e650", WechatAppSecret = "419533c510af02c4d2b618e7b8cd7088", IsApplyWechat = true };

                var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
                if (defaultEdition != null)
                {
                    defaultTenant.EditionId = defaultEdition.Id;
                }

                _context.Tenants.Add(chineseBabyTenant);
                _context.SaveChanges();
            }

            //疯狂懂你 公众号
            var creazyUnderstandYouPub = _context.Tenants.IgnoreQueryFilters().FirstOrDefault(t => t.TenancyName == "creazyUnderstandYou");
            if (creazyUnderstandYouPub == null)
            {
                creazyUnderstandYouPub = new Tenant("creazyUnderstandYou", "疯狂懂你公众号") { WechatAppId = "wx4d6619e0a3b7f3bc", WechatAppSecret = "cf63b0d69856dc0ce227ad9f4730090a", IsApplyWechat = true };

                var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
                if (defaultEdition != null)
                {
                    defaultTenant.EditionId = defaultEdition.Id;
                }

                _context.Tenants.Add(creazyUnderstandYouPub);
                _context.SaveChanges();
            }
        }
    }
}
