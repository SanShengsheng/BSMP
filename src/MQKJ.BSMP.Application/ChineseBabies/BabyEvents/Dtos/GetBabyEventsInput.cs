
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class GetBabyEventsInput : PagedSortedAndFilteredInputDto, IShouldNormalize, ISearchModel<BabyEvent, int>
    {

        public IncidentType? IncidentType { get; set; }
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
