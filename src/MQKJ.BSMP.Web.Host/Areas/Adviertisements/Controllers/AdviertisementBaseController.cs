using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;

namespace MQKJ.BSMP.Web.Host.Areas.Adviertisements.Controllers
{

    [Area("Adviertisements")]
    //[AllowAnonymous]
    [Route("api/[area]/[controller]")]
    public class AdviertisementBaseController : BSMPControllerBase
    {
    }
}