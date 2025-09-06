using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.PropMall;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
    public class BabyPropFeatureCreator
    {

        private readonly BSMPDbContext _context;

        public BabyPropFeatureCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateBabyPropFeature();
        }

        private void CreateBabyPropFeature()
        {
            if (_context.BabyPropFeatures.Any())
            {
                return;
            }
            var propFeatures = new List<BabyPropFeature>();
            var babyProps = _context.BabyProps.Include(s => s.BabyPropType).AsQueryable();
            var babyPropFeatureTypes = _context.BabyPropFeatureTypes.AsQueryable();
            foreach (var prop in babyProps)
            {
                var babyPropFeature = new BabyPropFeature();
                //每一个道具有一种特性
                //每一类道具的特性类别一样
                if (prop.BabyPropType.Title == "皮肤")
                {
                    var feature = babyPropFeatureTypes.FirstOrDefault(s => s.Group == 5);
                    var random = new Random();
                    babyPropFeature = new BabyPropFeature()
                    {
                        BabyProp = prop,
                        BabyPropFeatureType = feature,
                        Value = random.NextDouble(),
                    };
                }
                else
                {
                    var group = 0;
                    switch (prop.BabyPropType.Title)
                    {
                        case "房子":
                            group = 1; break;
                        case "车子":
                            group = 2;break;
                        case "管家":
                            group = 3; break;
                        case "保姆":
                            group = 4; break;
                    }
                    var features = babyPropFeatureTypes.WhereIf(group!=0,s => s.Group == group).ToList();
                    var random = new Random();
                    babyPropFeature = new BabyPropFeature()
                    {
                        BabyProp = prop,
                        BabyPropFeatureType = features[random.Next(0, 3)],
                        Value = random.NextDouble(),
                    };
                }
                propFeatures.Add(babyPropFeature);
            }
            _context.BabyPropFeatures.AddRange(propFeatures);
        }
    }
}
