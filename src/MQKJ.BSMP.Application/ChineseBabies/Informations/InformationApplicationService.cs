
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.Informations.Dtos;
using MQKJ.BSMP.ChineseBabies.Message.Dtos;
using MQKJ.BSMP.Common.RunHorseInformations;
using MQKJ.BSMP.EntityFrameworkCore.Repositories;
using MQKJ.BSMP.SystemMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// Information应用层服务的接口实现方法  
    ///</summary>

    public class InformationAppService :
        BsmpApplicationServiceBase<Information, Guid, InformationEditDto, InformationEditDto, GetInformationsInput, InformationListDto>, IInformationAppService
    {

        private readonly IFamilyAppService _familyAppService;
        //private readonly IDistributedCache _memoryCache;
        private readonly IRepository<SystemMessage, int> _systemMessageRepository;
        private readonly IInformationRepository _informationRepository;

        private readonly IRepository<RunHorseInformation, Guid> _runHorseRepository;
        public InformationAppService(IRepository<Information, Guid> repository,
            IFamilyAppService familyAppService,
            IRepository<SystemMessage, int> systemMessageRepository,
            IInformationRepository informationRepository,
            IRepository<RunHorseInformation, Guid> runHorseRepository) : base(repository)
        {
            _familyAppService = familyAppService;
            //_memoryCache = memoryCache;
            _systemMessageRepository = systemMessageRepository;
            _informationRepository = informationRepository;
            _runHorseRepository = runHorseRepository;
        }

        public async Task<HasNewInformationResponse> HasNewInformation(HasNewInformationRequest request)
        {
            var response = new HasNewInformationResponse();
            var query = _repository.GetAll()
                .Where(r => r.State == InformationState.Create && r.NoticeType== NoticeType.Default)
                .WhereIf(request.PlayerId.HasValue, r => r.ReceiverId == request.PlayerId)
                .WhereIf(request.FamilyId.HasValue, r => r.FamilyId == request.FamilyId)
                .WhereIf(request.InformationType.HasValue, r => r.Type == request.InformationType)
                .AsNoTracking()
                .OrderByDescending(o => o.CreationTime);
            //弹框消息

            response.PopoutMessage = await GetPopoutMessage(request);
            response.HasMessage = await query.AnyAsync();
            return response;
        }

        public async Task<List<HasNewInformationResponseMessage>> GetPopoutMessage(HasNewInformationRequest request)
        {
            if (request.PlayerId.HasValue &&
                request.FamilyId.HasValue &&
                request.InformationType.HasValue)
            {
                var popoutMessages = _repository.GetAll()
                    .Where(s => s.NoticeType == NoticeType.Popout &&
                        s.State == InformationState.Create &&
                        s.ReceiverId == request.PlayerId &&
                        s.FamilyId == request.FamilyId &&
                        (s.BabyId == null || s.BabyId == request.BabyId));


                var nonRechargePopoutMessages = popoutMessages.Where(s => s.Type != InformationType.Recharge);
                var rechargePopoutMessages = popoutMessages.Where(s => s.Type == InformationType.Recharge);
                var messagesReponse = await nonRechargePopoutMessages
                        .Take(5)
                        .Union(rechargePopoutMessages)
                        .Select(m => new HasNewInformationResponseMessage
                        {
                            BabyEventId = m.BabyEventId,
                            Content = m.Content,
                            Id = m.Id,
                            Image = m.Image,
                            Remark = m.Remark,
                            SystemInformationType = m.SystemInformationType,
                            Title = m.Title
                        })
                        .ToListAsync();

                //批量更新
                await _informationRepository.BatchUpdatePoperInfoState(request.FamilyId.Value, request.PlayerId.Value);

                return messagesReponse;
            }

            return new List<HasNewInformationResponseMessage>();
        }

        internal override IQueryable<Information> GetQuery(GetInformationsInput model)
        {

            var query = _repository.GetAllIncluding(s => s.Sender, r => r.Receiver)
                .Where(r => r.State == InformationState.Create)
                .WhereIf(model.PlayerId.HasValue, r => r.ReceiverId == model.PlayerId)
                .WhereIf(model.FamilyId.HasValue, r => r.FamilyId == model.FamilyId)
                .WhereIf(model.InformationType.HasValue, r => r.Type == model.InformationType)
                .OrderByDescending(r => r.CreationTime);

            if (query.Count() <= 0)
            {
                query = _repository.GetAllIncluding(s => s.Sender, r => r.Receiver)
                .WhereIf(model.PlayerId.HasValue, r => r.ReceiverId == model.PlayerId)
                .WhereIf(model.FamilyId.HasValue, r => r.FamilyId == model.FamilyId)
                .WhereIf(model.InformationType.HasValue, r => r.Type == model.InformationType)
                .OrderByDescending(r => r.CreationTime);
            }
            return query;
        }

        public async Task ModifyInforationState(ModifyInforationStateInput input)
        {
            List<Guid> Ids = new List<Guid>();

            foreach (var item in input.Ids)
            {
                Ids.Add(item);
            }
            var query = _repository.GetAll().Where(x => Ids.Contains(x.Id));

            foreach (var item in query)
            {
                item.State = InformationState.Readed;
                await _repository.UpdateAsync(item);
            }
        }
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<InformationListDto>> GetInformations(GetInformationsInput input)
        {
            var playerId = input.PlayerId;

            if (input.InformationType == InformationType.System)
            {
                input.PlayerId = null;
            }

            var response = new PagedResultDto<InformationListDto>();
            var infomationList = new List<InformationListDto>();
            if (input.InformationType == InformationType.Barrage)
            {
                // 获取系统消息
                var sysmessages = await _systemMessageRepository.GetAllListAsync(s => s.ExprieDateTime > DateTime.Now && s.StartDateTime < DateTime.Now && s.NoticeType == SystemMessages.NoticeType.All && s.PriorityLevel == 100 &&
               (s.PeriodType == PeriodType.NeverStop
               || (s.PeriodType == PeriodType.Minute && ((DateTime.Now.Minute - s.StartDateTime.Minute) % s.Period) == 0)));
                sysmessages.ForEach(s =>
                {
                    for (int i = 0; i < s.Count; i++)
                    {
                        infomationList.Add(new InformationListDto()
                        {
                            Content = s.Content
                        });
                    }
                });
            }

            if ((input.InformationType == InformationType.System || input.InformationType == InformationType.Recharge) && !input.FamilyId.HasValue)
            {
                throw new Exception("参数错误，请检查参数");
            }

            // 消息
            var query = _repository.GetAllIncluding(s => s.Sender, d => d.Receiver)
                .Where(s=>s.NoticeType== NoticeType.Default)
                .WhereIf(input.InformationType != null, s => s.Type == input.InformationType)
                .WhereIf(input.PlayerId != null, s => s.ReceiverId == input.PlayerId)
                .WhereIf(input.FamilyId != null, s => s.FamilyId == input.FamilyId)
                 .AsNoTracking();
            if (input.PageSize > 0)
            {
                input.MaxResultCount = input.PageSize;
            }
            if (input.PageIndex > 0)
            {
                input.SkipCount = (input.PageIndex - 1) * input.PageSize;
            }
            var count = query.Count();
            await query
                   .OrderBy(input.Sorting)
                   .PageBy(input).ForEachAsync(s =>
                   {
                       var infomation = ObjectMapper.Map<InformationListDto>(s);
                       if (s.SystemInformationType == SystemInformationType.BigBag && s.ReceiverId == playerId)
                       {
                           infomationList.Add(infomation);
                       }
                       else if (s.SystemInformationType != SystemInformationType.BigBag)
                       {
                           infomationList.Add(infomation);
                       }
                   });
            response = new PagedResultDto<InformationListDto>(
                        count,
                        infomationList
                );
            return response;
        }

        /// <summary>
        /// 获取跑马灯消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<RunHorseInformationListDto>> GetRunHorseInformations(GetRunHorseInformationsInput input)
        {
            var query = _runHorseRepository.GetAll()
                .Where(x => x.EndTime >= DateTime.Now)
                .WhereIf(input.PlayScene.HasValue, r => r.PlayScene == input.PlayScene);

            var count = await query.CountAsync();

            var entityList = await query
                    .AsNoTracking()
                    .OrderBy(input.Sorting)
                    .ThenByDescending(i => i.Priority)
                    .PageBy(input)
                    .ToListAsync();

            var entityListDtos = entityList.MapTo<List<RunHorseInformationListDto>>();

            return new PagedResultDto<RunHorseInformationListDto>(count, entityListDtos);
        }
    }
}


