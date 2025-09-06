using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MQKJ.BSMP.Tests.Seed
{
    public class EventGroupBabyEventCreator
    {

        private readonly BSMPDbContext _context;

        public EventGroupBabyEventCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateEventGroupBabyEvent();
        }

        private async void CreateEventGroupBabyEvent()
        {
            if (!_context.EventGroupBabyEvents.Any())
            {
                var eventGroups = new List<EventGroupBabyEvent>();
                //学习
                var babyEvents = _context.BabyEvents.Include(s => s.Options).Where(s => s.Type == IncidentType.Study);
                var group = await _context.EventGroups.FindAsync(1);
                foreach (var item in babyEvents)
                {
                    var eventGroupBabyEvent = new EventGroupBabyEvent()
                    {
                        EventId = item.Id,
                        GroupId = group.Id
                    };
                    eventGroups.Add(eventGroupBabyEvent);
                }

                //成长
                var babyGrowUpEvents = _context.BabyEvents.Include(s => s.Options).Where(s => s.Type == IncidentType.Growup || s.Type == IncidentType.Block);
                var growUpEventGroup = await _context.EventGroups.FindAsync(2);
                foreach (var item in babyGrowUpEvents)
                {
                    var eventGroupBabyEvent = new EventGroupBabyEvent()
                    {
                        EventId = item.Id,
                        GroupId = growUpEventGroup.Id
                    };
                    eventGroups.Add(eventGroupBabyEvent);
                }


                await _context.EventGroupBabyEvents.AddRangeAsync(eventGroups);
            }

        }
    }
}
