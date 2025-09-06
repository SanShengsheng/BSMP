using MQKJ.BSMP.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace MQKJ.BSMP.Tests.Seed
{
    public class FamilyCreator
    {

        private readonly BSMPDbContext _context;

        public FamilyCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateFamily();
        }

        private void CreateFamily()
        {
            if (_context.Families.Any())
            {
                return;
            }
            var familyList = new List<ChineseBabies.Family>();
            var players = _context.Players.ToList();
            var family1 = new ChineseBabies.Family()
            {
                MotherId = players.FirstOrDefault().Id,
                FatherId = players.ElementAtOrDefault(1).Id,
                Deposit = 5e10,
                Happiness = 100,
                Type = 1
            };
            familyList.Add(family1);
            var family2 = new ChineseBabies.Family()
            {
                MotherId = players.ElementAtOrDefault(2).Id,
                FatherId = players.ElementAtOrDefault(3).Id,
                Deposit = 5e10,
                Happiness = 100,
                Type = 1
            };
            familyList.Add(family2);
            _context.Families.AddRangeAsync(familyList);
        }
    }
}
