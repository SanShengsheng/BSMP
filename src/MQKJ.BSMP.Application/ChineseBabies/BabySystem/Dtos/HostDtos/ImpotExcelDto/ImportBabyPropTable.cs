using MQKJ.BSMP.Utils.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.BabySystem.Dtos.HostDtos.ImpotExcelDto
{
    public class ImportBabyPropTable
    {
        [ExcelColumn("道具id")]
        public int Id { get; set; }

        [ExcelColumn("道具名称")]
        public string Title { get; set; }

        [ExcelColumn("等级")]
        public int Level { get; set; }

        [ExcelColumn("初始")]
        public bool IsDefault { get; set; }

        [ExcelColumn("道具类型")]
        public int? BabyPropTypeId { get; set; }

        [ExcelColumn("性别属性")]
        public int? Gender { get; set; }

        [ExcelColumn("获取方式")]
        public int? GetWay { get; set; }

        [ExcelColumn("是否跑马灯")]
        public bool IsAfterBuyPlayMarquees { get; set; }

        /// <summary>
        /// 最大购买次数
        /// </summary>
        [ExcelColumn("最大上限")]
        public int MaxPurchasesNumber { get; set; }

        public ICollection<ImportBabyPropPriceTable> Prices { get; set; }

        public ICollection<ImportBabyPropTermTable> BabyPropTerms { get; set; }

        public ICollection<ImportBabyPropFeatureTable> BabyPropFeatures { get; set; }
    }

    public class ImportBabyPropPriceTable
    {
        public int Id { get; set; }

        //[ExcelColumn("道具id")]
        public int BabyPropId { get; set; }

        /// <summary>
        /// 货币类型
        /// </summary>
        [ExcelColumn("价格类型")]
        public int CurrencyType { get; set; }

        /// <summary>
        /// 资产值
        /// </summary>
        [ExcelColumn("资产值")]
        public double PropValue { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        [ExcelColumn("其他价格")]
        public double Price { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        [ExcelColumn("其他时间")]
        public double Validity { get; set; }
    }

    public class ImportBabyPropTermTable
    {
        public int Id { get; set; }

        //[ExcelColumn("道具id")]
        public int BabyPropId { get; set; }

        [ExcelColumn("解锁条件")]
        public int Code { get; set; }

        [ExcelColumn("条件参数")]
        public int? MinValue { get; set; }
        
    }

    public class ImportBabyPropFeatureTable
    {
        public int Id { get; set; }

        //[ExcelColumn("道具id")]
        public int BabyPropId { get; set; }

        [ExcelColumn("功能类型")]
        public int Code { get; set; }

        [ExcelColumn("功能属性")]
        public double Value { get; set; }

        public Guid BabyPropFeatureTypeId { get; set; }
    }
}
