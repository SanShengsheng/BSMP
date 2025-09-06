using Abp.AspNetCore.Mvc.ViewComponents;

namespace MQKJ.BSMP.Web.Views
{
    public abstract class BSMPViewComponent : AbpViewComponent
    {
        protected BSMPViewComponent()
        {
            LocalizationSourceName = BSMPConsts.LocalizationSourceName;
        }
    }
}
