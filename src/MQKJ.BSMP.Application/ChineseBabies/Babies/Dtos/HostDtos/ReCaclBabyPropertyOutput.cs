using MQKJ.BSMP.ChineseBabies.Babies.Dtos;
using System.Collections.Generic;

namespace MQKJ.BSMP.ChineseBabies
{
    public class ReCaclBabyPropertyOutput
    {
      public ReCaclBabyPropertyOutput()
        {
            BabySixProperty = new ReCaclBabyPropertyOutputBabySixProperty();
        }
        public ReCaclBabyPropertyOutputBabySixProperty BabySixProperty { get; set; }
    }

    public class ReCaclBabyPropertyOutputBabySixProperty
    {
        public int BabyId { get; set; }

        public BabySixBasicProperty BabySixBasicProperty { get; set; }

        public BabySixBasicProperty ProfessionAddition { get; set; }

        public BabySixBasicProperty FamilyAssetAddition { get; set; }

        public BabySixBasicProperty EventAward { get; set; }

        public BabySixBasicProperty BirthOwn { get; set; }

        public BabySixBasicProperty CurrentProperty { get; set; }
        /// <summary>
        /// 与原来的值偏差
        /// </summary>
        public  int TotalOffset { get; set; }
    }
}