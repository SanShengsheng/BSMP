
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.UnLocks;

namespace  MQKJ.BSMP.UnLocks.Dtos
{
    public class UnlockEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// UnLockerId
		/// </summary>
		public Guid UnLockerId { get; set; }



		/// <summary>
		/// BeUnLockerId
		/// </summary>
		public Guid BeUnLockerId { get; set; }




    }
}