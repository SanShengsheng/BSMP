using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using MQKJ.BSMP.ChineseBabies.Athetics.BuyFightCountRecords.Dtos;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Dtos;
using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies.Athetics.BuyFightCountRecords
{
    public class BuyFightCountRecordService : BsmpApplicationServiceBase<BuyFightCountRecord, Guid, BuyFightCountRecordEditDto, BuyFightCountRecordEditDto, GetBuyFightCountRecordInput, BuyFightCountRecordListDtos>, IBuyFightCountRecordService
    {
        public BuyFightCountRecordService(IRepository<BuyFightCountRecord, Guid> repository):base(repository)
        { }

        public Task<BuyFightCountOutput> BuyFightCount(BuyFightCountInput input)
        {
            throw new NotImplementedException();
        }

        internal override IQueryable<BuyFightCountRecord> GetQuery(GetBuyFightCountRecordInput model)
        {
            var query = _repository.GetAll();

            return query;
        }
    }
}
