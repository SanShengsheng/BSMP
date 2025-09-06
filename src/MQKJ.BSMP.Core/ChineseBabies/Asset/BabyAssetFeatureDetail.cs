using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Asset
{
    public class BabyAssetFeatureProperty
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public double Value { get; set; }
        /// <summary>
        /// 道具特性类型编号
        /// </summary>
        public Guid FeatureTypeId { get; set; }
    }
}
