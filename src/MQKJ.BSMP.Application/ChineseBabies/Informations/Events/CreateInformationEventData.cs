using Abp.Events.Bus;
using System;

namespace MQKJ.BSMP.ChineseBabies.Informations.Events
{
    public class CreateInformationEventData : EventData
    {
        public string Content { get; set; }
        public Guid? SenderId { get; set; }
        public Guid? ReceiverId { get; set; }
        public int? FamilyId { get; set; }
        public InformationType Type { get; set; }
    }
}