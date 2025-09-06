using Abp.Domain.Repositories;
using MQKJ.BSMP.ChineseBabies.Prestiges;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.IRepositories
{
    public interface IBabyPrestigeRepository:IRepository
    {
        IList<PrestigesRanksModel> GetAllRanks(int babyId);
    }
}
