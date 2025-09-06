

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Common.OperationActivities;

namespace MQKJ.BSMP.Common.OperationActivities.Dtos
{
    public class CreateOrUpdateOperationActivityInput
    {
        [Required]
        public OperationActivityEditDto OperationActivity { get; set; }

    }
}