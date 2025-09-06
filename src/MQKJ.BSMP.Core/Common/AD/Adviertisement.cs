using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.MultiTenancy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.Common.AD
{
    [Table("Adviertisements")]
    public class Adviertisement:FullAuditedEntity
    {
        public string Name { get; set; }

        /// <summary>
        /// 小程序appid
        /// </summary>
        public string AppId { get; set; }


        /// <summary>
        /// banner图
        /// </summary>
        public string BannerImagePath { get; set; }

        /// <summary>
        /// 插屏图
        /// </summary>
        public string InsertImagePath { get; set; }

        /// <summary>
        /// 悬浮图
        /// </summary>
        public string FixedImagePath { get; set; }

        /// <summary>
        /// 文字链接
        /// </summary>
        public string WordLink { get; set; }

        /// <summary>
        /// 推广路径
        /// </summary>
        public string ExpandPath { get; set; }

        /// <summary>
        /// 小程序logo
        /// </summary>
        public string MinPorgramLogo { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNumber { get; set; }

        public virtual Tenant Tenant { get; set; }

        public int TenantId { get; set; }

        public DateTime EndTime { get; set; }

        public AdviertisementState AdviertisementState { get; set; }


        public AdviertisementPlatform AdviertisementPlatform { get; set; }

        /// <summary>
        /// 公众号连接(跳转广告小程序的webview)
        /// </summary>
        public string PubLink { get; set; }

        /// <summary>
        /// 小程序码
        /// </summary>
        public string MinPorgramQRPath { get; set; }

    }

    public enum AdviertisementState
    {
        All = 0,

        /// <summary>
        /// 正常
        /// </summary>
        [EnumHelper.EnumDescription("正常")]
        Normal = 1,

        /// <summary>
        /// 关闭
        /// </summary>
        [EnumHelper.EnumDescription("关闭")]
        Close = 2
    }

    public enum AdviertisementPlatform
    {
        All = 0,

        /// <summary>
        /// 微信
        /// </summary>
        [EnumHelper.EnumDescription("微信")]
        WeChat = 1,

        /// <summary>
        /// 小盟
        /// </summary>
        [EnumHelper.EnumDescription("小盟")]
        XiaoMeng = 2,
    }
}
