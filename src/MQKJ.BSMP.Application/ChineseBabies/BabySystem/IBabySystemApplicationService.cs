

using Abp.Application.Services;
using MQKJ.BSMP.ChineseBabies.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using MQKJ.BSMP.ChineseBabies.BabySystem.Dtos.HostDtos;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// Reward应用层服务的接口方法
    ///</summary>
    public interface IBabySystemAppService : IApplicationService
    {
        ImportDataOutput ImportData(ImportDataInput input);

        Task<ChineseBabyRankOutput> Rank(ChineseBabyRankInput input);
        Task<IList<ImportDataTaskOutput>> TaskListAsync();
        /// <summary>
        /// 获取小程序列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IList<GetMiniProgramsOutput>> GetMiniPrograms(GetMiniProgramsInput input);
    }
}
