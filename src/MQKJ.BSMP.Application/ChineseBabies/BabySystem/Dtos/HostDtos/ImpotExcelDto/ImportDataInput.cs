using Abp.AutoMapper;
using Microsoft.AspNetCore.Http;
using MQKJ.BSMP.EnumHelper;
using OfficeOpenXml;
using System;
using System.Data;
using System.IO;

namespace MQKJ.BSMP.ChineseBabies
{
    public class ImportDataInput
    {
        /// <summary>
        /// 文件
        /// </summary>
        //[JsonIgnore]
        public IFormFile FormFile { get; set; }

        public TableDataType TableDataType { get; set; }

    }
    public class ImportDataTaskModel
    {
        public DataTable TaskInput { get; set; }
        public ExcelPackage ExcelFile { get; set; }
        public TableDataType TableDataType { get; set; }
        public int TaskIdentity { get; set; }
    }
    public class ImportDataTaskOutput
    {

        public string FileName { get; set; }
        public TableDataType FileDataType { get; set; }
        public string Size { get; set; }
        public DateTime CreationTime { get; set; } = System.DateTime.Now;
        public double Elapsed { get; set; } = 0;
        public ImportState State { get; set; }
        public string FileTypeDescription { get; set; }
    }

}