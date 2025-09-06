using Abp;
using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MQKJ.BSMP.Tests.Baby
{
    public class BabySystemAppService_Tests : BSMPTestBase
    {
        private readonly IBabySystemAppService _entityAppService;
        //private readonly IHostingEnvironment _hostingEnvironment;

        public ITestOutputHelper Output { get; }
        public BabySystemAppService_Tests(ITestOutputHelper outputHelper)
        {
            _entityAppService = Resolve<IBabySystemAppService>();
            Output = outputHelper;

        }
        /// <summary>
        /// 排行榜
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_Baby_Rank_ShouldNotBe_Null()
        {
            //act 
            var babies = await GetAllBabies();
            var baby = babies.LastOrDefault();
            //assert
            var result = await _entityAppService.Rank(new  ChineseBabyRankInput
            {
                BabyId = baby.Id
            });
            //var responseText = await resul.ReadAsStringAsync();
            //Output.WriteLine(responseText);
            result.ShouldNotBeNull();
        }

    }
}
