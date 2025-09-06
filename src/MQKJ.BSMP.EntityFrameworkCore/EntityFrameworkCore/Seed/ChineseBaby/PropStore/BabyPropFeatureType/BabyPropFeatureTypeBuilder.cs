using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
   public class BabyPropFeatureTypeBuilder
    {
        private readonly BSMPDbContext _context;

        public BabyPropFeatureTypeBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new BabyPropFeatureTypeCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
