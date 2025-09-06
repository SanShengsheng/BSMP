using MQKJ.BSMP.ChineseBabies;
using System.Collections.Generic;
using System.Linq;
namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
    public class ProfessionCreator
    {

        private readonly BSMPDbContext _context;

        public ProfessionCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateProfession();
        }

        private void CreateProfession()
        {
            var defaultProfessionAddList = new List<Profession>();
            var defaultProfessions = _context.Professions.Where(s => s.IsDefault).ToList();
            if (defaultProfessions.Count <= 0)
            {
                var fatherDefaultProfession = defaultProfessions.FirstOrDefault(s => s.Gender == Gender.Male);
                var reward = _context.Rewards.FirstOrDefault();

                if (fatherDefaultProfession == null)
                {
                    defaultProfessionAddList.Add(new Profession
                    {
                        Name = "小区保安",
                        Gender = Gender.Male,
                        Grade = 0,
                        IsDefault = true,
                        Salary = 3000,
                        SatisfactionDegree = 5,
                        RewardId = reward?.Id,
                        Costs=null
                    });
                }
                var motherDefaultProfession = defaultProfessions.FirstOrDefault(s => s.Gender == Gender.Female);
                if (motherDefaultProfession == null)
                {
                    defaultProfessionAddList.Add(new Profession
                    {
                        Name = "家庭主妇",
                        Gender = Gender.Female,
                        Grade = 0,
                        IsDefault = true,
                        Salary = 1500,
                        SatisfactionDegree = 5,
                        RewardId = reward?.Id,
                        Costs = null
                    });
                }
            }
            _context.Professions.AddRange(defaultProfessionAddList.Distinct());
        }
    }
}
