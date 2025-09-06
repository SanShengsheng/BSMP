
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
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using Abp.Application.Services.Dto;


using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.PlayerProfessions.Dtos;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// PlayerProfession应用层服务的接口方法
    ///</summary>
    public interface IPlayerProfessionAppService : BsmpApplicationService<PlayerProfession,int,PlayerProfessionEditDto,PlayerProfessionEditDto,GetPlayerProfessionsInput,PlayerProfessionListDto>
    {
        Task<PlayerProfessionListDto> GetProfessionForFamily(GetPlayerProfessionInput input);

        /// <summary>
        /// 获取已转职业的职业Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<int>> GetChangedProfessions(GetChangedProfessionsInput input);


    }
}
