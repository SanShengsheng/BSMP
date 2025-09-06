using Abp.Application.Services;
using MQKJ.BSMP.ChineseBabies.PropMall.BabyPropTerms.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies.PropMall.Props.Terms
{
  public  interface IBabyPropTermApplicationService : IApplicationService
    {
        Task<bool> ValideBabyPropTermSatisfyHandle(BabyPropBuyTermIsSatisfyInput input);
    }
}
