using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Json;
using Abp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Alipay;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Models;
using MQKJ.BSMP.Orders;
using MQKJ.BSMP.Pay.Alipay.Dtos;

namespace MQKJ.BSMP.Web.Mvc.Areas.ChineseBabies.Controllers
{
    [AllowAnonymous]
    [Area("ChineseBabies")]

    public class AliPayController : BSMPControllerBase
    {
        private readonly ICoinRechargeAppService _coinRechargeAppService;
        private readonly IFamilyAppService _familyAppService;
        //private readonly IOrderAppService _orderAppService;
        //private readonly ICoinRechargeRecordAppService _coinRechargeRecordAppService;
        private readonly IAlipayAppService _alipayAppService;

        public AliPayController(
            ICoinRechargeAppService coinRechargeAppService,
            IFamilyAppService familyAppService,
             //ICoinRechargeRecordAppService coinRechargeRecordAppService,
             IAlipayAppService alipayAppService
            )
        {
            _coinRechargeAppService = coinRechargeAppService;
            _familyAppService = familyAppService;
            //_coinRechargeRecordAppService = coinRechargeRecordAppService;
            _alipayAppService = alipayAppService;
        }

        public async Task<IActionResult> Index(int babyId, int familyId, Guid playerId)
        {
            var coinList = await _coinRechargeAppService.GetCoinRecharges(new BSMP.ChineseBabies.Dtos.GetCoinRechargesInput()
            {
                FamilyId = familyId,
                PlayerId = playerId
            });
            var family = await _familyAppService.GetBasicFamilyInfo(new GetBasicFamilyInput()
            {
                FamilyId = familyId,
            });
            var response = new H5AliPayOutput()
            {
                CoinList = coinList,
                FamilyInfo = family
            };
            return View(response);
        }
        /// <summary>
        /// 购买金币
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponseModel> BuyCoins([FromQuery] BuyCoinsInput input)
        {
            return await this.ApiTaskFunc(_coinRechargeAppService.BuyCoins(input));
        }
        /// <summary>
        /// 结果页
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Result([FromQuery] GetPayResultAsyncInput input)
        {
            Console.WriteLine($"=========fromQuery:{input.ToJsonString()}");
            var output = await _alipayAppService.GetPayResultAsync(input);
            return View(output);
        }
        /// <summary>
        /// 接收支付宝异步通知
        /// 参考网址：https://docs.open.alipay.com/203/105286#s6
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DontWrapResult]
        public async Task Receive_Notify([FromForm] AliPayNotifyRsultAsyncDto input)
        {
            var res = await _coinRechargeAppService.Receive_Notify(input);
            await Response.WriteAsync($"{(res.State ? "success" : "fail")}");
            //return View(res);
        }
        public IActionResult Detail()
        {
            return View();
        }
    }
}