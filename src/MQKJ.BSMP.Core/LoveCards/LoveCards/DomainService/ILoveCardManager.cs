

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.LoveCards;


namespace MQKJ.BSMP.LoveCards.DomainService
{
    public interface ILoveCardManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitLoveCard();



		 
      
         

    }
}
