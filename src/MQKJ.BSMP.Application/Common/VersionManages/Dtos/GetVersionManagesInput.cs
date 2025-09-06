
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.Common.Dtos
{
    public class GetVersionManagesInput : PagedSortedAndFilteredInputDto, IShouldNormalize, ISearchModel<VersionManage, int>
    {
        public string Version { get; set; }

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
