using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.CommonEnum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.ActiveApply
{
    /// <summary>
    /// 恋习大冒险-活动报名
    /// </summary>
    [Table("RiskActiveApplys")]
    public class RiskActiveApply : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 第x期
        /// </summary>
        public int Season { get; set; }
        /// <summary>
        /// 姓名/昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string WechatId { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string TeleNumber { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public EGender Gender { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime BirthDateTime { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 兴趣爱好
        /// </summary>
        public string Hobbies { get; set; }

        /// <summary>
        /// 自我介绍
        /// </summary>
        public string SelfIntroduction { get; set; }

        /// <summary>
        /// 交友宣言
        /// </summary>
        public string DeclarationOfDating { get; set; }

        /// <summary>
        /// 来源，从贴吧之类的
        /// </summary>
        public string Source { get; set; } = "gongZhongHao";

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
    }
}
