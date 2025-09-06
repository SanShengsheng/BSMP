using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MQKJ.BSMP.Tests.Seed
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

        private async void CreateEventGroup()
        {
            var eventGroups = new List<EventGroup>();
            var players = _context.Players;
            var eventGroup1 = new EventGroup();
            if (!_context.EventGroups.Any(s=>s.Description== "1个月"))
            {
                 eventGroup1 = new EventGroup()
                {
                    Description = "1个月"
                };
                eventGroups.Add(eventGroup1);
            }

            if (!_context.EventGroups.Any(s => s.Description == "2个月"))
            {
                var eventGroup2 = new EventGroup()
                {
                    Description = "2个月",
                    PrevGroup = eventGroup1
                };
                eventGroups.Add(eventGroup2);
            }
            if(eventGroups.Count>0)
            {
                await _context.EventGroups.AddRangeAsync(eventGroups);
            }
        }
    }
}
