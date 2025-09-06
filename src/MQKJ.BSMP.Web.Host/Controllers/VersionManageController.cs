using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Common;
using MQKJ.BSMP.Common.Dtos;
using MQKJ.BSMP.Common.VersionManages.Dtos;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Host.Controllers
{
    /// <summary>
    /// 版本管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VersionManageController : BSMPControllerBase
    {
        private readonly IVersionManageAppService _versionManageAppService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="versionManageAppService"></param>
        public VersionManageController(IVersionManageAppService versionManageAppService)
        {
            _versionManageAppService = versionManageAppService;
        }
        /// <summary>
        /// 获取最新的版本信息
        /// 需要在请求头传递abp-tenantId
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetLastestVersion")]
        public async Task<ApiResponseModel<VersionManageEditDto>> GetLastestVersion() => await this.ApiTaskFunc(_versionManageAppService.GetLastestVersion());


    }
}