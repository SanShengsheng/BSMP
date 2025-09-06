using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Players.Dtos;
using MQKJ.BSMP.Questions.Dtos;
using X.PagedList;

namespace MQKJ.BSMP.Web.Mvc.Controllers
{
    public class PlayersController : BSMPControllerBase
    {
        private readonly IPlayerAppService _playerAppService;

        public PlayersController(IPlayerAppService playerAppService)
        {
            _playerAppService = playerAppService;
        }
        public async Task<ActionResult> Index(int? page,string nickName,int? gender,int? ageRange,DateTime? startTime,DateTime? endTime)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;
            var filter = new GetPlayersInput()
            {
                SkipCount = (pageNumber - 1) * pageSize,
                NickName = nickName,
                Gender = gender ?? 0,
                AgeRange = ageRange ?? 0,
                StartTime = startTime ?? DateTime.Now.AddDays(-7),
                EndTime = endTime ?? DateTime.Now
            };

            var result = await _playerAppService.GetPagedPlayers(filter);
            var playerList = new StaticPagedList<PlayerListDto>(result.Items, pageNumber, pageSize, result.TotalCount);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_List",playerList);
            }
            else
            {
                return View(playerList);
            }
        }
    }
}