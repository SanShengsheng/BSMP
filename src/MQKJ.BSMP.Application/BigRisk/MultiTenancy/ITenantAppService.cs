using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.MultiTenancy.Dto;

namespace MQKJ.BSMP.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
