using Abp.AutoMapper;
using MQKJ.BSMP.ChineseBabies.BabyPropertyBaseDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos
{
    public class GetFightListOutput
    {
        public GetFightListBaby Baby { get; set; }

        /// <summary>
        /// 总积分
        /// </summary>
        public int GamePoint { get; set; }

        public DateTime CreationTime { get; set; }

        //public int RankingNumber { get; set; }
    }

    [AutoMapFrom(typeof(Baby))]
    public class GetFightListBaby : BabyPropertyBaseDto<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        //public double AgeDouble { get; set; }
        //public string AgeString { get; set; }
    }
}
