using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using MQKJ.BSMP.Tags.Dtos;
using X.PagedList;

namespace MQKJ.BSMP.Web.Models.Tags
{
    public class TagListViewModel
    {
        public StaticPagedList<TagListDto> Tags { get; set; }
        public List<SelectListItem> TagTypes { get; set; }
    }
}