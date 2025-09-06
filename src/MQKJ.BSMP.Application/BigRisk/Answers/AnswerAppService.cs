using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.Answers.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Answers
{
    /// <summary>
    /// Answer应用层服务的接口实现方法
    /// </summary>

    public class AnswerAppService : BSMPAppServiceBase, IAnswerAppService
    {
        private readonly IRepository<Answer, int> _answerRepository;



        /// <summary>
        /// 构造函数
        /// </summary>
        public AnswerAppService(
            IRepository<Answer, int> answerRepository

        )
        {
            _answerRepository = answerRepository;

        }


        /// <summary>
        /// 获取Answer的分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AnswerListDto>> GetPagedAnswers(GetAnswersInput input)
        {

            var query = _answerRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件

            var answerCount = await query.CountAsync();

            var answers = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var answerListDtos = ObjectMapper.Map<List <AnswerListDto>>(answers);
            var answerListDtos = answers.MapTo<List<AnswerListDto>>();

            return new PagedResultDto<AnswerListDto>(
                        answerCount,
                        answerListDtos
                );
        }


        /// <summary>
        /// 通过指定id获取AnswerListDto信息
        /// </summary>
        public async Task<AnswerListDto> GetAnswerByIdAsync(EntityDto<int> input)
        {
            var entity = await _answerRepository.GetAsync(input.Id);

            return entity.MapTo<AnswerListDto>();
        }

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetAnswerForEditOutput> GetAnswerForEdit(NullableIdDto<int> input)
        {
            var output = new GetAnswerForEditOutput();
            AnswerEditDto answerEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _answerRepository.GetAsync(input.Id.Value);

                answerEditDto = entity.MapTo<AnswerEditDto>();

                //answerEditDto = ObjectMapper.Map<List <answerEditDto>>(entity);
            }
            else
            {
                answerEditDto = new AnswerEditDto();
            }

            output.Answer = answerEditDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Answer的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateOrUpdateAnswer(CreateOrUpdateAnswerInput input)
        {

            if (input.Answer.Id.HasValue)
            {
                await UpdateAnswerAsync(input.Answer);
            }
            else
            {
                await CreateAnswerAsync(input.Answer);
            }
        }


        /// <summary>
        /// 新增Answer
        /// </summary>

        protected virtual async Task<AnswerEditDto> CreateAnswerAsync(AnswerEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = ObjectMapper.Map<Answer>(input);

            entity = await _answerRepository.InsertAsync(entity);
            return entity.MapTo<AnswerEditDto>();
        }

        /// <summary>
        /// 编辑Answer
        /// </summary>

        protected virtual async Task UpdateAnswerAsync(AnswerEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _answerRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _answerRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除Answer信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task DeleteAnswer(EntityDto<int> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _answerRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除Answer的方法
        /// </summary>

        public async Task BatchDeleteAnswersAsync(List<int> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _answerRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        public async Task<List<Answer>> GetAnswersByQuestionId(int input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            var answers = await _answerRepository.GetAllListAsync(s => s.QuestionID == input);
            var answerListDtos = answers.MapTo<List<Answer>>();
            return answerListDtos;
        }
        /// <summary>
        /// 导出Answer为excel表
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetAnswersToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}



        //// custom codes 

        //// custom codes end

    }
}


