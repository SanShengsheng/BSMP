using System;
using Abp.MultiTenancy;
using MQKJ.BSMP.Authorization.Users;

namespace MQKJ.BSMP.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }

        public String WechatAppId { get; set; }
        public String WechatAppSecret { get; set; }
        public bool IsApplyWechat { get; set; }
    }
}
