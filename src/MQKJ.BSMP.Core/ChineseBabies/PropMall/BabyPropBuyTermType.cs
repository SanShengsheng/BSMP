using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 道具条件类型表
    /// </summary>
    [Table("BabyPropBuyTermTypes")]
    public class BabyPropBuyTermType : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 编码
        /// </summary>
        public int Code  { get; set; }
        /// <summary>
        /// 条件名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 条件描述
        /// </summary>
        public string Description { get; set; }

        public BabyPropBuyTermTypeType Type { get; set; }

        public int? BabyPropTypeId { get; set; }

        public BabyPropType BabyPropType { get; set; }
    }
    public enum BabyPropBuyTermTypeType
    {
        /// <summary>
        /// 道具级别
        /// </summary>
        PropLevel=1,
        BabyAge=2,
        ArenaRank=3,
        FatherProfessionLevel=4,
        MotherProfessionLevel=5
    }
}
