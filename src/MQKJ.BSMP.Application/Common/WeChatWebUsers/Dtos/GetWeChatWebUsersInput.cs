
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.Common;

namespace MQKJ.BSMP.Common.Dtos
{
    public class GetWeChatWebUsersInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {

        public string NickName { get; set; }

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
