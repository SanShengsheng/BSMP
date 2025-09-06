using Abp.AutoMapper;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.WeChat;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.Players.WeChat.Dtos
{
    public class StartGameInput
    {
        public Guid GameId { get; set; }

        public Guid PlayerId { get; set; }

       // public GameType GameType { get; set; }

    }
}
