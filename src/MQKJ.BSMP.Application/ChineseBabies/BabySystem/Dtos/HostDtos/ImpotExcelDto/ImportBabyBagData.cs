using MQKJ.BSMP.Utils.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.BabySystem.Dtos.HostDtos.ImpotExcelDto
{
    public class ImportBabyBagData
    {
        /// <summary>
        /// Code值
        /// </summary>
        [ExcelColumn("ID")]
        public int Code { get; set; }


        /// <summary>
        /// 货币类型(购买的货币类型)
        /// </summary>
        [ExcelColumn("使用货币")]
        public int BuyCurrencyType { get; set; }

        /// <summary>
        /// 购买价格(金币或人民币)
        /// </summary>
        [ExcelColumn("购买价格")]
        public double Price { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [ExcelColumn("性别")]
        public int Gender { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [ExcelColumn("标题")]
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [ExcelColumn("描述")]
        public string Description { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        [ExcelColumn("顺序")]
        public int Sort { get; set; }

        /// <summary>
        /// 道具Id
        /// </summary>
        [ExcelColumn("道具1_id")]
        public int PropCode1 { get; set; }

        [ExcelColumn("道具1数量")]
        public int Count1 { get; set; }

        [ExcelColumn("道具1时效")]
        public int Validate1 { get; set; }

        /// <summary>
        /// 道具Id
        /// </summary>
        [ExcelColumn("道具2_id")]
        public int PropCode2 { get; set; }

        [ExcelColumn("道具2数量")]
        public int Count2 { get; set; }

        [ExcelColumn("道具2时效")]
        public int Validate2 { get; set; }

        /// <summary>
        /// 道具Id
        /// </summary>
        [ExcelColumn("道具3_id")]
        public int PropCode3 { get; set; }

        [ExcelColumn("道具3数量")]
        public int Count3 { get; set; }

        [ExcelColumn("道具3时效")]
        public int Validate3 { get; set; }

        /// <summary>
        /// 道具Id
        /// </summary>
        [ExcelColumn("道具4_id")]
        public int PropCode4 { get; set; }

        [ExcelColumn("道具4数量")]
        public int Count4 { get; set; }

        [ExcelColumn("道具4时效")]
        public int Validate4 { get; set; }

        /// <summary>
        /// 货币类型(赠送) 
        /// </summary>
        [ExcelColumn("货币ID")]
        public int? GiveCurrencyType { get; set; }

        [ExcelColumn("数量")]
        public double? GiveCount { get; set; }
    }
}
