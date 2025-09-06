using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MQKJ.BSMP.Tags.Dtos;

namespace MQKJ.BSMP.Web.Models.Tags
{
    public class TagEditViewModel
    {
        public TagListDto TagEditDto { get; set; }
        public List<SelectListItem> TagTypes { get; set; }
    }
}