using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.BigRisk.Players.Dtos;
using MQKJ.BSMP.Common.Adviertisements;
using MQKJ.BSMP.Common.Adviertisements.Dtos;
using MQKJ.BSMP.Models;

namespace MQKJ.BSMP.Web.Host.Areas.Adviertisements.Controllers
{

    public class AdviertisementController : AdviertisementBaseController
    {
        private readonly IAdviertisementApplicationService _adviertisementApplicationService;
        public AdviertisementController(IAdviertisementApplicationService adviertisementApplicationService)
        {
            _adviertisementApplicationService = adviertisementApplicationService;
        }

        /// <summary>
        /// 获取自己的明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("ClickAd")]
        public async Task<ApiResponseModel<ClickAdOutput>> ClickAd([FromQuery]ClickAdInput input)
        {
            input.IpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            return await this.ApiTaskFunc(_adviertisementApplicationService.ClickAd(input));
        }
    }
}