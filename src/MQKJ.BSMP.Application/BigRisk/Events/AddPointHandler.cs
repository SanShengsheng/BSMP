using System;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Handlers;
using Microsoft.Extensions.Logging;
using MQKJ.BSMP.BonusPoints;
using MQKJ.BSMP.Events.Datas;
using MQKJ.BSMP.PlayerExtensions;
using MQKJ.BSMP.PlayerExtensions.Dtos;
using MQKJ.BSMP.Players;

namespace MQKJ.BSMP.Events
{
    public class AddPointHandler : IAsyncEventHandler<StartGameComplatedEventData>, ITransientDependency
    {
        private readonly IPlayerExtensionAppService _playerExtensionAppService;
        private ILogger<AddPointHandler> _logger;

        public AddPointHandler(
            ILoggerFactory loggerFactory, IPlayerExtensionAppService playerExtensionAppService)
        {
            _playerExtensionAppService = playerExtensionAppService;
            _logger = loggerFactory.CreateLogger<AddPointHandler>();
        }

        public async Task HandleEventAsync(StartGameComplatedEventData eventData)
        {
            await _playerExtensionAppService.UpdateUserPoint(new UpdateUserPointRequest(eventData.InviterPlayerId, StaticBonusPointsCount.InviteFirendCount));
            await _playerExtensionAppService.UpdateUserPoint(new UpdateUserPointRequest(eventData.InviteePlayerId,
                StaticBonusPointsCount.InviteeCount));
        }
        
    }
}