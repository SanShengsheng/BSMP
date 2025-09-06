
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Json;
using Abp.Linq.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MQKJ.BSMP.HttpContextHelper;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Web;
using MQKJ.BSMP.Utils.Extensions;
using MQKJ.BSMP.Common.AD;
using MQKJ.BSMP.Common.Adviertisements.Dtos;
using MQKJ.BSMP.Players;

namespace MQKJ.BSMP.Common.Adviertisements
{
    /// <summary>
    /// EnterpirsePaymentRecord应用层服务的接口实现方法  
    ///</summary>
    //[AbpAuthorize]
    public class AdviertisementApplicationService : BSMPAppServiceBase, IAdviertisementApplicationService
    {


        private readonly IRepository<Adviertisement> _entityRepository;
        private readonly IRepository<Player,Guid> _playerRepository;
        private readonly IRepository<PlayerAdviertisement,Guid> _playerAdviertisementRepository;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public AdviertisementApplicationService(
        IRepository<Adviertisement> entityRepository,
        IRepository<Player, Guid> playerRepository,
        IRepository<PlayerAdviertisement, Guid> playerAdviertisementRepository
        )
        {
            _entityRepository = entityRepository;

            _playerRepository = playerRepository;

            _playerAdviertisementRepository = playerAdviertisementRepository;
        }

        public async Task<PagedResultDto<GetAdviertisementsOutput>> GetAdviertisements(GetAdviertisementsInput input)
        {
            if (input.TeantId.HasValue && input.TeantId == 0) input.TeantId = null;
            var query = _entityRepository.GetAll()
             .Include(a => a.Tenant)
             .Where(a => a.Tenant.IsDeleted == false)
             .Where(a => a.EndTime.Date >= DateTime.Now.Date)
             .WhereIf(input.AdviertisementState != AdviertisementState.All, x => x.AdviertisementState == input.AdviertisementState)
             .WhereIf(!string.IsNullOrEmpty(input.Name), x => x.Name == input.Name)
             .WhereIf(input.AdviertisementPlatform != AdviertisementPlatform.All, x => x.AdviertisementPlatform == input.AdviertisementPlatform)
             .WhereIf(input.TeantId.HasValue, x=> x.TenantId == input.TeantId);

            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            var entityListDtos = entityList.MapTo<List<GetAdviertisementsOutput>>();

            return new PagedResultDto<GetAdviertisementsOutput>(count, entityListDtos);
        }

        public async Task<GetAdviertisementForEditOutput> GetAdviertisementForEdit(NullableIdDto<int> input)
        {
            var output = new GetAdviertisementForEditOutput();
            AdviertisementDto adviertisementDto;
            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                adviertisementDto = entity.MapTo<AdviertisementDto>();
            }
            else
            {
                adviertisementDto = new AdviertisementDto();
            }

            output.AdviertisementDto = adviertisementDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Answer的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateOrUpdateAdviertisement(CreateOrUpdateAdviertisementInput input)
        {

            if (input.AdviertisementDto.Id.HasValue)
            {
                await UpdateAdviertisementAsync(input.AdviertisementDto);
            }
            else
            {
                await CreateAdviertisementAsync(input.AdviertisementDto);
            }
        }


        /// <summary>
        /// 新增Adviertisement
        /// </summary>

        protected virtual async Task<AdviertisementDto> CreateAdviertisementAsync(AdviertisementDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<Adviertisement>();

            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<AdviertisementDto>();
        }

        /// <summary>
        /// 编辑Answer
        /// </summary>

        protected virtual async Task UpdateAdviertisementAsync(AdviertisementDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }

        public async Task<ClickAdOutput> ClickAd(ClickAdInput input)
        {
            var output = new ClickAdOutput();
            if (input.AdId.HasValue && input.PlayerId.HasValue)
            {
                if (input.IpAddress.Contains(":"))
                {
                    input.IpAddress = input.IpAddress.Substring(input.IpAddress.LastIndexOf(':') + 1);
                }
                await _playerAdviertisementRepository.InsertAsync(new PlayerAdviertisement()
                {
                    AdviertisementId = input.AdId.Value,
                    IPAddress = input.IpAddress,
                    PlayerId = input.PlayerId.Value,
                    UUID = input.UUID
                });
                output.IsSuccess = true;
            }

            return output;
        }

        public async Task<PagedResultDto<GetAdviertisementsOutput>> GetAds(GetAdsInput input)
        {
            var query = _entityRepository.GetAll()
                .Where(a => a.AdviertisementState == AdviertisementState.Normal && a.TenantId == AbpSession.TenantId);

            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(c => c.CreationTime).AsNoTracking()
                    .ToListAsync();

            var entityListDtos = entityList.MapTo<List<GetAdviertisementsOutput>>();

            return new PagedResultDto<GetAdviertisementsOutput>(count, entityListDtos);
        }

        public async Task DeleteAd(DeleteAdInput input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            if (entity != null)
            {
                await _entityRepository.DeleteAsync(entity);
            }
        }
    }
}


