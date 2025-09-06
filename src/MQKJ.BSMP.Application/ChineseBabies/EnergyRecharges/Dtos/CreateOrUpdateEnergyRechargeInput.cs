

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class CreateOrUpdateEnergyRechargeInput
    {
        [Required]
        public EnergyRechargeEditDto EnergyRecharge { get; set; }

    }
}