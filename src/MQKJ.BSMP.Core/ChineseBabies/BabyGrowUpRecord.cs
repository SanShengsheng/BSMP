using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 成长记录表
    /// </summary>
    [Table("BabyGrowUpRecords", Schema = "dbo")]
    public class BabyGrowUpRecord : BabyPropertyBase<Guid>
    {
        public int BabyId { get; set; }

        /// <summary>
        /// 玩家的编号
        /// </summary>
        public int PlayerGuid { get; set; }

        public Guid? PlayerId { get; set; }
        /// <summary>
        /// 触发类型
        /// </summary>
        public TriggerType TriggerType { get; set; }
    }
    public enum TriggerType
    {
        /// <summary>
        /// 出生拥有
        /// </summary>
        BirthOwn=0,
        /// <summary>
        /// 父母职业附加
        /// </summary>
        ParentsProfessionAddition = 1,
        /// <summary>
        /// 继承父母职业加成
        /// </summary>
        InheritParentsProfessionAddition = 3,
        /// <summary>
        /// 继承家庭资产
        /// </summary>
        InheritFamilyAssetddition = 4,

        /// <summary>
        /// 成长事件
        /// </summary>
        BabyEvent = 100,
       /// <summary>
       /// 重新计算，在数据错误时，重新计算所有的宝宝属性
       /// </summary>
        ReCaculate=200,
    }
}