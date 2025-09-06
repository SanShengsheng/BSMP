
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.Common.OperationActivities;

namespace MQKJ.BSMP.Common.OperationActivities.Dtos
{
    public class GetOperationActivitiesInput : PagedSortedAndFilteredInputDto, IShouldNormalize,ISearchModel<OperationActivity,int>
    {
        /// <summary>
        /// 活动类型
        /// </summary>
        public ActivityType? ActivityType { get; set; }
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
