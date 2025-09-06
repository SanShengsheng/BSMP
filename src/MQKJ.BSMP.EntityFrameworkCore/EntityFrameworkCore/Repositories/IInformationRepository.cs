using Abp.Data;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.EntityFrameworkCore.Repositories
{
    public interface IInformationRepository : IRepositoryGuid<Information>
    {
        /// <summary>
        /// 批量置已读
        /// </summary>
        /// <param name="familyId"></param>
        /// <param name="receiverId"></param>
        /// <returns></returns>
        Task BatchUpdatePoperInfoState(int familyId, Guid receiverId);
    }

    public class InformationRepository : BSMPRepositoryBase<Information, Guid>, IInformationRepository
    {
        public InformationRepository(IDbContextProvider<BSMPDbContext> dbContextProvider) : base(dbContextProvider)
        {
            
        }

        public Task BatchUpdatePoperInfoState(int familyId, Guid receiverId)
        {
            var sql = @"update [Informations] set [state] = 2 where [type]=1 and [NoticeType] = 1 
                        and [state] = 1 and [ReceiverId] = @receiverId and [familyId] = @familyId";

            var @params = new SqlParameter[]
            {
                new SqlParameter("@receiverId", receiverId),
                new SqlParameter("@familyId", familyId)
            };

            return Context.Database.ExecuteSqlCommandAsync(sql, @params);
        }
    }
}
