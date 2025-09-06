using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.EnumHelper;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 家庭表
    /// </summary>
    [Table("Families")]
    public class Family : FullAuditedEntity<int>
    {
        public Guid FatherId { get; set; }
        public Guid MotherId { get; set; }
        public virtual Player Father { get; set; }
        public virtual Player Mother { get; set; }
        /// <summary>
        /// 存款
        /// </summary>
        private double _deposit { get; set; }
        public double Deposit
        {
            get => _deposit < 0 ? 0 : _deposit;
            set => SetDeposite(value);
        }
        /// <summary>
        /// 幸福度
        /// </summary>
        private double _happiness { get; set; }

        public double Happiness
        {
            get => _happiness;
            set => _happiness = value > 100 ? 100 : (value < 0 ? 0 : value);
        }
        [NotMapped]
        public string HappinessTitle
        {
            get
            {
                var res = "";
                if (Happiness == 100)
                {
                    res = "幸福恩爱";
                }
                else if (Happiness >= 75 && Happiness <= 99)
                {
                    res = "夫唱妇随";
                }
                else if (Happiness >= 50 && Happiness <= 74)
                {
                    res = "相敬如宾";
                }
                else if (Happiness >= 25 && Happiness <= 49)
                {
                    res = "貌合神离";
                }
                else if (Happiness >= 0 && Happiness <= 24)
                {
                    res = "同床异梦";
                }
                return res;
            }
        }
        private FamilyLevel _level { get; set; }
        /// <summary>
        /// 档次
        /// </summary>
        public FamilyLevel Level
        {
            get => _level;
            set => _level = value;
        }

        /// <summary>
        /// 家庭类型 1-单亲2-双亲
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 声望值
        /// </summary>
        public int Prestiges { get; set; }

        /// <summary>
        /// 备注(代理用)
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 充值金额(总共)
        /// </summary>
        public double ChargeAmount { get; set; }
        /// <summary>
        /// 补贴金额
        /// </summary>
        public double TotalSubsidyAmount { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 家庭状态
        /// </summary>
        public FamilyState FamilyState { get; set; }

        /// <summary>
        /// 虚拟充值(总)
        /// </summary>
        public double VirtualRecharge { get; set; }

        public virtual ICollection<Baby> Babies { get; set; }

        public AddOnStatus AddOnStatus { get; set; }
        public string AddOnNote { get; set; }

        [NotMapped]
        public Baby Baby
        {
            get
            {
                return Babies?.FirstOrDefault(b => b.State == BabyState.UnderAge);
            }
        }

        /// <summary>
        /// 最后一个宝宝
        /// </summary>
        [NotMapped]
        public Baby LatestBaby
        {
            get
            {
                return Babies?.OrderByDescending(b => b.CreationTime).FirstOrDefault();
            }
        }

        /// <summary>
        /// 家庭等级
        /// </summary>
        [NotMapped]
        public FamilyLevel FamilyLevel
        {
            get
            {
                if (Deposit <= 300000)
                {
                    return FamilyLevel.Poor;
                }
                if (Deposit <= 3000000)
                {
                    return FamilyLevel.WellOff;
                }

                return FamilyLevel.Rich;
            }
        }
        private FamilyLevel GetFamilyLevel(FamilyLevel level, double componentedMoney)
        {
            switch (level)
            {
                case FamilyLevel.Poor:
                    if (componentedMoney >= 2200000)
                    {
                        return FamilyLevel.Rich;
                    }
                    if (componentedMoney >= 600000)
                    {
                        return FamilyLevel.WellOff;
                    }
                    break;
                case FamilyLevel.WellOff:
                    if (componentedMoney <= 300000)
                    {
                        return FamilyLevel.Poor;
                    }
                    if (componentedMoney >= 2200000)
                    {
                        return FamilyLevel.Rich;
                    }
                    break;
                case FamilyLevel.Rich:
                    if (componentedMoney <= 300000)
                    {
                        return FamilyLevel.Poor;
                    }
                    if (componentedMoney <= 1500000)
                    {
                        return FamilyLevel.WellOff;
                    }
                    break;
            }
            return level;
        }

        private void SetDeposite(double value)
        {
            _deposit = value < 0 ? 0 : value;
            var level = GetFamilyLevel(_level, _deposit);
            Level = level;
        }
    }

    public enum AddOnStatus
    {
        [EnumDescription("未运行")]
        NotRunning = 0,
        [EnumDescription("运行中")]
        Running = 1,
        [EnumDescription("已隐藏")]
        Hide = 99
    }

    public enum FamilyLevel
    {
        [EnumDescription("所有")]
        /// <summary>
        /// 所有
        /// </summary>
        All = -1,

        [EnumDescription("贫困家庭")]
        /// <summary>
        /// 平困
        /// </summary>
        Poor = 0,
        [EnumDescription("小康家庭")]
        /// <summary>
        /// 小康
        /// </summary>
        WellOff = 1,
        [EnumDescription("富豪家庭")]
        /// <summary>
        /// 富豪
        /// </summary>
        Rich = 2
    }

    public enum RechargeRange
    {
        /// <summary>
        /// 所有
        /// </summary>
        [EnumDescription("所有")]
        All = 0,

        /// <summary>
        /// 0-100
        /// </summary>
        [EnumDescription("0-100")]
        FirstRange = 1,



        /// <summary>
        /// 100-500
        /// </summary>
        [EnumDescription("100-500")]
        SecondRange = 2,



        /// <summary>
        /// 500-1000
        /// </summary>
        [EnumDescription("500-1000")]
        ThirdRange = 3,
        /// <summary>
        /// 1000以上
        /// </summary>
        [EnumDescription("1000以上")]
        FourthRange =4,
        /// <summary>
        /// 无
        /// </summary>
        [EnumDescription("无")]
        None = 5,
           
    }

    /// <summary>
    /// 解散家庭的类型
    /// </summary>
    public enum DismissFamilyType
    {
        /// <summary>
        /// 协议解散
        /// </summary>
        AgreementDismiss =  1,

        /// <summary>
        /// 强制解散
        /// </summary>
        ForceDismiss = 2
    }

    /// <summary>
    /// 家庭状态
    /// </summary>
    public enum FamilyState
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 已解散
        /// </summary>
         Dismissed = 1,

         /// <summary>
         /// 解散中
         /// </summary>
         Dismissing = 2,

         /// <summary>
         /// 取消解散
         /// </summary>
         CancelDismiss = 3,


         /// <summary>
         /// 拒绝解散
         /// </summary>
         RefuseDismiss = 4

    }

}
