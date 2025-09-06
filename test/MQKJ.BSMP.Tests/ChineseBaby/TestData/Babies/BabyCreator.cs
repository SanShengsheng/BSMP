using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MQKJ.BSMP.Tests.Seed
{
    public class BabyCreator
    {

        private readonly BSMPDbContext _context;

        public BabyCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateBaby();
        }

        private async void CreateBaby()
        {
            var babyList = new List<ChineseBabies.Baby>();
            var family1 = _context.Families.FirstOrDefault();
            var family2 = _context.Families.LastOrDefault();
            var baby = IniBaby(family1.Id, 1);
            var babyTwo = IniBaby(family2.Id, 2, BabyState.Adult, 2);
            var baby3 = IniBaby(family2.Id, 3, BabyState.UnderAge, 2,2);
            babyList.Add(baby);
            babyList.Add(babyTwo);
            babyList.Add(baby3);
            await _context.Babies.AddRangeAsync(babyList);
        }
        /// <summary>
        /// 创建宝宝
        /// </summary>
        /// <returns></returns>
        private ChineseBabies.Baby IniBaby(int familyId, int Id, BabyState babyState = BabyState.UnderAge, int growEvent = 1,int birthOrder=1)
        {
            //创建宝宝
            var _random = new Random();
            string[] hospitals = new string[] { "圣玛丽亚", "圣马力诺", "玛利亚", "康辉", "默奇", "中美", "惠林顿" };
            var baby = new ChineseBabies.Baby
            {
                Intelligence = _random.Next(1, 11),
                Physique = _random.Next(1, 11),
                Imagine = _random.Next(1, 11),
                WillPower = _random.Next(1, 11),
                EmotionQuotient = _random.Next(1, 11),
                Charm = _random.Next(1, 11),
            };

            var list = new List<int>
            {
                baby.Intelligence,
                baby.Physique,
                baby.Imagine,
                baby.WillPower,
                baby.EmotionQuotient,
                baby.Charm
            };

            if (list.Count(v => v >= 10) > 2 || list.Sum() > 40)
            {
                return IniBaby(familyId, Id, babyState, growEvent);
            }
            //基本信息
            //baby.Id = Id;
            baby.FamilyId = familyId;
            baby.State = babyState;
            baby.BirthLength = 32.0d;
            baby.BirthWeight = 8.0d;
            baby.BirthHospital = hospitals[_random.Next(0, 7)];
            baby.Potential = 5000;
            baby.Gender = (Gender)_random.Next(1, 3);
            baby.GroupId = growEvent;
            baby.BirthOrder = birthOrder;
            baby.Energy = 100;
            return baby;
        }
    }
}
