using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using MQKJ.BSMP.BonusPoints;

namespace MQKJ.BSMP.BonusPointRecords.DomainServices
{
    /// <summary>
    /// BonusPointRecord领域层的业务管理
    /// </summary>
    public class BonusPointRecordManager :IBonusPointRecordManager
    {
        private readonly IRepository<BonusPointRecord, Guid> _bonuspointrecordRepository;

        /// <summary>
        /// BonusPointRecord的构造方法
        /// </summary>
        public BonusPointRecordManager(IRepository<BonusPointRecord, Guid> bonuspointrecordRepository)
        {
            _bonuspointrecordRepository = bonuspointrecordRepository;
        }
		
		
		/// <summary>
		///     初始化
		/// </summary>
		public void InitBonusPointRecord()
		{
			throw new NotImplementedException();
		}

		//TODO:编写领域业务代码


		
		//// custom codes 
		
        //// custom codes end

    }
}
