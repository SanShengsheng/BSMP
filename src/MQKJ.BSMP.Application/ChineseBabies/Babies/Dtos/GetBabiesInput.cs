using MQKJ.BSMP.Dtos;
using System;

namespace MQKJ.BSMP.ChineseBabies
{
    public class GetBabiesInput : PagedSortedAndFilteredInputDto
    {
        public GetBabiesInput()
        {
            Sorting = "Family.ChargeAmount desc";
        }
        /// <summary>
        /// 玩家编号
        /// </summary>
        public Guid PlayerGuid { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; set; }
        /// <summary>
        /// 查询的关键字
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 状态
        /// 注意：只能和年龄同时传递一个
        /// </summary>
        public BabyState State { get; set; }

        public override int PageIndex { get; set; } = 1;

        public override int PageSize { get; set; } = 1000;

    }
}