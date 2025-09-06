using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.Products
{
    [Table("Products")]
    public class Product:FullAuditedEntity
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 单个商品价格
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 商品状态 1-正常 2-禁用
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description { get; set; }
    }
}
