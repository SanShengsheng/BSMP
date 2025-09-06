using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// BabyGrowUpRecord应用层服务的接口实现方法
    ///</summary>
    //[AbpAuthorize]
    public class BabyGrowUpRecordAppService : BSMPAppServiceBase, IBabyGrowUpRecordAppService
    {
        private readonly IRepository<BabyGrowUpRecord, Guid> _entityRepository;

        /// <summary>
        /// 构造函数
        ///</summary>
        public BabyGrowUpRecordAppService(
        IRepository<BabyGrowUpRecord, Guid> entityRepository
        )
        {
            _entityRepository = entityRepository;
        }

        /// <summary>
        /// 获取BabyGrowUpRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<BabyGrowUpRecordListDto>> GetPaged(GetBabyGrowUpRecordsInput input)
        {
            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件

            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<BabyGrowUpRecordListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<BabyGrowUpRecordListDto>>();

            return new PagedResultDto<BabyGrowUpRecordListDto>(count, entityListDtos);
        }

        /// <summary>
        /// 通过指定id获取BabyGrowUpRecordListDto信息
        /// </summary>

        public async Task<BabyGrowUpRecordListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<BabyGrowUpRecordListDto>();
        }

        /// <summary>
        /// 获取编辑 BabyGrowUpRecord
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetBabyGrowUpRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetBabyGrowUpRecordForEditOutput();
            BabyGrowUpRecordEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<BabyGrowUpRecordEditDto>();

                //babyGrowUpRecordEditDto = ObjectMapper.Map<List<babyGrowUpRecordEditDto>>(entity);
            }
            else
            {
                editDto = new BabyGrowUpRecordEditDto();
            }

            output.BabyGrowUpRecord = editDto;
            return output;
        }

        /// <summary>
        /// 添加或者修改BabyGrowUpRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateBabyGrowUpRecordInput input)
        {
            if (input.BabyGrowUpRecord.Id.HasValue)
            {
                await Update(input.BabyGrowUpRecord);
            }
            else
            {
                await Create(input.BabyGrowUpRecord);
            }
        }

        /// <summary>
        /// 新增BabyGrowUpRecord
        /// </summary>

        protected virtual async Task<BabyGrowUpRecordEditDto> Create(BabyGrowUpRecordEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <BabyGrowUpRecord>(input);
            var entity = input.MapTo<BabyGrowUpRecord>();

            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<BabyGrowUpRecordEditDto>();
        }

        /// <summary>
        /// 编辑BabyGrowUpRecord
        /// </summary>

        protected virtual async Task Update(BabyGrowUpRecordEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除BabyGrowUpRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除BabyGrowUpRecord的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        public async Task AddBabyGrowUpRecord(BabyGrowUpRecordEditDto input)
        {
            await _entityRepository.InsertAsync(input.MapTo<BabyGrowUpRecord>());
        }

        /// <summary>
        /// 导出BabyGrowUpRecord为excel表,等待开发。
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