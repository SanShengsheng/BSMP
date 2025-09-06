using Abp.AutoMapper;
using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos
{
    [AutoMapFrom(typeof(Competition))]
    public class CompetitionListDtos : ISearchOutModel<Competition, Guid>
    {

    }
}
