

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using MQKJ.BSMP.LoveCardFiles;


namespace MQKJ.BSMP.LoveCardFiles.DomainService
{
    public interface ILoveCardFileManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitLoveCardFile();



		 
      
         

    }
}
