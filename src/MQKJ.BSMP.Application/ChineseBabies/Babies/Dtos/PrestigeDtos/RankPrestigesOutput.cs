using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Babies.Dtos.PrestigeDtos
{
    public class RankPrestigesOutput
    {
        /// <summary>
        /// 当前家庭信息
        /// </summary>
        public RankPrestigesItem SelfInfo { get; set; }
        /// <summary>
        /// 排行榜信息
        /// </summary>
        public PagedResultDto<RankPrestigesItem> PagedResultDto { get; set; }
        public static Func<RankPrestigesOutput,IReadOnlyList<RankPrestigesItem>, int, PagedResultDto<RankPrestigesItem>> NewPager = (RankPrestigesOutput self, IReadOnlyList<RankPrestigesItem> data, int totals) =>self.PagedResultDto = new PagedResultDto<RankPrestigesItem>(totals,data);
        public static RankPrestigesOutput NewInstance() => new RankPrestigesOutput();
        public int TimesLimit { get; set; } = 0;
    }
}
