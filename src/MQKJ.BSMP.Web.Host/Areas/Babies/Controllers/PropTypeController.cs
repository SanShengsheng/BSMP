using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MQKJ.BSMP.Web.Host.Areas.Babies.Controllers
{
    /// <summary>
    /// 道具类型
    /// </summary>
    public class PropTypeController : BabyBaseController
    {
        private readonly IBabyPropTypeAppService _babyPropTypeAppService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="babyPropAppTypeService"></param>
        public PropTypeController(BabyPropTypeAppService babyPropAppTypeService)
        {
            _babyPropTypeAppService = babyPropAppTypeService;
        }
        /// <summary>
        /// 获取道具类型列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetPropTypies")]
        [ResponseCache(Duration = 7200)]
        public async Task<ApiResponseModel<List<BabyPropTypeListDto>>> GetPropTypies() => await this.ApiTaskFunc(_babyPropTypeAppService.GetAll());

     
    }
}
