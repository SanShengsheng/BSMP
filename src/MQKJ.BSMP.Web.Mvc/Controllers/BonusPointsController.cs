using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MQKJ.BSMP.BonusPoints;
using MQKJ.BSMP.BonusPointRecords;
using MQKJ.BSMP.BonusPoints.Dtos;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Web.Models.BonusPoints;
using MQKJ.BSMP.BonusPointRecords.Dtos;
using X.PagedList;
using X.PagedList.Mvc.Core;
using Abp.AspNetCore.Mvc.Extensions;

namespace MQKJ.BSMP.Web.Mvc.Controllers
{
    public class BonusPointsController : BSMPControllerBase
    {
        private readonly IBonusPointAppService _bonusPointAppService;
        private readonly IBonusPointRecordAppService _bonusPointRecordAppService;

        public BonusPointsController(
            IBonusPointAppService bonusPointAppService,
            IBonusPointRecordAppService bonusPointRecordAppService
            )
        {
            _bonusPointAppService = bonusPointAppService;
            _bonusPointRecordAppService = bonusPointRecordAppService;
        }

        public async Task<IActionResult> Index(int? page, string nickName, DateTime? startTime, DateTime? endTime, int? eventId)
        {
            //ViewBag.nicName = nickName;

            //ViewBag.StartTime = startTime ?? DateTime.Now.AddDays(-7);

            //ViewBag.EndTime = endTime ?? DateTime.Now;

            //ViewBag.EventId = eventId ?? 0;

            var pageNumber = page ?? 1;
            var pageSize = 10;

            var filter = new GetBonusPointRecordsInput()
            {
                SkipCount = (pageNumber - 1) * pageSize,
                UserName = nickName,
                StartTime = startTime ?? DateTime.Now.AddDays(-7),
                EndTime = endTime ?? DateTime.Now,
                EventId = eventId ?? 0
            };

            var result = await _bonusPointRecordAppService.GetPagedBonusPointRecords(filter);

            var bonusRecords = new StaticPagedList<BonusPointRecordListDto>(result.Items, pageNumber, pageSize, result.TotalCount);

            var events = await _bonusPointAppService.GetAllScenesAsync(new GetBonusPointsInput());

            ViewBag.Events = events;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", bonusRecords);
            }
            else
            {
                return View(bonusRecords);
            }
        }

        public async Task<IActionResult> BonusPointsPage(GetBonusPointsInput input)
        {
            var bonuspoints = await _bonusPointAppService.GetPagedBonusPoints(new GetBonusPointsInput
            {
                MaxResultCount = BSMPConsts.MaxQueryCount
            });

            return View(bonuspoints);
        }
    }
}