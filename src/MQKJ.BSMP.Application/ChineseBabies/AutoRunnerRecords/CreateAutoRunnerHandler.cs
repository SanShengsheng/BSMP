using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using MQKJ.BSMP.ChineseBabies.AutoRunnerRecords.EventDatas;
using MQKJ.BSMP.ChineseBabies.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies.AutoRunnerRecords
{
    public class CreateAutoRunnerHandler : IAsyncEventHandler<CreateAutoRunnerRecordEventData>, ITransientDependency
    {
        private readonly IAutoRunnerRecordAppService _service;
        public CreateAutoRunnerHandler(IAutoRunnerRecordAppService service) => _service = service;
        public Task HandleEventAsync(CreateAutoRunnerRecordEventData eventData) =>
            _service.Add(ToEditDto(eventData));

        private AutoRunnerRecordEditDto ToEditDto(CreateAutoRunnerRecordEventData eventData) =>
            new AutoRunnerRecordEditDto
            {
                ActionType = eventData.ActionType,
                FamilyId = eventData.FamilyId,
                GroupId = eventData.GroupId,
                NewData = eventData.NewData,
                OriginalData = eventData.OriginalData,
                PlayerId = eventData.PlayerId,
                RelateionId = eventData.RelationId,
                Description = eventData.Description
            };
    }
}
