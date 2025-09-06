

using MQKJ.BSMP.ChineseBabies.Babies.Dtos;
using MQKJ.BSMP.ChineseBabies.Babies.Dtos.HostDtos;
using MQKJ.BSMP.ChineseBabies.Dtos;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// Baby应用层服务的接口方法
    ///</summary>
    public interface IBabyAppService : BsmpApplicationService<Baby, int, BabyEditDto, BabyEditDto, GetBabysInput, BabyListDto>
    {
        /// <summary>
        /// 宝宝列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetBabiesOutput> GetBabies(GetBabiesInput input);
        /// <summary>
        /// 宝宝列表（分页）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetBabiesByPageOutput> GetBabiesByPage(GetBabiesInput input);

        /// <summary>
        /// 获取宝宝基本信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetBabyBasicInfoOutput> GetBabyBasicInfo(GetBabyBasicInfoInput input);
        /// <summary>
        /// 取名
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GiveBabyNameOutput> GiveBabyName(GiveBabyNameInput input);
        /// <summary>
        /// 宝宝成长
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BabyGrowUpOutput> BabyGrowUp(BabyGrowUpInput input);
        /// <summary>
        /// 宝宝排名
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BabyRankListOutput> BabyRankList(BabyRankListInput input);

        /// <summary>
        /// 获取宝宝排行
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BabyRankingList_V2Output> BabyRankList_V2(BabyRankingList_V2Input input);
        /// <summary>
        /// 宝宝出生档案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BabyBirthRecordOutput> BabyBirthRecord(BabyBirthRecordInput input);

        /// <summary>
        /// 查看出生动画
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LookBirthMovieOutput> LookBirthMovie(LookBirthMovieInput input);
        /// <summary>
        /// 已成人宝宝信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
         Task<GetAudltBabyInfoOutput> GetAudltBabyInfo(GetAudltBabyInfoInput input);

        /// <summary>
        /// 获取第一个宝宝信息(测试用)
        /// </summary>
        /// <returns></returns>
        Task<Baby> GetFirstBaby();

        /// <summary>
        /// 获取资产排行
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetAssetRankingListOutput> GetAssetRankingList(GetAssetRankingListInput input);
        /// <summary>
        /// 重新计算宝宝属性
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ReCaclBabyPropertyOutput> ReCaclBabyProperty(ReCaclBabyPropertyInput input);
        /// <summary>
        /// 修复宝宝六维错误
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<FixBabyPropertyErrorOutput> FixBabyPropertyError(FixBabyPropertyErrorInput input);
    }
}
