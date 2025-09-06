

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.Friends;


namespace MQKJ.BSMP.Friends.DomainService
{
    public interface IFriendManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitFriend();



		 
      
         

    }
}
