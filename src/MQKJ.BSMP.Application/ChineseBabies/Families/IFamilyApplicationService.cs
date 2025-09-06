using Abp.Application.Services.Dto;
using MQKJ.BSMP.ChineseBabies.Babies.Dtos;
using MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.Families.Dtos;
using MQKJ.BSMP.ChineseBabies.Families.Dtos.HostDtos;
using MQKJ.BSMP.ChineseBabies.Families.Model;
using MQKJ.BSMP.ChineseBabies.HostDtos.FamilyDto;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// Family应用层服务的接口方法
    ///</summary>
    public interface IFamilyAppService : BsmpApplicationService<Family, int, FamilyEditDto, FamilyEditDto, GetFamilysInput, FamilyListDto>
    {

        /// <summary>
        /// 创建家庭
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CreateFamilyOutput> CreateFamily(CreateFamilyInput input);

        /// <summary>
        /// 获取家庭信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetFamilyOutput> GetFamily(GetFamilyInput input);
        /// <summary>
        /// 获取家庭信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetFamilyOutput2> GetFamily2(GetFamilyInput input);
        /// <summary>
        /// 生宝宝（比如二胎）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BirthNewBabyOutput> BirthNewBaby(BirthNewBabyInput input);


        Task<GetFamilyInfoOutput> GetFamilyInfo(GetFamilyInfoInput input);

        Task<GetFamilyStateOutput> GetFamilyState(GetFamilyStateInput input);

        /// <summary>
        /// 获取用户所有家庭
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetFamiliesWithPlayerIdOutput>> GetFamiliesWithPlayerId(GetFamiliesWithPlayerIdInput input);

        Task<CrontabUpdateFamilyLevelOutput> CrontabUpdateFamilyLevel(CrontabUpdateFamilyLevelInput input);
        Task<PagedResultDto<AgentFamilyOutput>> GetAgentFamilies(AgentFamilyInput input);
        Task UpdateAgentState(UpdateAgentStateInput input);


        /// <summary>
        /// 备注家庭
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<RemarkFamilyOutput> RemarkFamily(RemarkFamilyInput input);

        /// <summary>
        /// 获取所有家庭
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetAllFamilysListDto>> GetAllFamilys(GetAllFamilysInput input);

        /// <summary>
        /// 获取家庭宝宝列表（排除当前宝宝）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetFamilyElseBabiesByPageOutput> GetFamilyElseBabiesByPage(GetFamilyElseBabiesByPageInput input);

        /// <summary>
        /// 获取父亲/母亲
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetParentDetailOutput> GetParentDetail(GetParentDetailInput input);

        /// <summary>
        /// 获取别人家庭信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetOtherFamilyInfoOutput> GetOtherFamilyInfo(GetOtherFamilyInfoInput input);

        /// <summary>
        /// 发起解散家庭
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<RequestDismissFamilyOutput> RequestDismissFamily(RequestDismissFamilyInput input);

        /// <summary>
        /// 取消或者拒绝解散家庭
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CancelDismissFamilyOutput> CanceAndRefuselDismissFamily(CancelDismissFamilyInput input);

        /// <summary>
        /// 确认解散家庭
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ConfirmDismissFamilyOutput> ConfirmDismissFamily(ConfirmDismissFamilyInput input);

        /// <summary>
        /// 强制解散家庭成功后的处理的业务
        /// </summary>
        /// <returns></returns>
        Task ForceDismissFamilySuccess(UpdateOrderStateInput input);

        /// <summary>
        /// 解散家庭微信支付通知
        /// </summary>
        /// <returns></returns>
        //Task<string> DismissFamilyPayNotify();

        //Task GetForceDismissFamilyPayResult();

        string ExportFamiliesToExcel(GetAllFamilysInput input);
        /// <summary>
        /// 获取家庭基本信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
         Task<GetBabisFamilyInfoOutput> GetBasicFamilyInfo(GetBasicFamilyInput input);
    }
}
