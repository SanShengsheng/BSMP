

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.Common.Dtos
{
    public class CreateOrUpdateVersionManageInput
    {
        [Required]
        public VersionManageEditDto VersionManage { get; set; }

    }
}