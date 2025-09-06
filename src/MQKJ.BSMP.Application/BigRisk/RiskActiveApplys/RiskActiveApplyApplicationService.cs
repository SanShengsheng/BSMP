
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ActiveApply.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;



namespace MQKJ.BSMP.ActiveApply
{
    /// <summary>
    /// RiskActiveApply应用层服务的接口实现方法  
    ///</summary>
    public class RiskActiveApplyAppService : BSMPAppServiceBase, IRiskActiveApplyAppService
    {
        private readonly IRepository<RiskActiveApply, Guid> _entityRepository;

        //private readonly IRiskActiveApplyManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public RiskActiveApplyAppService(
        IRepository<RiskActiveApply, Guid> entityRepository
        //,IRiskActiveApplyManager entityManager
        )
        {
            _entityRepository = entityRepository;
            //_entityManager=entityManager;
        }


        /// <summary>
        /// 获取RiskActiveApply的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<RiskActiveApplyListDto>> GetPaged(GetRiskActiveApplysInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    //.OrderBy(input.Sorting).AsNoTracking()
                    .OrderByDescending(a=>a.CreationTime).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<RiskActiveApplyListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<RiskActiveApplyListDto>>();

            return new PagedResultDto<RiskActiveApplyListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取RiskActiveApplyListDto信息
        /// </summary>

        public async Task<RiskActiveApplyListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<RiskActiveApplyListDto>();
        }

        /// <summary>
        /// 获取编辑 RiskActiveApply
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetRiskActiveApplyForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetRiskActiveApplyForEditOutput();
            RiskActiveApplyEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<RiskActiveApplyEditDto>();

                //riskActiveApplyEditDto = ObjectMapper.Map<List<riskActiveApplyEditDto>>(entity);
            }
            else
            {
                editDto = new RiskActiveApplyEditDto();
            }

            output.RiskActiveApply = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改RiskActiveApply的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<CreateOrUpdateRiskActiveApplyOutput> CreateOrUpdate(CreateOrUpdateRiskActiveApplyInput input)
        {
            var res = new CreateOrUpdateRiskActiveApplyOutput();
            var editDto = new RiskActiveApplyEditDto();
            if (input.RiskActiveApply.Id.HasValue)
            {
                editDto = await Update(input.RiskActiveApply);
            }
            else
            {
                var random = new Random();
                var num = random.Next(1, 10000);
                input.RiskActiveApply.Code = (DateTime.Now.ToString("MMddHHmm") + num.ToString()).PadRight(12, '0');
                editDto = await Create(input.RiskActiveApply);
            }
            res.Code = editDto.Code;
            return res;
        }


        /// <summary>
        /// 新增RiskActiveApply
        /// </summary>

        protected virtual async Task<RiskActiveApplyEditDto> Create(RiskActiveApplyEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <RiskActiveApply>(input);
            var entity = input.MapTo<RiskActiveApply>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<RiskActiveApplyEditDto>();
        }

        /// <summary>
        /// 编辑RiskActiveApply
        /// </summary>

        protected virtual async Task<RiskActiveApplyEditDto> Update(RiskActiveApplyEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
            return entity.MapTo<RiskActiveApplyEditDto>();

        }



        /// <summary>
        /// 删除RiskActiveApply信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除RiskActiveApply的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 导出RiskActiveApply为excel表,等待开发。
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


