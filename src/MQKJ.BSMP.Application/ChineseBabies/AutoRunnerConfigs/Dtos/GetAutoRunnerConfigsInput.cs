
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.ChineseBabies;
using System;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class GetAutoRunnerConfigsInput : PagedSortedAndFilteredInputDto, IShouldNormalize, ISearchModel<AutoRunnerConfig, int>
    {
        public int? FamilyId { get; set; }
        public Guid? PlayerId { get; set; }
        public int? GroupId { get; set; }
        public int? ProfressionId { get; set; }
        public AutoRunnerState? State { get; set; }
        public ConsumeLevel? ConsumeLevel { get; set; }
        public FamilyLevel? FamilyLevel { get; set; }
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
