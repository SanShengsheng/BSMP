using Abp.Application.Services.Dto;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Models
{
    public class RankingModel
    {
        public PagedResultDto<GetRankingListOutput> RankingListDto { get; set; }

        /// <summary>
        /// 自己的信息
        /// </summary>
        public GetRankingListOutput SelfRankingInfo { get; set; }
    }
}
