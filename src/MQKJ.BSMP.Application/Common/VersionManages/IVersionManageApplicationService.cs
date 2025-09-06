using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Common.Dtos;
using MQKJ.BSMP.Common.VersionManages.Dtos;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Common
{
    /// <summary>
    /// VersionManage应用层服务的接口方法
    ///</summary>
    public interface IVersionManageAppService : BsmpApplicationService<VersionManage, int, VersionManageEditDto, VersionManageEditDto, GetVersionManagesInput, VersionManageListDto>
    {
        /// <summary>
        /// 获取最新的版本信息
        /// </summary>
        /// <returns></returns>
        Task<VersionManageEditDto> GetLastestVersion();

    }
}
