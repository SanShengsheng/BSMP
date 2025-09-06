using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.EntityFrameworkCore;
using MQKJ.BSMP.Players;
using System.Collections.Generic;
using System.Linq;
namespace MQKJ.BSMP.Tests.Seed
{
    public class PlayerCreator
    {

        private readonly BSMPDbContext _context;
     
        public PlayerCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreatePlayer();
        }

        private void CreatePlayer()
        {
            if (_context.Players.Any())
            {
                return;
            }
            var players = new List<Player>();
            var tom = new Players.Player()
            {
                NickName = "tom",
                HeadUrl = "http://www.tom.com/head.jpg",
                Gender = 1,
                TenantId=1,
            };
            players.Add(tom);
            var kimi = new Players.Player()
            {
                NickName = "kimi",
                HeadUrl = "http://www.kimi.com/head.jpg",
                Gender = 2,
                TenantId = 1,
            };
            players.Add(kimi);
            var john = new Players.Player()
            {
                NickName = "john",
                HeadUrl = "http://www.john.com/head.jpg",
                Gender = 1,
                TenantId = 1,
            };
            players.Add(john);
            var lili = new Players.Player()
            {
                NickName = "LiLi",
                HeadUrl = "http://www.lili.com/head.jpg",
                Gender = 2,
                TenantId = 1,
            };
            players.Add(lili);
            _context.Players.AddRange(players);
        }
    }
}
