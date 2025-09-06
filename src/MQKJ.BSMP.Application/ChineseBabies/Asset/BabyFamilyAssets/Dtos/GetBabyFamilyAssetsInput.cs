
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.ChineseBabies.Backpack;
using System;

namespace MQKJ.BSMP.ChineseBabies.Backpack.Dtos
{
    public class GetBabyFamilyAssetsInput : PagedSortedAndFilteredInputDto, IShouldNormalize,ISearchModel<BabyFamilyAsset,Guid>
    {

        public int BabyId { get; set; }

        public int PropTypeId { get; set; }

        public Guid PlayerGuid { get; set; }

        public int FamilyId { get; set; }

        public Gender? Gender { get; set; }


        /// <summary>
        /// 正常化排序使用
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime";
            }
        }

    }
}
