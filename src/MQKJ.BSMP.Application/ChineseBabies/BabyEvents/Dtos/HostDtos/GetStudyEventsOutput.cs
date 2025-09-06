using MQKJ.BSMP.ChineseBabies.Asset.BabyAssetFeatures;
using MQKJ.BSMP.ChineseBabies.Babies.Dtos.HostDtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MQKJ.BSMP.ChineseBabies
{
    public class GetStudyEventsOutput
    {
        public GetStudyEventsOutput()
        {
            StudyEvents = new List<GetStudyEventsOutputStudyEvent>();
            StudyEmergencies = new List<GetStudyEventsOutputStudyEvent>();
            Setting = new GetStudyEventsOutputEventSetting();
        }

        public GetStudyEventsOutputEventSetting Setting { get; set; }
        public GetStudyEventsOutputGroupInfo GroupInfo { get; set; }
        private List<GetStudyEventsOutputStudyEvent> _studyEvents;

        public List<GetStudyEventsOutputStudyEvent> StudyEvents
        {
            // 道具特性 生效
            get
            {
                var res = _studyEvents.OrderByDescending(o => o.Priority).ToList();
                res.ForEach(s =>
                {
                    if (AssetFeature != null)
                    {
                        // cd
                        s.Event.CountDownOrigin = s.Event.CountDown;
                        s.Event.CountDown = s.Event.CountDown - Convert.ToInt32(AssetFeature.CD * s.Event.CountDown);
                        //  金币&精力
                        s.Options.ToList().ForEach(d =>
                        {
                            // 消耗-金币
                            d.Consume.CoinCountOrigin = d.Consume.CoinCount;
                            d.Consume.CoinCount -= Convert.ToInt32(AssetFeature.Coin * d.Consume.CoinCount);
                            // 消耗-精力
                            d.Consume.EnergyOrigin = d.Consume.Energy;
                            d.Consume.Energy -= Convert.ToInt32(AssetFeature.Energy * d.Consume.Energy);
                            // 奖励-属性
                            var propertyAddtion = AssetFeature.PropertyAddtion;
                            d.Reward.PropertyAddtion = propertyAddtion;
                            d.Reward.Charm += Convert.ToInt32(d.Reward.Charm * propertyAddtion);
                            d.Reward.EmotionQuotient += Convert.ToInt32(d.Reward.EmotionQuotient * propertyAddtion);
                            d.Reward.Imagine += Convert.ToInt32(d.Reward.Imagine * propertyAddtion);
                            d.Reward.Intelligence += Convert.ToInt32(d.Reward.Intelligence * propertyAddtion);
                            d.Reward.Physique += Convert.ToInt32(d.Reward.Physique * propertyAddtion);
                            d.Reward.WillPower += Convert.ToInt32(d.Reward.WillPower * propertyAddtion);

                        });
                    }
                });
                return res;
            }
            set => _studyEvents = value;
        }
        private List<GetStudyEventsOutputStudyEvent> _studyEmergencies;
        public List<GetStudyEventsOutputStudyEvent> StudyEmergencies
        {
            get => _studyEmergencies.OrderByDescending(o => o.Priority).ToList();
            set => _studyEmergencies = value;
        }
        public AssetFeatureDto AssetFeature { get; set; }
    }

    public class GetStudyEventsOutputEventSetting
    {
        /// <summary>
        /// 系数，一胎系数=1；二胎系数=2；以此类推
        /// </summary>
        public int Coefficient { get; set; } = 1;
    }

    public class GetStudyEventsOutputGroupInfo
    {
        public string GroupName { get; set; }
    }

    public class GetStudyEventsOutputStudyEvent
    {
        public GetStudyEventsOutputStudyEvent()
        {

        }
        public StudyEventBabyEvent Event { get; set; }

        public IEnumerable<StudyEventOptions> Options
        {
            get => _options.OrderBy(s => s.Code).ToList();
            set => _options = value;
        }

        private IEnumerable<StudyEventOptions> _options;

        public GetStudyEventsOutputRecord Record { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        [NotMapped]
        public int Priority
        {
            get
            {
                if (Event.IsBlock && Record == null)
                {
                    return 999;
                }
                else if (Record != null && Record.State == EventRecordState.WaitOther)
                {
                    return 998;
                }
                else if (Record != null && Record.State == EventRecordState.Handling)
                {
                    return 997;
                }
                else if (Record != null && Record.State == EventRecordState.Handled)
                {
                    return 1;
                }
                else
                {
                    return 996;
                }
            }
        }
    }
    /// <summary>
    /// 事件记录
    /// </summary>
    public class GetStudyEventsOutputRecord
    {
        /// <summary>
        /// 对方的选择项
        /// </summary>
        public int? TheOtherSelectOptionId { get; set; }
        public long? EndDateTime { get; set; }
        public EventRecordState State { get; set; }
        /// <summary>
        /// 爸爸选什么
        /// </summary>
        public int? FatherOptionId { get; set; }
        /// <summary>
        /// 妈妈选什么
        /// </summary>
        public int? MotherOptionId { get; set; }
    }
    /// <summary>
    /// 选项
    /// </summary>
    public class StudyEventOptions
    {
        public int Id { get; set; }
        /// <summary>
        /// 选项内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 奖励
        /// </summary>
        public StudyEventOptionsAward Reward { get; set; }
        /// <summary>
        /// 消耗
        /// </summary>
        public StudyEventOptionsConsume Consume { get; set; }
        [JsonIgnore]
        public int Code { get; set; }
    }
    /// <summary>
    /// 消耗
    /// </summary>
    public class StudyEventOptionsConsume
    {
        public int Id { get; set; }

        /// <summary>
        /// 金币数量
        /// </summary>
        public int CoinCount
        {
            get => coinCount;
            set => coinCount = value;
        }

        private int coinCount { get; set; }
        /// <summary>
        /// 精力
        /// </summary>
        public int Energy { get; set; }
        public virtual int CoinCountOrigin { get; internal set; }
        public virtual int EnergyOrigin { get; internal set; }
    }

    /// <summary>
    /// 处理事件对宝宝属性加成
    /// </summary>
    public class HanleEventBabyBaseProperty : BabyPropertyDto
    {

    }
    /// <summary>
    /// 奖励
    /// </summary>
    public class StudyEventOptionsAward : BabyPropertyDto
    {
        //    public HanleEventBabyBaseProperty BabyBaseProperty { get; set; }

        /// <summary>
        /// 金币数量
        /// </summary>
        public int CoinCount { get; set; }
        public virtual double PropertyAddtion { get; internal set; }
        ///// <summary>
        ///// 精力
        ///// </summary>
        //public int Energy { get; set; }
        ///// <summary>
        ///// 健康
        ///// </summary>
        //public int Healthy { get; set; }
    }

    public class StudyEventBabyEvent
    {
        //TODO需特殊处理
        /// <summary>
        /// 配图
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 学习次数
        /// </summary>
        public int Time { get; set; } = 0;


        /// <summary>
        /// 事件编号
        /// </summary>
        public int Id { get; set; }
        ///// <summary>
        ///// 事件类型
        ///// </summary>
        //public IncidentType Type { get; set; }

        /// <summary>
        /// 是否阻断
        /// </summary>
        public bool IsBlock { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public OperationType OperationType { get; set; }
        /// <summary>
        /// 旁白
        /// </summary>
        public string Aside { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 倒计时
        /// </summary>
        public int CountDown { get; set; }
        /// <summary>
        /// 触发条件类型
        /// </summary>
        public ConditionType ConditionType { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 允许学习的最大次数
        /// </summary>
        public int? StudyAllowMaxTime { get; set; }
        /// <summary>
        /// 学习类型
        /// </summary>
        public StudyType StudyType { get; set; }
        /// <summary>
        /// 学习类型描述
        /// </summary>
        public string StudyTypeDescription { get; set; }
        public virtual int CountDownOrigin { get; internal set; }
    }

}