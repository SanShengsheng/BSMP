using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.Answers.Dtos;
using MQKJ.BSMP.Questions;

namespace MQKJ.BSMP.Answers
{
    /// <summary>
    /// Answer应用层服务的接口方法
    /// </summary>
    public interface IAnswerAppService : IApplicationService
    {
        /// <summary>
        /// 获取Answer的分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<AnswerListDto>> GetPagedAnswers(GetAnswersInput input);

		/// <summary>
		/// 通过指定id获取AnswerListDto信息
		/// </summary>
		Task<AnswerListDto> GetAnswerByIdAsync(EntityDto<int> input);


        /// <summary>
        /// 导出Answer为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetAnswersToExcel();

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetAnswerForEditOutput> GetAnswerForEdit(NullableIdDto<int> input);

        //todo:缺少Dto的生成GetAnswerForEditOutput


        /// <summary>
        /// 添加或者修改Answer的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateAnswer(CreateOrUpdateAnswerInput input);


        /// <summary>
        /// 删除Answer信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteAnswer(EntityDto<int> input);


        /// <summary>
        /// 批量删除Answer
        /// </summary>
        Task BatchDeleteAnswersAsync(List<int> input);

        Task<List<Answer>> GetAnswersByQuestionId(int input);
        //// custom codes 

        //// custom codes end
    }
}
