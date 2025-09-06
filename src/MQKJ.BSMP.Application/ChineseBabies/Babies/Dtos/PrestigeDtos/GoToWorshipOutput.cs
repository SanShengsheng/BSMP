using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Babies.Dtos.PrestigeDtos
{
     public class GoToWorshipOutput
    {
        /// <summary>
        /// 当前家庭资产
        /// </summary>
        public double DepositAfter { get; set; }
        /// <summary>
        /// 之前的家庭资产
        /// </summary>
        public double DepositBefore { get; set; }
        /// <summary>
        /// 获得金币数量
        /// </summary>
        public int Coins { get; set; }
       
    }
}
