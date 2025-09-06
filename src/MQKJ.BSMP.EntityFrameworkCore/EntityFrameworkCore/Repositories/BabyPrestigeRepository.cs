using Abp.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Prestiges;
using MQKJ.BSMP.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;
using Abp.Domain.Uow;
using Abp.Dapper.Repositories;

namespace MQKJ.BSMP.EntityFrameworkCore.Repositories
{
    public class BabyPrestigeRepository : BSMPRepositoryBase<Baby>, IBabyPrestigeRepository
    {
        private IDbContextProvider<BSMPDbContext> _dbContextProvider;
        private IDapperRepository<Family> _dapperRepo;
        public BabyPrestigeRepository(IDbContextProvider<BSMPDbContext> dbContextProvider, IDapperRepository<Family> dapperRepo) : base(dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
            _dapperRepo = dapperRepo;


        }
        [UnitOfWork(IsDisabled =true)]
        public  IList<PrestigesRanksModel> GetAllRanks(int babyId)
        {
            var sql = new StringBuilder(); 
            var _queryresult = new List<PrestigesRanksModel>();
            sql.Append("SELECT* ");
            sql.Append("FROM ");
            sql.Append("(");
            sql.Append("    SELECT f.Id AS FamilyId, ");
            sql.Append("           f.Prestiges AS Popularity, ");
            sql.Append("           b.Id AS BabyId, ");
            sql.Append("           b.AgeString AS Age, ");
            sql.Append("           b.Name AS BabyName, ");
            
            sql.Append("           (ROW_NUMBER() OVER(ORDER BY f.Prestiges DESC, ");
            sql.Append("                                        b.WillPower + b.Imagine + b.EmotionQuotient + b.Physique + b.Intelligence DESC, ");
            sql.Append("                                        f.CreationTime ASC ");
            sql.Append("                              ) ");
            sql.Append("           ) AS Rank ");
            sql.Append("    FROM dbo.Babies AS b ");
            sql.Append("        LEFT JOIN dbo.Families AS f ");
            sql.Append("            ON b.FamilyId = f.Id ");
            sql.Append("    WHERE b.State = 1 ");
            sql.Append("          AND f.IsDeleted = 0 ");
            sql.Append("          AND b.IsDeleted = 0 ");
            sql.Append("          AND b.Id IN( ");
            sql.Append("                          SELECT MAX(b1.Id) FROM dbo.Babies AS b1 GROUP BY b1.FamilyId ");
            sql.Append("                      ) ");
            sql.Append(") AS t ");
            sql.Append("WHERE t.Rank ");
            sql.Append("      BETWEEN 1 AND 100 ");
            sql.Append($"      OR t.BabyId = @babyId ");
            // TODO:添加ORM
           return  _dapperRepo.Query<PrestigesRanksModel>(sql.ToString(),new { babyId =babyId }).ToList();
        }
    }
}
