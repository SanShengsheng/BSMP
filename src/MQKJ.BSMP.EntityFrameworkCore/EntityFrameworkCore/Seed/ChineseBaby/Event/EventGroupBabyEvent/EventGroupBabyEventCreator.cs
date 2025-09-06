using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
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

        private void CreateEventGroupBabyEvent()
        {
            //TODO 增加种子判断
            if (!_context.EventGroupBabyEvents.Any())
            {
                var studys =  _context.BabyEvents.Where(t => t.Type == IncidentType.Study).ToList();
                var growups =  _context.BabyEvents.Where(t => t.Type == IncidentType.Growup).ToList();
                var groups =  _context.EventGroups.ToList();
                var scount =(int)Math.Ceiling(studys.Count * 1f / groups.Count);
                var gcount = (int)Math.Ceiling(growups.Count * 1f  / groups.Count);
                var i = 0;
                foreach(var group in groups)
                {
                    var topStudys = studys.Skip(i * scount).Take(scount);
                    var topGrowups = growups.Skip(i * gcount).Take(gcount);

                    foreach(var s in topStudys)
                    {
                         _context.EventGroupBabyEvents.Add(new EventGroupBabyEvent
                        {
                            GroupId = group.Id,
                            EventId = s.Id
                        });
                    }

                    foreach (var s in topGrowups)
                    {
                         _context.EventGroupBabyEvents.Add(new EventGroupBabyEvent
                        {
                            GroupId = group.Id,
                            EventId = s.Id
                        });
                    }
                    i++;
                }
            }

        }
    }
}
