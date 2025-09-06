using Abp.Configuration;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.SceneTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.SceneTypes
{
    public class SceneTypeBuilder
    {
        private readonly BSMPDbContext _dbContext;

        public SceneTypeBuilder(BSMPDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public void Create()
        {
            new SceneTypeCreator(_dbContext).Create();
            _dbContext.SaveChanges();
        }
    }
}
