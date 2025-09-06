using MQKJ.BSMP.Dtos;
using System;

namespace MQKJ.BSMP.ChineseBabies
{
    public class GetFamilyElseBabiesByPageInput : PagedSortedAndFilteredInputDto
    {
        /// <summary>
        /// 玩家编号
        /// </summary>
        public Guid PlayerGuid { get; set; }
        /// <summary>
        /// 家庭编号
        /// </summary>
        public int FamilyId { get; set; }
        /// <summary>
        /// 当前宝宝编号
        /// </summary>
        public int BabyId { get; set; }
      

        public override int PageIndex { get; set; } = 1;

        public override int PageSize { get; set; } = 1000;
    }
}