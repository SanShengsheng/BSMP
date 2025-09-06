using System.Linq;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
    public class SystemSettingCreator
    {

        private readonly BSMPDbContext _context;

        public SystemSettingCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateSystemSetting();
        }

        private void CreateSystemSetting()
        {
            var systemSetting = new SystemSetting();
            if (!_context.SystemSettings.Any(s => s.Code == 1))
            {
                _context.SystemSettings.Add(new SystemSetting
                {
                    Code = 1,
                    Name = "iOS buy gold coin button isAllow show",
                    Value = "true",
                    Description = "iOS端购买金币是否显示",
                    GroupName = "payment",
                });
            }
             if (!_context.SystemSettings.Any(s => s.Code == 2))
            {
                _context.SystemSettings.Add(new SystemSetting
                {
                    Code = 2,
                    Name = "iOS buy energy button isAllow show",
                    Value = "true",
                    Description = "iOS端购买精力是否显示",
                    GroupName = "payment",
                });
            }
            if (!_context.SystemSettings.Any(s => s.Code == 3))
            {
                _context.SystemSettings.Add(new SystemSetting
                {
                    Code = 3,
                    Name = "iOS isAllow buy gold coin",
                    Value = "false",
                    Description = "iOS端是否允许购买金币",
                    GroupName = "payment",
                });
            }
             if (!_context.SystemSettings.Any(s => s.Code == 4))
            {
                _context.SystemSettings.Add(new SystemSetting
                {
                    Code = 4,
                    Name = "iOS isAllow buy energy",
                    Value = "true",
                    Description = "iOS端是否允许购买精力",
                    GroupName = "payment",
                });
            }
            ///1000~2000 初始值
            if (!_context.SystemSettings.Any(s => s.Code == 1000))
            {
                _context.SystemSettings.Add(new SystemSetting
                {
                    Code = 1000,
                    Name = "home initial",
                    Value = "150000",
                    Description = "家庭初始资金",
                    GroupName = "initial",
                });
            }
            if (!_context.SystemSettings.Any(s => s.Code == 1001))
            {
                _context.SystemSettings.Add(new SystemSetting
                {
                    Code = 1001,
                    Name = "RENAME_BABY_CONSUME_COIN",
                    Value = "200000",
                    Description = "改名消耗金币",
                    GroupName = "initial",
                });
            }

            //赛季配置信息
            if (!_context.SystemSettings.Any(s => s.Code == 1002))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                sb.Append("StartTime:\"" + "00:00" + "\",");
                sb.Append("EndTime:\"" + "23:50" + "\",");
                sb.Append("FatherDefaultFightCount:\"" + 10 + "\",");
                sb.Append("MotherDefaultFightCount:\"" + 10 + "\",");
                sb.Append("Price:\"" + 50000 + "\",");
                sb.Append("MaxFightCount:\"" + 10 + "\",");
                sb.Append("CanPKCount:\"" + 10 + "\",");
                sb.Append("RankingShowPlayerCount:\"" + 10 + "\",");
                sb.Append("RewardPlayerCount:\"" + 100 + "\",");
                sb.Append("}");
                _context.SystemSettings.Add(new SystemSetting
                {
                    Code = 1002,
                    Name = "SEASON_CONFIG",
                    Value = sb.ToString(),
                    Description = "赛季配置信息",
                    GroupName = "seasonconfig",
                });
            }

            if (!_context.SystemSettings.Any(s => s.Code == 1003))
            {
                _context.SystemSettings.Add(new SystemSetting
                {
                    Code = 1003,
                    Name = "ATHLETICS_INFORMATION_WIN_COUNT",
                    Value = "50,100,200,500,800,1000##100,200,500,800,1000",//##前面是跑马灯对战消息对战次数  后面是竞技场对战消息对战次数
                    Description = "竞技场消息胜利次数",
                    GroupName = "athleticsInformationWinCount",
                });
            }

            if (!_context.SystemSettings.Any(s => s.Code == 1004))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                sb.Append("PayRMB:\"" + 10 + "\",");//花钱
                sb.Append("Validate:\"" + 24 + "\"");//有效期(小时)
                sb.Append("}");
                _context.SystemSettings.Add(new SystemSetting
                {
                    Code = 1004,
                    Name = "DISMISS_FAMILY",
                    Value = sb.ToString(),
                    Description = "解散家庭配置",
                    GroupName = "initial",
                });
            }
            if (!_context.SystemSettings.Any(s => s.Code == 1005))
            {
                _context.SystemSettings.Add(new SystemSetting
                {
                    Code = 1005,
                    Name = "DANGRADING_SETTING",
                    Value = "15,40,100,300",//青铜、白银、黄金、钻石、王者 最大值
                    Description = "段位积分配置",
                    GroupName = "initial",
                });
            }
            if (!_context.SystemSettings.Any(s => s.Code == 1006))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                sb.Append("DangradingPoint:\"" + "15,40,100,300" + "\",");//青铜、白银、黄金、钻石、王者
                sb.Append("CointReward:\"" + "4000,500,2000,500" + "\",");//依次是高于自己的对手打赢、打输，低于自己的对手打赢、打输
                sb.Append("DangradingReward:\"" + "7#-1,20#-1,25#-1,29#-1" + "\"");//道具code-有效期 白银、黄金、钻石、王者
                sb.Append("}");
                _context.SystemSettings.Add(new SystemSetting
                {
                    Code = 1006,
                    Name = "ATHLETICS_REWARD_COINTCOUNT",
                    Value = sb.ToString(),
                    Description = "竞技场奖励配置",
                    GroupName = "initial",
                });
            }
            //宝宝出生档案 属性品质级别
            if (!_context.SystemSettings.Any(s => s.Code == 1007))
            {
                _context.SystemSettings.Add(new SystemSetting
                {
                    Code = 1007,
                    Name = "AttributeDetermine",
                    Value = "21,24,27", //偏弱、普通、优秀
                    Description = "出生属性判定",
                    GroupName = "initial",
                });
            }
            //if (!_context.SystemSettings.Any(s => s.Code == 1004))
            //{
            //    _context.SystemSettings.Add(new SystemSetting
            //    {
            //        Code = 1004,
            //        Name = "ATHLETICS_INFORMATION_WIN_COUNT",
            //        Value = "100,200,500,800,1000",
            //        Description = "竞技场消息胜利次数",
            //        GroupName = "athleticsInformationWinCount",
            //    });
            //}

            //if (!_context.SystemSettings.Any(s => s.Code == 1002))
            //{
            //    _context.SystemSettings.Add(new SystemSetting
            //    {
            //        Code = 1002,
            //        Name = "BUY_FIGHT_COUNT_MAX",
            //        Value = "10",
            //        Description = "购买对战次数最大值",
            //        GroupName = "fightCountConfig",
            //    });
            //}
            //if (!_context.SystemSettings.Any(s => s.Code == 1003))
            //{
            //    _context.SystemSettings.Add(new SystemSetting
            //    {
            //        Code = 1003,
            //        Name = "BUY_FIGHT_COUNT_UNITPRICE",
            //        Value = "100000",
            //        Description = "购买对战次数单价",
            //        GroupName = "fightCountConfig",
            //    });
            //}

            //if (!_context.SystemSettings.Any(s => s.Code == 1004))
            //{
            //    _context.SystemSettings.Add(new SystemSetting
            //    {
            //        Code = 1004,
            //        Name = "DEFAULT_FIGHTCOUNT",
            //        Value = "10",
            //        Description = "每天默认送的对战次数",
            //        GroupName = "initial",
            //    });
            //}

            //if (!_context.SystemSettings.Any(s => s.Code == 1005))
            //{
            //    _context.SystemSettings.Add(new SystemSetting
            //    {
            //        Code = 1005,
            //        Name = "CAN_PK_COUNT",
            //        Value = "10",
            //        Description = "可PK人数",
            //        GroupName = "initial",
            //    });
            //}

            //if (!_context.SystemSettings.Any(s => s.Code == 1006))
            //{
            //    _context.SystemSettings.Add(new SystemSetting
            //    {
            //        Code = 1006,
            //        Name = "RANKING_SHOW_PLAYER_COUNT",
            //        Value = "10",
            //        Description = "排行榜显示人数",
            //        GroupName = "initial",
            //    });
            //}

            //if (!_context.SystemSettings.Any(s => s.Code == 1006))
            //{
            //    _context.SystemSettings.Add(new SystemSetting
            //    {
            //        Code = 1006,
            //        Name = "ATHLETICS_START_TIME",
            //        Value = "00:00",
            //        Description = "竞技场开始时间",
            //        GroupName = "athleticsTime",
            //    });
            //}

            //if (!_context.SystemSettings.Any(s => s.Code == 1007))
            //{
            //    _context.SystemSettings.Add(new SystemSetting
            //    {
            //        Code = 1007,
            //        Name = "ATHLETICS_END_TIME",
            //        Value = "23:50",
            //        Description = "竞技场结束时间",
            //        GroupName = "athleticsTime",
            //    });
            //}


            //if (!_context.SystemSettings.Any(s => s.Code == 2000))
            //{
            //    _context.SystemSettings.Add(new SystemSetting
            //    {
            //        Code = 2000,
            //        Name = "withdraw is open validate ip address",
            //        Value = "false",
            //        Description = "是否开启提现IP地址强验证",
            //        GroupName = "withdraw",
            //    });
            //}
        }
    }
}
