using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.BabyEvents.Dtos
{
    public class GetCoupeEventAndRechargeMessageOutput
    {
        public GetCoupeEventAndRechargeMessageOutput()
        {
            this.BabyEventRecords = new List<BabyEventRecord>();

            this.Informations = new List<Information>();
        }

        public List<BabyEventRecord> BabyEventRecords { get; set; }

        public List<Information> Informations { get; set; }
    }
}
