using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.TagTypes;


namespace MQKJ.BSMP.TagTypes.DomainServices
{
    public interface ITagTypeManager : IDomainService
    {
        
        /// <summary>
        /// 初始化方法
        /// </summary>
        void InitTagType();


		
		//// custom codes 
		
        //// custom codes end

    }
}
