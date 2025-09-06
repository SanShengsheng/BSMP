using Abp.Application.Services.Dto;
using MQKJ.BSMP.LoveCard.LoveCards.Dtos;
using MQKJ.BSMP.LoveCardFiles.Dtos;
using MQKJ.BSMP.LoveCards.Dtos;
using MQKJ.BSMP.MiniappServices.LoveCard.Models;
using MQKJ.BSMP.MiniappServices.Models;
using MQKJ.BSMP.UnLocks.Dtos;
using MQKJ.BSMP.Utils.WechatPay.Dtos;
using System.Threading.Tasks;

namespace MQKJ.BSMP.MiniappServices.LoveCard
{
    public interface ILoveCardMiniappService
    {
        //GetOpenIdOutput GetOpenId(GetOpenIdInput input);

        Task<UpLoadLoveCardFileOutput> UploadFile(UploadLoveCardFileDto input);

        Task<SaveCardOutput> SaveCard(SaveCardInput input);
        Task<SaveCardOutput> SaveOtherInfo(SaveCardInput input);
        Task<SaveCardOutput> SaveOtherInfo2(SaveCardOtherInput input);
        /// <summary>
        /// 获取名片列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LoveCardListDto>> GetCardList(GetLoveCardsInput input);

        /// <summary>
        /// 创建编辑名片
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CreateOrUpdateLoveCardOutput> CreateOrUpdateLoveCard(CreateOrUpdateLoveCardInput input);

        /// <summary>
        /// 解锁微信号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<string> UnlockWeChatAccount(UnlockWeChatAccountInput input);

        Task<MiniProgramPayOutput> UnlockWeChatAccount(UnlockWeChatAccountInput input);


        /// <summary>
        /// 获取解锁结果
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetUnLockResultOutput> GetUnLockResult(GetUnLockResultInput input);

    }
}
