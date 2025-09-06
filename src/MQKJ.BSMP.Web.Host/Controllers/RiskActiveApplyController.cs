using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ActiveApply;
using MQKJ.BSMP.ActiveApply.Dtos;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Models;

namespace MQKJ.BSMP.Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiskActiveApplyController : BSMPControllerBase
    {
        private readonly RiskActiveApplyAppService _riskActiveApplyAppService;
        public RiskActiveApplyController(RiskActiveApplyAppService riskActiveApplyAppService)
        {
            _riskActiveApplyAppService = riskActiveApplyAppService;
        }

        [HttpPost]
        [Route("CreateOrUpdate")]
        public ApiResponseModel<CreateOrUpdateRiskActiveApplyOutput> CreateOrUpdate(CreateOrUpdateRiskActiveApplyInput input) =>
             this.ApiFunc(() => _riskActiveApplyAppService.CreateOrUpdate(input).GetAwaiter().GetResult());

    }
}