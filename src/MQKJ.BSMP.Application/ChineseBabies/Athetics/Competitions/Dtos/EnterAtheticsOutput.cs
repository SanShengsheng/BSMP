using Abp.AutoMapper;
using MQKJ.BSMP.ChineseBabies.Athletics;
using MQKJ.BSMP.ChineseBabies.BabyPropertyBaseDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos
{
    //[AutoMapFrom(typeof(Baby))]
    public class EnterAtheticsOutput
    {
        public EnterAtheticsOutput()
        {
            this.Baby = new EnterAtheticsBaby();
        }

        /// <summary>
        /// 对战次数
        /// </summary>
        public int FightCount { get; set; }

        /// <summary>
        /// 成功次数
        /// </summary>
        public int WinCount { get; set; }

        /// <summary>
        /// 失败次数
        /// </summary>
        public int FailCount { get; set; }
        
        /// <summary>
        /// 是否有车
        /// </summary>
        public bool IsHasCar { get; set; }


        /// <summary>
        /// 是否有房
        /// </summary>
        public bool IsHasHouse { get; set; }

        /// <summary>
        /// 是否有佣人
        /// </summary>
        public bool IsHasButler { get; set; }

        /// <summary>
        /// 是否有管家
        /// </summary>
        public bool IsHasServant { get; set; }

        public bool IsHasSkip { get; set; }

        /// <summary>
        /// 房子代码
        /// </summary>
        //public int HouseCode { get; set; }
        /// <summary>
        /// 皮肤代码
        /// </summary>
        public int SkinCode { get; set; }

        /// <summary>
        /// 段位
        /// </summary>
        public DanGrading DanGrading { get; set; }


        /// <summary>
        /// 竞技场是否开启
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        public EnterAtheticsBaby Baby { get; set; }

        public List<string> DangradingDescriptions { get; set; }
    }

    [AutoMapFrom(typeof(Baby))]
    public class EnterAtheticsBaby
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public double AgeDouble { get; set; }
        public string AgeString { get; set; }

        /// <summary>
        /// 智力
        /// </summary>
        public int Intelligence { get; set; }
        /// <summary>
        /// 体魄
        /// </summary>
        public int Physique { get; set; }
        /// <summary>
        /// 想象
        /// </summary>
        public int Imagine { get; set; }
        /// <summary>
        /// 意志
        /// </summary>
        public int WillPower { get; set; }
        /// <summary>
        /// 情商
        /// </summary>
        public int EmotionQuotient { get; set; }
        /// <summary>
        /// 魅力
        /// </summary>
        public int Charm { get; set; }
        /// <summary>
        /// 健康
        /// </summary>
        public int Healthy { get; set; }
        /// <summary>
        /// 精力
        /// </summary>
        public int Energy { get; set; }
    }
}
