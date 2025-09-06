using Abp.Application.Services;
using MQKJ.BSMP.ChineseBabies.Babies.Dtos.PrestigeDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies.Babies
{
    public interface IBabyPrestigeAppService:IApplicationService
    {
        Task<GoToWorshipOutput> GoToWorship(GoToWorshipInput input);
        Task<RankPrestigesOutput> RankPrestigesByBaby(string babyId);
        
    }
}
