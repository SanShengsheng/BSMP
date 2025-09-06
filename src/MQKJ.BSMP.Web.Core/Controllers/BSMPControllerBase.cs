using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace MQKJ.BSMP.Controllers
{
    public abstract class BSMPControllerBase: AbpController
    {
        protected BSMPControllerBase()
        {
            LocalizationSourceName = BSMPConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
