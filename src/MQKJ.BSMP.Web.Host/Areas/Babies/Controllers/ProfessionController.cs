using Abp.Application.Services.Dto;
using Abp.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.Professions.Dtos;
using MQKJ.BSMP.Models;
using MQKJ.BSMP.Utils.WechatPay.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Host.Areas.Babies.Controllers
{
    /// <summary>
    /// 职业接口
    /// </summary>
    public class ProfessionController : BabyBaseController
    {
        private readonly IProfessionAppService _professionAppService;

        private static int count = 1;

        //private IDistributedCache _redisMemoryCache;

        //private RedisHelpers.CustomRedisHelper _redisHelper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="professionAppService"></param>
        public ProfessionController(IProfessionAppService professionAppService)
        {
            _professionAppService = professionAppService;

            //_redisHelper = new RedisHelpers.CustomRedisHelper(redisMemoryCache);
        }

        /// <summary>
        ///获取职业列表接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("GetProfessionList")]
        public async Task<ApiResponseModel<PagedResultDto<ProfessionListDto>>> GetProfessionList([FromBody]GetProfessionsInput input)
        {
            return await this.ApiTaskFunc(_professionAppService.GetProfessions(input));
        }

        /// <summary>
        ///获取职业列表接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetProfessionList")]
        public async Task<ApiResponseModel<PagedResultDto<ProfessionListDto>>> GetProfessionList_V2(GetProfessionsInput input)
        {
            return await this.ApiTaskFunc(_professionAppService.GetProfessions(input));
        }

        /// <summary>
        /// 转职接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [HttpPost("ChangeProfession")]
        public async Task<ApiResponseModel<MiniProgramPayOutput>> ChangeProfession([FromBody]ChangeProfessionInput input)
        {
            return await this.ApiTaskFunc(_professionAppService.ChangeProfession(input));
        }

        /// <summary
        /// 获取职业信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetProfessionInfo")]
        public async Task<ApiResponseModel<ProfessionListDto>> GetProfessionInfo(GetProfessionInput input)
        {
            return await this.ApiTaskFunc(_professionAppService.GetProfession(input));
        }

        /// <summary>
        /// 获取转职结果
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("GetChangeProfessionResult")]
        public async Task<ApiResponseModel> GetChangeProfessionResult([FromBody]QueryChangeResultInput input) =>
            await this.ApiTaskFunc(_professionAppService.QueryChangePorfessionResult(input));


        /// <summary>
        /// 获取转职结果
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetChangeProfessionResult")]
        public async Task<ApiResponseModel> GetChangeProfessionResult_V2(QueryChangeResultInput input) =>
            await this.ApiTaskFunc(_professionAppService.QueryChangePorfessionResult(input));

        /// <summary>
        /// 获取父母消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetParentProfession")]
        public async Task<ApiResponseModel<GetParentProfessionOutput>> GetParentProfession(GetParentProfessionInput input)
        {
            return await this.ApiTaskFunc(_professionAppService.GetParentProfession(input));
        }

        [HttpGet("AddRedisData")]
        public async Task<string> AddRedisData()
        {
            bool isSuccess = RedisHelper.Set((count + 1).ToString(), $"恭喜xxx宝宝的爸爸转职为xxx，获得222金币");

            var str = String.Empty;
            if (isSuccess)
            {
                str = "保存redis成功";
            }
            else
            {
                str = "保存redis失败";
            }

            return await Task.FromResult(str);
        }

        [HttpGet("GetRedisData")]
        public async Task<string> GetRedisData()
        {
            List<string> lst = new List<string>();

            for (int i = 0; i < 3; i++)
            {
                object obj = RedisHelper.Get(HorseRaceLampMessageType.BabyBirth);

                lst.Add(obj.ToJsonString());
            }

            return await Task.FromResult(lst.ToJsonString());
        }

        /// <summary>
        /// 虚拟转职
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("VirtualChangeProfession")]
        public async Task<ApiResponseModel<MiniProgramPayOutput>> VirtualChangeProfession(VirtualChangeProfessionInput input)
        {
            return await this.ApiTaskFunc(_professionAppService.VirtualChangeProfession(input));
        }

        /// <summary>
        /// 支付成功|失败回调
        /// </summary>
        /// <returns></returns>
        //[HttpPost("WechatPayNotify")]
        //public Task WechatPayNotify()
        //{
        //    Logger.Warn("支付通知接口");

        //}
    }
}