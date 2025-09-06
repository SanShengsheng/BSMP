using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.Tags;
using System.ComponentModel.DataAnnotations;

namespace MQKJ.BSMP.Tags.Dto
{
    public class GetTagsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        /// <summary>
        /// 模糊搜索使用的关键字
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 是否加载相关项
        /// </summary>
        public bool WithDetail { get; set; }

        //// custom codes 

        //// custom codes end

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
