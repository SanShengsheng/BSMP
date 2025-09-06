using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;

namespace MQKJ.BSMP.Web.Host.Areas.Athletics.Controllers.V1
{
    [Area("V1/Athletics")]
    //[AllowAnonymous]
    [Route("api/[area]/[controller]")]
    [ApiExplorerSettings(GroupName = "chinesebaby")]
    public class AthleticsBaseController : BSMPControllerBase
    {
    }
}