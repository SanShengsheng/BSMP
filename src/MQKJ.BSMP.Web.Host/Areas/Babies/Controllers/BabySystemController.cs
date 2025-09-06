using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Uow;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.BabySystem.Dtos.HostDtos;
using MQKJ.BSMP.Models;

namespace MQKJ.BSMP.Web.Host.Areas.Babies.Controllers
{
    public class BabySystemController : BabyBaseController
    {
        private IBabySystemAppService _babySystemAppService;

        public BabySystemController(IBabySystemAppService babySystemAppService)
        {
            _babySystemAppService = babySystemAppService;
        }
        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("ImportData")]
        //[UnitOfWork]
        public  ApiResponseModel<ImportDataOutput> ImportData([FromForm]ImportDataInput input)
        {
            if (Request.ContentType != "application/x-www-form-urlencoded" && Request.HasFormContentType)
            {
                var formFile = Request.Form.Files.First();
                input.FormFile = formFile;
            }
            var response =  _babySystemAppService.ImportData(input);
            return this.ApiFunc<ImportDataOutput>(() => response);
        }

        /// <summary>
        /// 宝宝排行榜
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("RankingList")]
        public async Task<ApiResponseModel<ChineseBabyRankOutput>> RankingList(ChineseBabyRankInput input) => await this.ApiTaskFunc(_babySystemAppService.Rank(input));

        [HttpGet("GetMiniPrograms")]
        public async Task<ApiResponseModel<IList<GetMiniProgramsOutput>>> GetMiniPrograms(GetMiniProgramsInput input)
        {
            return await this.ApiTaskFunc(_babySystemAppService.GetMiniPrograms(input));
        }
    }
}