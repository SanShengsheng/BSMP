using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.LoveCards.Dtos
{
    public class GetOwnShareAndLikedListInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public Guid PlayerId { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime";
            }
        }
    }
}
