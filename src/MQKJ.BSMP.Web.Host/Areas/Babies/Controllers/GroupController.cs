using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Groups.Dtos;
using MQKJ.BSMP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Host.Areas.Babies.Controllers
{
    public class GroupController : BabyBaseController
    {
        private readonly IEventGroupAppService _service;
        public GroupController(IEventGroupAppService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("events")]
        public ApiResponseModel AddEvents(AddEventsInput input)
        {
            return this.ApiAction(() => _service.AddEvents(input));
        }
    }
}
