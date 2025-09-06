
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.LoveCardFiles;

namespace  MQKJ.BSMP.LoveCardFiles.Dtos
{
    public class LoveCardFileEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// BSMPFileId
		/// </summary>
		public Guid BSMPFileId { get; set; }



		/// <summary>
		/// UserId
		/// </summary>
		public Guid UserId { get; set; }

        public Guid LoveCardId { get; set; }




    }
}