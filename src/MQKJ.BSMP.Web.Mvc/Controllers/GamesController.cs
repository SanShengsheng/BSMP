using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;

namespace MQKJ.BSMP.Web.Mvc.Controllers
{
    public class GamesController :  BSMPControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}