using System;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.PropUseRecords;

namespace MQKJ.BSMP.PropUseRecords.Dtos
{
    public class PropUseRecordEditDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }


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