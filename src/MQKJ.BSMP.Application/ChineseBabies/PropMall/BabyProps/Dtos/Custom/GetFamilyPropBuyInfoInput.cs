using Abp.Runtime.Validation;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;

namespace MQKJ.BSMP.ChineseBabies
{
    public class GetFamilyPropBuyInfoInput 
    {
        public int[] PropIds { get; set; }

        public  int  FamilyId { get; set; }

        public int BabyId { get; set; }


    }
}