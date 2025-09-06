using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    [Table("ImportDataRecords")]
    public class ImportDataRecord : Entity<int>, IHasCreationTime, ISoftDelete
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件数据类型
        /// </summary>
        public TableDataType FileDataType { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string Size { get; set; }
        public DateTime CreationTime { get; set; } = System.DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public string Exception { get; set; } = string.Empty;
        //_watch.Elapsed.TotalSeconds
        public double Elapsed { get; set; } = 0;
        public ImportState State { get; set; }
    }
    public enum ImportState
    {
        Completed = 1,
        Failed = 0,
        Pending = 2
    }
}
