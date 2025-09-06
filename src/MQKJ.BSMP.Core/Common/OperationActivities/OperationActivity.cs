using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.Common.OperationActivities
{
    /// <summary>
    /// 运营活动
    /// </summary>
    [Table("OperationActivities")]
    public class OperationActivity : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        /// <summary>
        /// 运营活动链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 副标题
        /// </summary>
        public string SubTitle { get; set; }
        /// <summary>
        /// 是否主动弹出
        /// </summary>
        public bool IsPopup { get; set; }
        /// <summary>
        /// 过期日期
        /// </summary>
        public DateTime? ExpireDateTime { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string Img { get; set; }
        /// <summary>
        /// 活动类型
        /// </summary>
        public ActivityType ActivityType { get; set; }
    }
    public enum ActivityType
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// H5
        /// </summary>
        H5 = 1
    }
}
