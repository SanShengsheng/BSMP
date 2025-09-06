using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Families.Dtos
{
    public class RequestDismissFamilyInput
    {
        /// <summary>
        /// 要解散的家庭Id
        /// </summary>
        public int FamilyId { get; set; }

        /// <summary>
        /// 发起人
        /// </summary>
        public Guid PlayerGuid { get; set; }

        /// <summary>
        /// 解散类型
        /// </summary>
        public DismissFamilyType DismissType { get; set; }
    }
}
