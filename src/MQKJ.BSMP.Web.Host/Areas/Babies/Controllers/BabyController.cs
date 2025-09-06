using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Babies.Dtos;
using MQKJ.BSMP.ChineseBabies.Babies.Dtos.HostDtos;
using MQKJ.BSMP.Models;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Host.Areas.Babies.Controllers
{
    /// <summary>
    /// 宝宝接口
    /// </summary>
    public class BabyController : BabyBaseController
    {
        private readonly IBabyAppService _babyAppService;

        public BabyController(IBabyAppService babyAppService)
        {
            _babyAppService = babyAppService;
        }
        /// <summary>
        /// 获取宝宝
        ///  https://lanhuapp.com/web/#/item/board/proto/edit/entry?pid=38cbffb8-30a0-474e-b59c-6b06330d6e0f&imgId=dc742d18-6d8a-40bc-bd7b-4ed8c2237b4b&from=img
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBabies")]
        public async Task<ApiResponseModel<GetBabiesOutput>> GetBabies(GetBabiesInput input) => await this.ApiTaskFunc(_babyAppService.GetBabies(input));
        /// <summary>
        /// 获取宝宝（分页）
        ///  https://lanhuapp.com/web/#/item/board/proto/edit/entry?pid=38cbffb8-30a0-474e-b59c-6b06330d6e0f&imgId=dc742d18-6d8a-40bc-bd7b-4ed8c2237b4b&from=img
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBabiesByPage")]
        public async Task<ApiResponseModel<GetBabiesByPageOutput>> GetBabiesByPage(GetBabiesInput input) => await this.ApiTaskFunc(_babyAppService.GetBabiesByPage(input));
        /// <summary>
        /// 获取宝宝基本信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetBabyBasicInfo")]
        public async Task<ApiResponseModel<GetBabyBasicInfoOutput>> GetBabyBasicInfo(GetBabyBasicInfoInput input) => await this.ApiTaskFunc(_babyAppService.GetBabyBasicInfo(input));

        /// <summary>
        /// 取名
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("GiveBabyName")]
        public async Task<ApiResponseModel<GiveBabyNameOutput>> GiveBabyName([FromBody]GiveBabyNameInput input) => await this.ApiTaskFunc(_babyAppService.GiveBabyName(input));

        /// <summary>
        /// 宝宝成长（属性改变）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("BabyGrowUp")]
        public async Task<ApiResponseModel<BabyGrowUpOutput>> BabyGrowUp([FromBody]BabyGrowUpInput input) => await this.ApiTaskFunc(_babyAppService.BabyGrowUp(input));

        /// <summary>
        /// 宝宝排行榜
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("BabyRankList")]
        public async Task<ApiResponseModel<BabyRankListOutput>> BabyRankList(BabyRankListInput input) => await this.ApiTaskFunc(_babyAppService.BabyRankList(input));



        /// <summary>
        /// 家庭资产排行榜
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetAssetRankingList")]
        public async Task<ApiResponseModel<GetAssetRankingListOutput>> GetAssetRankingList(GetAssetRankingListInput input) => await this.ApiTaskFunc(_babyAppService.GetAssetRankingList(input));

        /// <summary>
        /// 获取宝宝排名新版
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("BabyRankList/V2")]
        public async Task<ApiResponseModel<BabyRankingList_V2Output>> BabyRankList_V2(BabyRankingList_V2Input input) => await this.ApiTaskFunc(_babyAppService.BabyRankList_V2(input));


        /// <summary>
        /// 宝宝出生档案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("BabyBirthRecord")]
        public async Task<ApiResponseModel<BabyBirthRecordOutput>> BabyBirthRecord(BabyBirthRecordInput input) => await this.ApiTaskFunc(_babyAppService.BabyBirthRecord(input));

        /// <summary>
        ///  查看宝宝出生动画
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("LookBirthMovie")]
        public async Task<ApiResponseModel<LookBirthMovieOutput>> LookBirthMovie([FromBody]LookBirthMovieInput input) => await this.ApiTaskFunc(_babyAppService.LookBirthMovie(input));
        /// <summary>
        /// 查看已成人宝宝信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetAudltBabyInfo")]
        public async Task<ApiResponseModel<GetAudltBabyInfoOutput>> GetAudltBabyInfo(GetAudltBabyInfoInput input) => await this.ApiTaskFunc(_babyAppService.GetAudltBabyInfo(input));
   
    }
}