
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.ChineseBabies;
using System;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class GetCoinRechargesInput : PagedSortedAndFilteredInputDto, IShouldNormalize,ISearchModel<CoinRecharge,int>
    {
        public int? FamilyId { get; set; }


        public Guid? PlayerId { get; set; }


        /// <summary>
        /// 正常化排序使用
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            }
        }

    }
}
