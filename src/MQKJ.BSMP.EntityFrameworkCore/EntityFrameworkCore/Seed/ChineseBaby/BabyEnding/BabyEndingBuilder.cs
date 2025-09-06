using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.BonusPoints
{
    public class BabyEndingBuilder
    {
        private readonly BSMPDbContext _dbContext;

        public BabyEndingBuilder(BSMPDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create()
        {
            new BabyEndingCreator(_dbContext).Create();
            _dbContext.SaveChanges();
        }
    }
}
