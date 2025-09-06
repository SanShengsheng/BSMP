using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.Informations.Events;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies.Informations
{
    public class InformationEventHandler :
        IEventHandler<CreateInformationEventData>,
        ITransientDependency,
        IAsyncEventHandler<CreateInformationEventData>
    {
        private readonly IInformationAppService _service;

        public InformationEventHandler(IInformationAppService service)
        {
            _service = service;
        }

        public void HandleEvent(CreateInformationEventData eventData)
        {
            _service.Add(ToInformationModel(eventData));
        }

        private InformationEditDto ToInformationModel(CreateInformationEventData eventData)
        {
            return new InformationEditDto
            {
                Content = eventData.Content,
                FamilyId = eventData.FamilyId,
                ReceiverId = eventData.ReceiverId,
                SenderId = eventData.SenderId,
                State = InformationState.Create,
                Type = eventData.Type
            };
        }

        public Task HandleEventAsync(CreateInformationEventData eventData)
        {
            return _service.Add(ToInformationModel(eventData));
        }
    }
}