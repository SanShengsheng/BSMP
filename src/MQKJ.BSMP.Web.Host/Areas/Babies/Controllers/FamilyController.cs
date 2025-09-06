using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Babies;
using MQKJ.BSMP.ChineseBabies.Babies.Dtos.PrestigeDtos;
using MQKJ.BSMP.ChineseBabies.Families.Dtos;
using MQKJ.BSMP.ChineseBabies.Families.Dtos.HostDtos;
using MQKJ.BSMP.ChineseBabies.HostDtos.FamilyDto;
using MQKJ.BSMP.Filters;
using MQKJ.BSMP.Models;
using System;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Host.Areas.Babies.Controllers
{
    /// <summary>
    /// 家庭接口
    /// </summary>
    public class FamilyController : BabyBaseController
    {
        private readonly IFamilyAppService _familyAppService;
        private readonly IBabyPrestigeAppService _babyPrestigeAppService;

        public FamilyController(IFamilyAppService familyAppService, IBabyPrestigeAppService babyPrestigeAppService)
        {
            _familyAppService = familyAppService;
            _babyPrestigeAppService = babyPrestigeAppService;
        }
        /// <summary>
        /// 创建家庭，初始化家庭及宝宝
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("CreateFamily")]
        [ForbidRepeatActionFilter]
        public async Task<ApiResponseModel<CreateFamilyOutput>> CreateFamily([FromBody]CreateFamilyInput input) =>await this.ApiTaskFunc( _familyAppService.CreateFamily(input));

        /// <summary>
        /// 获取家庭信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetFamily")]
        [Obsolete]
        public async Task<ApiResponseModel<GetFamilyOutput>> GetFamily(GetFamilyInput input) => await this.ApiTaskFunc(_familyAppService.GetFamily(input));
        /// <summary>
        /// 获取家庭信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("v2/GetFamily")]
        public async Task<ApiResponseModel<GetFamilyOutput2>> GetFamily2(GetFamilyInput input) => await this.ApiTaskFunc(_familyAppService.GetFamily2(input));
        /// <summary>
        /// 生宝宝
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("BirthNewBaby")]
        [ForbidRepeatActionFilter]
        public async Task<ApiResponseModel<BirthNewBabyOutput>> BirthNewBaby([FromBody]BirthNewBabyInput input) => await this.ApiTaskFunc(_familyAppService.BirthNewBaby(input));

        /// <summary>
        /// 获取家庭状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetFamilyState")]
        public async Task<ApiResponseModel<GetFamilyStateOutput>> GetFamilyState(GetFamilyStateInput input) => await this.ApiTaskFunc(_familyAppService.GetFamilyState(input));

        /// <summary>
        /// 定时更新家庭档次
        /// </summary>
        [HttpPost("CrontabUpdateFamilyLevel")]
        public  Task<ApiResponseModel<CrontabUpdateFamilyLevelOutput>> CrontabUpdateFamilyLevel([FromBody]CrontabUpdateFamilyLevelInput input) => this.ApiTaskFunc(_familyAppService.CrontabUpdateFamilyLevel(input));

        /// <summary>
        ///  获取家庭其他宝宝列表（分页）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetFamilyElseBabiesByPage")]
        public Task<ApiResponseModel<GetFamilyElseBabiesByPageOutput>> GetFamilyElseBabiesByPage(GetFamilyElseBabiesByPageInput input) => this.ApiTaskFunc(_familyAppService.GetFamilyElseBabiesByPage(input));

        /// <summary>
        ///  获取父/母亲详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetParentDetail")]
        public Task<ApiResponseModel<GetParentDetailOutput>> GetParentDetail(GetParentDetailInput input) => this.ApiTaskFunc(_familyAppService.GetParentDetail(input));

        /// <summary>
        /// 获取对方家庭信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetOtherFamilyInfo")]
        public async Task<ApiResponseModel<GetOtherFamilyInfoOutput>> GetOtherFamilyInfo(GetOtherFamilyInfoInput input)
        {
            return await this.ApiTaskFunc(_familyAppService.GetOtherFamilyInfo(input)); 
        }

        /// <summary>
        /// 发起解散家庭
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("RequestDismissFamily")]
        public async Task<ApiResponseModel<RequestDismissFamilyOutput>> RequestDismissFamily([FromBody]RequestDismissFamilyInput input)
        {
            return await this.ApiTaskFunc(_familyAppService.RequestDismissFamily(input));
        }

        /// <summary>
        /// 同意解散家庭
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("AgreeDismissFamily")]
        public async Task<ApiResponseModel<ConfirmDismissFamilyOutput>> AgreeDismissFamily([FromBody]ConfirmDismissFamilyInput input)
        {
            return await this.ApiTaskFunc(_familyAppService.ConfirmDismissFamily(input));
        }

        /// <summary>
        /// 取消或者拒绝解散家庭
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("CanceAndRefuselDismissFamily")]
        public async Task<ApiResponseModel<CancelDismissFamilyOutput>> AgreeDismissFamily([FromBody]CancelDismissFamilyInput input)
        {
            return await this.ApiTaskFunc(_familyAppService.CanceAndRefuselDismissFamily(input));
        }
        /// <summary>
        /// 声望排行榜
        /// </summary>
        /// <param name="babyId"></param>
        /// <returns></returns>
        [HttpGet("RankPrestigesByBaby")]
        public async Task<ApiResponseModel<RankPrestigesOutput>> RankPrestigesByBaby(string babyId) => await this.ApiTaskFunc(_babyPrestigeAppService.RankPrestigesByBaby(babyId));
        /// <summary>
        /// 开始膜拜
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("GoToWorship")]
        public async Task<ApiResponseModel<GoToWorshipOutput>> GoToWorship([FromBody]GoToWorshipInput input) => await this.ApiTaskFunc(_babyPrestigeAppService.GoToWorship(input));
        ///// <summary>
        ///// 强制解散家庭的回调
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost("DismissFamilyNotify")]
        //[DontWrapResult]
        //public Task<string> DismissFamilyNotify()
        //{
        //    return _familyAppService.DismissFamilyPayNotify();
        //}
    }
}