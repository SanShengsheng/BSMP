
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


using MQKJ.BSMP.Friends;
using MQKJ.BSMP.Friends.Dtos;
using MQKJ.BSMP.Friends.DomainService;
using MQKJ.BSMP.Friends.Authorization;
using MQKJ.BSMP.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Players;

namespace MQKJ.BSMP.Friends
{
    /// <summary>
    /// Friend应用层服务的接口实现方法  
    ///</summary>
    //[AbpAuthorize]
    public class FriendAppService : BSMPAppServiceBase, IFriendAppService
    {
        private readonly IRepository<Friend, Guid> _entityRepository;

        private readonly IFriendManager _entityManager;

        private readonly IRepository<Player, Guid> _playerEntityRepository;

        //private readonly BSMPDbContext _dbContext;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public FriendAppService(
        IRepository<Friend, Guid> entityRepository
        ,IFriendManager entityManager
        , IRepository<Player, Guid> playerEntityRepository
        //, BSMPDbContext dbContext
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
            _playerEntityRepository = playerEntityRepository;
            //_dbContext = dbContext;
        }


        /// <summary>
        /// 获取Friend的分页列表信息 自己邀请的人
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		//[AbpAuthorize(FriendPermissions.Query)] 
        public async Task<PagedResultDto<FriendListDto>> GetPagedFriends(GetFriendsInput input)
		{
            //判断是否每天的凌晨12点
            //var currentTime = DateTime.Now.TimeOfDay.ToString().Split('.')[0];

            var query = _entityRepository.GetAllIncluding(f => f.MyFriend).Where(f => f.PlayerId == input.PlayerId);

            //if (currentTime == "00:00:00")
            //{
            //    foreach (var item in query)
            //    {
            //        if (item.HeartCount != 3)
            //        {
            //            item.HeartCount = 3;

            //            await _entityRepository.UpdateAsync(item);
            //        }
            //    }
            //}

            var count = await query.CountAsync();

            var entityList = await query.AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            var entityListDtos =entityList.OrderByDescending(x => x.LastModificationTime).OrderByDescending(x => x.CreationTime).MapTo<List<FriendListDto>>();

			return new PagedResultDto<FriendListDto>(count,entityListDtos);
		}

        public async Task<PagedResultDto<FriendListDto>> GetPagedOthers(GetFriendsInput input)
        {

            var query = _entityRepository.GetAll().
                Where(f => f.FriendId == input.PlayerId).
                OrderBy(f => f.PlayerId);

            var count = await query.CountAsync();

            var playerIds = await query.Select(s => s.PlayerId).ToListAsync();

            var friendQuery = await _playerEntityRepository.GetAll().
                Where(s=> playerIds.Contains(s.Id)).
                AsNoTracking().
                OrderBy(f => f.Id).
                ToListAsync();

            if (friendQuery.Count() == 0)
            {
                return null;
            }

            var entityList = await query
                    .PageBy(input)
                    .ToListAsync();

            
            var entityListDtos = entityList.MapTo<List<FriendListDto>>();

            var friends = new List<MyFriend>(entityListDtos.Count());

            friends = friendQuery.MapTo(friends);

            for (int i = 0; i < entityListDtos.Count(); i++)
            {
                entityListDtos[i].MyFriend = new MyFriend();

                entityListDtos[i].MyFriend.NickName = friendQuery[i].NickName;

                entityListDtos[i].MyFriend.HeadUrl = friendQuery[i].HeadUrl;

                entityListDtos[i].MyFriend.Id = friendQuery[i].Id;
            }

            entityListDtos = entityListDtos.OrderByDescending(x => x.CreationTime).OrderByDescending(x => x.LastModificationTime).ToList();

            return new PagedResultDto<FriendListDto>(count, entityListDtos);
        }

        public async Task<int> GetUrgeCount(GetUrgeCountDto input)
        {
            return await _entityRepository.CountAsync(c => c.IsUrge == true && c.PlayerId == input.PlayerId);
        }


		/// <summary>
		/// 通过指定id获取FriendListDto信息
		/// </summary>
		//[AbpAuthorize(FriendPermissions.Query)] 
		public async Task<FriendListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<FriendListDto>();
		}

		/// <summary>
		/// 获取编辑 Friend
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		//[AbpAuthorize(FriendPermissions.Create,FriendPermissions.Edit)]
		public async Task<GetFriendForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetFriendForEditOutput();
            FriendEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<FriendEditDto>();

				//friendEditDto = ObjectMapper.Map<List<friendEditDto>>(entity);
			}
			else
			{
				editDto = new FriendEditDto();
			}

			output.Friend = editDto;
			return output;
		}


        public async Task<PagedResultDto<GetAllFriendListOutput>> GetAllFriendList(GetAllFriendListDto input)
        {
            var inviterId = Guid.Empty;

            var inviteeId = Guid.Empty;

            var players = _playerEntityRepository.GetAll().Distinct().AsNoTracking().OrderByDescending(c => c.LastModificationTime);

            if (!string.IsNullOrEmpty(input.InviterName))
            {
                inviterId = players.FirstOrDefault(p => p.NickName.Contains(input.InviterName)).Id;
            }

            if (!string.IsNullOrEmpty(input.InviteeName))
            {
                inviteeId = players.FirstOrDefault(p => p.NickName.Contains(input.InviteeName)).Id;
            }
            var friendLst = await _entityRepository.GetAll()
                .WhereIf(inviterId != Guid.Empty && inviteeId != Guid.Empty, x => x.PlayerId == inviterId && x.FriendId == inviteeId)
                .AsNoTracking()
                .ToListAsync();

            var lst = new List<GetAllFriendListOutput>();

            foreach (var item in friendLst)
            {

                var inviterPlayer = players.FirstOrDefault(p => p.Id == item.PlayerId);
                var inviteePlayer = players.FirstOrDefault(p => p.Id == item.FriendId);

                if (inviterPlayer != null && inviteePlayer != null)
                {
                    var getAllFriendListOutput = new GetAllFriendListOutput();

                    getAllFriendListOutput.Id = item.Id;

                    getAllFriendListOutput.InviterName = inviterPlayer.NickName;

                    getAllFriendListOutput.InviteeName = inviteePlayer.NickName;

                    getAllFriendListOutput.Floor = item.Floor;

                    lst.Add(getAllFriendListOutput);
                }
            }

            var count = friendLst.Count();

            return new PagedResultDto<GetAllFriendListOutput>(count, lst);
        }

        [HttpGet]
        public async Task UpdateFloor(UpdateFloorDto input)
        {
            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.FirstOrDefaultAsync(f => f.Id == input.Id);

                entity.Floor = input.Floor;

                await _entityRepository.UpdateAsync(entity);
            }

        }

		/// <summary>
		/// 添加或者修改Friend的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		//[AbpAuthorize(FriendPermissions.Create,FriendPermissions.Edit)]
		public async Task CreateOrUpdate(CreateOrUpdateFriendInput input)
		{

			if (input.Friend.Id.HasValue)
			{
				await Update(input.Friend);
			}
			else
			{
				await Create(input.Friend);
			}
		}

		/// <summary>
		/// 新增Friend
		/// </summary>
		//[AbpAuthorize(FriendPermissions.Create)]
		protected virtual async Task<FriendEditDto> Create(FriendEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Friend>(input);
            var entity=input.MapTo<Friend>();
			
			entity = await _entityRepository.InsertAsync(entity);

			return entity.MapTo<FriendEditDto>();
		}

		/// <summary>
		/// 编辑Friend
		/// </summary>
		//[AbpAuthorize(FriendPermissions.Edit)]
		protected virtual async Task Update(FriendEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}

        /// <summary>
        /// 更新催促状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateUrgeState(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            if (entity != null)
            {
                if (entity.IsUrge)
                    entity.IsUrge = false;
                else
                    entity.IsUrge = true;

                await _entityRepository.UpdateAsync(entity);
            }
        }

        /// <summary>
        /// 更新心的数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateHeartCount(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            if (entity.HeartCount != 0)
                entity.HeartCount -= 1;

            await _entityRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 更新关卡
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateBarrier(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            if (entity.Floor - 3 <= 1)
                entity.Floor = 1;
            else
                entity.Floor -= 3;

            await _entityRepository.UpdateAsync(entity);
        }


        /// <summary>
        /// 删除Friend信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[AbpAuthorize(FriendPermissions.Delete)]
        public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除Friend的方法
		/// </summary>
		//[AbpAuthorize(FriendPermissions.BatchDelete)]
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        /// <summary>
        /// 导出Friend为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}

    }
}


