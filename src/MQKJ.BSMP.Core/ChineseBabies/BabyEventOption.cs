using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 事件选项表
    /// </summary>
    [Table("BabyEventOptions")]
    public class BabyEventOption:FullAuditedEntity
    {
        /// <summary>
        /// 选项内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 选项图片
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// 选项序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 事件Id
        /// </summary>
        public int BabyEventId { get; set; }
        public BabyEvent BabyEvent { get; set; }

        /// <summary>
        /// 事件奖励id
        /// </summary>
        public int? RewardId { get; set; }

        /// <summary>
        /// 事件消耗Id
        /// </summary>
        public int? ConsumeId { get; set; }
        public Reward Reward { get; set; }
        public Reward Consume { get; set; }
        /// <summary>
        /// 选项编码，导入数据使用
        /// </summary>
        public int? Code { get; set; }
        public virtual ICollection<BabyEventRecord> BabyEventRecords { get; set; }
        /// <summary>
        /// 是否为道具，如果为道具，购买后会在家庭资产中加载
        /// </summary>
        public bool IsProp { get; set; }
    }
}
