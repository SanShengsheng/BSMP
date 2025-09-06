using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.Answers;
using MQKJ.BSMP.Questions.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Authorization;
using MQKJ.BSMP.Questions.Authorization;
using MQKJ.BSMP.Authorization.Users;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.EntityFrameworkCore;
using MQKJ.BSMP.Tags;
using MQKJ.BSMP.TagTypes;
using MQKJ.BSMP.Utils.Tools;
using NPOI.SS.UserModel;
using OfficeOpenXml;
using System.IO;

namespace MQKJ.BSMP.Questions
{
    /// <summary>
    /// Question应用层服务的接口实现方法
    /// </summary>

    public class QuestionAppService : BSMPAppServiceBase, IQuestionAppService
    {
        private readonly IRepository<Question, int> _questionRepository;
        private readonly IRepository<Answer, int> _answerRepository;
        private readonly IRepository<QuestionTag, int> _questionTagRepository;
        private readonly IRepository<Tag, int> _tagRepository;
        private readonly IRepository<TagType, int> _tagTypeRepository;
        /// <summary>
        /// 构造函数
        /// </summary>
        public QuestionAppService(IRepository<Question, int> questionRepository, 
            IRepository<Answer, int> answerRepository, 
            IRepository<QuestionTag> questionTagRepository,
            IRepository<Tag> tagRepository,
            IRepository<TagType> tagTypeRepository
            )
        { 
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _questionTagRepository = questionTagRepository;
            _tagRepository = tagRepository; 
            _tagTypeRepository = tagTypeRepository;
        }

        /// <summary>
        /// 获取Question的分页列表信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<QuestionListDto>> GetPagedQuestions(GetQuestionsInput input)
        {
            var currentUser = (await GetCurrentUserAsync());
            var isLookAllQuestions = await PermissionChecker.IsGrantedAsync(QuestionAppPermissions.Question_AllQuestions);
            var filter = !string.IsNullOrEmpty(input.Filter) ? input.Filter : null;

            var query = _questionRepository.GetAllIncluding(q => q.Answers, q => q.Scene, q => q.Auditor, q => q.Creator,q=>q.CheckOne)
                        .Include(q => q.QuestionTags).ThenInclude(t => t.Tag).ThenInclude(tt => tt.TagType)
                        .WhereIf(!isLookAllQuestions, q => q.CreatorUserId == currentUser.Id)//权限
                .WhereIf(input.PursuingGender != null, q => q.Pursuer == input.PursuingGender)
                .WhereIf(input.SceneId > 0, q => q.SceneId == input.SceneId)
                .WhereIf(input.State != null, q => q.State == input.State)
                        .WhereIf(input.State != null || input.State == QuestionState.All, q => q.State == input.State)
                         .WhereIf(input.CreatorId > 0, q => q.CreatorUserId == input.CreatorId)
                        .WhereIf(input.Tags != null && input.Tags.Count > 0, q => q.QuestionTags.LongCount() >= input.Tags.Count && q.QuestionTags.Select(qt => qt.TagId).Intersect(input.Tags).LongCount() >= input.Tags.Count)
                        .WhereIf(filter != null, q => (q.BackgroundStoryFemale.Contains(filter)) || q.BackgroundStoryMale.Contains(filter))
                        .OrderByDescending(s => s.Id).AsNoTracking();
            //   .OrderBy(input.Sorting).AsNoTracking()
            var questionCount = await query.AsQueryable().CountAsync();
            // TODO:根据传入的参数添加过滤条件
            // AbpRoles

            var questions = await query.PageBy(input).ToListAsync();
            // var questionListDtos = ObjectMapper.Map<List <QuestionListDto>>(questions);
            var questionListDtos = questions.MapTo<List<QuestionListDto>>();

            return new PagedResultDto<QuestionListDto>(
                questionCount,
                        questionListDtos
                );
        }

        public async  Task<IEnumerable<Question>> GetQuestions(GetQuestionsRequest request)
        {

            var queryids = _questionRepository.GetAll()
                .WhereIf(request.AnswerCount.HasValue,
                    q => q.Answers.Count() >= request.AnswerCount.Value)
                .WhereIf(request.QuestionGender.HasValue,
                    q => q.Pursuer == request.QuestionGender.Value)
                .WhereIf(request.QuestionState.HasValue,
                    q => q.State == request.QuestionState.Value)
                .WhereIf(request.TagId.HasValue,
                    q => q.QuestionTags.Any(qt => qt.TagId == request.TagId.Value))
                .WhereIf(request.ExcludeIds != null && request.ExcludeIds.Any(),
                    q => !request.ExcludeIds.Contains(q.Id)).OrderBy(i => Guid.NewGuid())     //随机生成
                .Take(request.Top)
                .Select(q => q.Id);

            var query = _questionRepository.GetAllIncluding(a => a.Answers)
                .Include(a => a.DefaultImg)
                .Include(a => a.Scene)
                .Include(a => a.QuestionTags)
                .ThenInclude(a=>a.Tag)
                .Include(a => a.CheckOne)
                .Where(q => queryids.Contains(q.Id));
               
       
            var result = await query.ToListAsync();

            return result;
        }

        /// <summary>
        /// 获取Question集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
    
        public async Task<PagedResultDto<QuestionListDto>> GetAll(GetQuestionsInput input)
        {
            var currentUser = (await GetCurrentUserAsync());
            var isLookAllQuestions = await PermissionChecker.IsGrantedAsync(QuestionAppPermissions.Question_AllQuestions);
            var filter = !string.IsNullOrEmpty(input.Filter) ? input.Filter : null;

            var query = _questionRepository.GetAllIncluding(q => q.Answers, q => q.Scene,q=>q.CheckOne)
                       // .Include(q => q.QuestionTags).ThenInclude(t => t.Tag).ThenInclude(tt => tt.TagType)
                     .WhereIf(!isLookAllQuestions, q => q.CreatorUserId == currentUser.Id)//权限
             //.WhereIf(input.PursuingGender != null, q => q.Pursuer == input.PursuingGender)
             //.WhereIf(input.SceneId > 0, q => q.SceneId == input.SceneId)
             //.WhereIf(input.State != null, q => q.State == input.State)
             //        .WhereIf(input.State != null || input.State == QuestionState.All, q => q.State == input.State)
             //         .WhereIf(input.CreatorId > 0, q => q.CreatorUserId == input.CreatorId)
             //        .WhereIf(input.Tags != null && input.Tags.Count > 0, q => q.QuestionTags.LongCount() >= input.Tags.Count && q.QuestionTags.Select(qt => qt.TagId).Intersect(input.Tags).LongCount() >= input.Tags.Count)
             //        .WhereIf(filter != null, q => (q.BackgroundStoryFemale.Contains(filter)) || q.BackgroundStoryMale.Contains(filter))
              .OrderByDescending(s => s.Id).AsNoTracking();
             //.OrderBy(input.Sorting).AsNoTracking();
            var questionCount = query.AsQueryable().Count();
            //TODO: 根据传入的参数添加过滤条件
            var questions = await query.PageBy(input).ToListAsync();
            var questionListDtos = ObjectMapper.Map<List<QuestionListDto>>(questions);

            return new PagedResultDto<QuestionListDto>(questionCount, questionListDtos);
        }

        /// <summary>
        /// 通过指定id获取QuestionListDto信息
        /// </summary>
        public async Task<QuestionListDto> GetQuestionByIdAsync(EntityDto<int> input)
        {
            var entity = await _questionRepository.GetAllIncluding(x => x.Answers).FirstOrDefaultAsync(q => q.Id == input.Id);

            return entity.MapTo<QuestionListDto>();
        }

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetQuestionForEditOutput> GetQuestionForEdit(NullableIdDto<int> input)
        {
            var output = new GetQuestionForEditOutput();
            QuestionEditDto questionEditDto;

            if (input.Id.HasValue)
            {
                var query = _questionRepository.GetAll();
                var entity = await query.Include(q => q.Answers)
                    .Include(q => q.QuestionTags).ThenInclude(t => t.Tag).ThenInclude(tt => tt.TagType).AsNoTracking()
                    .FirstOrDefaultAsync(q => q.Id == input.Id.Value);

                questionEditDto = entity.MapTo<QuestionEditDto>();


                //questionEditDto = ObjectMapper.Map<List <questionEditDto>>(entity);
            }
            else
            {
                questionEditDto = new QuestionEditDto();
            }

            output.Question = questionEditDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Question的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateOrUpdateQuestion(CreateOrUpdateQuestionInput input)
        {
            var quesionId = input.Id ?? 0;
            if (input.Id.HasValue)
            {
                await UpdateQuestionAsync(input);
            }
            else
            {
                await CreateQuestionAsync(input);
            }
        }

        /// <summary>
        /// 冻结问题
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(QuestionAppPermissions.Question_AuditQuestion)]
        public async Task FreezeQuestion(EntityDto<int> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            var entity = await _questionRepository.GetAsync(input.Id);
            entity.State = QuestionState.Freeze;
            await _questionRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 审核问题
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(QuestionAppPermissions.Question_AuditQuestion)]
        public async Task PassQuestion(EntityDto<int> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            var entity = await _questionRepository.GetAsync(input.Id);
            entity.State = QuestionState.Confrimed;
            entity.AuditorId = (await GetCurrentUserAsync()).Id;
            entity.AuditDateTime = DateTime.Now;
            await _questionRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 上线问题
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(QuestionAppPermissions.Question_AuditQuestion)]
        public async Task SetOnline(EntityDto<int> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            var entity = await _questionRepository.GetAsync(input.Id);
            entity.State = QuestionState.Online;
            //entity.AuditorId = (await GetCurrentUserAsync()).Id;
            //entity.AuditDateTime = DateTime.Now;
            await _questionRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 新增Question
        /// </summary>

        protected virtual async Task<QuestionEditDto> CreateQuestionAsync(CreateOrUpdateQuestionInput input)
        {
            //TODO:新增前的逻辑判断，是否允许新增
            //input.DefaultImgId = null;
            var entity = ObjectMapper.Map<Question>(input);

            entity = await _questionRepository.InsertAsync(entity);

            return entity.MapTo<QuestionEditDto>();
        }


        /// <summary>
        /// 编辑Question
        /// </summary>

        protected virtual async Task<Question> UpdateQuestionAsync(CreateOrUpdateQuestionInput input)
        {
            //TODO:更新前的逻辑判断，是否允许更新
            var entity = await _questionRepository.GetAllIncluding(a => a.QuestionTags, a => a.Answers).AsNoTracking().FirstOrDefaultAsync(q => q.Id == input.Id.Value);
            //var entity = await _questionRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);
            //  更新选项
            var result = await _questionRepository.UpdateAsync(entity);
            foreach (var answer in entity.Answers)
            {
                await _answerRepository.InsertOrUpdateAsync(answer);
            }
            //更新标签
            foreach (var questionTag in entity.QuestionTags)
            {
                await _questionTagRepository.InsertOrUpdateAsync(questionTag);
            }
            return result;
        }

        /// <summary>
        /// 删除Question信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task DeleteQuestion(EntityDto<int> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _questionRepository.DeleteAsync(input.Id);
        }


        //public async Task<QuestionStatisicsOutput> GetQuestionStatisics(GetQuestionStatisicsDto input)
        public async Task<QuestionStatisicsOutput> GetQuestionStatisics()
        {
            var output = new QuestionStatisicsOutput();

            var query = _questionRepository.GetAll()
                //.WhereIf(input.StartTime != null && input.EndTime != null, q => q.CreationTime >= input.StartTime && q.CreationTime <= input.EndTime)
                .Include(t => t.QuestionTags)
                .Include(s => s.Scene).
                AsNoTracking();

            if (query.Count() <= 0)
            {
                return output;
            }

            output.QuestionCount = await query.LongCountAsync();

            output.OnlineQuestionCount = await query.CountAsync(o => o.State == QuestionState.Online);

            output.OfflineQuesitonCount = await query.CountAsync(o => o.State == QuestionState.Freeze);

            output.MaleQuestionCount = query.Count(m => m.Pursuer == QuestionGender.M);

            output.FemaleQuestionCount = query.Count(m => m.Pursuer == QuestionGender.F);

            //output.

            var questionScenes = query.GroupBy(x => x.Scene.SceneName);
            var questionScenesCount = questionScenes.LongCount();
            //场景占比
            foreach (var item in questionScenes)
            {
                var questionScene = new QuestionScene();

                questionScene.SceneName = item.Key;

                //questionScene.QuestionScenePercent = Math.Round(item.LongCount() / questionScenesCount * 100.0,2);
                questionScene.QuestionScenePercent = Math.Round((float)item.LongCount() / output.QuestionCount * 100,2);

                output.QuestionScenes.Add(questionScene);
            }

            //var questionTags = _questionTagRepository.GetAll().Include(x => x.Tag).ThenInclude(x => x.TagType).AsNoTracking();

            ////私密度占比
            //var privacys = questionTags.Where(t => t.Tag.TagType.Code == StaticTagTypeCode.Privacy).GroupBy(n => n.Tag.TagName);

            //var privacyCount = privacys.LongCount();

            //List<QuestionPrivacy> questionPrivacyList = new List<QuestionPrivacy>();

            //foreach (var item in privacys)
            //{
            //    var privacy = new QuestionPrivacy();

            //    privacy.Name = item.Key;

            //    privacy.Percent = Math.Round(item.LongCount() / privacyCount * 100.0, 2);

            //    questionPrivacyList.Add(privacy);
            //}

            //output.QuestionPrivacieList = questionPrivacyList;

            ////关系占比
            //var relationDegrees = questionTags.Where(t => t.Tag.TagType.Code == StaticTagTypeCode.RelationDegree).GroupBy(n => n.Tag.TagName);

            //var relationDegreeCount = relationDegrees.LongCount();

            //List<QuestionRelationDegree> questionRelationDegreeList = new List<QuestionRelationDegree>();

            //foreach (var item in relationDegrees)
            //{
            //    var relationDegree = new QuestionRelationDegree();

            //    relationDegree.Name = item.Key;

            //    relationDegree.Percent = Math.Round(item.LongCount() / relationDegreeCount * 100.0, 2);

            //    questionRelationDegreeList.Add(relationDegree);
            //}

            //output.QuestionRelationDegreeList = questionRelationDegreeList;
            var questionIds = query.Select(q => q.Id);

            var questionTagTypes = _questionTagRepository.GetAll()
                .Include(x => x.Tag)
                .ThenInclude(x => x.TagType)
                .Where(x => questionIds.Contains(x.QuestionId))
                .GroupBy(x => x.Tag.TagType.Code);

            foreach (var item in questionTagTypes)
            {
                var questionTags = item.GroupBy(x => x.Tag.TagName);

                switch (item.Key)
                {
                    case StaticTagTypeCode.Privacy: //私密度的
                        foreach (var questionTagType in questionTags)
                        {
                            QuestionPrivacy questionPrivacy = new QuestionPrivacy();

                            questionPrivacy.QuestionTagName = questionTagType.Key;

                            questionPrivacy.QuestionTagCount = questionTagType.Count();

                            output.QuestionPrivacieList.Add(questionPrivacy);
                        }
                        break;
                    case StaticTagTypeCode.RelationDegree: //关系维度
                        foreach (var questionTagType in questionTags)
                        {
                            QuestionRelationDegree questionRelationDegree = new QuestionRelationDegree();

                            questionRelationDegree.QuestionTagName = questionTagType.Key;

                            questionRelationDegree.QuestionTagCount = questionTagType.Count();

                            output.QuestionRelationDegreeList.Add(questionRelationDegree);
                        }
                        break;
                    case StaticTagTypeCode.ApplyGender: //适用年龄
                        break;
                    case StaticTagTypeCode.TopicTag: //话题标签
                        break;
                    case StaticTagTypeCode.QuestionProperty: //性质分类
                        break;
                    case StaticTagTypeCode.QuestionQuality: //问题质量
                        break;
                    default:
                        break;
                }
            }
            return output;
        }

        /// <summary>
        /// 批量删除Question的方法
        /// </summary>

        public async Task BatchDeleteQuestionsAsync(List<int> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _questionRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        public async Task<List<User>> GetCreators()
        {
            var creatorList = await _questionRepository.GetAllIncluding(g => g.Creator).ToListAsync();
            var creators = creatorList.Select(x => x.Creator).Distinct().ToList();
            return creators;
        }

        /// <summary>
        /// 导出Question为excel表
        /// </summary>
        /// <returns></returns>
        //public async Task GetQuestionsToExcel()
        //{
        //    var questions = await _questionRepository.GetAllIncluding(a => a.Answers, s => s.Scene).ToListAsync();

        //    OfficeHelp.ExportExcel(questions);
        //}

        public async Task<string> ExportExcel()
        {
            var questions = await _questionRepository.GetAllIncluding(q => q.Answers, q => q.Scene)
                           .Include(q => q.QuestionTags).ThenInclude(t => t.Tag).ThenInclude(tt => tt.TagType).ToListAsync();

            //FileInfo file = new FileInfo("c:/");
            using (ExcelPackage package = new ExcelPackage())
            {
                var tagTypes = _tagTypeRepository.GetAll().AsNoTracking();

                var privateDensityTagType = tagTypes.FirstOrDefault(s => s.Code == "Privacy");

                var releationDegreeTagType = tagTypes.FirstOrDefault(r => r.Code == "RelationshipDegree");

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("sheet");//创建sheet

                worksheet.Cells[1, 1].Value = "序号";
                worksheet.Cells[1, 2].Value = "男生画面";
                worksheet.Cells[1, 3].Value = "男生选项1";
                worksheet.Cells[1, 4].Value = "男生选项2";
                worksheet.Cells[1, 5].Value = "男生选项3";
                worksheet.Cells[1, 6].Value = "女生画面";
                worksheet.Cells[1, 7].Value = "女生选项1";
                worksheet.Cells[1, 8].Value = "女生选项2";
                worksheet.Cells[1, 9].Value = "女生选项3";
                worksheet.Cells[1, 10].Value = "关系";
                worksheet.Cells[1, 11].Value = "场景";
                worksheet.Cells[1, 12].Value = "私密度";
                //int colNum = 9; //列数
                int rowNum = questions.Count;

                for (int i = 1; i < rowNum; i++)
                {
                    worksheet.Cells[i + 1, 1].Value = questions[i].Id + 1;//id
                    //worksheet.Cells[i, 0,i + 2, 0].Merge = true;

                    worksheet.Cells[i + 1, 2].Value = FilterHtmlHelp.NoHtml(questions[i].BackgroundStoryMale);//男生画面
                    //worksheet.Cells[i, 1,i + 2,1].Merge = true;

                    var maleAnswers = questions[i].Answers.Where(m => m.QuestionType == QuestionGender.M).ToList();//男生选项
                    worksheet.Cells[i + 1, 3].Value = maleAnswers[0].Title;//1
                    worksheet.Cells[i + 1, 4].Value = maleAnswers[1].Title;//2
                    worksheet.Cells[i + 1, 5].Value = maleAnswers[2].Title;//3


                    worksheet.Cells[i + 1, 6].Value = FilterHtmlHelp.NoHtml(questions[i].BackgroundStoryFemale);//女生画面
                    //worksheet.Cells[i,3,i + 2,3].Merge = true;

                    var feMaleAnswers = questions[i].Answers.Where(m => m.QuestionType == QuestionGender.F).ToList();//女生选项
                    worksheet.Cells[i + 1, 7].Value = feMaleAnswers[0].Title;
                    worksheet.Cells[i + 1, 8].Value = feMaleAnswers[1].Title;
                    worksheet.Cells[i + 1, 9].Value = feMaleAnswers[2].Title;

                    var QuestionTagCount = questions[i].QuestionTags.Count();
                    if (QuestionTagCount  == 0)
                    {
                        worksheet.Cells[i + 1, 10].Value = "无";
                    }
                    else
                    {
                        var questionTags = questions[i].QuestionTags.ToList();
                        if (questionTags[0].Tag == null && questionTags[0].Tag.TagType == null)
                        {
                            worksheet.Cells[i + 1, 10].Value = "无";
                        }
                        else
                        {
                            var questionTag = questionTags.FirstOrDefault(t => t.Tag.TagTypeId == releationDegreeTagType.Id);
                            if (questionTag == null)
                            {
                                worksheet.Cells[i + 1, 10].Value = "无";
                            }
                            else
                            {
                                worksheet.Cells[i + 1, 10].Value = questionTag.Tag.TagName;
                            }

                        }
                    }
                    //worksheet.Cells[i,5,i + 2,5].Merge = true;

                    worksheet.Cells[i + 1, 11].Value = questions[i].Scene.SceneName;//场景
                    //worksheet.Cells[i,6,i + 2,6].Merge = true;

                    if (QuestionTagCount == 0)
                    {
                        worksheet.Cells[i + 1, 12].Value = "无";
                    }
                    else
                    {
                        var questionTags = questions[i].QuestionTags.ToList();
                        if (questionTags[0].Tag == null && questionTags[0].Tag.TagType == null)
                        {
                            worksheet.Cells[i + 1, 12].Value = "无";
                        }
                        else
                        {
                            var questionTag1 = questionTags.FirstOrDefault(t => t.Tag.TagTypeId == privateDensityTagType.Id);
                            if (questionTag1 == null)
                            {
                                worksheet.Cells[i + 1, 12].Value = "无";
                            }
                            else
                            {
                                worksheet.Cells[i + 1, 12].Value = questionTag1.Tag.TagName;
                            }

                        }
                    }
                    //worksheet.Cells[i, 7, i + 2, 7].Merge = true;
                }

                var xlFile = FileHelp.GetFileInfo("题库表.xlsx");


                package.SaveAs(xlFile);

                return xlFile.FullName;
            }
            
        }



        //// custom codes 

        //// custom codes end

    }
}


