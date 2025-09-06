using Abp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Players.WeChat
{
    public interface IWeChatTestAppService : IApplicationService
    {
        /// <summary>
        /// 获取等待开始的游戏房间列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
         Task<JsonResult> GetWaitingStartGameTaskList();

    }
}
