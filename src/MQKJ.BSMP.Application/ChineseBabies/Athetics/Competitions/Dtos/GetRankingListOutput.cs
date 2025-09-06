using Abp.AutoMapper;
using MQKJ.BSMP.ChineseBabies.Athletics;
using MQKJ.BSMP.ChineseBabies.BabyPropertyBaseDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos
{
    [AutoMapFrom(typeof(Competition))]
    public class GetRankingListOutput
    {
        public Guid Id { get; set; }

        public GetRankingListBaby Baby { get; set; }

        /// <summary>
        /// 总积分
        /// </summary>
        public int GamePoint { get; set; }

        /// <summary>
        /// 排名
        /// </summary>
        //public int RankingNumber { get; set; }

        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 自己的排名
        /// </summary>
        public int SelfNumber { get; set; }

        public int FamilyId { get; set; }

        public int BabyId { get; set; }

        /// <summary>
        /// 段位
        /// </summary>
        public DanGrading DanGrading { get; set; }
    }

    [AutoMapFrom(typeof(Baby))]
    public class GetRankingListBaby : BabyPropertyBaseDto<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int Age
        {
            get
            {
                return Convert.ToInt32(AgeDouble.ToString().Split('.')[0]);
            }

            set
            {

            }
        }

        public double AgeDouble { get; set; }
        public string AgeString { get; set; }
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
        public bool IsHasCar { get; set; }

        public bool IsHasSkip { get; set; }

        /// <summary>
        /// 皮肤代码
        /// </summary>
        public int SkinCode { get; set; }
    }
}
