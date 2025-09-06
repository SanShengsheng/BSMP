using MQKJ.BSMP.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Tests.Seed
{
   public class EventGroupBabyEventBuilder
    {
        private readonly BSMPDbContext _context;

        public EventGroupBabyEventBuilder(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new EventGroupBabyEventCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
