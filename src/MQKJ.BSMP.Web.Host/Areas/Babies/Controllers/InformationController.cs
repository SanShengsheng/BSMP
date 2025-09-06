using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.Informations.Dtos;
using MQKJ.BSMP.ChineseBabies.Message.Dtos;
using MQKJ.BSMP.Models;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MQKJ.BSMP.Web.Host.Areas.Babies.Controllers
{
    /// <summary>
    /// 消息接口
    /// </summary>
    public class InformationController : BabyBaseController
    {
        private readonly IInformationAppService _service;
        public InformationController(IInformationAppService service)
        {
            _service = service;
        }
        
        /// <summary>
        /// 判断是否有新消息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpGet("HasNewMessage")]
        public async Task<ApiResponseModel> HasNewMessage(HasNewInformationRequest request) =>await this.ApiTaskFunc(_service.HasNewInformation(request));

        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetInformations")]
        public async Task<ApiResponseModel<PagedResultDto<InformationListDto>>> GetInformations(GetInformationsInput input)
        {
            //return await this.ApiTaskFunc(_service.PageSearch(input));
            return await this.ApiTaskFunc(_service.GetInformations(input));

        }

        /// <summary>
        /// 修改消息状态(已读)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("UpdateInforationState")]
        public async Task<ApiResponseModel> UpdateInforationState([FromBody]object input)
        {
            string inputJsonStr = JsonConvert.SerializeObject(input);

            var result = JsonConvert.DeserializeObject<ModifyInforationStateInput>(inputJsonStr);


            return await this.ApiTaskAction(_service.ModifyInforationState(result));
        }

        /// <summary>
        /// 获取跑马灯消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetRunHorseInformations")]
        public async Task<ApiResponseModel<PagedResultDto<RunHorseInformationListDto>>> GetRunHorseInformations(GetRunHorseInformationsInput input)
        {
            return await this.ApiTaskFunc(_service.GetRunHorseInformations(input));
        }

    }
}
