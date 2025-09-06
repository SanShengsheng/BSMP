using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.ChangeProfessionCosts.Dtos
{
    public class GetByProfessionIdInput
    {
        public int ProfessionId { get; set; }

        /// <summary>
        /// 充值类型
        /// </summary>
        public CostType CostType { get; set; }
    }
}
