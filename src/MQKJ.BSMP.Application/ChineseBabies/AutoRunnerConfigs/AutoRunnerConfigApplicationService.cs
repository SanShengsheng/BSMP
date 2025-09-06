
using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies.AutoRunnerConfigs.Dtos;
using MQKJ.BSMP.ChineseBabies.Dtos;


namespace MQKJ.BSMP.ChineseBabies
{

    [ApiExplorerSettings(IgnoreApi = true)]
    public class AutoRunnerConfigAppService :
        BsmpApplicationServiceBase<AutoRunnerConfig, Int32, AutoRunnerConfigEditDto, AutoRunnerConfigEditDto, GetAutoRunnerConfigsInput, AutoRunnerConfigListDto>, IAutoRunnerConfigAppService
    {
        public AutoRunnerConfigAppService(IRepository<AutoRunnerConfig, int> repository) : base(repository)
        {
        }

        public async Task AddOrUpdateConfig(AutoRunnerConfigEditDto input)
        {
            var config = await _repository.FirstOrDefaultAsync(r => r.FamilyId == input.FamilyId && r.PlayerId == input.PlayerId);
            if (config == null)
            {
                config = new AutoRunnerConfig
                {
                    CreationTime = DateTime.UtcNow,
                    FamilyId = input.FamilyId,
                    PlayerId = input.PlayerId,
                    BuyCount = input.BuyCount,
                    StudyLevel = input.StudyLevel,
                    ConsumeLevel = input.ConsumeLevel,
                    LevelAction = input.LevelAction
                };

                await _repository.InsertAsync(config);
            }
            else
            {
                config.LevelAction = input.LevelAction;
                config.ConsumeLevel = input.ConsumeLevel;
                config.BuyCount = input.BuyCount;
                config.StudyLevel = input.StudyLevel;
                await _repository.UpdateAsync(config);
            }

        }

        public async Task<AutoRunnerConfigListDto> GetAutoConfigAsync(GetConfigRequest request)
        {
            var config = await _repository.FirstOrDefaultAsync(r => r.FamilyId == request.FamilyId && r.PlayerId == request.PlayerId);

            return config.MapTo<AutoRunnerConfigListDto>();
        }

        internal override IQueryable<AutoRunnerConfig> GetQuery(GetAutoRunnerConfigsInput model)
        {
            return _repository.GetAll()
                .WhereIf(model.FamilyId.HasValue, a => a.FamilyId == model.FamilyId)
                .WhereIf(model.PlayerId.HasValue, a => a.PlayerId == model.PlayerId)
                .WhereIf(model.GroupId.HasValue, a => a.GroupId == model.GroupId)
                .WhereIf(model.ProfressionId.HasValue, a => a.ProfressionId == model.ProfressionId)
                .WhereIf(model.ConsumeLevel.HasValue, a => a.ConsumeLevel == model.ConsumeLevel)
                .WhereIf(model.FamilyLevel.HasValue, a => a.FamilyLevel == model.FamilyLevel);
        }
    }
}


