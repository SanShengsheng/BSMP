using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;
using Newtonsoft.Json;

namespace MQKJ.BSMP.Players
{
    public class PlayerExtension : FullAuditedEntity
    {
        /// <summary>
        /// 玩家编号
        /// </summary>
        public virtual Guid PlayerGuid { get; set; }

        [JsonIgnore]
        public virtual Player Player { get; set; }
        /// <summary>
        /// 游戏积分（爱豆）
        /// </summary>
        public virtual int LoveScore { get; set; }

        /// <summary>
        /// 是否解锁微信号
        /// </summary>
        public virtual bool IsUnLock { get; set; }

        /// <summary>
        /// 体力值
        /// </summary>
        [DefaultValue(30)]
        public int Stamina { get; set; }

        /// <summary>
        /// 星座
        /// </summary>
        public string Constellation { get; set; }

        /// <summary>
        /// 自我介绍
        /// </summary>
        [MaxLength]
        public string Introduce { get; set; }
        /// <summary>
        /// 职业
        /// </summary>
        public string Profession { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// 体重
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// 备用字段
        /// </summary>
        public  virtual  int ExtensionFiled1 { get; set; }

        /// <summary>
        /// 备用字段
        /// </summary>
        public virtual int ExtensionFiled2 { get; set; }

        /// <summary>
        /// 备用字段
        /// </summary>
        public virtual int ExtensionFiled3 { get; set; }

        /// <summary>
        /// 备用字段
        /// </summary>
        public virtual string ExtensionFiled4 { get; set; }

        /// <summary>
        /// 备用字段
        /// </summary>
        public virtual string ExtensionFiled5 { get; set; }

        /// <summary>
        /// 备用字段
        /// </summary>
        public virtual string ExtensionFiled6 { get; set; }

    }
}
