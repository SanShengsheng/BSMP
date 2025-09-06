using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.Scenes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Mapping
{
    public class SceneMap : IEntityTypeConfiguration<Scene>
    {
        void IEntityTypeConfiguration<Scene>.Configure(EntityTypeBuilder<Scene> builder)
        {
            builder.HasIndex(e => new { e.SceneName }).IsUnique();
        }
    }
}
