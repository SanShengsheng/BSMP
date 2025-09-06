using MQKJ.BSMP.ChineseBabies.Asset.BabyAssetFeatures;
using MQKJ.BSMP.ChineseBabies.Babies.Dtos.HostDtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;

namespace MQKJ.BSMP.ChineseBabies
{
    public class GetGrowUpEventsOutput
    {
        public GetGrowUpEventsOutput()
        {
            GrowUpEvents = new List<GetGrowUpEventsOutputGrowUpEvent>();
            GrowUpEmergencies = new List<GetGrowUpEventsOutputGrowUpEvent>();

        }
        public GetGrowUpEventsOutputGroupInfo GroupInfo { get; set; }

        private IEnumerable<GetGrowUpEventsOutputGrowUpEvent> _growUpEvents;

        public IEnumerable<GetGrowUpEventsOutputGrowUpEvent> GrowUpEvents
        {
            // 道具特性 生效
            get
            {
                var res = _growUpEvents.OrderByDescending(o => o.Priority);
                foreach (var s in res)
                {
                    if (AssetFeature != null)
                    {
                        // cd
                        s.Event.CountDownOrigin = s.Event.CountDown;
                        s.Event.CountDown = s.Event.CountDown - Convert.ToInt32(AssetFeature.CD * s.Event.CountDown);
                        //  金币&精力
                        foreach (var d in s.Options)
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
                        };
                    }
                };
                return res;
            }
            set => _growUpEvents = value;
        }

        private IEnumerable<GetGrowUpEventsOutputGrowUpEvent> _growUpEmergencies;
        public IEnumerable<GetGrowUpEventsOutputGrowUpEvent> GrowUpEmergencies
        {
            get => _growUpEmergencies.OrderByDescending(o => o.Priority);
            set => _growUpEmergencies = value;
        }
        /// <summary>
        /// 道具特性
        /// </summary>
        public AssetFeatureDto AssetFeature { get; set; }
    }
    public class GetGrowUpEventsOutputGroupInfo
    {
        public string GroupName { get; set; }
    }

    public class GetGrowUpEventsOutputGrowUpEvent
    {
        public GetGrowUpEventsOutputGrowUpEvent()
        {
            Event = new GetGrowUpEventsOutputEvent();
            Options = new List<GetGrowUpEventsOutputEventOptions>();
            Record = new GetGrowUpEventsOutputRecord();
        }
        public GetGrowUpEventsOutputEvent Event { get; set; }

        private IEnumerable<GetGrowUpEventsOutputEventOptions> _options;

        public IEnumerable<GetGrowUpEventsOutputEventOptions> Options
        {
            get => _options.OrderBy(s => s.Code);
            set => _options = value;
        }

        public GetGrowUpEventsOutputRecord Record { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        [NotMapped]
        public int Priority
        {
            get
            {
                if (Event.IsBlock && (Record == null || Record.State != EventRecordState.Handled))
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
    public class GetGrowUpEventsOutputRecord
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
    public class GetGrowUpEventsOutputEventOptions
    {
        public int Id { get; set; }
        /// <summary>
        /// 选项内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 奖励
        /// </summary>
        public GrowUpEventOptionsAward Reward { get; set; }
        /// <summary>
        /// 消耗
        /// </summary>
        public GrowUpEventOptionsConsume Consume { get; set; }
        [JsonIgnore]
        public int Code { get; set; }
    }
    /// <summary>
    /// 消耗
    /// </summary>
    public class GrowUpEventOptionsConsume
    {
        /// <summary>
        /// 金币数量
        /// </summary>
        public int CoinCount { get; set; }
        public virtual int CoinCountOrigin { get; set; }

        /// <summary>
        /// 精力
        /// </summary>
        public int Energy { get; set; }
        public virtual int EnergyOrigin { get; internal set; }
    }

    /// <summary>
    /// 处理事件对宝宝属性加成
    /// </summary>
    public class HanleGrowUpEventBabyBaseProperty : BabyPropertyDto
    {

    }
    /// <summary>
    /// 奖励
    /// </summary>
    public class GrowUpEventOptionsAward : BabyPropertyDto
    {
        //public HanleGrowUpEventBabyBaseProperty BabyBaseProperty { get; set; }
        /// <summary>
        /// 金币数量
        /// </summary>
        public int CoinCount { get; set; }
        public virtual double PropertyAddtion { get; internal set; }

        /// <summary>
        /// 精力
        /// </summary>
        //public int Energy { get; set; }
        ///// <summary>
        ///// 健康
        ///// </summary>
        //public int Healthy { get; set; }
    }

    public class GetGrowUpEventsOutputEvent
    {
        //TODO需特殊处理
        /// <summary>
        /// 配图
        /// </summary>
        public string Image { get; set; }

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
        private int _countDown;

        /// <summary>
        /// 倒计时  减去了属性加成
        /// </summary>
        public int CountDown
        {
            get => _countDown;
            set => _countDown = value;
        }
        /// <summary>
        /// 倒计时 原值
        /// </summary>
        public int CountDownOrigin { get; set; }
        /// <summary>
        /// 触发条件类型
        /// </summary>
        public ConditionType ConditionType { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

    }
}