using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Tags;
using MQKJ.BSMP.Tags.Dto;
using MQKJ.BSMP.TagTypes;
using MQKJ.BSMP.TagTypes.Dtos;
using MQKJ.BSMP.Web.Models.Tags;
using X.PagedList;
using MQKJ.BSMP.Tags.Dtos;

namespace MQKJ.BSMP.Web.Mvc.Controllers
{
    public class TagController : BSMPControllerBase
    {
        private readonly ITagAppService _tagAppService;
        private readonly ITagTypeAppService _tagTypeAppService;

        public TagController(ITagAppService tagAppService, ITagTypeAppService tagTypeAppService)
        {
            _tagAppService = tagAppService;
            _tagTypeAppService = tagTypeAppService;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;
            //input.WithDetail = true;
            var types = await _tagTypeAppService.GetPagedTagTypes(new GetTagTypesInput
            {
                MaxResultCount = BSMPConsts.MaxQueryCount
            });
            var Tags = await _tagAppService.GetPagedTags(new GetTagsInput()
            {
                WithDetail = true,
                SkipCount = (pageNumber - 1) * pageSize,
            });
            var tags = new StaticPagedList<TagListDto>(Tags.Items, pageNumber, pageSize, Tags.TotalCount);
            var model = new TagListViewModel
            {
                Tags = tags,
                TagTypes = types.Items.Select(i => new SelectListItem(i.TypeName, i.Id.ToString())).ToList()
            };


            return View(model);
        }

        public async Task<IActionResult> EditModal(int tagId)
        {
            var model = new TagEditViewModel();
            var types = await _tagTypeAppService.GetPagedTagTypes(new GetTagTypesInput
            {
                MaxResultCount = BSMPConsts.MaxQueryCount
            });
            model.TagEditDto = await _tagAppService.GetTagByIdAsync(new EntityDto(id: tagId));
            model.TagTypes = types.Items.Select(i => 
                new SelectListItem(i.TypeName, i.Id.ToString(), model.TagEditDto.TagTypeId == i.Id)).ToList();

            return View("_EditTagModal", model);
        }
    }
}