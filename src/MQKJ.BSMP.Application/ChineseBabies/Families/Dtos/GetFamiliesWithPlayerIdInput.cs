using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Families.Dtos
{
    public class GetFamiliesWithPlayerIdInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public int? BabyAge { get; set; }

        /// <summary>
        /// 外挂状态
        /// </summary>
        public AddOnStatus? AddOnState { get; set; }

        public int? MaxDeposit { get; set; }

        public int? MinDeposit { get; set; }

        public string Remark { get; set; }

        public int? AgentId { get; set; }

        public string BabyName { get; set; }

        /// <summary>
        /// 玩家Id
        /// </summary>
        //[Required]
        public Guid? PlayerId { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime";
            }
        }
    }
}
