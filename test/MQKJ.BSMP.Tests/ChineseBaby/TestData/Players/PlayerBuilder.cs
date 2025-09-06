using MQKJ.BSMP.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Tests.Seed
{
   public class PlayerBuilder
    {
        private readonly BSMPDbContext _context;

        public PlayerBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new PlayerCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
