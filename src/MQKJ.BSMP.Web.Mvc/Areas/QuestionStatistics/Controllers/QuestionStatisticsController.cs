using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Questions;
using MQKJ.BSMP.Questions.Dtos;

namespace MQKJ.BSMP.Web.Mvc.Areas.QuestionStatistics.Controllers
{
    [Area("QuestionStatistics")]
    public class QuestionStatisticsController : BSMPControllerBase
    {
        //private readonly IQuestionAppService _questionAppService;

        public  IActionResult Index()
        {
            //var output = await _questionAppService.GetQuestionStatisics(new GetQuestionStatisicsDto()
            //{
            //    StartTime = startTime,
            //    EndTime = endTime
            //});

            return View();
        }
    }
}