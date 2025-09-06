
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
using MQKJ.BSMP.ChineseBabies.AutoRunnerConfigs.Dtos;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// AutoRunnerConfig应用层服务的接口方法
    ///</summary>
    public interface IAutoRunnerConfigAppService : BsmpApplicationService<AutoRunnerConfig, Int32, AutoRunnerConfigEditDto, AutoRunnerConfigEditDto, GetAutoRunnerConfigsInput, AutoRunnerConfigListDto>
    {
        Task<AutoRunnerConfigListDto> GetAutoConfigAsync(GetConfigRequest request);
        Task AddOrUpdateConfig(AutoRunnerConfigEditDto input);
    }
}
