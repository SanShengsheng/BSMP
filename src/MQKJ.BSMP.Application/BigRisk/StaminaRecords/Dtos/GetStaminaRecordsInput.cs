
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.StaminaRecords;

namespace MQKJ.BSMP.StaminaRecords.Dtos
{
    public class GetStaminaRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {

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
