using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Models.DebugModel
{
    public class DebugGetBabiesInput
    {
        public string BabyId { get; set; }
        public string BabyName { get; set; }
        public string FamilyId { get; set; }
        public string Mother { get; set; }
        public string Father { get; set; }
        public string FatherNickName { get; set; }
        public string MotherNickName { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class DebugGetBabiesOutput
    {
        public string BabyName { get; set; }
        public int FamilyId { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsAdult { get; set; }
        public int BabyId { get; set; }
    }
    public class SetUpEndOutput { 
        public string Message { get; set; }
        public bool Success { get; set; }
    }
    public class SetUpEndInput {
        public string BabyId { get; set; }
    }
}
