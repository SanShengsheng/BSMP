using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MQKJ.BSMP.ChineseBabies.Athetics.Competitions.CompetitionApplicationService;

namespace MQKJ.BSMP.ChineseBabies.Babies.Dtos.HostDtos
{
    public class GetAssetRankingListModel
    {
        public int FamilyId { get; set; }


        public int BabyId { get; set; }

        /// <summary>
        /// 家庭资产
        /// </summary>
        public double Asset { get; set; }

        public string BabyName { get; set; }

        //[NotMapped]
        public DateTime CreationTime { get; set; }

        public int RankingNumber { get; set; }

        public EquipmentPropModel EquipmentProp { get; set; } 
    }
}
