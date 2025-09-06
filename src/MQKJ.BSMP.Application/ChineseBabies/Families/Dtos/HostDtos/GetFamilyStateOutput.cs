namespace MQKJ.BSMP.ChineseBabies
{
    public class GetFamilyStateOutput
    {
        public bool isCreatedFamily { get; set; }

        public GetFamilyStateOutputFamilyStateInfo FamilyStateInfo { get; set; }
    }

    public class GetFamilyStateOutputFamilyStateInfo
    {
        public int FamilyId { get; set; }

        public int BabyCount { get; set; }

        public GetFamilyStateOutputFamilyStateInfoNowBaby LastBaby { get; set; }

    }

    public class GetFamilyStateOutputFamilyStateInfoNowBaby
    {
        public int BabyId { get; set; }

        public BabyState State { get; set; }
    }
}