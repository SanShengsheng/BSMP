using System;
using System.ComponentModel.DataAnnotations;

namespace MQKJ.BSMP.ChineseBabies
{
    public class GiveBabyNameInput
    {
        /// <summary>
        /// 宝宝编号
        /// </summary>
        public int BabyId { get; set; }
        /// <summary>
        /// 玩家编号
        /// </summary>
        public Guid PlayerGuid { get; set; }
        /// <summary>
        ///  宝宝名字
        /// </summary>
        [MaxLength(8)]
        public string NewName { get; set; }
    }
}