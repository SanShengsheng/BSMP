using MQKJ.BSMP.Common.Adviertisements.Dtos;
using MQKJ.BSMP.MultiTenancy;
using MQKJ.BSMP.MultiTenancy.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MQKJ.BSMP.Web.Areas.Adviertisements.Models
{
    public class AdviertisementModel
    {

        public List<TenantDto> Tenants { get; set; }

        public StaticPagedList<GetAdviertisementsOutput> staticPagedList { get; set; }
    }
}
