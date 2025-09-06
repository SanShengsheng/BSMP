using System.Linq;
using Abp.Domain.Repositories;
using MQKJ.BSMP.Common.SensitiveWords.DomainService;
using MQKJ.BSMP.Common.SensitiveWords.Dtos;



namespace MQKJ.BSMP.Common.SensitiveWords
{
    /// <summary>
    /// SensitiveWord应用层服务的接口实现方法  
    ///</summary>
    public class SensitiveWordAppService : BsmpApplicationServiceBase<SensitiveWord, int, SensitiveWordEditDto, SensitiveWordEditDto, GetSensitiveWordsInput, SensitiveWordListDto>, ISensitiveWordAppService
    {
        private readonly IRepository<SensitiveWord, int> _entityRepository;

        private readonly ISensitiveWordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public SensitiveWordAppService(
        IRepository<SensitiveWord, int> entityRepository
        , ISensitiveWordManager entityManager
        ) : base(entityRepository)
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
        }

        internal override IQueryable<SensitiveWord> GetQuery(GetSensitiveWordsInput model)
        {
            throw new System.NotImplementedException();
        }
    }
}


