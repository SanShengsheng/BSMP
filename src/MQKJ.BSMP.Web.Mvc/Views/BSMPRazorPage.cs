using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;

namespace MQKJ.BSMP.Web.Views
{
    public abstract class BSMPRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected BSMPRazorPage()
        {
            LocalizationSourceName = BSMPConsts.LocalizationSourceName;
        }
    }
}
