using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.TagTypes.Dtos;

namespace MQKJ.BSMP.Web.Models.TagTypes
{
    public class TagTypeListViewModel
    {
        public PagedResultDto<TagTypeListDto> TagTypes { get; set; }
    }
}
