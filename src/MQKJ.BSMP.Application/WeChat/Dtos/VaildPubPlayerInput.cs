using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class VaildPubPlayerInput
    {
        //[Required]
        public string Code { get; set; }

        public Guid? PlayerId { get; set; }
    }
}
