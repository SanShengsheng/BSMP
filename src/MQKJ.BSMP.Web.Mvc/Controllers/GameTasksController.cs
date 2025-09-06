using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.GameTasks.Dtos;
using X.PagedList;

namespace MQKJ.BSMP.Web.Mvc.Controllers
{
    public class GameTasksController : BSMPControllerBase
    {
        private readonly IGameTaskAppService _gameTaskAppService;
        public GameTasksController(IGameTaskAppService gameTaskAppService)
        {
            _gameTaskAppService = gameTaskAppService;
        }
        public async Task<IActionResult> Index(int? page,DateTime? startTime,DateTime? endTime,string nickName,int? taskType,int? taskState,int? seekType,int? relationType)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;

            var result = await _gameTaskAppService.GetPagedGameTasks(new GetGameTasksInput()
            {
                SkipCount = (pageNumber - 1) * pageSize,
                StartTime = startTime ?? DateTime.Now.AddDays(-7),
                EndTime = endTime ?? DateTime.Now,
                NickName = nickName,
                TaskType = taskType ?? 0,
                TaskState = taskState ?? 0,
                SeekType = seekType ?? 0,
                RelationType = relationType ?? 0
            });

            var gameTasks = new StaticPagedList<GameTaskListDto>(result.Items, pageNumber, pageSize, result.TotalCount);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", gameTasks);
            }
            else
            {
                return View(gameTasks);
            }
        }
    }
}