using MQKJ.BSMP.ChineseBabies.BabyEvents.Dtos;
using MQKJ.BSMP.ChineseBabies.Dtos;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// BabyEvent应用层服务的接口方法
    ///</summary>
    public interface IBabyEventAppService :
        BsmpApplicationService<BabyEvent, int, BabyEventEditDto, BabyEventEditDto, GetBabyEventsInput, BabyEventListDto>
    {
        /// <summary>
        /// 获取学习事件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetStudyEventsOutput> GetStudyEvents(GetStudyEventsInput input);

        /// <summary>
        /// 获取成长事件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetGrowUpEventsOutput> GetGrowUpEvents(GetGrowUpEventsInput input);

        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<HandleEventOutput> HandleEvent(HandleEventInput input);

        Task<BabyEvent> ValidEventSubmit(HandleEventInput input);

        //Task<HandleEventOutput> AddOrUpdateBabyEventRecord(HandleEventInput input, BabyEvent babyEvent);

        Task<BabyGrowUpRecordOutput> BabyGrowUpRecord(BabyGrowUpRecordInput input);

        Task<BabyGoOnGrowUpOutput> BabyGoOnGrowUp(BabyGoOnGrowUpInput input);

        /// <summary>
        /// 获取双人事件与充值消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetCoupeEventAndRechargeMessageOutput> GetCoupeEventAndRechargeMessage(GetCoupeEventAndRechargeMessageInput input);

        Task<DeleteBabyEventRecordOutput> DeleteBabyEventRecord(DeleteBabyEventRecordInput input);
    }
}
