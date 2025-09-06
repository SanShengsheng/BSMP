using System;
using Abp.Events.Bus;

namespace MQKJ.BSMP.Events.Datas
{
    public class ChangedPointEventData : EventData
    {
        public ChangedPointEventData(Guid playerId, int pointId, int count)
        {
            PlayerId = playerId;
            BonusPointId = pointId;
            GatherCount = count;
        }
        public Guid PlayerId { get; set; }
        public int BonusPointId { get; set; }
        public int GatherCount { get; set; }

    }
}