using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.Families.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MQKJ.BSMP.Web.Areas.ChineseBabies.Models
{
    public class FamilyModel
    {
        public List<CoinRechargeListDto> CoinRechargeListDtos { get; set; }

        public StaticPagedList<GetAllFamilysListDto> GetAllFamilysListDtos { get; set; }
    }
}
