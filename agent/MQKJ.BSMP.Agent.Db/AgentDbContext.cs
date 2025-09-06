using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Common;
using MQKJ.BSMP.Common.SensitiveWords;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Agent.Db
{
    public class AgentDbContext : DbContext
    {
        public AgentDbContext() { }
        public AgentDbContext(DbContextOptions<AgentDbContext> options) : base(options) { }

        #region 养娃
        public DbSet<Baby> Babies { get; set; }

        public DbSet<Family> Families { get; set; }

        public DbSet<BabyActivity> BabyActivities { get; set; }

        public DbSet<BabyActivityRecord> BabyActivityRecords { get; set; }

        public DbSet<BabyEnding> BabyEndings { get; set; }
        public DbSet<BabyEventOption> BabyEventOptions { get; set; }

        public DbSet<BabyEvent> BabyEvents { get; set; }

        public DbSet<BabyEventRecord> BabyEventRecords { get; set; }

        public DbSet<BabyGrowUpRecord> BabyGrowUpRecords { get; set; }

        public DbSet<ChangeProfessionCost> ChangeProfessionCosts { get; set; }

        public DbSet<EventGroup> EventGroups { get; set; }

        public DbSet<EventGroupBabyEvent> EventGroupBabyEvents { get; set; }

        public DbSet<Information> Informations { get; set; }

        public DbSet<Profession> Professions { get; set; }

        public DbSet<PlayerProfession> PlayerProfessions { get; set; }

        public DbSet<MqAgent> MqAgents { get; set; }

        public DbSet<Reward> Rewards { get; set; }


        public DbSet<SensitiveWord> SensitiveWords { get; set; }

        public DbSet<CoinRecharge> CoinRecharges { get; set; }


        public DbSet<EnergyRecharge> EnergyRecharges { get; set; }

        public DbSet<CoinRechargeRecord> CoinRechargeRecords { get; set; }

        public DbSet<EnergyRechargeRecord> EnergyRechargeRecords { get; set; }
        public DbSet<AutoRunnerConfig> AutoRunnerConfigs { get; set; }
        public DbSet<AutoRunnerRecord> AutoRunnerRecords { get; set; }

        public DbSet<EnterpirsePaymentRecord> EnterpirsePaymentRecords { get; set; }

        #endregion
    }
}
