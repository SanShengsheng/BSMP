using Abp;
using JCSoft.WX.Framework.Api;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Professions.Dtos;
using MQKJ.BSMP.MiniappServices;
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
    public class ProfessionAppService_Tests: BSMPTestBase
    {
        private readonly IProfessionAppService _professionAppService;

        public ProfessionAppService_Tests()
        {
            _professionAppService = Resolve<IProfessionAppService>();
        }

        /// <summary>
        ///  转职
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_ChangeProfession()
        {
            var playerProfessions = await GetAllPlayerProfessions();
            var playerProfession = playerProfessions.LastOrDefault();
            
            var result = await _professionAppService.ChangeProfession(new ChangeProfessionInput()
            {
                Attach = "附加消息",
                Body = "公司信息",
                ClientType = BigRisks.WeChat.WechatPay.ClientType.MinProgram,
                Code = "333",
                CostType = CostType.Money,
                FamilyId = playerProfession.FamilyId,
                PlayerId = playerProfession.Family.Mother.Id,
                ProfessionId = playerProfession.ProfessionId,
                Totalfee = 20
            });

            result.ShouldBeOfType(typeof(MiniProgramPayOutput));
        }

        //public async Task CreatePlayerProfression()
        //{
        //    var babies = await GetAllBabies();
        //    var baby = babies.FirstOrDefault();
        //    var professions = await GetAllProfessions();
        //}
    }
}
