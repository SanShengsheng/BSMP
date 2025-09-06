using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.SceneTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.SceneTypes
{
    public class SceneTypeCreator
    {
        private readonly BSMPDbContext _dbContext;

        public SceneTypeCreator(BSMPDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create()
        {
            CreateSceneType();
        }

        public void CreateSceneType()
        {
            var sceneTypeEntity = _dbContext.SceneTypes.IgnoreQueryFilters().FirstOrDefault(x => x.TypeName == StaticSceneTypeName.DefaultSceneTypeName);

            if (sceneTypeEntity == null)
            {
                sceneTypeEntity = _dbContext.SceneTypes.Add(new SceneType() { TypeName = StaticSceneTypeName.DefaultSceneTypeName }).Entity;
                _dbContext.SaveChanges();
            }
        }
    }
}
