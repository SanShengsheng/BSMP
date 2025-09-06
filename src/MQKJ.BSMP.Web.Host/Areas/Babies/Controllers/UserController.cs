using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.BigRisk.Players.Dtos;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Models;
using MQKJ.BSMP.Players;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Host.Areas.Babies.Controllers
{
    /// <summary>
    /// 宝宝接口
    /// </summary>
    public class UserController : BabyBaseController
    {
        private readonly IPlayerAppService _playerAppService;

        public UserController(IPlayerAppService playerAppService)
        {
            _playerAppService = playerAppService;
        }

        /// <summary>
        /// 修改用户头像信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ApiResponseModel> Post(WechatPlayerInfoRequest request) => 
            this.ApiTaskAction(_playerAppService.UpdatePlayerWechatInfo(request));
    }
}