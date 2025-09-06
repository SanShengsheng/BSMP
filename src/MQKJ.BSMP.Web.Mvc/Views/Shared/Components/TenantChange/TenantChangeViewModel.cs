using Abp.AutoMapper;
using MQKJ.BSMP.Sessions.Dto;

namespace MQKJ.BSMP.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}
