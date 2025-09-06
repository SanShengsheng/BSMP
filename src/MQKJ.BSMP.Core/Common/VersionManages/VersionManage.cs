using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    ///  版本管理 
    /// </summary>
    [Table("VersionManages")]
  public  class VersionManage : FullAuditedEntity, IMustHaveTenant
    {
        /// <summary>
        /// 是否弹出
        /// </summary>
        public bool IsPopup { get; set; }
        /// <summary>
        /// 是否强制更新
        /// </summary>
        public bool IsForceUpdate { get; set; }
        /// <summary>
        /// 更新日志
        /// </summary>
        public string  ReleaseLog { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark { get; set; }
        public int TenantId { get; set; }
    }
}
