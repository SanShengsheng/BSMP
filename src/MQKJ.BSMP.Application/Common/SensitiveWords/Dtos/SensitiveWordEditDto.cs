
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Common.SensitiveWords;

namespace  MQKJ.BSMP.Common.SensitiveWords.Dtos
{
    public class SensitiveWordEditDto :  IAddModel<SensitiveWord, int>, IEditModel<SensitiveWord, int>
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }         


        
		/// <summary>
		/// Content
		/// </summary>
		public string Content { get; set; }




    }
}