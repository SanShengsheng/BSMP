using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Babies.Dtos.HostDtos
{
    public class BabyRankingModel
    {
        public BabyRankingModel()
        {
            this.PropertyDto = new BabyPropertyDto();
        }

        public BabyPropertyDto PropertyDto { get; set; }

        public int BabyId { get; set; }

        public int FamilyId { get; set; }

        public string AgeString { get; set; }

        public int Age { get; set; }

        public double AgeDouble { get; set; }

        /// <summary>
        /// 六维属性总值
        /// </summary>
        public int TotalValue { get; set; }

        public string BabyName { get; set; }

        public DateTime CreationTime { get; set; }

        public int RankingNumber { get; set; }

        /// <summary>
        /// 家庭资产
        /// </summary>
        public int Deposit { get; set; }
    }
}
