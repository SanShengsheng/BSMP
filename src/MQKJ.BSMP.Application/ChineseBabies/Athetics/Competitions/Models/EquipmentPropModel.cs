using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Models
{
    public class EquipmentPropModel
    {
        public int FamilyId { get; set; }
        public int BabyId { get; set; }
        public bool IsHasHouse { get; set; }
        public bool IsHasCar { get; set; }
        public bool IsHasServant { get; set; }
        public bool IsHasButler { get; set; }

        public bool IsHasSkip { get; set; }

        /// <summary>
        /// 房子代码
        /// </summary>
        public int HouseCode { get; set; }
        /// <summary>
        /// 皮肤代码
        /// </summary>
        public int SkinCode { get; set; }

    }
}
