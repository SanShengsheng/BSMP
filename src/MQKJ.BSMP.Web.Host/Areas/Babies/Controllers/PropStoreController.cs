using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.Filters;
using MQKJ.BSMP.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MQKJ.BSMP.Web.Host.Areas.Babies.Controllers
{
    /// <summary>
    /// 道具商城
    /// </summary>
    public class PropStoreController : BabyBaseController
    {
        private readonly IBabyPropAppService _babyPropAppService; 

        public PropStoreController(BabyPropAppService babyPropAppService)
        {
            _babyPropAppService = babyPropAppService;
        }
        /// <summary>
        ///  获取道具
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetProps")]
        [ResponseCache(Duration =20)]
        public async Task<ApiResponseModel<PagedResultDto<GetBabyPropsOutput>>> GetProps(GetBabyPropsInput input) => await this.ApiTaskFunc(_babyPropAppService.GetPage(input));

        /// <summary>
        ///  购买道具
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("PostBuyProp")]
        [ForbidRepeatActionFilter]
        public async Task<ApiResponseModel<PostBuyPropOutput>> PostBuyProp([FromBody]PostBuyPropInput input) => await this.ApiTaskFunc(_babyPropAppService.PostBuyPropAsync(input));

        /// <summary>
        ///  获取家庭购买信息
        ///  目的：因为担心查询比较慢，故与获取道具列表接口分开
        ///  备注：主要返回的状态：已拥有，未获得，未解锁和已装备（接口不返回已装备的信息）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetPropBuyInfo")]
        //[ResponseCache(Duration = 5)]

        public async Task<ApiResponseModel<ICollection< GetFamilyPropBuyInfoOutput>>> GetFamilyPropBuyInfo(GetFamilyPropBuyInfoInput input) => await this.ApiTaskFunc(_babyPropAppService.GetFamilyPropBuyInfo(input));

    }
}
