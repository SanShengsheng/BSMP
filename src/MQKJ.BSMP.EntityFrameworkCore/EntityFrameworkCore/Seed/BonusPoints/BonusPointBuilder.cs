using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.BonusPoints
{
    public class BonusPointBuilder
    {
        private readonly BSMPDbContext _dbContext;

        public BonusPointBuilder(BSMPDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create()
        {
            new BonusPointCreator(_dbContext).Create();
            _dbContext.SaveChanges();
        }
    }
}
