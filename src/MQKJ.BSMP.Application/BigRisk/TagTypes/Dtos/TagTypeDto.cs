using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Tags;
using MQKJ.BSMP.TagTypes;


namespace MQKJ.BSMP.TagTypes.Dtos
{
    public class TagTypeDto 
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }


        /// <summary>
        /// 类别名
        /// </summary>
        public string TypeName { get; set; }


        public string Code { get; set; }
        //// custom codes 

        //// custom codes end
    }
}