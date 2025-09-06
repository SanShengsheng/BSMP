using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class GetGameRoomStateOutput
    {
        public string MethodName { get; set; }

        public MsgCodeEnum MsgCodeEnum { get; set; }

        public Guid OtherPlayerId { get; set; }

        public bool OtherIsOnLine { get; set; }

        public object Result { get; set; }
    }
}
