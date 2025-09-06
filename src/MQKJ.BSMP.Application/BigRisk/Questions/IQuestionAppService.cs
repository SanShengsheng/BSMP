using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MQKJ.BSMP.Questions.Dtos;
using MQKJ.BSMP.Questions;
using MQKJ.BSMP.Authorization.Users;
using System;

namespace MQKJ.BSMP.Questions
{
    /// <summary>
    /// Question应用层服务的接口方法
    /// </summary>
    public interface IQuestionAppService : IApplicationService
    {
        /// <summary>
        /// 获取Question的分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<QuestionListDto>> GetPagedQuestions(GetQuestionsInput input);

        Task<IEnumerable<Question>> GetQuestions(GetQuestionsRequest request);

		/// <summary>
		/// 通过指定id获取QuestionListDto信息
		/// </summary>
		Task<QuestionListDto> GetQuestionByIdAsync(EntityDto<int> input);


        /// <summary>
        /// 导出Question为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetQuestionsToExcel();

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetQuestionForEditOutput> GetQuestionForEdit(NullableIdDto<int> input);

        //todo:缺少Dto的生成GetQuestionForEditOutput


        /// <summary>
        /// 添加或者修改Question的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateQuestion(CreateOrUpdateQuestionInput input);


        /// <summary>
        /// 删除Question信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteQuestion(EntityDto<int> input);


        /// <summary>
        /// 批量删除Question
        /// </summary>
        Task BatchDeleteQuestionsAsync(List<int> input);

        /// <summary>
        /// 获取所有的创建者
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetCreators();

        /// <summary>
        /// 获取问题的统计数据
        /// </summary>
        /// <returns></returns>
        //Task<QuestionStatisicsOutput> GetQuestionStatisics(GetQuestionStatisicsDto input);
        Task<QuestionStatisicsOutput> GetQuestionStatisics();

        Task<string> ExportExcel();


        //// custom codes 

        //// custom codes end
    }
}
