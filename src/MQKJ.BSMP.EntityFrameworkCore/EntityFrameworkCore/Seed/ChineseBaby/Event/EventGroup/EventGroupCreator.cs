using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
    public class EventGroupCreator
    {

        private readonly BSMPDbContext _context;

        public EventGroupCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateEventGroup();
        }

        private  void CreateEventGroup()
        {
            if (!_context.EventGroups.Any())
            {
                var group_1 = new EventGroup()
                {
                    Description = "刚出生",
                };

                 _context.EventGroups.Add(group_1);

                var group_2 = new EventGroup
                {
                    Description = "满月",
                    PrevGroupId = group_1.Id
                };

                 _context.EventGroups.Add(group_2);
                var group_3 = new EventGroup
                {
                    Description = "婴儿",
                    PrevGroupId = group_2.Id
                };

                 _context.EventGroups.Add(group_3);
            }
        }
    }
}
