using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;

namespace MQKJ.BSMP.Web.Mvc.Areas.GamblingModel.Controllers
{
    [Area("GamblingModel")]
    public class AnswerQuestionStatisticsController : BSMPControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}