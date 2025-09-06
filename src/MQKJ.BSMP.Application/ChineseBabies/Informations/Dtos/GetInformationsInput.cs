
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.ChineseBabies;
using System;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class GetInformationsInput : PagedSortedAndFilteredInputDto, IShouldNormalize, ISearchModel<Information, Guid>
    {

        public InformationType? InformationType { get; set; }

        //自己的Id
        public Guid? PlayerId { get; set; }

        public int? FamilyId { get; set; }

        public override int PageSize { get; set; } 

        /// <summary>
        /// 正常化排序使用
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }
        }

    }
}
