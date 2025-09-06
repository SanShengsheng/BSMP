using Abp.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Host.Areas.Agents.Controllers
{
    [Area("agents")]
    //[AllowAnonymous]
    [Route("api/[area]/[controller]")]
    [ApiExplorerSettings(GroupName = "agents")]
    public abstract class AgentBaseController : BSMPControllerBase
    {
    }
}
