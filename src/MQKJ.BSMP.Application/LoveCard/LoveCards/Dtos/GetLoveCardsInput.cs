
using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.LoveCards;
using MQKJ.BSMP.Players;
using System;

namespace MQKJ.BSMP.LoveCards.Dtos
{
    public class GetLoveCardsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public Guid PlayerId { get; set; }
        public int Gender { get; set; }

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
