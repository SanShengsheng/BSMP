using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.EntityFrameworkCore;
using System.Linq;

namespace MQKJ.BSMP.Tests.Seed
{
    public class BabyEventRecordsCreator
    {

        private readonly BSMPDbContext _context;

        public BabyEventRecordsCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateBabyEventRecords();
            CreateBabyDoneEventRecords();
        }

        private async void CreateBabyEventRecords()
        {
            if (!_context.BabyEventRecords.Any())
            {
                var babyEvent = _context.BabyEvents.Include(s => s.Options).FirstOrDefault();
                var baby = _context.Babies.Include(s => s.Family).FirstOrDefault();
                var datetime = new long();
                var option = babyEvent.Options.FirstOrDefault();
                var record = new BabyEventRecord()
                {
                    EndTimeStamp = datetime,
                    EventId = babyEvent.Id,
                    PlayerId = baby.Family.MotherId,
                    FamilyId = baby.FamilyId,
                    OptionId = option.Id,
                    State = EventRecordState.WaitOther
                };
                await _context.BabyEventRecords.AddAsync(record);
            }
        }

        private async void CreateBabyDoneEventRecords()
        {
            if (!_context.BabyEventRecords.Any(s => s.State == EventRecordState.Handled))
            {
                var babyEvent = _context.BabyEvents.Find(3);
                if (babyEvent!=null)
                {
                    var datetime = new long();
                    var baby = _context.Babies.Include(s => s.Family).FirstOrDefault();
                    var option = babyEvent.Options.FirstOrDefault();
                    var record = new BabyEventRecord()
                    {
                        EndTimeStamp = datetime,
                        EventId = babyEvent.Id,
                        PlayerId = baby.Family.MotherId,
                        FamilyId = baby.FamilyId,
                        OptionId = option.Id,
                        State = EventRecordState.Handled
                    };
                    await _context.BabyEventRecords.AddAsync(record);
                }
            }
        }
    }
}
