using System;
using Abp.Events.Bus;

namespace MQKJ.BSMP.Events.Datas
{
    public class StartGameComplatedEventData : EventData
    {
        public Guid InviterPlayerId { get; set; }
        public Guid InviteePlayerId { get; set; }
    }
}