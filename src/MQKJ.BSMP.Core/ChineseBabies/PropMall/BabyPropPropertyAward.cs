using MQKJ.BSMP.ChineseBabies.Asset;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.PropMall
{
    [Table("BabyPropPropertyAwards")]
    public class BabyPropPropertyAward : BabyAssetAwardBase<Guid>
    {
        /// <summary>
        /// 事件加成方式
        /// </summary>
        public EventAdditionType EventAdditionType { get; set; }

        public int Code { get; set; }
    }
}
