using Abp.AutoMapper;
using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos
{
    [AutoMapFrom(typeof(SeasonManagement))]
    public class GetAthleticsInfoOutput
    {
        public int SeasonNumber { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int MaxFightCount { get; set; }

        public int Price { get; set; }

        public int CanPKCount { get; set; }
    }
}
