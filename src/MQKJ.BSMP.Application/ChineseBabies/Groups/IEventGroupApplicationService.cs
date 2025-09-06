
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
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using Abp.Application.Services.Dto;


using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Groups.Dtos;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// EventGroup应用层服务的接口方法
    ///</summary>
    public interface IEventGroupAppService :
        BsmpApplicationService<EventGroup, int, EventGroupEditDto, EventGroupEditDto, GetEventGroupsInput, EventGroupListDto>
    {
        LinkedList<EventGroup> BuildNode();
        LinkedListNode<EventGroup> GetCurrentNode(int groupId);
        LinkedListNode<EventGroup> GetNextNode(int groupId);
        LinkedListNode<EventGroup> GetInitGroup();
        void ResetNode();
        Task AddEvents(AddEventsInput input);
    }
}
