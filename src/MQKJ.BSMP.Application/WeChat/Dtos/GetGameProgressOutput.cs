using Abp.AutoMapper;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.GameTasks.Dtos;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Players.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    [AutoMapFrom(typeof(GameTask))]
    public class GetGameProgressOutput
    {
        public TaskState State { get; set; }

        /// <summary>
        /// 任务类型 3三题关  5五题关 10十题关
        /// </summary>
        public TaskType TaskType { get; set; }

        /// <summary>
        /// 任务约定内容(邀请方约定内容)
        /// </summary>
        public string AppointmentContent { get; set; }

        /// <summary>
        /// 关系程度
        /// </summary>  1-普通，2-暧昧，3-情侣，4-夫妻
        public int RelationDegree { get; set; }

        /// <summary>
        /// 被邀请方约定内容
        /// </summary>
        public string InviteeAppointmentContent { get; set; }

        public OtherPlayer OtherPlayer { get; set; }

        public MsgCodeEnum MsgCodeEnum { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime CurrentTime { get; set; }

        public GetGameProgressOutput()
        {
            OtherPlayer = new OtherPlayer();
        }

    }

    [AutoMapFrom(typeof(Player))]
    public class OtherPlayer
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }


        /// <summary>
        /// NickName
        /// </summary>
        public string NickName { get; set; }


        /// <summary>
        /// HeadUrl
        /// </summary>
        public string HeadUrl { get; set; }


        /// <summary>
        /// Gender
        /// </summary>
        public int Gender { get; set; }
    }
}
