using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.Players;
using Abp.AutoMapper;

namespace MQKJ.BSMP.GameTasks.Dtos
{
    [AutoMapFrom(typeof(GameTask))]
    public class GameTaskListDto
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


        public string InviteeAppointmentContent { get; set; }
        


        /// <summary>
        /// FriendRelationship
        /// </summary>
        //public string FriendRelationship { get; set; }

        public RelationDegree RelationDegree { get; set; }

        /// <summary>
        /// SeekType
        /// </summary>
        public SeekType SeekType { get; set; }


        /// <summary>
        /// InviterPlayerId
        /// </summary>
        public Guid InviterPlayerId { get; set; }

        /// <summary>
        /// 邀请方实体
        /// </summary>
        public Player Inviter { get; set; }
        /// <summary>
        /// InviteePlayerId
        /// </summary>
        public Guid InviteePlayerId { get; set; }

        /// <summary>
        /// 被邀请方实体
        /// </summary>
        public Player Invitee { get; set; }


        /// <summary>
        /// InvitationLink
        /// </summary>
        public string InvitationLink { get; set; }


        /// <summary>
        /// ValidInterval
        /// </summary>
        public int ValidInterval { get; set; }
        

        public DateTime CreationTime { get; set; }






        //// custom codes 

        //// custom codes end
    }
}