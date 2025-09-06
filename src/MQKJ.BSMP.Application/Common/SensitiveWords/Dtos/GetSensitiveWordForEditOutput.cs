

using System.Collections.Generic;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.Common.SensitiveWords;

namespace MQKJ.BSMP.Common.SensitiveWords.Dtos
{
    public class GetSensitiveWordForEditOutput
    {

        public SensitiveWordEditDto SensitiveWord { get; set; }

    }
}