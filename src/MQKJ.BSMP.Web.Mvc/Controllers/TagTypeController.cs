using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.TagTypes;
using MQKJ.BSMP.TagTypes.Authorization;
using MQKJ.BSMP.TagTypes.Dtos;
using MQKJ.BSMP.Web.Models.TagTypes;

namespace MQKJ.BSMP.Web.Mvc.Controllers
{
    public class TagTypeController : BSMPControllerBase
    {
        private readonly ITagTypeAppService _tagTypeAppService;

        public TagTypeController(ITagTypeAppService tagTypeAppService)
        {
            _tagTypeAppService = tagTypeAppService;
        }

        [AbpAuthorize(TagTypeAppPermissions.TagType)]
        public async Task<IActionResult> Index(GetTagTypesInput input)
        {
            var model = await _tagTypeAppService.GetPagedTagTypes(input);
            return View(new TagTypeListViewModel
            {
                TagTypes = model
            });
        }

        public async Task<IActionResult> EditModal(int tagTypeId)
        {
            var tagType = await _tagTypeAppService.GetTagTypeByIdAsync(new EntityDto(tagTypeId));
            return View("_EditTagTypeModal", tagType);
        }
    }
}