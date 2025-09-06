

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.StaminaRecords;

namespace MQKJ.BSMP.StaminaRecords.Dtos
{
    public class CreateOrUpdateStaminaRecordInput
    {
        [Required]
        public StaminaRecordEditDto StaminaRecord { get; set; }

    }
}