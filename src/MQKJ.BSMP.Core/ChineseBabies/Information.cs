using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Players;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 消息表
    /// </summary>
    [Table("Informations")]
    public class Information : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发送者
        /// </summary>
        public Guid? SenderId { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        public Guid? ReceiverId { get; set; }

        /// <summary>
        /// 家庭Id
        /// </summary>
        public int? FamilyId { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public InformationType Type { get; set; }
        public Player Sender { get; set; }
        public Player Receiver { get; set; }
        public InformationState State { get; set; }
        /// <summary>
        /// 系统消息类型
        /// 注意此类型消息，如果累计了大于1条未读，在调用hasNewMessage时会
        /// 置为已读
        /// </summary>
        public SystemInformationType SystemInformationType { get; set; }
        /// <summary>
        /// 是否为弹窗
        /// </summary>
        public NoticeType NoticeType { get; set; }

        public int? BabyEventId { get; set; }

        public BabyEvent BabyEvent { get; set; }
        /// <summary>
        /// 备注
        /// 当SystemInformationType为payoff时，记录的值为【双方】父母的工资
        /// </summary>
        public string Remark { get; set; }

        public string Image { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        ///// <summary>
        ///// 播放间隔
        ///// </summary>
        //public int Interval { get; set; }

        /// <summary>
        /// 宝宝Id
        /// </summary>
        public int? BabyId { get; set; }
    }

    public enum InformationState
    {
        Create = 1,
        Readed = 2,
        Cancel = 99
    }

    public enum NoticeType
    {
        Default=0,
        /// <summary>
        /// 弹框
        /// </summary>
        Popout=1,

        /// <summary>
        /// 置顶
        /// </summary>
        Roof = 2,

    }
    public enum InformationType
    {
        /// <summary>
        ///系统消息 
        /// </summary>
        [Description("系统消息")]
        System = 2,
        /// <summary>
        /// 家庭事件消息
        /// </summary>
        [Description("家庭事件消息")]
        Event = 1,
        /// <summary>
        ///跑马灯消息 
        /// </summary>
        [Description("跑马灯消息")]
        Barrage = 3,
        /// <summary>
        ///家庭充值消息 
        /// </summary>
        [Description("家庭充值消息")]
        Recharge = 4,
        /// <summary>
        ///家庭解散 
        /// </summary>
        [Description("家庭解散消息")]
        DismissFamily = 5,
        ///// <summary>
        /////竞技场消息 
        ///// </summary>
        //[Description("竞技场消息")]
        //Athletics = 5,
        ///// <summary>
        /////竞技场跑马灯消息 
        ///// </summary>
        //[Description("竞技场跑马灯消息")]
        //AthleticsBarrage = 6


    }
    /// <summary>
    /// 系统消息子类型
    /// </summary>
    public enum SystemInformationType
    {
        /// <summary>
        /// 默认，无意义
        /// </summary>
        Default=0,
        /// <summary>
        /// 发工资
        /// </summary>
        [Description("发工资")]
        PayOff = 1,
        [Description("生日")]
        BirthDay=2,
        [Description("事件完成")]
        EventCompleted=3,
        [Description("宝宝属性更新")]
        BabyPropertyUpdate=4,
        [Description("家庭充值消息")]
        Recharge = 5,
        [Description("发起解散家庭")]
        DismissFamily = 6,
        [Description("解散成功")]
        DismissFamilySuccess = 7,
        [Description("大礼包")]
        BigBag = 8,
    }
}
