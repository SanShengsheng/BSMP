
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;


using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.Groups.Dtos;
using Abp;
using JCSoft.WX.Framework.Models;

namespace MQKJ.BSMP.ChineseBabies
{
    public class EventGroupAppService :
        BsmpApplicationServiceBase<EventGroup, int, EventGroupEditDto, EventGroupEditDto, GetEventGroupsInput, EventGroupListDto>,
        IEventGroupAppService

    {

        private static LinkedList<EventGroup> _groupNode;
        private static object _lock = new object();
        private readonly IRepository<EventGroupBabyEvent, int> _groupEventsRepository;
        public EventGroupAppService(IRepository<EventGroup, int> repository,
            IRepository<EventGroupBabyEvent, int> groupEventsRepository) : base(repository)
        {
            _groupEventsRepository = groupEventsRepository;
        }

        public LinkedList<EventGroup> BuildNode()
        {
            if (_groupNode == null)
            {
                lock (_lock)
                {
                    if (_groupNode == null)
                    {
                        _groupNode = GetEventGroupNode();
                    }
                }
            }

            return _groupNode;
        }

        private LinkedList<EventGroup> GetEventGroupNode()
        {
            var groups = GetQuery(new GetEventGroupsInput())
                .Include(r => r.EventGroupBabyEvent)
                    .ThenInclude(r => r.BabyEvent);
                
            if (groups.Any())
            {
                LinkedList<EventGroup> result = new LinkedList<EventGroup>();
                var first = groups.FirstOrDefault(g => !g.PrevGroupId.HasValue);
                if (first != null)
                {
                    result.AddFirst(first);
                    var dict = groups.Where(g => g.PrevGroupId.HasValue).Distinct().ToDictionary(k => k.PrevGroupId);
                    var currentId = first.Id;

                    while (true)
                    {
                        if (dict.ContainsKey(currentId))
                        {
                            var next = dict[currentId];
                            result.AddLast(next);
                            currentId = next.Id;
                        }
                        else
                        {
                            break;
                        }
                    }
                    return result;
                }
                else
                {
                    Logger.Error($"事件组为空，无法生成");
                }
            }
            return null;
        }

        public LinkedListNode<EventGroup> GetCurrentNode(int groupId)
        {
            var node = BuildNode();
            var group = node.FirstOrDefault(g => g.Id == groupId);
            if (group != null)
            {
                return node.Find(group);
            }

            return null;
        }

        public  LinkedListNode<EventGroup> GetNextNode(int groupId)
        {
            var current =  GetCurrentNode(groupId);
            return current?.Next;
        }

        internal override IQueryable<EventGroup> GetQuery(GetEventGroupsInput model)
        {
            return _repository.GetAll();
        }

        public void ResetNode() => _groupNode = null;



        public  LinkedListNode<EventGroup> GetInitGroup() => BuildNode()?.First;

        public async Task AddEvents(AddEventsInput input)
        {
            var group = await _repository.FirstOrDefaultAsync(input.GroupId);
            if (group == null)
            {
                throw new AbpException("未找到事件组");
            }

           await _groupEventsRepository.DeleteAsync(k => k.GroupId == group.Id);
            //_groupEventsRepository.Insert(new EventGroupBabyEvent
            //{
            //    GroupId = 1,
            //    EventId = 1,
                
            //});
            foreach (var eventId in input.EventIds)
            {
                await _groupEventsRepository.InsertAsync(new EventGroupBabyEvent
                {
                    GroupId = group.Id,
                    EventId = eventId
                });
            }


        }   
    }
}


