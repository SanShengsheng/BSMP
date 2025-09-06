using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Common.OperationActivities;
using MQKJ.BSMP.Common.OperationActivities.Dtos;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MQKJ.BSMP.Web.Host.Controllers
{
    /// <summary>
    /// 运营活动
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OperationActivityController : BSMPControllerBase
    {
        private readonly IOperationActivityAppService _operationActivityAppService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="operationActivityAppService"></param>
        public OperationActivityController(IOperationActivityAppService operationActivityAppService)
        {
            _operationActivityAppService = operationActivityAppService;
        }

        /// <summary>
        /// 获取运营活动
        /// 需要在请求头传递abp-tenantId
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetOperationActivities")]
        public async Task<ApiResponseModel<IEnumerable<OperationActivityListDto>>> GetOperationActivities(GetOperationActivitiesInput input) => await this.ApiTaskFunc(_operationActivityAppService.Search(input));


    }
}
