using System.Collections.Generic;

namespace MQKJ.BSMP.ChineseBabies
{
    //public class GetFamilyPropBuyInfoOutput
    //{
    //    public List<GetFamilyPropBuyInfoOutputProp> Prop { get; set; }
    //}

    public class GetFamilyPropBuyInfoOutput
    {   
        /// <summary>
        /// 道具编号
        /// </summary>
        public int PropId { get; set; }
        /// <summary>
        /// 是否已拥有
        /// </summary>
        public bool IsHave { get; set; }
        /// <summary>
        /// 是否达到解锁条件
        /// </summary>
        public bool IsUnlock { get; set; }
        /// <summary>
        /// 是否装备中
        /// </summary>
        public bool IsEquipmenting { get; set; }
        /// <summary>
        /// 获得途径，如竞技场道具不允许购买
        /// </summary>
        public GetWay GetWay { get; set; }
        /// <summary>
        /// 是否允许购买，如竞技场道具不允许购买
        /// </summary>
        public bool IsAllow { get; set; }
        /// <summary>
        /// 购买条件
        /// </summary>
        public ICollection<GetFamilyPropBuyInfoOutputPropTerm> Terms { get; set; }
    }

    /// <summary>
    /// 购买条件
    /// </summary>
    public class GetFamilyPropBuyInfoOutputPropTerm
    {
        /// <summary>
        /// 条件编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 是否满足条件
        /// </summary>
        public bool IsSatisfy { get; set; }

        public string Title { get; set; }
    }
}