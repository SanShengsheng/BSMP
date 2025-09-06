using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.PropMall;
using MQKJ.BSMP.Filters;
using MQKJ.BSMP.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MQKJ.BSMP.Web.Host.Areas.Babies.Controllers
{
    /// <summary>
    /// 道具商城
    /// </summary>
    public class PropBagController : BabyBaseController
    {
        private readonly IBabyPropBagAppService _babyPropBagAppService; 

        public PropBagController(BabyPropBagAppService babyPropBagAppService)
        {
            _babyPropBagAppService = babyPropBagAppService;
        }

        /// <summary>
        ///  获取最新的大礼包
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetPropBagLastest")]
        public async Task<ApiResponseModel<GetPropBagLastestOutput>> GetPropBagLastest(GetPropBagLastestInput input) => await this.ApiTaskFunc(_babyPropBagAppService.GetPropBagLastest(input));

        /// <summary>
        ///  购买大礼包
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("PostBuyPropBag")]
        [ForbidRepeatActionFilter]
        public async Task<ApiResponseModel<PostBuyPropBagOutput>> PostBuyPropBag([FromBody]PostBuyPropBagInput input) => await this.ApiTaskFunc(_babyPropBagAppService.PostBuyPropBag(input));

        //[HttpGet("GetBoughtPropBagPayResult")]
        //public async Task<ApiResponseModel<UpdateOrderStateOutput>> GetBoughtPropBagPayResult(UpdateOrderStateInput input)
        //{
        //    return await this.ApiTaskFunc(_babyPropBagAppService.GetBoughtBigBagPayResult(input));
        //}

        ///// <summary>
        ///// 购买大礼包回调
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost("BoughtPropBagNotify")]
        //public async Task<ApiResponseModel<string>> BoughtPropBagNotify()
        //{
        //    Logger.Debug("购买大礼包回调");
        //    return await this.ApiTaskFunc(_babyPropBagAppService.BoughtBigBagPayNotify());
        //}
    }
}
