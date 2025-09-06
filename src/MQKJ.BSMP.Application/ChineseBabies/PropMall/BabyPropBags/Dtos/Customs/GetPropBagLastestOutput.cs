using System;
using System.Collections.Generic;

namespace MQKJ.BSMP.ChineseBabies.PropMall
{
    public class GetPropBagLastestOutput
    {

        public GetPropBagLastestOutputBasicInfo BasicInfo { get; set; }

        public ICollection<GetPropBagLastestOutputChild> Props { get; set; }

    }

    public class GetPropBagLastestOutputChild
    {
        public string Title { get; set; }

        public double Validity { get; set; }
        public int Count { get; set; }
    }

    public class GetPropBagLastestOutputBasicInfo
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Img { get; set; }
        /// <summary>
        /// 价格（人民币）
        /// </summary>
        public decimal PriceRMB { get; set; }
        /// <summary>
        /// 价格（金币）
        /// </summary>
        public double PriceGoldCoin { get; set; }

        public int Code { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsDisabled { get; set; }
    }
}