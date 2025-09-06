using MQKJ.BSMP.ChineseBabies.Babies.Dtos.HostDtos;

namespace MQKJ.BSMP.ChineseBabies
{
    public class BabyGrowUpRecordOutput
    {
        public BabyGrowUpRecordOutputBabyProperty BabyProperty { get; set; }

        public bool HasChange { get; set; }

    }

    public class BabyGrowUpRecordOutputBabyProperty:BabyPropertyDto
    {

    }
}