using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.PropMall
{
    /// <summary>
    /// 道具大礼包&道具中间表
    /// </summary>
    [Table("BabyPropBagAndBabyProps")]
    public class BabyPropBagAndBabyProp : FullAuditedEntity<Guid>
    {

        public int BabyPropId { get; set; }

        public BabyProp BabyProp { get; set; }

        public Guid BabyPropBagId { get; set; }

        public BabyPropBag BabyPropBag { get; set; }

        public int BabyPropPriceId { get; set; }

        public BabyPropPrice BabyPropPrice { get; set; }
        [DefaultValue(1)]
        public int Count { get; set; }
    }
}
