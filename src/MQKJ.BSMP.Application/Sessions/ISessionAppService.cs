using System.Threading.Tasks;
using Abp.Application.Services;
using MQKJ.BSMP.Sessions.Dto;

namespace MQKJ.BSMP.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
