
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.Friends;
using System;

namespace MQKJ.BSMP.Friends.Dtos
{
    public class GetFriendsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        /// <summary>
        /// 玩家Id
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// 正常化排序使用
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime";
            }
        }

    }
}
