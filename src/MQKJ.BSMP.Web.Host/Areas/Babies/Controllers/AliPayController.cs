using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;

namespace MQKJ.BSMP.Web.Host.Areas.Babies.Controllers
{
    [AllowAnonymous]
    [Area("ChineseBabies")]

    public class AliPayController : BSMPControllerBase
    {
        
        public AliPayController()
        {

        }

        }
}