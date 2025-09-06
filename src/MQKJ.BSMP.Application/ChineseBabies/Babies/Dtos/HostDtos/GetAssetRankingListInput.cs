using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Babies.Dtos.HostDtos
{
    public class GetAssetRankingListInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public int? BabyId { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Asset";
            }
        }
    }
}
