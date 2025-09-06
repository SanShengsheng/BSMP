using Abp.Events.Bus;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.AutoRunnerRecords.EventDatas
{
    public class CreateAutoRunnerRecordEventData : EventData
    {
        public CreateAutoRunnerRecordEventData(
            int familyId, 
            Guid playerId, 
            int groupId, 
            ActionType actionType, 
            string relationId,
            string description)
        {
            FamilyId = familyId;
            PlayerId = playerId;
            GroupId = groupId;
            ActionType = actionType;
            RelationId = relationId;
            Description = description;
        }

        /// <summary>
        /// FamilyId
        /// </summary>
        public int FamilyId { get; set; }



        /// <summary>
        /// PlayerId
        /// </summary>
        public Guid PlayerId { get; set; }



        /// <summary>
        /// GroupId
        /// </summary>
        public int GroupId { get; set; }
        public ActionType ActionType { get; set; }
        public string OriginalData { get; set; }
        public string NewData { get; set; }
        public string RelationId { get; set; }
        public string Description { get; set; }
    }
}
