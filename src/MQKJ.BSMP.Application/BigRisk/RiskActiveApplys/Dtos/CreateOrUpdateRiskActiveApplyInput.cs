

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ActiveApply;

namespace MQKJ.BSMP.ActiveApply.Dtos
{
    public class CreateOrUpdateRiskActiveApplyInput
    {
        [Required]
        public RiskActiveApplyEditDto RiskActiveApply { get; set; }

    }
}