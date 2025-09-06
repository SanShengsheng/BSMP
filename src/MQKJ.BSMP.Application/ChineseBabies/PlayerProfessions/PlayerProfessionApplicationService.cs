
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;


using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.PlayerProfessions.Dtos;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// PlayerProfession应用层服务的接口实现方法  
    ///</summary>
    //[AbpAuthorize]
    public class PlayerProfessionAppService : 
        BsmpApplicationServiceBase<PlayerProfession,int,PlayerProfessionEditDto, PlayerProfessionEditDto,GetPlayerProfessionsInput, PlayerProfessionListDto>
        , IPlayerProfessionAppService
    {
        

        /// <summary>
        /// 构造函数 
        ///</summary>
        public PlayerProfessionAppService(
        IRepository<PlayerProfession, int> entityRepository
        
        ):base(entityRepository)
        {
            
        }

        internal override IQueryable<PlayerProfession> GetQuery(GetPlayerProfessionsInput model)
        {
            return _repository.GetAll();
        }

        public async Task<PlayerProfessionListDto> GetProfessionForFamily(GetPlayerProfessionInput input)
        {
            var playerProfession = await _repository.GetAll().Include(p => p.Profession).ThenInclude(r => r.Reward).Include(x => x.Profession.Costs).FirstOrDefaultAsync(p => p.PlayerId == input.PlayerId && p.FamilyId == input.FamilyId);

            if (playerProfession == null)
                return null;
            else
                return playerProfession.MapTo<PlayerProfessionListDto>();

        }

        public async Task<List<int>> GetChangedProfessions(GetChangedProfessionsInput input)
        {
            var professionIds = await _repository.GetAll().Select(x => x.ProfessionId).ToListAsync();

            return professionIds;
        }
    }
}


