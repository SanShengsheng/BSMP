using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MQKJ.BSMP.GameRecords;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.Questions;
using MQKJ.BSMP.Scenes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Players.WeChat
{
    //[AbpAuthorize]
    public class WeChatTestAppService : BSMPAppServiceBase
    {
        //private IDistributedCache _memoryCache;

        public WeChatTestAppService(
      //IDistributedCache memoryCache
            )
        {
            //_memoryCache = memoryCache;

        }

        ///// <summary>
        ///// 获取等待开始的游戏房间列表
        ///// </summary>
        ///// <returns></returns>
        //public async Task<JsonResult> GetWaitingStartGameTaskList()
        //{
        //    var result = await _gameTaskRepository.GetAllIncluding(g => g.Inviter).Where(a => a.InviteePlayerId == null).Select(s => new { gameId = s.Id, playerId = s.InviterPlayerId, playerName = s.Inviter.NickName }).ToListAsync();
        //    return new JsonResult(result);
        //}
        [HttpPost]
        public async  void SetRedis(string key, string value,string token)
        {
            if (token=="asdfghjkl;'")
            {
                await RedisHelper.SetAsync(key, value + DateTime.Now.ToString());
            }
        }

        /// <summary>
        /// 获取redis值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async  Task<string> GetRedis(string key)
        {
            var valueByte = await RedisHelper.GetAsync(key);
            var valueString = valueByte;//Encoding.UTF8.GetString(valueByte);
            return valueString;
        }

        public async Task Test(string key)
        {
             await Task.CompletedTask;
        }
    }
}
