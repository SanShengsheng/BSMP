

//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using MQKJ.BSMP.ActiveApply;

//namespace MQKJ.BSMP.EntityMapper.RiskActiveApplys
//{
//    public class RiskActiveApplyCfg : IEntityTypeConfiguration<RiskActiveApply>
//    {
//        public void Configure(EntityTypeBuilder<RiskActiveApply> builder)
//        {

//            builder.ToTable("RiskActiveApplys", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
//			builder.Property(a => a.Season).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
//			builder.Property(a => a.NickName).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
//			builder.Property(a => a.Gender).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
//			builder.Property(a => a.BirthDateTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
//			builder.Property(a => a.Height).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
//			builder.Property(a => a.Address).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
//			builder.Property(a => a.Hobbies).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
//			builder.Property(a => a.SelfIntroduction).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
//			builder.Property(a => a.DeclarationOfDating).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
//			builder.Property(a => a.Source).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


//        }
//    }
//}


