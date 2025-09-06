using MQKJ.BSMP.Scenes;
using MQKJ.BSMP.SceneTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.Scenes
{
    public class SceneCreator
    {
        private readonly BSMPDbContext _context;

        public SceneCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateScene();
        }

        private void CreateScene()
        {
            //先添加场景类型
            if (_context.SceneTypes.FirstOrDefault(x => x.TypeName == StaticSceneTypeName.DefaultSceneTypeName) == null)
            {
                _context.SceneTypes.Add(new SceneType() { TypeName = StaticSceneTypeName.DefaultSceneTypeName });
                _context.SaveChanges();
            }
            //客厅
            if (_context.Scenes.FirstOrDefault(x => x.SceneName == StaticSceneName.LivingRoomScene) == null)
            {
                _context.Scenes.Add(new Scene()
                {
                    SceneName = StaticSceneName.LivingRoomScene,
                    SceneTypeId = _context.SceneTypes.FirstOrDefault(x => x.TypeName == StaticSceneTypeName.DefaultSceneTypeName).Id,
                });
                _context.SaveChanges();
            }

            //学校
            if (_context.Scenes.FirstOrDefault(x => x.SceneName == StaticSceneName.SchoolScene) == null)
            {
                _context.Scenes.Add(new Scene()
                {
                    SceneName = StaticSceneName.SchoolScene,
                    SceneTypeId = _context.SceneTypes.FirstOrDefault(x => x.TypeName == StaticSceneTypeName.DefaultSceneTypeName).Id,
                });
                _context.SaveChanges();
            }

            //影院
            if (_context.Scenes.FirstOrDefault(x => x.SceneName == StaticSceneName.CinemaScene) == null)
            {
                _context.Scenes.Add(new Scene()
                {
                    SceneName = StaticSceneName.CinemaScene,
                    SceneTypeId = _context.SceneTypes.FirstOrDefault(x => x.TypeName == StaticSceneTypeName.DefaultSceneTypeName).Id,
                });
                _context.SaveChanges();
            }

            //步行街
            if (_context.Scenes.FirstOrDefault(x => x.SceneName == StaticSceneName.PromenadeStreetScene) == null)
            {
                _context.Scenes.Add(new Scene()
                {
                    SceneName = StaticSceneName.PromenadeStreetScene,
                    SceneTypeId = _context.SceneTypes.FirstOrDefault(x => x.TypeName == StaticSceneTypeName.DefaultSceneTypeName).Id,
                });
                _context.SaveChanges();
            }

            //饭店
            if (_context.Scenes.FirstOrDefault(x => x.SceneName == StaticSceneName.HotelScene) == null)
            {
                _context.Scenes.Add(new Scene()
                {
                    SceneName = StaticSceneName.HotelScene,
                    SceneTypeId = _context.SceneTypes.FirstOrDefault(x => x.TypeName == StaticSceneTypeName.DefaultSceneTypeName).Id,
                });
                _context.SaveChanges();
            }

            //旅游
            if (_context.Scenes.FirstOrDefault(x => x.SceneName == StaticSceneName.TourismScene) == null)
            {
                _context.Scenes.Add(new Scene()
                {
                    SceneName = StaticSceneName.TourismScene,
                    SceneTypeId = _context.SceneTypes.FirstOrDefault(x => x.TypeName == StaticSceneTypeName.DefaultSceneTypeName).Id,
                });
                _context.SaveChanges();
            }

            //KTV
            if (_context.Scenes.FirstOrDefault(x => x.SceneName == StaticSceneName.KTVScene) == null)
            {
                _context.Scenes.Add(new Scene()
                {
                    SceneName = StaticSceneName.KTVScene,
                    SceneTypeId = _context.SceneTypes.FirstOrDefault(x => x.TypeName == StaticSceneTypeName.DefaultSceneTypeName).Id,
                });
                _context.SaveChanges();
            }

            //卧室
            if (_context.Scenes.FirstOrDefault(x => x.SceneName == StaticSceneName.BedroomScene) == null)
            {
                _context.Scenes.Add(new Scene()
                {
                    SceneName = StaticSceneName.BedroomScene,
                    SceneTypeId = _context.SceneTypes.FirstOrDefault(x => x.TypeName == StaticSceneTypeName.DefaultSceneTypeName).Id,
                });
                _context.SaveChanges();
            }

            //公园
            if (_context.Scenes.FirstOrDefault(x => x.SceneName == StaticSceneName.ParkScene) == null)
            {
                _context.Scenes.Add(new Scene()
                {
                    SceneName = StaticSceneName.ParkScene,
                    SceneTypeId = _context.SceneTypes.FirstOrDefault(x => x.TypeName == StaticSceneTypeName.DefaultSceneTypeName).Id,
                });
                _context.SaveChanges();
            }

            //社交
            if (_context.Scenes.FirstOrDefault(x => x.SceneName == StaticSceneName.SocialSoftwareScene) == null)
            {
                _context.Scenes.Add(new Scene()
                {
                    SceneName = StaticSceneName.SocialSoftwareScene,
                    SceneTypeId = _context.SceneTypes.FirstOrDefault(x => x.TypeName == StaticSceneTypeName.DefaultSceneTypeName).Id,
                });
                _context.SaveChanges();
            }
        }
    }
}
