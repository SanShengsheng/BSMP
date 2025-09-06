using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class GetGameProgressInput
    {

        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
    }
}
