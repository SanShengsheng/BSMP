using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using MQKJ.BSMP.ChineseBabies.Dtos;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies.BabyGrowUpRecords
{
    public class GrowUpEventHandler :
        IEventHandler<BabyGrowUpRecordEditDto>,
        IAsyncEventHandler<BabyGrowUpRecordEditDto>,
        ITransientDependency
    {
        private readonly IBabyGrowUpRecordAppService _service;

        public GrowUpEventHandler(IBabyGrowUpRecordAppService service) => _service = service;

        public void HandleEvent(BabyGrowUpRecordEditDto eventData) => _service.CreateOrUpdate(new CreateOrUpdateBabyGrowUpRecordInput()
        {
            BabyGrowUpRecord = eventData
        });

        public Task HandleEventAsync(BabyGrowUpRecordEditDto eventData) => _service.CreateOrUpdate(new CreateOrUpdateBabyGrowUpRecordInput()
        {
            BabyGrowUpRecord = eventData
        });
    }
}