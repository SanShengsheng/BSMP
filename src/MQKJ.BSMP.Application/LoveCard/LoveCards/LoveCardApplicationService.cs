
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.LoveCard.LoveCards.Dtos;
using MQKJ.BSMP.LoveCardOptions;
using MQKJ.BSMP.LoveCardOptions.Dtos;
using MQKJ.BSMP.LoveCards.DomainService;
using MQKJ.BSMP.LoveCards.Dtos;
using MQKJ.BSMP.PlayerLabels;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Utils.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MQKJ.BSMP.LoveCards
{
    /// <summary>
    /// LoveCard应用层服务的接口实现方法  
    ///</summary>
    //[AbpAuthorize]
    public class LoveCardAppService : BSMPAppServiceBase, ILoveCardAppService
    {
        private readonly IRepository<LoveCard, Guid> _entityRepository;

        private readonly IRepository<LoveCardOption, Guid> _loveCardOptionRepository;

        private readonly IRepository<Player, Guid> _playerRepository;

        private readonly IRepository<PlayerExtension> _playerExtensionRepository;

        private readonly IRepository<PlayerLabel, Guid> _playerLabelRepository;

        //private readonly BSMPDbContext  _bSMPDbContext;

        private readonly ILoveCardManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LoveCardAppService(
        IRepository<LoveCard, Guid> entityRepository
       , IRepository<LoveCardOption, Guid> loveCardOptionRepository
       , ILoveCardManager entityManager
       , IRepository<Player, Guid> playerRepository
       , IRepository<PlayerExtension> playerExtensionRepository
       , IRepository<PlayerLabel, Guid> playerLabelRepository
        //, BSMPDbContext bSMPDbContext
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;

            _loveCardOptionRepository = loveCardOptionRepository;

            _playerRepository = playerRepository;

            _playerExtensionRepository = playerExtensionRepository;

            _playerLabelRepository = playerLabelRepository;

            //_bSMPDbContext = bSMPDbContext;
        }


        /// <summary>
        /// 获取LoveCard的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		//[AbpAuthorize(LoveCardPermissions.Query)] 
        public async Task<PagedResultDto<LoveCardListDto>> GetPaged(GetLoveCardsInput input)
        {
            var query = _entityRepository.GetAll()
                .Include(lb => lb.PlayerLabels)
                .Include(p => p.Player).ThenInclude(e => e.PlayerExtension)
                .Include(f => f.LoveCardFiles).ThenInclude(f => f.BSMPFile)
                .Where(p => p.Player.Gender != input.Gender && p.PlayerId != input.PlayerId && p.State == (int)LoveCardState.Audited);

            // TODO:根据传入的参数添加过滤条件
            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            var loveCardIds = entityList.Select(c => c.Id);

            var likes = _loveCardOptionRepository.GetAll().Where(l => l.OptionPlayerId == input.PlayerId).AsNoTracking();

            var tempEntityList = new List<LoveCard>();

            foreach (var item in entityList)
            {
                item.LoveCardOption = likes.FirstOrDefault(l => l.LoveCardId == item.Id);

                tempEntityList.Add(item);
            }

            // var entityListDtos = ObjectMapper.Map<List<LoveCardListDto>>(entityList);
            var entityListDtos = tempEntityList.MapTo<List<LoveCardListDto>>();

            return new PagedResultDto<LoveCardListDto>(count, entityListDtos);
        }
        public async Task<PagedResultDto<LoveCardListDto>> GetAllCardList(GetAllCardListInput input)
        {
            var query = _entityRepository.GetAll()
                .Include(lb => lb.PlayerLabels)
                .Include(p => p.Player).ThenInclude(e => e.PlayerExtension)
                .Include(f => f.LoveCardFiles).ThenInclude(f => f.BSMPFile)
                .WhereIf(input.StartTime != null && input.EndTime != null, x => x.CreationTime > input.StartTime && x.CreationTime < input.EndTime);

            // TODO:根据传入的参数添加过滤条件
            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();


            // var entityListDtos = ObjectMapper.Map<List<LoveCardListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<LoveCardListDto>>();

            return new PagedResultDto<LoveCardListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取LoveCardListDto信息
        /// </summary>
        //[AbpAuthorize(LoveCardPermissions.Query)] 
        public async Task<LoveCardListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<LoveCardListDto>();
        }

        /// <summary>
        /// 获取编辑 LoveCard
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[AbpAuthorize(LoveCardPermissions.Create,LoveCardPermissions.Edit)]
        public async Task<GetLoveCardForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetLoveCardForEditOutput();
            LoveCardEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<LoveCardEditDto>();

                //loveCardEditDto = ObjectMapper.Map<List<loveCardEditDto>>(entity);
            }
            else
            {
                editDto = new LoveCardEditDto();
            }

            output.LoveCard = editDto;
            return output;
        }

        public async Task<LoveCardListDto> GetLoveCardById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAll()
                 .Include(lb => lb.PlayerLabels)
                .Include(p => p.Player).ThenInclude(e => e.PlayerExtension)
                .Include(f => f.LoveCardFiles).ThenInclude(f => f.BSMPFile)
                .FirstOrDefaultAsync(c => c.Id == input.Id);

            return entity.MapTo<LoveCardListDto>();
        }

        public async Task<PagedResultDto<LoveCardListDto>> GetOwnShareAndLikedList(GetOwnShareAndLikedListInput input)
        {
            var LoveCardIds = await _loveCardOptionRepository.GetAll()
                .Where(x => x.OptionPlayerId == input.PlayerId && x.IsLiked == true)
                .AsNoTracking()
                .Select(x => x.LoveCardId)
                .ToListAsync();

            var query = _entityRepository.GetAll()
                 .Include(lb => lb.PlayerLabels)
                .Include(p => p.Player).ThenInclude(e => e.PlayerExtension)
                .Include(f => f.LoveCardFiles).ThenInclude(f => f.BSMPFile)
                .Where(x => LoveCardIds.Contains(x.Id))
                .AsNoTracking();

            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            var entityListDtos = entityList.MapTo<List<LoveCardListDto>>();

            return new PagedResultDto<LoveCardListDto>(count, entityListDtos);

        }

        public async Task UpdateLoveCardState(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.FirstOrDefaultAsync(c => c.Id == input.Id);

            entity.State = (int)LoveCardState.Audited;

            await _entityRepository.UpdateAsync(entity);
        }

        public async Task OptionCardState(OptionCardStateDto input)
        {

            var entity = await _entityRepository.FirstOrDefaultAsync(x => x.Id == input.LoveCardId);

            if (entity != null)
            {
                if (input.LoveCardOptionType == LoveCardOptionType.Like)
                {
                    //entity.IsLiked = entity.IsLiked == true ? false : true;

                    entity.LikeCount += 1;

                }
                else if (input.LoveCardOptionType == LoveCardOptionType.SaveAlbum)
                {
                    entity.SaveCount += 1;
                }
                else if (input.LoveCardOptionType == LoveCardOptionType.Share)
                {
                    entity.ShareCount += 1;
                }

                await _entityRepository.UpdateAsync(entity);
            }

            else
            {
                Logger.Error($"查询的卡片实体是空的,传的参数:{input.LoveCardId}");
            }

            await RecordCardOption(input);

        }

        private async Task RecordCardOption(OptionCardStateDto input)
        {
            var cardOptionEntity = await _loveCardOptionRepository.FirstOrDefaultAsync(o => o.LoveCardId == input.LoveCardId && o.OptionPlayerId == input.OptionPlayerId);

            if (cardOptionEntity != null && input.LoveCardOptionType == LoveCardOptionType.Like)
            {
                cardOptionEntity.IsLiked = cardOptionEntity.IsLiked == true ? false : true;

                await _loveCardOptionRepository.UpdateAsync(cardOptionEntity);

            }
            else
            {
                var entity = new LoveCardOption();

                if (input.LoveCardOptionType == LoveCardOptionType.Like)
                {
                    entity.IsLiked = true;
                }
                entity.LoveCardId = input.LoveCardId;
                entity.OptionPlayerId = input.OptionPlayerId;
                entity.LoveCardOptionType = input.LoveCardOptionType;
                await _loveCardOptionRepository.InsertAsync(entity);
            }
        }


        /// <summary>
        /// 添加或者修改LoveCard的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[AbpAuthorize(LoveCardPermissions.Create,LoveCardPermissions.Edit)]
        public async Task CreateOrUpdate(CreateOrUpdateLoveCardInputbak input)
        {

            if (input.LoveCard.Id.HasValue)
            {
                await Update(input.LoveCard);
            }
            else
            {
                await Create(input.LoveCard);
            }
        }

        /// <summary>
        /// 新增LoveCard
        /// </summary>
        //[AbpAuthorize(LoveCardPermissions.Create)]
        protected virtual async Task<LoveCardEditDto> Create(LoveCardEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LoveCard>(input);
            var entity = input.MapTo<LoveCard>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<LoveCardEditDto>();
        }

        /// <summary>
        /// 编辑LoveCard
        /// </summary>
        //[AbpAuthorize(LoveCardPermissions.Edit)]
        protected virtual async Task Update(LoveCardEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 获取个人名片信息
        /// </summary>
        /// <param name="input"></param>
        public async Task<LoveCardListDto> GetLoveCardInfo(GetLoveCardInfoInput input)
        {
            var entity = await _entityRepository.GetAll()
                .Include(p => p.Player).ThenInclude(e => e.PlayerExtension)
                .Include(l => l.PlayerLabels)
                .Include(f => f.LoveCardFiles).ThenInclude(f => f.BSMPFile)
                .FirstOrDefaultAsync(c => c.PlayerId == input.PlayerId);

            if (entity != null)
            {
                return entity.MapTo<LoveCardListDto>();
            }
            else
            {
                return null;
            }
        }

        public async Task<CreateOrUpdateLoveCardOutput> CreateOrUpdateLoveCard(CreateOrUpdateLoveCardInput input)
        {
            var player = await _playerRepository.GetAll().Include(x => x.PlayerExtension).FirstOrDefaultAsync(p => p.Id == input.PlayerId);


            if (player != null)
            {



                player.Gender = (int)input.Gender;

                player.BirthDate = input.BirthDate.Value;

                //player.PlayerExtension.Constellation = CalculationConstellation(player.BirthDate.Value);

                player.WeChatAccount = input.WeChatAccount;

                player.Domicile = input.Domicile;

                await _playerRepository.UpdateAsync(player);

                //await _playerExtensionRepository.UpdateAsync(player.PlayerExtension);

                if (input.Label == null)
                {
                    return null;
                }


                if (input.Id.HasValue)
                {
                    return await UpdateLoveCard(input);
                }
                else
                {
                    return await CreateLoveCard(input);
                }

            }
            else
            {
                return null;
            }
        }

        public async Task<CreateOrUpdateLoveCardOutput> UpdateLoveCardOtherInfo(UpdateLoveCardOtherInfoInput input)
        {
            var player = await _playerRepository.GetAll().Include(x => x.PlayerExtension).FirstOrDefaultAsync(p => p.Id == input.PlayerId);


            if (player != null)
            {
                var playerExtension = await _playerExtensionRepository.FirstOrDefaultAsync(p => p.PlayerGuid == input.PlayerId);
                playerExtension.Introduce = input.Introduce;
                playerExtension.Profession = input.Profession;
                playerExtension.Weight = input.Weight;
                playerExtension.Height = input.Height;
                await _playerExtensionRepository.UpdateAsync(playerExtension);

                return new CreateOrUpdateLoveCardOutput() { LoveCardId = input.LoveCardId };
            }
            else
            {
                return null;
            }
        }
        private string CalculationConstellation(DateTime dateTime)
        {
            var month = dateTime.Month;
            var day = dateTime.Day;
            if (month == 12 && day >= 21 || month == 1 && day <= 19)
                return "摩羯座";
            if (month == 1 && day >= 20 || month == 2 && day <= 18)
                return "水瓶座";
            if (month == 2 && day >= 19 || month == 3 && day <= 20)
                return "双鱼座";
            if (month == 3 && day >= 21 || month == 4 && day <= 19)
                return "白羊座";
            if (month == 4 && day >= 20 || month == 5 && day <= 20)
                return "金牛座";
            if (month == 5 && day >= 21 || month == 6 && day <= 21)
                return "双子座";
            if (month == 6 && day >= 22 || month == 7 && day <= 22)
                return "巨蟹座";
            if (month == 7 && day >= 23 || month == 8 && day <= 22)
                return "狮子座";
            if (month == 8 && day >= 23 || month == 9 && day <= 22)
                return "处女座";
            if (month == 9 && day >= 23 || month == 10 && day <= 23)
                return "天秤座";
            if (month == 10 && day >= 24 || month == 11 && day <= 22)
                return "天蝎座";
            if (month == 11 && day >= 23 || month == 12 && day <= 20)
                return "射手座";
            return "未知";
        }

        private async Task<CreateOrUpdateLoveCardOutput> UpdateLoveCard(CreateOrUpdateLoveCardInput input)
        {

            var labels = new List<PlayerLabel>();

            var loveCard = await _entityRepository.FirstOrDefaultAsync(c => c.Id == input.Id.Value);

            loveCard.StyleCode = input.StyleCode;


            var labelIds = _playerLabelRepository.GetAll().Where(l => l.LoveCardId == input.Id).Select(x => x.Id);

            if (labelIds.Count() > 0)
            {
                await _playerLabelRepository.DeleteAsync(x => labelIds.Contains(x.Id));
            }

            var labelArr = input.Label.Split(',');

            var i = 0;
            foreach (var item in labelArr)
            {
                var playerLabel = new PlayerLabel();

                playerLabel.LabelContent = labelArr[i];

                playerLabel.LoveCardId = input.Id.Value;

                labels.Add(playerLabel);

                i++;
            }

            loveCard.PlayerLabels = labels;

            await _entityRepository.UpdateAsync(loveCard);

            return new CreateOrUpdateLoveCardOutput()
            {
                LoveCardId = loveCard.Id,
                CardCode = loveCard.CardCode
            };
        }

        private async Task<CreateOrUpdateLoveCardOutput> CreateLoveCard(CreateOrUpdateLoveCardInput input)
        {
            var carCode = string.Empty;

            if (input.Gender == PlayerGender.Man)
            {
                carCode = 1 + RandomHelper.GenerateRandomCode(5);
            }
            else if (input.Gender == PlayerGender.WoMan)
            {
                carCode = 2 + RandomHelper.GenerateRandomCode(5);
            }
            else
            {
                Logger.Warn($"传过来的性别是未知");
                return new CreateOrUpdateLoveCardOutput();
            }

            var loveCard = await _entityRepository.InsertAsync(new LoveCard()
            {
                PlayerId = input.PlayerId,
                CardCode = carCode,
                State = (int)LoveCardState.UnAudited,
                StyleCode = input.StyleCode
            });

            var labels = input.Label.Split(',');

            foreach (var item in labels)
            {
                var playerLabel = new PlayerLabel()
                {
                    LoveCardId = loveCard.Id,

                    LabelContent = item
                };

                await _playerLabelRepository.InsertAsync(playerLabel);
            }

            return new CreateOrUpdateLoveCardOutput()
            {
                LoveCardId = loveCard.Id,
                CardCode = loveCard.CardCode
            };
        }

        public async Task<UpdateLoveCardPageViewsOutput> UpdateLoveCardPageViews(UpdateLoveCardPageViewsInput input)
        {
            var entity = await _entityRepository.FirstOrDefaultAsync(x => x.Id == input.LoveCardId);

            if (entity != null)
            {
                entity.ViewsCount += 1;

                await _entityRepository.UpdateAsync(entity);

                return new UpdateLoveCardPageViewsOutput()
                {
                    ViewsCount = entity.ViewsCount
                };
            }

            return new UpdateLoveCardPageViewsOutput()
            {
                ViewsCount = -1
            };
        }

        //public async Task UpdateLoveCardLabel(UpdateLoveCardLabelInput input)
        //{
        //    var entity = await _entityRepository.FirstOrDefaultAsync(l => l.Id == input.LoveCardId);

        //    if (entity != null)
        //    {
        //        entity.PlayerLabels = 
        //    }
        //}

        /// <summary>
        /// 获取赞与分享的人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetLikeAndSharePlayerListOutput>> GetLikeAndSharePlayerList(GetLikeAndSharePlayerListInput input)
        {
            var query = _loveCardOptionRepository.GetAll()
                .Include(p => p.OptionPlayer)
                .Where(o => o.LoveCardId == input.LoveCardId && (o.IsLiked == true || o.LoveCardOptionType == LoveCardOptionType.Share))
                .OrderByDescending(t => t.CreationTime)
                .AsNoTracking();

            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();


            var entityListDtos = entityList.MapTo<List<GetLikeAndSharePlayerListOutput>>();

            return new PagedResultDto<GetLikeAndSharePlayerListOutput>(count, entityListDtos);

        }


        /// <summary>
        /// 删除LoveCard信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[AbpAuthorize(LoveCardPermissions.Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除LoveCard的方法
        /// </summary>
        //[AbpAuthorize(LoveCardPermissions.BatchDelete)]
        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 导出LoveCard为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}

    }
}


