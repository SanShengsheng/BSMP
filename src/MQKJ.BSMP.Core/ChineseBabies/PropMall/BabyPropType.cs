using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 道具类别表
    /// </summary>
    [Table("BabyPropTypes")]
    public class BabyPropType : FullAuditedEntity
    {
        public string Title { get; set; }
        
        public string Description { get; set; }

        public string Img { get; set; }

        public int Code { get; set; }

        public int Sort { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 装备对象
        /// </summary>
        public EquipmentAbleObject EquipmentAbleObject { get; set; }
        /// <summary>
        /// 最大可装备数量
        /// </summary>
        [DefaultValue(1)]
        public bool MaxEquipmentCount { get; set; }

    }
    /// <summary>
    /// 装备对象
    /// </summary>
    public enum EquipmentAbleObject
    {
        Any = 0,
        Baby = 1,
        Family = 2
    }
}
