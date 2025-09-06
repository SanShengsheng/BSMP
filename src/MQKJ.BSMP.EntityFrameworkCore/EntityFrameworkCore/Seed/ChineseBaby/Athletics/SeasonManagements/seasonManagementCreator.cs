using MQKJ.BSMP.ChineseBabies.Athletics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed.ChineseBaby.Athletics
{
    public class SeasonManagementCreator
    {
        private readonly BSMPDbContext _context;

        public SeasonManagementCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateSeason();
        }

        private void CreateSeason()
        {
            var isHas = _context.SeasonManagements.Any(s => s.IsCurrent == true);
            if (!isHas)
            {
                var systemConfig = _context.SystemSettings.FirstOrDefault(s => s.Code == 1002).Value;
                var configValue = JsonConvert.DeserializeObject<SeasonManagement>(systemConfig);
                configValue.IsCurrent = true;
                configValue.SeasonNumber = 1;
                configValue.StartTime = configValue.StartTime.ToUniversalTime();
                configValue.EndTime = configValue.EndTime.ToUniversalTime();
                _context.SeasonManagements.Add(configValue);
            }
        }
    }
}
