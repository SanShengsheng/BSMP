using Abp.Application.Services.Dto;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.Families.Dtos;

namespace MQKJ.BSMP.Web.Mvc.Areas.ChineseBabies.Controllers
{
    public class H5AliPayOutput
    {
        public H5AliPayOutput()
        {
        }

        public PagedResultDto<CoinRechargeListDto> CoinList { get; set; }
        public GetBabisFamilyInfoOutput FamilyInfo { get; set; }
    }
}