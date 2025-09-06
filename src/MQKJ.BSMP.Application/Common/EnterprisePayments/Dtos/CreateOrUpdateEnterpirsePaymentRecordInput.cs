

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Common;

namespace MQKJ.BSMP.Common.Dtos
{
    public class CreateOrUpdateEnterpirsePaymentRecordInput
    {
        [Required]
        public EnterpirsePaymentRecordEditDto EnterpirsePaymentRecord { get; set; }

    }
}