using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Backpack;
using MQKJ.BSMP.ChineseBabies.Backpack.Dtos;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.Filters;
using MQKJ.BSMP.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MQKJ.BSMP.Web.Host.Areas.Babies.Controllers
{
    /// <summary>
    /// 家庭资产
    /// </summary>
    public class FamilyAssetController : BabyBaseController
    {
        private readonly IBabyFamilyAssetAppService _babyFamilyAssetAppService;
        private readonly IBabyPropTypeAppService _babyPropTypeAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="babyFamilyAssetAppService"></param>
        public FamilyAssetController(BabyFamilyAssetAppService babyFamilyAssetAppService
            , BabyPropTypeAppService babyPropAppTypeService)
        {
            _babyFamilyAssetAppService = babyFamilyAssetAppService;
            _babyPropTypeAppService = babyPropAppTypeService;

        }

        /// <summary>
        ///  获取家庭资产列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetPage")]
        [ResponseCache(Duration = 5)]
        public async Task<ApiResponseModel<GetBabyFamilyAssetByPageOutput>> GetPage(GetBabyFamilyAssetsInput input) => await this.ApiTaskFunc(_babyFamilyAssetAppService.GetPage(input));


        /// <summary>
        ///  更换装备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("PostChangeAsset")]
        [ForbidRepeatActionFilter]
        public async Task<ApiResponseModel<PostChangeAssetOutput>> PostChangeAsset([FromBody]PostChangeAssetInput input) => await this.ApiTaskFunc(_babyFamilyAssetAppService.PostChangeAsset(input));

        /// <summary>
        /// 获取家庭资产类型列表，
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetAllAssetTypes")]
        [ResponseCache(Duration = 7200)]
        public async Task<ApiResponseModel<List<BabyPropTypeListDto>>> GetAllAssetTypes() => await this.ApiTaskFunc(_babyPropTypeAppService.GetAllAssetTypes());
    }
}
