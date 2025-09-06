using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Web.Models.DebugModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Areas.DebugModel.Controllers
{
    [Area("DebugModel")]
    public class ModifyGrownController: BSMPControllerBase
    {
        private readonly IRepository<Baby> _babyRepository;
        private readonly IRepository<EventGroup> _eventGroupRepository;
        private readonly IRepository<BabyEventRecord,Guid> _eventRecordRepository;
        private readonly IRepository<EventGroupBabyEvent> _eventGroupEventRepository;
        public ModifyGrownController(IRepository<Baby> babyRepository,
            IRepository<EventGroup> eventGroupRepository, 
            IRepository<BabyEventRecord, Guid> eventRecordRepository,
            IRepository<EventGroupBabyEvent> eventGroupEventRepository
            ) {
            _babyRepository = babyRepository;
            _eventGroupRepository = eventGroupRepository;
            _eventRecordRepository = eventRecordRepository;
            _eventGroupEventRepository = eventGroupEventRepository;
        }
        [HttpGet]
        public async Task<IList<DebugGetBabiesOutput>> GetBabies(DebugGetBabiesInput input) {
            if (input.PageIndex == 0) input.PageIndex = 1;
            if (input.PageSize == 0) input.PageSize = 10;
            return await _babyRepository.GetAllIncluding(s => s.Family, s => s.Family.Father, s => s.Family.Mother)
                .WhereIf(int.TryParse(input.BabyId, out int _babyid), b => b.Id == _babyid)
                .WhereIf(!string.IsNullOrWhiteSpace(input.BabyName), b => b.Name!=null &&b.Name.Contains(input.BabyName))
                .WhereIf(int.TryParse(input.FamilyId, out int _familyId), b => b.FamilyId == _familyId)
                .WhereIf(Guid.TryParse(input.Mother, out Guid _mother), b => b.Family.MotherId == _mother)
                .WhereIf(Guid.TryParse(input.FamilyId, out Guid _father), b => b.Family.FatherId == _father)
                .WhereIf(!string.IsNullOrWhiteSpace(input.FatherNickName), b => b.Family.Father.NickName.Contains(input.FatherNickName))
                .WhereIf(!string.IsNullOrWhiteSpace(input.MotherNickName), b => b.Family.Mother.NickName.Contains(input.MotherNickName))
                .OrderByDescending(s=>s.Id)
                .Skip((input.PageIndex - 1) * input.PageSize)
                .Take(input.PageSize)
                .Select((baby) => new DebugGetBabiesOutput
                {
                    Mother = baby.Family.Mother.NickName,
                    Father = baby.Family.Father.NickName,
                    BabyName = baby.Name,
                    CreationTime = baby.CreationTime,
                    FamilyId = baby.FamilyId,
                    IsAdult = baby.State == BabyState.Adult,
                    BabyId =baby.Id
                })  
                .ToListAsync();
        }
        public async Task<IActionResult> SetUpTheLastEventGroup([FromBody]SetUpEndInput input) {
            var _result = new SetUpEndOutput { Success =true };
            if (!int.TryParse(input.BabyId, out int _babyId))
            {
                _result.Message = "宝宝标识不能为空";
                _result.Success = false;
            }
            else {
                var _baby = await _babyRepository.GetAllIncluding(s=>s.Family).Where(s=>s.Id == _babyId).FirstAsync();
                if (_baby.State == BabyState.Adult)
                {
                    _result.Message = "宝宝已经成年";
                    _result.Success = false;
                }
                else {
                    var _endgroup = await _eventGroupRepository.GetAll().OrderBy(s => s.Code).LastOrDefaultAsync();
                    
                   // var _endGroupbabyevents = await _eventGroupEventRepository.GetAllIncluding(s => s.EventGroup, s => s.BabyEvent).Where(s => s.GroupId == _endgroup.Id&&s.BabyEvent.Type== IncidentType.Growup&&!s.BabyEvent.IsDeleted).ToListAsync();
                    var _groups = await _eventRecordRepository.GetAllIncluding(s => s.Event).Where(s => s.BabyId == _babyId && s.Event.Type == IncidentType.Growup).ToListAsync();
                    if (_groups.Any())
                    {
                        _groups.ForEach(item =>
                        {
                            item.EndTime = item.EndTime.HasValue ? System.DateTime.Now : item.EndTime;
                            item.EndTimeStamp = item.EndTimeStamp.HasValue ? new DateTimeOffset(System.DateTime.Now).ToUnixTimeSeconds() : item.EndTimeStamp;
                            item.State = EventRecordState.Handled;
                        });
                    }
                    var _prevGroup = await _eventGroupRepository.GetAll().Where(s => s.Id == _endgroup.PrevGroupId).FirstOrDefaultAsync();
                    var _prevGroupbabyevents = await _eventGroupEventRepository.GetAllIncluding(s => s.EventGroup, s => s.BabyEvent).Where(s => s.GroupId == _prevGroup.Id && s.BabyEvent.Type == IncidentType.Growup && !s.BabyEvent.IsDeleted).ToListAsync();
                    //_endGroupbabyevents.ForEach(f =>
                    //{
                    //    var currevent = _eventRecordRepository.FirstOrDefault(record => record.EventId == f.Id);
                    //    if (currevent != null)
                    //    {
                    //        currevent.State = EventRecordState.Handled;
                    //    }
                    //    else
                    //    {
                    //        var _eventOpts = _eventOptionsRepository.GetAll().Where(opts => opts.BabyEventId == f.Id).ToList();
                    //        _eventRecordRepository.Insert(new BabyEventRecord
                    //        {
                    //            EndTimeStamp = (new DateTimeOffset(System.DateTime.Now)).ToUnixTimeSeconds(),
                    //            EventId = f.Id,
                    //            PlayerId = _baby.Family.FatherId,
                    //            FamilyId = _baby.FamilyId,
                    //            OptionId = _eventOpts.First().Id,
                    //            State = EventRecordState.Handled,
                    //            BabyId = _babyId
                    //        });
                    //    }
                    //});
                    _baby.AgeDouble = _prevGroupbabyevents.Select(s=>s.BabyEvent).OrderByDescending(s => s.Code).First().Age.Value;
                    _baby.AgeString = _prevGroupbabyevents.Select(s => s.BabyEvent).OrderByDescending(s => s.Code).First().AgeString;
                    _baby.GroupId = _endgroup.Id;
                    
                }
            }    
            return Json(_result);
        }
        public async Task<IActionResult> SetUpAdult([FromBody]SetUpEndInput input) {
            var _result = new SetUpEndOutput { Success = true };
            if (!int.TryParse(input.BabyId, out int _babyId))
            {
                _result.Message = "宝宝标识不能为空";
                _result.Success = false;
            }
            else {
                var _baby = await _babyRepository.FirstOrDefaultAsync(_babyId);
                if (_baby.State == BabyState.Adult)
                {
                    _result.Message = "宝宝已经成年";
                }
                else
                    _baby.State = BabyState.Adult;
            }
            return Json(_result);
           
        }
        public IActionResult Index() {
            return View();
        }
    }
}
