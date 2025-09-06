using System;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.GameTasks;

namespace MQKJ.BSMP.GameTasks.Dtos
{
    public class GameTaskEditDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }


        /// <summary>
        /// State
        /// </summary>
        public TaskState State { get; set; }


        /// <summary>
        /// TaskType
        /// </summary>
        public TaskType TaskType { get; set; }


        /// <summary>
        /// AppointmentContent
        /// </summary>
        public string AppointmentContent { get; set; }


        /// <summary>
        /// FriendRelationship
        /// </summary>
        public string FriendRelationship { get; set; }


        /// <summary>
        /// SeekType
        /// </summary>
        public SeekType SeekType { get; set; }


        /// <summary>
        /// InviterPlayerId
        /// </summary>
        public Guid InviterPlayerId { get; set; }


        /// <summary>
        /// InviteePlayerId
        /// </summary>
        public Guid InviteePlayerId { get; set; }


        /// <summary>
        /// InvitationLink
        /// </summary>
        public string InvitationLink { get; set; }


        /// <summary>
        /// ValidInterval
        /// </summary>
        public int ValidInterval { get; set; }
    }
}