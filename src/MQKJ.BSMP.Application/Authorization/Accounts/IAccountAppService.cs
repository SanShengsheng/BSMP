using System.Threading.Tasks;
using Abp.Application.Services;
using MQKJ.BSMP.Authorization.Accounts.Dto;

namespace MQKJ.BSMP.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
