using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Web.Areas.ChineseBabies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Areas.ChineseBabies.Controllers
{
    [Area("ChineseBabies")]
    public class ImportDataController : BSMPControllerBase
    {
        private readonly IBabySystemAppService _babySystemService;
        public ImportDataController(IBabySystemAppService babySystemService)
        {
            _babySystemService = babySystemService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [UnitOfWork(IsDisabled = true)]
        public IActionResult DoPost(ImportDataInput input)
        {
            var result = _babySystemService.ImportData(input);
            return Json(result);
        }
        [HttpGet]
        public IActionResult Types()
        {
            var values = System.Enum.GetValues(typeof(TableDataType));
            var _result = new TableDataTypeItem[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                _result[i] = new TableDataTypeItem
                {
                    Description = EnumHelper.EnumHelper.GetDescription((TableDataType)(values.GetValue(i))),
                    Value = (int)values.GetValue(i)
                };
            }
            return Json(_result);
        }
        [HttpGet]
        public async Task<IActionResult> TaskList()
        {

            return Json(await _babySystemService.TaskListAsync());
        }
    }
}
