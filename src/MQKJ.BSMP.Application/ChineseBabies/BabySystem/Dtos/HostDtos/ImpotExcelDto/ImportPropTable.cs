using MQKJ.BSMP.Utils.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.BabySystem.Dtos.HostDtos.ImpotExcelDto
{
    public class ImportPropTable
    {
        [ExcelColumn("道具id")]
        public int PropCode { get; set; }

        [ExcelColumn("道具名称")]
        public string Title { get; set; }

        [ExcelColumn("等级")]
        public int Level { get; set; }

        [ExcelColumn("初始")]
        public bool IsDefault { get; set; }

        [ExcelColumn("道具类型")]
        public int BabyPropTypeCode { get; set; }

        [ExcelColumn("性别属性")]
        public int Gender { get; set; }

        [ExcelColumn("获取方式")]
        public int GetWay { get; set; }

        /// <summary>
        /// 货币类型
        /// </summary>
        [ExcelColumn("价格类型")]
        public int CurrencyType { get; set; }

        /// <summary>
        /// 资产值1
        /// </summary>
        [ExcelColumn("资产值1")]
        public double? PropValue1 { get; set; }

        /// <summary>
        /// 资产值2
        /// </summary>
        [ExcelColumn("资产值2")]
        public double? PropValue2 { get; set; }

        /// <summary>
        /// 资产值3
        /// </summary>
        [ExcelColumn("资产值3")]
        public double? PropValue3 { get; set; }

        /// <summary>
        /// 其他价格1
        /// </summary>
        [ExcelColumn("价格1")]
        public double? Price1 { get; set; }

        /// <summary>
        /// 其他价格2
        /// </summary>
        [ExcelColumn("价格2")]
        public double? Price2 { get; set; }

        /// <summary>
        /// 其他价格3
        /// </summary>
        [ExcelColumn("价格3")]
        public double? Price3 { get; set; }

        /// <summary>
        /// 时间间隔1
        /// </summary>
        [ExcelColumn("时间1")]
        public double? Interval1 { get; set; }

        /// <summary>
        /// 时间间隔2
        /// </summary>
        [ExcelColumn("时间2")]
        public double? Interval2 { get; set; }


        /// <summary>
        /// 时间间隔3
        /// </summary>
        [ExcelColumn("时间3")]
        public double? Interval3 { get; set; }

        /// <summary>
        /// 解锁条件
        /// </summary>
        [ExcelColumn("解锁条件1")]
        public int? Term1 { get; set; }

        /// <summary>
        /// 条件参数
        /// </summary>
        [ExcelColumn("条件参数1")]
        public int? TermArgumet1 { get; set; }

        /// <summary>
        /// 解锁条件
        /// </summary>
        [ExcelColumn("解锁条件2")]
        public int? Term2 { get; set; }

        /// <summary>
        /// 条件参数
        /// </summary>
        [ExcelColumn("条件参数2")]
        public int? TermArgumet2 { get; set; }


        /// <summary>
        /// 解锁条件
        /// </summary>
        [ExcelColumn("解锁条件3")]
        public int? Term3 { get; set; }

        /// <summary>
        /// 条件参数
        /// </summary>
        [ExcelColumn("条件参数3")]
        public int? TermArgumet3 { get; set; }

        /// <summary>
        /// 解锁条件
        /// </summary>
        [ExcelColumn("解锁条件4")]
        public int? Term4 { get; set; }

        /// <summary>
        /// 条件参数
        /// </summary>
        [ExcelColumn("条件参数4")]
        public int? TermArgumet4 { get; set; }

        ///// <summary>
        ///// 最大上限
        ///// </summary>
        //[ExcelColumn("最大上限")]
        //public int MaxBuyCount { get; set; }

        /// <summary>
        /// 功能类(propfeaturetype)
        /// </summary>
        [ExcelColumn("功能类型")]
        public int? FeatureType { get; set; }

        /// <summary>
        /// 功能属性(值)
        /// </summary>
        [ExcelColumn("功能属性")]
        public double? FeatureValue { get; set; }

        [ExcelColumn("属性奖励id")]
        public int AwardId { get; set; }

        /// <summary>
        /// 图片名称
        /// </summary>
        [ExcelColumn("图片名称")]
        public string ImageName { get; set; }


        [ExcelColumn("是否跑马灯")]
        public bool IsAfterBuyPlayMarquees { get; set; }

        /// <summary>
        /// 最大购买次数
        /// </summary>
        [ExcelColumn("最大上限")]
        public int MaxPurchasesNumber { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        //public int Code { get; set; }
        [ExcelColumn("是否继承")]
        public bool IsInheritAble { get; set; }

        /// <summary>
        /// 触发显示的道具Code
        /// </summary>
        [ExcelColumn("触发道具")]
        public int? TriggerShowPropCode { get; set; }

        /// <summary>
        /// 触发显示的道具Code
        /// </summary>
        [ExcelColumn("触发道具2")]
        public int? TriggerNextShowPropCode { get; set; }
    }
}
