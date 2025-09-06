using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Babies.Dtos.HostDtos
{
    public class BabyRankingList_V2Input : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public int? BabyId { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "TotalValue";
            }
        }
    }
}
