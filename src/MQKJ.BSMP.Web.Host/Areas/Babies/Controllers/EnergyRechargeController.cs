using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.EnergyRecharges.Dtos;
using MQKJ.BSMP.Models;

namespace MQKJ.BSMP.Web.Host.Areas.Babies.Controllers
{
    public class EnergyRechargeController : BabyBaseController
    {
        private readonly IEnergyRechargeAppService _energyRechargeAppService;

        public EnergyRechargeController(IEnergyRechargeAppService energyRechargeAppService)
        {
            _energyRechargeAppService = energyRechargeAppService;
        }

        /// <summary>
        /// 获取精力充值数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetEnergyRecharges")]
        public async Task<ApiResponseModel<PagedResultDto<EnergyRechargeListDto>>> GetEnergyRecharges(GetEnergyRechargesInput input)
        {
            return await this.ApiTaskFunc(_energyRechargeAppService.PageSearch(input));
        }

        /// <summary>
        /// 购买精力
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("BuyEnergy")]
        public async Task<ApiResponseModel<BuyEnergyOutput>> BuyEnergy([FromBody]BuyEnergyInput input)
        {
            return await this.ApiTaskFunc(_energyRechargeAppService.BuyEnergy(input));
        }
    }
}