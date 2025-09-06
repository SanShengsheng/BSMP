
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.ChineseBabies;
using System;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class GetProfessionsInput : PagedSortedAndFilteredInputDto, IShouldNormalize, ISearchModel<Profession, int>
    {

        public Gender Gender { get; set; }

        public int FamilyId { get; set; }

        public Guid PlayerId { get; set; }

        public CostType CostType { get; set; }

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
