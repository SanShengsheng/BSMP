using System;
using System.Collections.Generic;
using System.Text;
using NPOI;


namespace MQKJ.BSMP.Utils.Tools
{
    public static class OfficeHelp
    {
        public static void ExportExcel<T>(List<T> dataList)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            //设置第一行标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("男生画面");
            row1.CreateCell(2).SetCellValue("选项");
            row1.CreateCell(3).SetCellValue("女生画面");
            row1.CreateCell(4).SetCellValue("选项");
            row1.CreateCell(5).SetCellValue("关系");
            row1.CreateCell(6).SetCellValue("场景");
            row1.CreateCell(7).SetCellValue("私密度");

            //将数据写入每一行
            for (int i = 0; i < dataList.Count; i++)
            {
                //NPOI.SS.UserModel.IRow rowTemp = sheet1.CreateRow(dataList[]);
            }
        }
    }
}
