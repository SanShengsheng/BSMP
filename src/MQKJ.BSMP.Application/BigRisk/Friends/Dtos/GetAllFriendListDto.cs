using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Friends.Dtos
{
    public class GetAllFriendListDto : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public string InviterName { get; set; }

        public string InviteeName { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            }
        }
    }
}
