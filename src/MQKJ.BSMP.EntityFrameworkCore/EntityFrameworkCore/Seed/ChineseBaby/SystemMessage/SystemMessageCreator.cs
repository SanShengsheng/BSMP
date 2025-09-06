using MQKJ.BSMP.SystemMessages;
using System;
using System.Linq;
namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
    public class SystemMessageCreator
    {

        private readonly BSMPDbContext _context;

        public SystemMessageCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateSystemSetting();
        }

        private void CreateSystemSetting()
        {
            var systemSetting = new SystemSetting();
            if (!_context.SystemMessages.Any(s => s.Code == 1))
            {
                _context.SystemMessages.Add(new SystemMessage
                {
                    Code = 1,
                    Content = "周末大放送！半价金币，轻松换职业～",
                    StartDateTime = new DateTime(2019, 2, 23, 12, 0, 0),
                    ExprieDateTime = new DateTime(2019, 2, 24, 23, 59, 59),
                    NoticeType = NoticeType.All,
                    PeriodType = PeriodType.Minute,
                    PriorityLevel = 100,
                    Count = 3,
                    Period = 10,
                });
            }

        }
    }
}
