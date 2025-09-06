using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.BonusPoints;


namespace MQKJ.BSMP.BonusPoints.DomainServices
{
    public interface IBonusPointManager : IDomainService
    {
        
        /// <summary>
        /// 初始化方法
        /// </summary>
        void InitBonusPoint();


		
		//// custom codes 
		
        //// custom codes end

    }
}
