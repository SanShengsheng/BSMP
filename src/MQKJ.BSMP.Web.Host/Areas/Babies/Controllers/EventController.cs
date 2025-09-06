using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.BabyEvents.Dtos;
using MQKJ.BSMP.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Host.Areas.Babies.Controllers
{
    public class EventController : BabyBaseController
    {
        private readonly IBabyEventAppService _appService;

        public EventController(IBabyEventAppService appService)
        {
            _appService = appService;
        }


        /// <summary>
        /// 获取成长事件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetGrowUpEvents")]
        public async Task<ApiResponseModel<GetGrowUpEventsOutput>> GetGrowUpEvents(GetGrowUpEventsInput input)
        {
          
            var output = await this.ApiTaskFunc(_appService.GetGrowUpEvents(input));
        

            return output;
        }

        /// <summary>
        /// 获取学习事件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetStudyEvents")]
        public async Task<ApiResponseModel<GetStudyEventsOutput>> GetStudyEvents(GetStudyEventsInput input) => await this.ApiTaskFunc(_appService.GetStudyEvents(input));

        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("HandleEvent")]
        public async Task<ApiResponseModel<HandleEventOutput>> HandleEvent([FromBody]HandleEventInput input) => await this.ApiTaskFunc(_appService.HandleEvent(input));


        /// <summary>
        /// 宝宝成长记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("BabyGrowUpRecord")]
        public async Task<ApiResponseModel<BabyGrowUpRecordOutput>> BabyGrowUpRecord(BabyGrowUpRecordInput input) => await this.ApiTaskFunc(_appService.BabyGrowUpRecord(input));



        /// <summary>
        /// 宝宝继续成长
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("BabyGoOnGrowUp")]
        public async Task<ApiResponseModel<BabyGoOnGrowUpOutput>> BabyGoOnGrowUp([FromBody]BabyGoOnGrowUpInput input) => await this.ApiTaskFunc(_appService.BabyGoOnGrowUp(input));


        /// <summary>
        /// 获取双人事件与充值消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetCoupeEventAndRechargeMessage")]
        public async Task<ApiResponseModel<GetCoupeEventAndRechargeMessageOutput>> GetCoupeEventAndRechargeMessage(GetCoupeEventAndRechargeMessageInput input) => await this.ApiTaskFunc(_appService.GetCoupeEventAndRechargeMessage(input));


        /// <summary>
        /// 删除宝宝记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete("DeleteBabyEventRecord")]
        public async Task<ApiResponseModel<DeleteBabyEventRecordOutput>> DeleteBabyEventRecord([FromBody]DeleteBabyEventRecordInput input) => await this.ApiTaskFunc(_appService.DeleteBabyEventRecord(input));

    }
}