using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Extensions;
using Abp.AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Common.AD;
using MQKJ.BSMP.Common.Adviertisements;
using MQKJ.BSMP.Common.Adviertisements.Dtos;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.MultiTenancy;
using MQKJ.BSMP.Web.Areas.Adviertisements.Models;
using X.PagedList;

namespace MQKJ.BSMP.Web.Mvc.Areas.Adviertisements.Controllers
{
    [Area("Adviertisements")]
    public class AdviertisementController : BSMPControllerBase
    {
        private readonly IAdviertisementApplicationService _adviertisementApplicationService;
        private readonly ITenantAppService _tenantAppService;

        public AdviertisementController(IAdviertisementApplicationService adviertisementApplicationService,ITenantAppService tenantAppService)
        {
            _adviertisementApplicationService = adviertisementApplicationService;

            _tenantAppService = tenantAppService;
        }
        public async Task<IActionResult> Index(int? page,string name, AdviertisementPlatform platform, AdviertisementState state,int? teantId)
        {
            var tenants = await _tenantAppService.GetAll(new Abp.Application.Services.Dto.PagedResultRequestDto()
            {
                SkipCount = 0,
                MaxResultCount = 9999
            });
            var model = new AdviertisementModel();
            var pageNumber = page ?? 1;
            var pageSize = 10;
            var result = await _adviertisementApplicationService.GetAdviertisements(new GetAdviertisementsInput()
            {
                SkipCount = (pageNumber - 1) * pageSize,
                Name = name,
                AdviertisementPlatform = platform,
                AdviertisementState = state,
                TeantId = teantId
            });

            model.Tenants = tenants.Items.ToList();

            var adviertisements = new StaticPagedList<GetAdviertisementsOutput>(result.Items, pageNumber, pageSize, result.TotalCount);

            model.staticPagedList = adviertisements;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", model);
            }
            else
            {
                return View(model);
            }

        }

        public async Task<IActionResult> EditAd(int? adId)
        {
            var editModel = new EditAdModel();

            var ad = await _adviertisementApplicationService.GetAdviertisementForEdit(new NullableIdDto<int>(adId));
            if (ad != null)
            {
                ad.AdviertisementDto.MapTo(editModel);

                var tenants = await _tenantAppService.GetAll(new Abp.Application.Services.Dto.PagedResultRequestDto()
                {
                    SkipCount = 0,
                    MaxResultCount = 9999
                });

                editModel.Tenants = tenants.Items.ToList();
            }

            return View("_EditAdModal", editModel);
        }
    }
}