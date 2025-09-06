using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 宝宝表
    /// </summary>
    [Table("Babies")]
    public class Baby : BabyPropertyBase<int>
    {
        //private string name;

        /// <summary>
        /// 宝宝名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }
        public int FamilyId { get; set; }
        public Family Family { get; set; }
        /// <summary>
        /// 封面图
        /// </summary>
        public string CoverImage { get; set; }
        /// <summary>
        /// 宝宝状态1-未成年2-已成年
        /// </summary>
        public BabyState State { get; set; }
        public int? BabyEndingId { get; set; }

        /// <summary>
        /// 宝宝结局
        /// </summary>
        public BabyEnding BabyEnding { get; set; }
        /// <summary>
        /// 当前阶段ID
        /// </summary>
        public int? GroupId { get; set; }
        public EventGroup Group { get; set; }
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
        /// <summary>
        /// 第几胎
        /// </summary>
        public int BirthOrder { get; set; }
        /// <summary>
        /// 成长潜力
        /// </summary>
        public int Potential { get; set; }
        /// <summary>
        /// 成长总值
        /// </summary>
        public double GrowthTotal { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        public double AgeDouble { get; set; }
        private string _ageString { get; set; }

        public string AgeString
        {
            get => string.IsNullOrEmpty(_ageString) ? "刚出生" : _ageString;
            set => _ageString = value;
        }
        /// <summary>
        /// 爸爸是否查看宝宝出生动画
        /// </summary>
        public bool IsWatchBirthMovieFather { get; set; }
        /// <summary>
        /// 妈妈是否查看宝宝出生动画
        /// </summary>
        public bool IsLoadBirthMovieMother { get; set; }

        public bool IsLookEndingFather { get; set; }

        public bool IsLookEndingMother { get; set; }

        /// <summary>
        /// 精力
        /// </summary>
        private int _energy { get; set; }

        public override int Energy
        {
            get => _energy > 100 ? 100 : (_energy < 0 ? 0 : _energy);
            set => _energy = value > 100 ? 100 : (_energy < 0 ? 0 : value);
        }
        /// <summary>
        /// 健康
        /// </summary>

        private int _healthy { get; set; }
        public override int Healthy
        {
            get => _healthy;
            set => _healthy = value > 100 ? 100 : (value < 0 ? 0 : value);
        }
        [NotMapped]
        public string HealthyTitle
        {
            get
            {
                var res = "";
                if (Healthy == 100)
                {
                    res = "强壮";
                }
                else if (Healthy >= 75 && Healthy <= 99)
                {
                    res = "健康";
                }
                else if (Healthy >= 50 && Healthy <= 74)
                {
                    res = "瘦弱";
                }
                else if (Healthy >= 25 && Healthy <= 49)
                {
                    res = "生病";
                }
                else if (Healthy >= 0 && Healthy <= 24)
                {
                    res = "濒死";
                }
                return res;
            }
        }
    }

    public enum Gender
    {
        /// <summary>
        /// 所有人
        /// </summary>
        All = 0,

        /// <summary>
        /// 女
        /// </summary>
        Female = 2,

        /// <summary>
        /// 男
        /// </summary>
        Male = 1
    }

    public enum BabyState
    {
        /// <summary>
        /// 未成年
        /// </summary>
        [Description("未成年")]
        UnderAge = 1,
        /// <summary>
        /// 已成年
        /// </summary>
        [Description("已成年")]
        Adult = 2
    }
}
