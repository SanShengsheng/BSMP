using Abp.AutoMapper;
using MQKJ.BSMP.Common.AD;
using MQKJ.BSMP.Common.Adviertisements.Dtos;
using MQKJ.BSMP.MultiTenancy;
using MQKJ.BSMP.MultiTenancy.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Areas.Adviertisements.Models
{
    [AutoMapFrom(typeof(AdviertisementDto))]
    public class EditAdModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

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

        public string PubLink { get; set; }
        public string MinPorgramQRPath { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNumber { get; set; }

        public virtual int TenantId { get; set; }

        public AdviertisementState AdviertisementState { get; set; }


        public AdviertisementPlatform AdviertisementPlatform { get; set; }

        public List<TenantDto> Tenants { get; set; }
        public string AppId { get; set; }

        public DateTime EndTime { get; set; }
    }
}
