using System;

namespace MQKJ.BSMP.ChineseBabies
{
    public class PostBuyPropOutput
    {
        /// <summary>
        /// 是否弹出
        /// </summary>
        public bool IsPopupChangeEquipment { get; set; }

        public Guid AssetId { get; set; }
    }
}