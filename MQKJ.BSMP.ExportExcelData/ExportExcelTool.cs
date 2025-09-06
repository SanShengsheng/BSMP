using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MQKJ.BSMP.ExportExcelData
{
    public class ExportExcelTool
    {
        public void GetSheetValues(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            if (file != null)
            {
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorkbook workbook = package.Workbook;

                    if (workbook != null)
                    {
                        if (workbook.Worksheets.Count > 0)
                        {
                            ExcelWorksheet worksheet = workbook.Worksheets.First();
                            //获取表格的列数和行数
                            int rowCount = worksheet.Dimension.Rows;
                            int ColCount = worksheet.Dimension.Columns;

                        }
                    }
                }
            }
        }
    }
}
