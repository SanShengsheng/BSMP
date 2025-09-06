

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.Likes;


namespace MQKJ.BSMP.Likes.DomainService
{
    public interface ILikeManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitLike();



		 
      
         

    }
}
