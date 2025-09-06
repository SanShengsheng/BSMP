using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using MQKJ.BSMP.BonusPointRecords;
using MQKJ.BSMP.BonusPointRecords.Dtos;
using MQKJ.BSMP.BonusPoints;
using MQKJ.BSMP.BonusPoints.Dtos;
using MQKJ.BSMP.Events.Datas;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace MQKJ.BSMP.Events
{
    public class ChangedPointAddRecordHandler : IAsyncEventHandler<ChangedPointEventData>, 
        ITransientDependency
    {
        private readonly IBonusPointRecordAppService _bonusPointAppService;
        private readonly ILogger<ChangedPointAddRecordHandler> _logger;
        public ChangedPointAddRecordHandler(IBonusPointRecordAppService bonusPointAppService,
            ILoggerFactory loggerFactory)
        {
            _bonusPointAppService = bonusPointAppService;
            _logger = loggerFactory.CreateLogger<ChangedPointAddRecordHandler>();
        }
       

        public async Task HandleEventAsync(ChangedPointEventData eventData)
        {
            await _bonusPointAppService.CreateOrUpdateBonusPointRecord(new CreateOrUpdateBonusPointRecordInput()
            {
                BonusPointRecord = new BonusPointRecordEditDto
                {
                    BonusPointId = eventData.BonusPointId,
                    GatherCount = eventData.GatherCount,
                    PlayerId = eventData.PlayerId
                }
            });
        }
    }
}