using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
   public class BabyPropFeatureBuilder
    {
        private readonly BSMPDbContext _context;

        public BabyPropFeatureBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new BabyPropFeatureCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
