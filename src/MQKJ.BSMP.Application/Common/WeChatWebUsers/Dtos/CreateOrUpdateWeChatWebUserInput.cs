

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Common;

namespace MQKJ.BSMP.Common.Dtos
{
    public class CreateOrUpdateWeChatWebUserInput
    {
        [Required]
        public WeChatWebUserEditDto WeChatWebUser { get; set; }

    }
}