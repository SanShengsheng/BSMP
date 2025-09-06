using MQKJ.BSMP.ChineseBabies.Babies.Dtos.HostDtos;

namespace MQKJ.BSMP.ChineseBabies
{
    public class BabyBirthRecordOutput
    {
        /// <summary>
        /// 出生信息
        /// </summary>
        public BabyBirthInfo BabyBirthInfo { get; set; }
        /// <summary>
        /// 出生属性
        /// </summary>
        public BabyBirthRecordOutputBabyProperty BabyBirthProperty { get; set; }
        public int[] Levels { get; set; }
    }

    public class BabyBirthInfo
    {

        /// <summary>
        /// 宝宝姓名
        /// </summary>
        public string Name { get; set; }
       
        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// 出生身长
        /// </summary>
        public double BirthLength { get; set; }
        /// <summary>
        /// 出生体重
        /// </summary>
        public double BirthWeight { get; set; }
        /// <summary>
        /// 出生医院
        /// </summary>
        public string BirthHospital { get; set; }
    }
    public class BabyAttributDetermine
    {
        public string Value { get; set; }
    }
    public class BabyBirthRecordOutputBabyProperty : BabyPropertyDto
    {


    }
}