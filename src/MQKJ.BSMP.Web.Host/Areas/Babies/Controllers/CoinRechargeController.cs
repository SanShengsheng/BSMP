using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Web.Models;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.Models;

namespace MQKJ.BSMP.Web.Host.Areas.Babies.Controllers
{
    public class CoinRechargeController : BabyBaseController
    {

        private readonly ICoinRechargeAppService _coinRechargeAppService;

        public CoinRechargeController(ICoinRechargeAppService coinRechargeAppService)
        {
            _coinRechargeAppService = coinRechargeAppService;
        }

        /// <summary>
        /// 获取金币充值数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetCoinRecharges")]
        public async Task<ApiResponseModel<PagedResultDto<CoinRechargeListDto>>> GetCoinRecharges(GetCoinRechargesInput input)
        {
            return await this.ApiTaskFunc(_coinRechargeAppService.GetCoinRecharges(input));
        }

        [HttpGet("GetVirtualCoins")]
        public Task<ApiResponseModel<PagedResultDto<CoinRechargeListDto>>> GetVirtualCoins(GetCoinRechargesInput input)
        {
            return this.ApiTaskFunc(_coinRechargeAppService.GetVirtualCoins(input));
        }

        /// <summary>
        /// 购买金币
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("BuyCoins")]
        public async Task<ApiResponseModel> BuyCoins([FromQuery]BuyCoinsInput input)
        {
            return await this.ApiTaskFunc(_coinRechargeAppService.BuyCoins(input));
        }
       
        /// <summary>
        /// 充值金币微信支付回调
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("RechargeCoinNotify")]
        public async Task<ApiResponseModel> RechargeCoinNotify([FromQuery]BuyCoinsInput input)
        {
            return await this.ApiTaskFunc(_coinRechargeAppService.BuyCoins(input));
        }

        /// <summary>
        /// 获取充值结果
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetCoinRechargeResult")]
        public async Task<ApiResponseModel<UpdateOrderStateOutput>> GetCoinRechargeResult(UpdateOrderStateInput input)
        {
            return await this.ApiTaskFunc(_coinRechargeAppService.GetCoinRechargeResult(input));
        }

        [HttpGet]
        [Route("queryorder")]
        public Task<ApiResponseModel<UpdateOrderStateOutput>> QueryOrderState(UpdateOrderStateInput input) =>
            this.ApiTaskFunc(_coinRechargeAppService.QueryOrderState(input));

        /// <summary>
        /// 虚拟充值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("VirtualRecharge")]
        public async Task<ApiResponseModel<UpdateOrderStateOutput>> VirtualRecharge([FromBody]VirtualRechargeInput input)
        {
            return await this.ApiTaskFunc(_coinRechargeAppService.VirtualRecharge(input));
        }

        /// <summary>
        /// 养娃的通知接口
        /// </summary>
        /// <returns></returns>
        [HttpPost("WechatPayNotify")]
        [DontWrapResult]
        public Task<string> WechatPayNotify()
        {
            return _coinRechargeAppService.WechatPayNotify();
        }
    }
}