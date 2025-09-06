using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies.Backpack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 商品功能(属性)表
    /// </summary>
    [Table("BabyPropFeatureTypes")]
    public class BabyPropFeatureType : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 功能值
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 功能描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 特性类别
        /// </summary>
        public FeatureType Type { get; set; }
        /// <summary>
        /// 事件加成类型
        /// </summary>
        public EventAdditionType EventAdditionType { get; set; }
        /// <summary>
        /// 组
        /// </summary>
        public int Group { get; set; }
        /// <summary>
        /// 是否为附加属性
        /// </summary>
        public bool IsAddition { get; set; }
    }

    public enum FeatureType
    {
        /// <summary>
        /// 功能
        /// </summary>
        Function=0,
        /// <summary>
        /// 属性
        /// </summary>
        BabyProperty=1,
    }
    /// <summary>
    /// 事件加成类型
    /// </summary>
    public enum EventAdditionType
    {
        /// <summary>
        /// 成长事件
        /// </summary>
        GrowUp = 1,
        /// <summary>
        /// 学习事件
        /// </summary>
        Study = 2,
        /// <summary>
        /// 成长&学习事件
        /// </summary>
        GrowUpAndStudy = 3,
        /// <summary>
        /// 竞技场
        /// </summary>
        Arena = 4,

        /// <summary>
        /// 道具加成
        /// </summary>
        PropAddititon = 5
    }
}
