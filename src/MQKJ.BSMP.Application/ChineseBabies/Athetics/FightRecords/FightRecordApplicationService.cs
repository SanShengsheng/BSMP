using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using MQKJ.BSMP.ChineseBabies.Athetics.FightRecords.Dtos;
using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies.Athetics.FightRecords
{
    public class FightRecordApplicationService : BsmpApplicationServiceBase<FightRecord, Guid, FightRecordEditDto, FightRecordEditDto, GetFightRecordInput, FightRecordListDtos>, IFightRecordApplicationService
    {
        public FightRecordApplicationService(IRepository<FightRecord, Guid> repository):base(repository)
        { }
        
        internal override IQueryable<FightRecord> GetQuery(GetFightRecordInput model)
        {
            var query = _repository.GetAll();

            return query;
        }
    }
}
