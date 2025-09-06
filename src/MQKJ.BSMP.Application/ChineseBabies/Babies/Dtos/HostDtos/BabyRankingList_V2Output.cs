using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Babies.Dtos.HostDtos
{
    public class BabyRankingList_V2Output
    {
        public BabyRankingModel selfInfo { get; set; }

        public PagedResultDto<BabyRankingModel> pagedResultDto { get; set; }
    }
}
