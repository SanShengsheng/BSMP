using Abp.Runtime.Validation;
using MQKJ.BSMP.ChineseBabies.Families.Model;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Families.Dtos
{
    public class GetAllFamilysInput : PagedSortedAndFilteredInputDto, IShouldNormalize, ISearchModel<Family, int>
    {
        public string BabyName { get; set; }

        public string FatherName { get; set; }

        public string MotherName { get; set; }

        public FamilyLevel? FamilyLevel { get; set; }

        //public int MaxRecharge { get; set; }

        //public int MinRecharge { get; set; }

            public RechargeRange RechargeRange { get; set; }

        public DateTime? StartTime { get; set; }


        public DateTime? EndTime { get; set; }
        public FamilyModelOrder [] Orders { get; set; }
        public List<int> TenantIds { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
