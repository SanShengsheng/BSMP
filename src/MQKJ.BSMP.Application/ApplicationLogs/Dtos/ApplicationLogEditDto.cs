
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ApplicationLogs;

namespace  MQKJ.BSMP.ApplicationLogs.Dtos
{
    public class ApplicationLogEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }         


        
		/// <summary>
		/// Application
		/// </summary>
		public string Application { get; set; }



		/// <summary>
		/// Logged
		/// </summary>
		public DateTime Logged { get; set; }



		/// <summary>
		/// Level
		/// </summary>
		public string Level { get; set; }



		/// <summary>
		/// Message
		/// </summary>
		public string Message { get; set; }



		/// <summary>
		/// Logger
		/// </summary>
		public string Logger { get; set; }



		/// <summary>
		/// Callsite
		/// </summary>
		public string Callsite { get; set; }



		/// <summary>
		/// Exception
		/// </summary>
		public string Exception { get; set; }




    }
}