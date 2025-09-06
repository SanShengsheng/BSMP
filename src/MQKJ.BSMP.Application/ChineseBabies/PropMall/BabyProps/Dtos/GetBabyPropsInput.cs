
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class GetBabyPropsInput : PagedSortedAndFilteredInputDto, IShouldNormalize,ISearchModel<BabyProp,int>
    {
        public int PropTypeId { get; set; }

        public int? BabyId { get; set; }

        public int? FamilyId { get; set; }

        public Gender? Gender { get; set; }

        /// <summary>
        /// 正常化排序使用
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Level";
            }
        }

    }
}
