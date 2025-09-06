using MQKJ.BSMP.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Tests.ChineseBaby.TestData.ProfessionCosts
{
    public class ProfessionCostCreator
    {
        private readonly BSMPDbContext _context;

        public ProfessionCostCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateProfessionCost();
        }

        private void CreateProfessionCost()
        {
            var professions = _context.Professions;

            var list = new List<ChineseBabies.ChangeProfessionCost>();

            foreach (var item in professions)
            {
                var professionCost = new ChineseBabies.ChangeProfessionCost()
                {
                    Cost = 20,
                    CostType = ChineseBabies.CostType.Money,
                    ProfessionId = item.Id
                };
                list.Add(professionCost);
            }

            _context.AddRange(list);
        }

    }
}
