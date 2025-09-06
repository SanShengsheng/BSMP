using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Authorization
{
    public static class ChinesePermissions
    {
        #region 家庭

        public const string Node = "Pages.Families";

        /// <summary>
        /// 获取所有 家庭
        ///</summary>
        public const string GetAllFamilys = "Pages.GetAllFamilys";

        /// <summary>
        /// 为家庭充值金币
        /// </summary>
        public const string SupplementCoinRecharge = "Pages.SupplementCoinRecharge";

        /// <summary>
        /// 导出家庭数据
        /// </summary>
        public const string ExportFamiliesToExcel = "Pages.ExportFamiliesToExcel";
        #endregion

        #region 

        #endregion


        #region 流水
        /// <summary>
        /// 流水记录
        /// </summary>
        public const string GetAllRunWaterRecords = "Pages.RunWaterRecord.GetAllRunWaterRecords";

        /// <summary>
        /// 导出流水数据
        /// </summary>
        public const string ExportRunWaterRecordToExcel = "Pages.RunWaterRecord.ExportRunWaterRecordToExcel";

        #endregion
    }
}
