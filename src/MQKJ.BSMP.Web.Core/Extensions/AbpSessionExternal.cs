using Abp.Dependency;
using Abp.Runtime.Session;
using MQKJ.BSMP.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQKJ.BSMP.Extensions
{
    public static class AbpSessionExternal
    {
        public static string GetClaimValue(string claimType)
        {
            var princpalAccessor = IocManager.Instance.Resolve<IPrincipalAccessor>();

            var claim = princpalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == claimType);

            if (string.IsNullOrEmpty(claim?.Value))
            {
                return null;
            }

            return claim.Value;
        }
    }
}
