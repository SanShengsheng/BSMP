namespace MQKJ.BSMP.ChineseBabies.Families.Dtos.HostDtos
{
    public class UpdateAgentStateInput
    {
        public int FamilyId { get; set; }
        public AddOnStatus? Status { get; set; }
        public string Note { get; set; }
    }
}