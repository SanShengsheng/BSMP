

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.Feedbacks;


namespace MQKJ.BSMP.Feedbacks.DomainService
{
    public interface IFeedbackManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitFeedback();



		 
      
         

    }
}
