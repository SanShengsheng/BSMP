using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Models;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.TestModels;
using MQKJ.BSMP.ChineseBabies.Athletics;
using MQKJ.BSMP.ChineseBabies.Babies.Dtos;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.Families.Dtos;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Models;

namespace MQKJ.BSMP.Web.Host.Areas.Athletics.Controllers.V1
{
    public class AthleticsController : AthleticsBaseController
    {
        private readonly IFamilyAppService _familyAppService;

        private readonly IBabyAppService _babyAppService;

        private readonly ICompetitionApplicationService _competitionApplicationService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="familyAppService"></param>
        /// <param name="babyAppService"></param>
        public AthleticsController(IFamilyAppService familyAppService, 
            IBabyAppService babyAppService,
            ICompetitionApplicationService competitionApplicationService
            )
        {
            _familyAppService = familyAppService ?? throw new ArgumentNullException(nameof(familyAppService));

            _babyAppService = babyAppService;

            _competitionApplicationService = competitionApplicationService;
        }

        /// <summary>
        /// 进入竞技场
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("EnterAthetics")]
        public async Task<ApiResponseModel<EnterAtheticsOutput>> JoinAthleticsSpace(EnterAtheticsInput input)
        {
            return await this.ApiTaskFunc(_competitionApplicationService.EnterAthetics(input));
        }

        /// <summary>
        /// 获取对战排行榜
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetRankingList")]
        public async Task<ApiResponseModel<RankingModel>> GetRankingList(GetRankingListInput input)
        {
            return await this.ApiTaskFunc(_competitionApplicationService.GetRankingList(input));
        }

        /// <summary>
        /// 获取上期排行榜
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[HttpGet("GetLatestRankingList")]
        //public async Task<ApiResponseModel<RankingModel>> GetLatestRankingList(GetRankingListInput input)
        //{
        //    return await this.ApiTaskFunc(_competitionApplicationService.GetLatestRankingList(input));
        //}

        /// <summary>
        /// 获取可选对手
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetFightList")]
        public async Task<ApiResponseModel<PagedResultDto<GetRankingListOutput>>> GetFightList(GetRankingListInput input)
        {
            return await this.ApiTaskFunc(_competitionApplicationService.GetFightList(input));
        }

        /// <summary>
        /// 开始对战
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("StartFight")]
        public async Task<ApiResponseModel<StartFightOutput>> StartFight([FromBody]StartFightInput input)
        {
            return await this.ApiTaskFunc(_competitionApplicationService.StartFight(input));
        }

        /// <summary>
        /// 结束对战
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("EndFight")]
        public async Task<ApiResponseModel<EndFightOutput>> EndFight([FromBody]EndFightInput input)
        {
            return await this.ApiTaskFunc(_competitionApplicationService.EndFight(input));
        }

        /// <summary>
        /// 购买对战次数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("BuyFightCount")]
        public async Task<ApiResponseModel<BuyFightCountOutput>> BuyFightCount([FromBody]BuyFightCountInput input)
        {
            return await this.ApiTaskFunc(_competitionApplicationService.BuyFightCount(input));
        }

        ///// <summary>
        ///// 获取竞技场跑马灯消息
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("GetAthleticsRunHorseInformations")]
        //public async Task<ApiResponseModel<string[]>> GetAthleticsRunHorseInformations()
        //{
        //    return await this.ApiTaskFunc(_competitionApplicationService.GetRunHorseInformations());
        //}

        /// <summary>
        /// 获取竞技场消息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAthleticsInformations")]
        public async Task<ApiResponseModel<PagedResultDto<GetAthleticsInformationsListDtos>>> GetAthleticsInformations(GetAthleticsInformationsInput input)
        {
            return await this.ApiTaskFunc(_competitionApplicationService.GetAthleticsInformations(input));
        }


        /// <summary>
        /// 获取当前竞技场信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAthleticsInfo")]
        public async Task<ApiResponseModel<GetAthleticsInfoOutput>> GetAthleticsInfo()
        {
            return await this.ApiTaskFunc(_competitionApplicationService.GetAthleticsInfo());
        }



        /// <summary>
        /// 测试获取 随机属性
        /// </summary>
        /// <param name="babyId"></param>
        /// <returns></returns>
        [HttpGet("TestRandomAttribute")]
        public ApiResponseModel<BabyAttributeCode> TestRandomAttribute(int babyId)
        {
            return this.ApiFunc(() => _competitionApplicationService.TestRandomAttribute(babyId));
        }

        /// <summary>
        /// 测试获取 对战结果
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("TestFightResult")]
        public async Task<ApiResponseModel<TestFightResultOutput>> TestFightResult(TestFightResultInput input)
        {
            return await this.ApiTaskFunc(_competitionApplicationService.TestFightResult(input));
        }

        [HttpGet("TimingAwardPrize")]
        public async Task TimingAwardPrize()
        {
            await _competitionApplicationService.TimingAwardPrize();
        }
    }
}