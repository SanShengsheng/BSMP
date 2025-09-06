using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace MQKJ.BSMP.Web.Host.SignalRChat
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomGroup : IGroupManager
    {

        private readonly ConcurrentDictionary<string, string> _connections = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 添加到组
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="groupName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AddToGroupAsync(string connectionId, string groupName, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 从组移除
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="groupName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task RemoveFromGroupAsync(string connectionId, string groupName, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
       
    }
}
