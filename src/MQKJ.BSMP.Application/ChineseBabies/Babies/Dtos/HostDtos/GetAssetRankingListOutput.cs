using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Babies.Dtos.HostDtos
{
    public class GetAssetRankingListOutput
    {
        public GetAssetRankingListModel SelfInfo { get; set; }

        public PagedResultDto<GetAssetRankingListModel> pagedResultDto { get; set; }
    }
}
