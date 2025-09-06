using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.BonusPoints;


namespace MQKJ.BSMP.BonusPointRecords.DomainServices
{
    public interface IBonusPointRecordManager : IDomainService
    {
        
        /// <summary>
        /// 初始化方法
        /// </summary>
        void InitBonusPointRecord();


		
		//// custom codes 
		
        //// custom codes end

    }
}
