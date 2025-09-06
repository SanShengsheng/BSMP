using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.PropUseRecords;
using MQKJ.BSMP.Players;

namespace MQKJ.BSMP.PropUseRecords.Dtos
{
    public class PropUseRecordListDto : FullAuditedEntityDto<Guid>
    {



        /// <summary>
        /// PropType
        /// </summary>
        public PropType PropType { get; set; }


        /// <summary>
        /// UseTime
        /// </summary>
        public DateTime UseTime { get; set; }


        /// <summary>
        /// PlayerId
        /// </summary>
        public Guid PlayerId { get; set; }


        /// <summary>
        /// Player
        /// </summary>
        public Player Player { get; set; }
    }
}