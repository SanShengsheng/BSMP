using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;

namespace MQKJ.BSMP.Scenes.Dto
{
    public class GetSceneInput : PagedAndSortedResultRequestDto, IShouldNormalize
    {

        public string FilterText { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            }
        }
    }
}
