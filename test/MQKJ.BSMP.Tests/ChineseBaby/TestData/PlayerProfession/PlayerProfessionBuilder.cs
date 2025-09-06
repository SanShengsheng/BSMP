using MQKJ.BSMP.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Tests.Seed
{
   public class PlayerProfessionBuilder
    {
        private readonly BSMPDbContext _context;

        public PlayerProfessionBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new PlayerProfessionCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
