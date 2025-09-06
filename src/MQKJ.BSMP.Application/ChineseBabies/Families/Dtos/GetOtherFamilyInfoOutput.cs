using Abp.AutoMapper;
using MQKJ.BSMP.ChineseBabies.Athletics;
using MQKJ.BSMP.ChineseBabies.BabyPropertyBaseDtos;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Families.Dtos
{
    [AutoMapFrom(typeof(Family))]
    public class GetOtherFamilyInfoOutput
    {
        public int Id { get; set; }

        public GetOtherFamilyInfoPlayer Father { get; set; }
        public GetOtherFamilyInfoPlayer Mother { get; set; }
        /// <summary>
        /// 存款
        /// </summary>
        private double Dseposit { get; set; }

        public double Happiness { get; set; }


        public string HappinessTitle { get; set; }

        public int Level { get; set; }

        public GetOtherFamilyInfoBaby LatestBaby { get; set; }

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
        /// <summary>
        /// 是否有皮肤
        /// </summary>
        public bool IsHasSkip { get; set; }
        /// <summary>
        /// 声望值
        /// </summary>
        public int Prestiges { get; set; }
        /// <summary>
        /// 今日被膜拜次数
        /// </summary>
        public int TimesToday { get; set; }
        /// <summary>
        /// 每日膜拜次数限制
        /// </summary>
        public int Maxlimit { get; set; }

        /// <summary>
        /// 是否已经膜拜过
        /// </summary>
        public bool IsWorshiped { get; set; }
        /// <summary>
        /// 是否到达今日最大膜拜次数
        /// </summary>
        public bool IsLimited { get; set; }
        /// <summary>
        /// 每日可膜拜的最大次数
        /// </summary>
        public int ToWorshipMax { get; set; }


    }

    [AutoMapFrom(typeof(Baby))]
    public class GetOtherFamilyInfoBaby : BabyPropertyBaseDto<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public double AgeDouble { get; set; }
        public string AgeString { get; set; }

        /// <summary>
        /// 段位
        /// </summary>
        public DanGrading DanGrading { get; set; }
        /// <summary>
        /// 皮肤
        /// </summary>
        public int Skin { get; set; }
    }

    [AutoMapFrom(typeof(Player))]
    public class GetOtherFamilyInfoPlayer
    {
        public string NickName { get; set; }


        public string HeadUrl { get; set; }
    }
}
