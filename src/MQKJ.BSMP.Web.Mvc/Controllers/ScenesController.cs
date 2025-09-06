using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.SceneFiles.SceneFiles.Dto;
using MQKJ.BSMP.SceneManage.SceneFiles;
using MQKJ.BSMP.Scenes;
using MQKJ.BSMP.Scenes.Dto;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Mvc.Controllers
{
    public class ScenesController : BSMPControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly ISceneAppService _sceneAppService;

        private readonly ISceneFileAppService _sceneFileAppService;

        public ScenesController(
            IHostingEnvironment hostingEnvironment,
            ISceneAppService sceneAppService,
            ISceneFileAppService sceneFileAppService
            )
        {
            _hostingEnvironment = hostingEnvironment;

            _sceneAppService = sceneAppService;

            _sceneFileAppService = sceneFileAppService;
        }
        public async Task<IActionResult> Index()
        {
            var dtos = await _sceneAppService.GetPagedScenesAsync(new GetSceneInput()
            {
                MaxResultCount = BSMPConsts.MaxQueryCount
            });

            return View(dtos);
        }

        public IActionResult SceneImagePage(int Id)
        {
            EntityDto<int> dto = new EntityDto<int>();

            dto.Id = Id;

            var list = _sceneFileAppService.GetSceneFile(dto);

            ViewData["imageList"] = list;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImageFile(IFormFile fileinput, int sceneId, string sceneFileName)
        {
            UploadSceneFileDto input = new UploadSceneFileDto();

            input.FormFile = fileinput;

            input.SceneId = sceneId;

            input.SceneFileName = sceneFileName;

            await _sceneFileAppService.UploaSceneFileAsync(input);

            return Content("上传成功");
        }

    }
}