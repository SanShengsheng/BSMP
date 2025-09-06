using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.PlayerExtensions.Dtos
{
    public class SetIntroduceOfOneSelfInput
    {
        public Guid PlayerId { get; set; }

        /// <summary>
        /// 自我介绍
        /// </summary>
        public string Introduce { get; set; }
    }
}
