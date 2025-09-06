
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.ChineseBabies;
using System;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class GetAutoRunnerRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize, ISearchModel<AutoRunnerRecord, Guid>
    {
        public Guid? PlayerId { get; set; }
        public int? FamilyId { get; set; }
        public int? BabyId { get; set; }
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
