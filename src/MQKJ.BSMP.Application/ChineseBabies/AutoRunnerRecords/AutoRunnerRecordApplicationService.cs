
using System;
using System.Linq;
using Abp.Domain.Repositories;
using MQKJ.BSMP.ChineseBabies.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MQKJ.BSMP.ChineseBabies
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AutoRunnerRecordAppService :
        BsmpApplicationServiceBase<AutoRunnerRecord, Guid, AutoRunnerRecordEditDto, AutoRunnerRecordEditDto, GetAutoRunnerRecordsInput, AutoRunnerRecordListDto>,
        IAutoRunnerRecordAppService
    {
        public AutoRunnerRecordAppService(IRepository<AutoRunnerRecord, Guid> repository) : base(repository)
        {
        }

        internal override IQueryable<AutoRunnerRecord> GetQuery(GetAutoRunnerRecordsInput model)
        {
            return _repository.GetAll()
                .WhereIf(model.PlayerId.HasValue, a => a.PlayerId == model.PlayerId)
                .WhereIf(model.FamilyId.HasValue, a => a.FamilyId == model.FamilyId)
                .WhereIf(model.BabyId.HasValue, a => a.BabyId == model.BabyId);
        }
    }
}


