using MQKJ.BSMP.Utils.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.BabySystem.Dtos.HostDtos.ImpotExcelDto
{
    public class ImportPropPropertyAwardTable
    {
        /// <summary>
        /// 智力
        /// </summary>
        [ExcelColumn("属性1")]
        public string Intelligence { get; set; }

        /// <summary>
        /// 数值1
        /// </summary>
        [ExcelColumn("数值1")]
        public int IntelligenceValue { get; set; }


        /// <summary>
        /// 情商
        /// </summary>
        [ExcelColumn("属性2")]
        public string EmotionQuotient { get; set; }


        /// <summary>
        /// 数值2
        /// </summary>
        [ExcelColumn("数值2")]
        public int EmotionQuotientValue { get; set; }

        /// <summary>
        /// 体魄
        /// </summary>
        [ExcelColumn("属性3")]
        public string Physique { get; set; }

        /// <summary>
        /// 数值3
        /// </summary>
        [ExcelColumn("数值3")]
        public int PhysiqueValue { get; set; }


        /// <summary>
        /// 意志(专注)
        /// </summary>
        [ExcelColumn("属性4")]
        public string WillPower { get; set; }

        /// <summary>
        /// 数值4
        /// </summary>
        [ExcelColumn("数值4")]
        public int WillPowerValue { get; set; }


        /// <summary>
        /// 想象
        /// </summary>
        [ExcelColumn("属性5")]
        public string Imagine { get; set; }

        /// <summary>
        /// 数值5
        /// </summary>
        [ExcelColumn("数值5")]
        public int ImagineValue { get; set; }


        /// <summary>
        /// 魅力
        /// </summary>
        [ExcelColumn("属性6")]
        public string Charm { get; set; }

        /// <summary>
        /// 数值6
        /// </summary>
        [ExcelColumn("数值6")]
        public int CharmValue { get; set; }

        [ExcelColumn("id")]
        public int Code { get; set; }
    }
}
