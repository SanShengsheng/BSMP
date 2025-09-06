using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.BabySystem.Dtos.HostDtos.ImpotExcelDto
{
  public  class ImportProfessionTable : FullAuditedEntity<int>
    {

        /// <summary>
        /// 职业名称
        /// </summary>
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// 档次
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// 职业所属性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 薪资
        /// </summary>
        public double Salary { get; set; }

        /// <summary>
        /// 职业附带图片
        /// </summary>
        public string ImagePath { get; set; }

        public int? RewardId { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public int? ProductId { get; set; }

     
        /// <summary>
        /// 幸福指数（家庭幸福度）
        /// </summary>
        public double SatisfactionDegree { get; set; }
        /// <summary>
        /// 是否是默认职业
        /// </summary>
        public bool IsDefault { get; set; }

        public int Code { get; set; }
    }
}
