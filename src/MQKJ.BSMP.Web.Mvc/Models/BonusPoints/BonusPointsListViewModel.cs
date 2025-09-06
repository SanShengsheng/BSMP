using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using MQKJ.BSMP.BonusPointRecords.Dtos;
using MQKJ.BSMP.BonusPoints.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Models.BonusPoints
{
    public class BonusPointsListViewModel
    {
        public PagedResultDto<BonusPointRecordListDto> Records { get; set; }
        public PagedResultDto<BonusPointListDto> Events { get; set; }
    }
}
