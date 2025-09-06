using MQKJ.BSMP.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
   public class OptionsBuilder
    {
        private readonly BSMPDbContext _context;

        public OptionsBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new OptionsCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
