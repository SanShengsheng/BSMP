
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Abp.AutoMapper;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using MQKJ.BSMP.ChineseBabies.Dtos;



namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// BabyEventRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class BabyEventRecordAppService : BSMPAppServiceBase, IBabyEventRecordAppService
    {
        private readonly IRepository<BabyEventRecord, Guid> _entityRepository;
        /// 构造函数 
        ///</summary>
        public BabyEventRecordAppService(IRepository<BabyEventRecord, Guid> entityRepository)
        {
            _entityRepository = entityRepository;
        }


        /// <summary>
        /// 获取BabyEventRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<BabyEventRecordListDto>> GetPaged(GetBabyEventRecordsInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<BabyEventRecordListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<BabyEventRecordListDto>>();

            return new PagedResultDto<BabyEventRecordListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取BabyEventRecordListDto信息
        /// </summary>

        public async Task<BabyEventRecordListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<BabyEventRecordListDto>();
        }

        /// <summary>
        /// 获取编辑 BabyEventRecord
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetBabyEventRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetBabyEventRecordForEditOutput();
            BabyEventRecordEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<BabyEventRecordEditDto>();

                //babyEventRecordEditDto = ObjectMapper.Map<List<babyEventRecordEditDto>>(entity);
            }
            else
            {
                editDto = new BabyEventRecordEditDto();
            }

            output.BabyEventRecord = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改BabyEventRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateBabyEventRecordInput input)
        {

            if (input.BabyEventRecord.Id.HasValue)
            {
                await Update(input.BabyEventRecord);
            }
            else
            {
                await Create(input.BabyEventRecord);
            }
        }


        /// <summary>
        /// 新增BabyEventRecord
        /// </summary>

        protected virtual async Task<BabyEventRecordEditDto> Create(BabyEventRecordEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <BabyEventRecord>(input);
            var entity = input.MapTo<BabyEventRecord>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<BabyEventRecordEditDto>();
        }

        /// <summary>
        /// 编辑BabyEventRecord
        /// </summary>

        protected virtual async Task Update(BabyEventRecordEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除BabyEventRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除BabyEventRecord的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 导出BabyEventRecord为excel表,等待开发。
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


