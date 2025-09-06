

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class CreateOrUpdateEnergyRechargeRecordInput
    {
        [Required]
        public EnergyRechargeRecordEditDto EnergyRechargeRecord { get; set; }

    }
}