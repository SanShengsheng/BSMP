using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
   public class ProfessionBuilder
    {
        private readonly BSMPDbContext _context;

        public ProfessionBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new ProfessionCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
