using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.AspNetCore.Mvc.Extensions;
using Abp.Json;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Authorization;
using MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos;
using MQKJ.BSMP.ChineseBabies.Families.Dtos;
using MQKJ.BSMP.ChineseBabies.Families.Model;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Web.Areas.ChineseBabies.Models;
using X.PagedList;

namespace MQKJ.BSMP.Web.Mvc.Areas.ChineseBabies.Controllers
{
    [AbpMvcAuthorize(ChinesePermissions.Node)]
    [Area("ChineseBabies")]
    public class FamilyController : BSMPControllerBase
    {

        public IFamilyAppService _familyAppService;

        public ICoinRechargeAppService _coinRechargeAppService;

        public FamilyController(IFamilyAppService familyAppService, ICoinRechargeAppService coinRechargeAppService)
        {
            _familyAppService = familyAppService;

            _coinRechargeAppService = coinRechargeAppService;
        }

        [AbpMvcAuthorize(ChinesePermissions.GetAllFamilys)]
        public async Task<IActionResult> Index(int? page, string fatherName, string motherName, string babyName, FamilyLevel? familyLevel, RechargeRange rechargeLevel, DateTime? startTime, DateTime? endTime, List<int> TenantIds)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;
            TenantIds = TenantIds.Count == 0 ? new List<int>() { 7, 295 } : TenantIds;
            var model = new FamilyModel();

            var result = await _familyAppService.GetAllFamilys(new GetAllFamilysInput()
            {
                TenantIds = TenantIds,
                FatherName = fatherName,
                MotherName = motherName,
                SkipCount = (pageNumber - 1) * pageSize,
                BabyName = babyName,
                FamilyLevel = familyLevel == null ? FamilyLevel.All : familyLevel,
                RechargeRange = rechargeLevel,
                StartTime = startTime ?? DateTime.Now.Date.AddDays(-30),
                EndTime = endTime ?? DateTime.Now.AddDays(1).Date.AddMilliseconds(-1)
            });

            var famlies = new StaticPagedList<GetAllFamilysListDto>(result.Items, pageNumber, pageSize, result.TotalCount);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", famlies);
            }
            else
            {
                return View(famlies);
            }
        }
        [AbpMvcAuthorize(ChinesePermissions.GetAllFamilys)]
        public async Task<IActionResult> IndexAjax([FromBody]FamilyModelInput input)
        {
            var pageNumber = input.Page ?? 1;
            var pageSize = 10;

            var model = new FamilyModel();

            var result = await _familyAppService.GetAllFamilys(new GetAllFamilysInput()
            {
                TenantIds = input.TenantIds,
                FatherName = input.FatherName,
                MotherName = input.MotherName,
                SkipCount = (pageNumber - 1) * pageSize,
                BabyName = input.BabyName,
                FamilyLevel = input.FamilyLevel,
                RechargeRange = input.RechargeLevel,
                StartTime = input.StartTime ?? DateTime.Now.Date.AddDays(-30),
                EndTime = input.EndTime ?? DateTime.Now.AddDays(1).Date.AddMilliseconds(-1),
                Orders = input.Orders
            });

            var famlies = new StaticPagedList<GetAllFamilysListDto>(result.Items, pageNumber, pageSize, result.TotalCount);
            return PartialView("_List", famlies);


        }
        [AbpMvcAuthorize(ChinesePermissions.SupplementCoinRecharge)]
        public async Task<IActionResult> RechargeCoin(int familyId, int coinCount, string ordeNum)
        {
            var result = await _coinRechargeAppService.SupplementCoinRecharge(new SupplementCoinRechargeInput()
            {
                FamilyId = familyId,
                CoinCount = coinCount,
                OrdeNum = ordeNum
            });

            return Content(result.ToJsonString());
        }

        [AbpMvcAuthorize(ChinesePermissions.ExportFamiliesToExcel)]
        public IActionResult ExportToExcel([FromBody]FamilyModelInput input)
        {
            var filePath = _familyAppService.ExportFamiliesToExcel(new GetAllFamilysInput()
            {
                FatherName = input.FatherName,
                MotherName = input.MotherName,
                BabyName = input.BabyName,
                FamilyLevel = input.FamilyLevel,
                RechargeRange = input.RechargeLevel,
                StartTime = input.StartTime ?? DateTime.Now.Date.AddDays(-30),
                EndTime = input.EndTime ?? DateTime.Now.AddDays(1).Date.AddMilliseconds(-1),
                Orders = input.Orders
            });


            return Content(filePath);
        }

        public async Task<IActionResult> SendMsgCode()
        {
            var result = await _coinRechargeAppService.SendMessageValideCode();

            return Content(result.ToString());
        }
    }
}