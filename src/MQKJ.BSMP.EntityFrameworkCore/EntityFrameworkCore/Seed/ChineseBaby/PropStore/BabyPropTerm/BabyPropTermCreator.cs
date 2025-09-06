using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.PropMall;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
    public class BabyPropTermCreator
    {

        private readonly BSMPDbContext _context;

        public BabyPropTermCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateBabyPropTerm();
        }

        private void CreateBabyPropTerm()
        {
            if (_context.BabyPropTerms.Any() )
            {
                return;
            }
            var propTerms = new List<BabyPropTerm>();
            var props = _context.BabyProps.Include(s=>s.BabyPropType).AsQueryable().AsNoTracking();
            var propTermTypes = _context.BabyPropBuyTermTypes.AsQueryable().AsNoTracking().ToList();
            var ages = new int[] { 0, 1, 2, 3, 4 };
            foreach (var prop in props)
            {
                switch (prop.BabyPropType.Title)
                {
                    case "皮肤":
                        propTerms.AddRange(AddPropTerm(new int[] { 1, 2 }, propTermTypes, prop));
                        break;
                    case "房子":
                        propTerms.AddRange(AddPropTerm(new int[] { 4, 5, 3 }, propTermTypes, prop));
                        break;
                    case "车子":
                        propTerms.AddRange(AddPropTerm(new int[] { 3, 7 }, propTermTypes, prop));
                        break;
                    case "管家":
                        propTerms.AddRange(AddPropTerm(new int[] { 4, 5, 3, 8 }, propTermTypes, prop));
                        break;
                    case "保姆":
                        propTerms.AddRange(AddPropTerm(new int[] { 1, 2 }, propTermTypes, prop));
                        break;
                }
            }
            _context.BabyPropTerms.AddRange(propTerms);
        }
        /// <summary>
        /// 添加道具条件
        /// </summary>
        /// <param name="termTypeCodes"></param>
        /// <param name="propTermTypes"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        private List<BabyPropTerm> AddPropTerm(int[] termTypeCodes, List<BabyPropBuyTermType> propTermTypes,BabyProp prop)
        {
            var propTerms = new List<BabyPropTerm>();
            var termTypes = propTermTypes.Where(s => termTypeCodes.Contains(s.Code));
            var random = new Random();
            foreach (var termType in termTypes)
            {
                var propTerm = new BabyPropTerm();
                if (termType.Code == 1)
                {
                    var maxAge = random.Next(0, 5);
                    //年龄
                    propTerm = new BabyPropTerm()
                    {
                        BabyPropBuyTermId = termType.Id,
                        BabyPropId = prop.Id,
                        MaxValue = maxAge,
                        MinValue = maxAge > 0 ? maxAge - 1 : 0,
                    };
                }
                else
                {
                    propTerm = new BabyPropTerm()
                    {
                        BabyPropBuyTermId = termType.Id,
                        BabyPropId = prop.Id,
                        MinValue = random.Next(1, 5),
                    };
                }
                propTerms.Add(propTerm);
            }
            return propTerms;
        }
    }
}
