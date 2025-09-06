using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;

namespace MQKJ.BSMP.Web.Host.Areas.Babies.Controllers
{
    [Area("baby")]
    [AllowAnonymous]
    [Route("api/[area]/[controller]")]
    [ApiExplorerSettings(GroupName = "chinesebaby")]
    public abstract class BabyBaseController : BSMPControllerBase
    {
    }
}