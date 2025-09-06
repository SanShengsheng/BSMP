using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class GetWechatUserInfoInput
    {
        public bool SessionKeyIsValidate { get; set; }

        public string Code { get; set; }

        [Required]
        public string Iv { get; set; }

        [Required]
        [NotNull]
        public string EncryptedData { get; set; }
    }
}
