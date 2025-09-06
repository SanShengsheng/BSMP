using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Tests.ChineseBaby.TestData.Professions
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
            var list = new List<Profession>();

            var maleProfession = new Profession()
            {
                Salary = 2000,
                SatisfactionDegree = 20,
                Description = "老师",
                Name = "老师",
                Gender = Gender.Female,
                Grade = 2,
                IsDefault = true
            };

            list.Add(maleProfession);

            var femaleProfession = new Profession()
            {
                Salary = 2000,
                SatisfactionDegree = 20,
                Description = "老师",
                Name = "老师",
                Gender = Gender.Male,
                Grade = 2,
                IsDefault = true
            };

            list.Add(femaleProfession);

            _context.Professions.AddRange(list);
        }
    }
}
