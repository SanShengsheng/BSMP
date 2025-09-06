using Abp;
using JCSoft.WX.Framework.Api;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Professions.Dtos;
using MQKJ.BSMP.MiniappServices;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Utils.WechatPay.Dtos;
using MQKJ.BSMP.WeChatPay;
using NPOI.SS.Formula.Functions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MQKJ.BSMP.Tests.ChineseBaby
{
    public class PlayerAppService_Tests: BSMPTestBase
    {
        private readonly IPlayerAppService _playerAppService;

        public PlayerAppService_Tests()
        {
            _playerAppService = Resolve<IPlayerAppService>();
        }

        /// <summary>
        ///  登录迁移老数据
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PlayerLogin_Should_MigrateOldData()
        {
          
            
          
        }
    }
}
