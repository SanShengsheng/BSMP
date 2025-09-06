using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Questions;
using MQKJ.BSMP.Questions.Dtos;
using MQKJ.BSMP.Scenes;
using MQKJ.BSMP.Scenes.Dto;
using MQKJ.BSMP.Tags;
using MQKJ.BSMP.TagTypes;
using MQKJ.BSMP.TagTypes.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Collections.Extensions;
using X.PagedList;
using MQKJ.BSMP.SceneManage.SceneFiles;

namespace MQKJ.BSMP.Web.Controllers
{
    //[AbpMvcAuthorize(PermissionNames.Pages_Questions)]
    public class QuestionsController : BSMPControllerBase
    {
        private readonly IQuestionAppService _questionAppService;
        private readonly ISceneAppService _sceneAppService;
        private readonly ITagAppService _tagAppService;
        private readonly ISceneFileAppService _iSceneFileAppService;
        private readonly ITagTypeAppService _tagTypeAppService;

        public QuestionsController(IQuestionAppService questionAppService, ISceneAppService sceneAppService, ITagAppService tagAppService, ISceneFileAppService iSceneFileAppService, ITagTypeAppService tagTypeAppService)
        {
            _questionAppService = questionAppService;
            _sceneAppService = sceneAppService;
            _tagAppService = tagAppService;
            _iSceneFileAppService = iSceneFileAppService;
            _tagTypeAppService = tagTypeAppService;
        }

        public async Task<ActionResult> Index(int? page, QuestionState? questionState, QuestionGender? pursuingGender, string sceneId , List<int> tags,int creatorId,string filter)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;
            tags.Sort();
            var filterParams = new GetQuestionsInput()
            {
                SkipCount = (pageNumber - 1) * pageSize,
                State = questionState,
                PursuingGender = pursuingGender,
                Tags = tags,
                SceneId = sceneId == null ? 0 : int.Parse(sceneId),
                CreatorId = creatorId,
                Filter = filter
            };
        
            var result = await _questionAppService.GetPagedQuestions(filterParams);
            var questionLists = new StaticPagedList<QuestionListDto>(result.Items, pageNumber, pageSize, result.TotalCount); // Paging not implemented yet
            var scenes = await _sceneAppService.GetAllScenesAsync(new GetSceneInput());
            ViewBag.Scenes = scenes;
            ViewBag.Creators = await _questionAppService.GetCreators();
            //ViewBag.Tags = (await _tagAppService.GetPagedTags(new GetTagsInput() { MaxResultCount = 100, WithDetail = true })).Items;
            //ViewBag.SceneFiles = _iSceneFileAppService.GetSceneFile(new EntityDto() { Id = scenes.FirstOrDefault() == null ? 0 : scenes.FirstOrDefault().Id });
            ViewBag.SceneFiles = (  _iSceneFileAppService.GetAllAsync()).OrderBy(s=>s.SceneId).ToList();
            ViewBag.TagType =
                (await _tagTypeAppService.GetPagedTagTypes(new GetTagTypesInput() { MaxResultCount = 100, WithDetail = true }))
                .Items;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_List",questionLists);
            }
            else
            {
                return View(questionLists);
            }
        }

        public async Task<ActionResult> EditQuestionModal(int questionId)
        {
            var result = await _questionAppService.GetQuestionForEdit(new NullableIdDto(questionId));
            var scenes = await _sceneAppService.GetAllScenesAsync(new GetSceneInput());
            ViewBag.Scenes = scenes;
            ViewBag.SceneFiles = (_iSceneFileAppService.GetAllAsync()).OrderBy(s => s.SceneId).ToList();
            ViewBag.TagType =
                (await _tagTypeAppService.GetPagedTagTypes(new GetTagTypesInput() { MaxResultCount = 100, WithDetail = true }))
                .Items;
            return View("_EditQuestionModal", result);
        }

        public async Task<ActionResult> ExportToExcel()
        {
            var name = await _questionAppService.ExportExcel();

            return Content(name);
        }
    }
}
