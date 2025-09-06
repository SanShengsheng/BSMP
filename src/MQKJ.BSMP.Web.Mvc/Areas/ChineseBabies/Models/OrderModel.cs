using Abp.Application.Services.Dto;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.MiniappServices.BigRisk.Models;
using MQKJ.BSMP.Orders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Areas.ChineseBabies.Models
{
    public class OrderModel
    {
        public PagedResultDto<CoinRechargeListDto> CoinRechargeListDto { get; set; }

        public PagedResultDto<OrderListDto> OrderListDto { get; set; }

        public List<BSMPTenantDto> Tenants { get; set; }
    }
}
