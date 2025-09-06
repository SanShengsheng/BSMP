
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.Common.SensitiveWords;

namespace MQKJ.BSMP.Common.SensitiveWords.Dtos
{
    public class GetSensitiveWordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize,ISearchModel<SensitiveWord,int>
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
