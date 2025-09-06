using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MQKJ.BSMP.Tests.Seed
{
    public class PlayerProfessionCreator
    {

        private readonly BSMPDbContext _context;

        public PlayerProfessionCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreatePlayerProfession();
        }

        private async void CreatePlayerProfession()
        {
            if (_context.PlayerProfessions.Any())
            {
                return;
            }
            var playerProfessions = new List<PlayerProfession>();
            var family = _context.Families;
            var professions = await _context.Professions.ToListAsync();
            foreach (var item in family)
            {
                var femaleProfession = professions.FirstOrDefault(s => s.Gender == Gender.Female);
                var maleProfession = professions.FirstOrDefault(s => s.Gender == Gender.Male);
                var motherProfession = new PlayerProfession()
                {
                    FamilyId = item.Id,
                    PlayerId = item.MotherId,
                    ProfessionId = femaleProfession.Id,
                    IsCurrent=true,
                };
                var fatherProfession = new PlayerProfession()
                {
                    FamilyId = item.Id,
                    PlayerId = item.FatherId,
                    ProfessionId = maleProfession.Id,
                    IsCurrent=true,
                };
                playerProfessions.Add(motherProfession);
                playerProfessions.Add(fatherProfession);
            }
            await _context.PlayerProfessions.AddRangeAsync(playerProfessions);
        }
    }
}
