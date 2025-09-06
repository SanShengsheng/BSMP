using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.Linq.Extensions;
using Abp.MultiTenancy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.HttpContextHelper;
using MQKJ.BSMP.MultiTenancy;
using MQKJ.BSMP.Orders.Authorization;
using MQKJ.BSMP.Orders.DomainService;
using MQKJ.BSMP.Orders.Dtos;
using MQKJ.BSMP.Utils.Tools;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Orders
{
    /// <summary>
    /// Order应用层服务的接口实现方法
    ///</summary>
    //[AbpAuthorize]
    public class OrderAppService : BSMPAppServiceBase, IOrderAppService
    {
        private readonly IRepository<Order, Guid> _entityRepository;

        private readonly IOrderManager _entityManager;

        private readonly IRepository<Tenant> _tenantRepository;

        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly IRepository<MqAgent> _agentRepository;

        private const string ORDERFILENAME =  "{0}到{1}的订单列表.xlsx";

        public IEventBus EventBus { get; set; }

        /// <summary>
        /// 构造函数
        ///</summary>
        public OrderAppService(IRepository<Order, Guid> entityRepository, 
            IOrderManager entityManager,
            IHostingEnvironment hostingEnvironment,
            IRepository<Tenant> tenantRepository,
            IRepository<MqAgent> agentRepository
            )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;

            EventBus = NullEventBus.Instance;

            _hostingEnvironment = hostingEnvironment;

            _tenantRepository = tenantRepository;

            _agentRepository = agentRepository;
        }

        /// <summary>
        /// 获取Order的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		//[AbpAuthorize(OrderPermissions.Query)]
        public async Task<PagedResultDto<OrderListDto>> GetPaged(GetOrdersInput input)
        {
            input.PageIndex = input.PageIndex <= 1 ? 1 : input.PageIndex;
            input.MaxResultCount = input.PageSize <= 0 ? 10 : input.PageSize;
            input.SkipCount = (input.PageIndex - 1) * input.PageSize;

            var query = SearchOrderList(input);
            // TODO:根据传入的参数添加过滤条件

            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            //获取所有的租户
            var teants = _tenantRepository.GetAll().Select(t => new { t.Id, t.Name }).ToList();

            var agentPlayerIds = _agentRepository.GetAll().Select(a => a.PlayerId);

            var entityListDtos = entityList.MapTo<List<OrderListDto>>();

            foreach (var item in entityListDtos)
            {
                item.TenantName = item.TenantId != 0 ? teants.FirstOrDefault(t => t.Id == item.TenantId).Name : "无";

                if (item.Family != null)
                {
                    var agentCount = agentPlayerIds.Count(a => a.Value == item.Family.FatherId || a.Value == item.Family.MotherId);

                    if (agentCount == 1)
                    {
                        item.AgentName = "单代理";
                    }
                    else if (agentCount == 2)
                    {
                        item.AgentName = "双代理";
                    }
                    else
                    {
                        item.AgentName = "无代理";
                    }
                }
                else
                {
                    item.AgentName = "无代理";
                }
            }

            return new PagedResultDto<OrderListDto>(count, entityListDtos);
        }

        private IQueryable<Order> SearchOrderList(GetOrdersInput input)
        {

            var query = _entityRepository.GetAll().Include(p => p.Player)
                .Include(f => f.Family)
                .WhereIf(input.OrderState != OrderState.UnKnow, x => x.State == input.OrderState)
                .Where(x => x.CreationTime >= input.StartTime && x.CreationTime <= input.EndTime)
                .WhereIf(input.TenantId != 0, x => x.TenantId == input.TenantId)
                .WhereIf(!string.IsNullOrEmpty(input.UserName), x => x.Player.NickName.Contains(input.UserName))
                .WhereIf(!string.IsNullOrEmpty(input.OrderNumber), x => x.OrderNumber == input.OrderNumber || x.TransactionId == input.OrderNumber)
                .WhereIf(input.PaymentType != 0, x => x.PaymentType == input.PaymentType)
                .WhereIf(input.Amount != 0, x => x.Payment == input.Amount);

            return query;
        }

        /// <summary>
        /// 通过指定id获取OrderListDto信息
        /// </summary>
        //[AbpAuthorize(OrderPermissions.Query)]
        public async Task<OrderListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<OrderListDto>();
        }

        /// <summary>
        /// 获取编辑 Order
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[AbpAuthorize(OrderPermissions.Create, OrderPermissions.Edit)]
        public async Task<GetOrderForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetOrderForEditOutput();
            OrderEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<OrderEditDto>();

                //orderEditDto = ObjectMapper.Map<List<orderEditDto>>(entity);
            }
            else
            {
                editDto = new OrderEditDto();
            }

            output.Order = editDto;
            return output;
        }

        /// <summary>
        /// 添加或者修改Order的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[AbpAuthorize(OrderPermissions.Create, OrderPermissions.Edit)]
        public async Task CreateOrUpdate(CreateOrUpdateOrderInput input)
        {
            if (input.Order.Id.HasValue)
            {
                await Update(input.Order);
            }
            else
            {
                await Create(input.Order);
            }
        }

        /// <summary>
        /// 新增Order
        /// </summary>
        //[AbpAuthorize(OrderPermissions.Create)]
        protected virtual async Task<OrderEditDto> Create(OrderEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Order>(input);
            var entity = input.MapTo<Order>();

            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<OrderEditDto>();
        }

        /// <summary>
        /// 编辑Order
        /// </summary>
        //[AbpAuthorize(OrderPermissions.Edit)]
        protected virtual async Task Update(OrderEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除Order信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[AbpAuthorize(OrderPermissions.Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除Order的方法
        /// </summary>
        //[AbpAuthorize(OrderPermissions.BatchDelete)]
        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        public async Task<OrderEditDto> QueryOrder(QueryOrderInput input)
        {
            var entity = await _entityRepository.FirstOrDefaultAsync(o => o.OrderNumber == input.OrderNumber);

            if (entity == null)
            {
                return null;
            }
            else
            {
                return entity.MapTo<OrderEditDto>();
            }
        }

        public async Task UpdateOrderState(WechatPayUpdateOrderStateInput input)
        {
            var entity = await _entityRepository.FirstOrDefaultAsync(o => o.OrderNumber == input.OrderNumber);
            Logger.Debug($"订单查询，OrderApplicationService,UpdateOrderState");
            if (entity.State == OrderState.Paid)
            {
                return;
            }
            if (input.PayAmount != entity.Payment)
            {
                throw new Exception($"订单金额与支付结果不一致，请检查，订单号：{input.OrderNumber}, 订单金额：{entity.Payment}, 支付金额:{input.PayAmount}");
            }

            entity.State = input.IsSuccess ? OrderState.Paid : OrderState.Failed;

            entity.PaymentTime = DateTime.ParseExact(input.EndTime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
            entity.PaymentData = input.PaymentData;
            entity.SettlementTotalFee = input.RealAmount;
            entity.TransactionId = input.TransactionId;

            await _entityRepository.UpdateAsync(entity);

            await EventBus.TriggerAsync(new OrderSuccessEventData()
            {
                Order = entity
            });
        }

        public async Task CreateOrder(CreateOrderInput input)
        {
            input.State = OrderState.UnPaid;
            await _entityRepository.InsertAsync(input.MapTo<Order>());
        }

        public async Task UpdateBabyOrderState(WechatPayUpdateOrderStateInput input)
        {
            var order = await _entityRepository.FirstOrDefaultAsync(o => o.OrderNumber == input.OrderNumber);

            if (input.PayAmount != order.Payment)
            {
                throw new Exception($"订单金额与支付结果不一致，请检查，订单号：{input.OrderNumber}, 订单金额：{order.Payment}, 支付金额:{input.PayAmount}");
            }

            order.State = input.IsSuccess ? OrderState.Paid : OrderState.Failed;
            order.PaymentTime = DateTime.ParseExact(input.EndTime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
            order.PaymentData = input.PaymentData;
            order.SettlementTotalFee = input.RealAmount;
            order.TransactionId = input.TransactionId;

            await _entityRepository.UpdateAsync(order);
        }

        public string GetToExcel(GetOrdersInput input)
        {

            var fileName = string.Format(ORDERFILENAME, input.StartTime.ToString("yyyyMMdd"), input.EndTime.ToString("yyyyMMdd"));
            FileInfo file = new FileInfo(Path.Combine(_hostingEnvironment.WebRootPath, fileName));
            file.Delete();
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("订单列表");//创建sheet
                worksheet.Cells[1, 1].Value = "创建日期";
                worksheet.Cells[1, 2].Value = "商户订单号";
                worksheet.Cells[1, 3].Value = "微信订单号";
                worksheet.Cells[1, 4].Value = "付款人";
                worksheet.Cells[1, 5].Value = "代理";
                worksheet.Cells[1, 6].Value = "支付金额";
                worksheet.Cells[1, 7].Value = "付款日期";
                worksheet.Cells[1, 8].Value = "游戏平台";
                worksheet.Cells[1, 9].Value = "充值平台";
                worksheet.Cells[1, 10].Value = "付款状态";
                worksheet.Cells[1, 11].Value = "家庭Id";
                //worksheet.Cells.Style.ShrinkToFit = true;
                //worksheet.Cells.AutoFitColumns();

                //样式
                worksheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //worksheet.Cells.Style.WrapText = true;
                var query = SearchOrderList(input).OrderByDescending(x => x.CreationTime);

                var agentPlayerIds = _agentRepository.GetAll().Select(a => a.PlayerId);

                //获取所有的租户
                var teants = _tenantRepository.GetAll().Select(t => new { t.Id,t.Name}).ToList();
                int r = 1;
                foreach (var item in query)
                {
                    worksheet.Cells[r + 1, 1].Value = item.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Cells[r + 1, 2].Value = item.OrderNumber;
                    worksheet.Cells[r + 1, 3].Value = item.State == OrderState.Paid ? item.TransactionId : "无";
                    if (item.Player == null)
                    {
                        worksheet.Cells[r + 1, 4].Value = "无";
                    }
                    else
                    {
                        worksheet.Cells[r + 1, 4].Value = item.Player.NickName;
                    }

                    if (item.Family != null)
                    {
                        var count = agentPlayerIds.Count(a => a.Value == item.Family.FatherId || a.Value == item.Family.MotherId);

                        if (count == 1)
                        {
                            worksheet.Cells[r + 1, 5].Value = "单代理";
                        }
                        else if (count == 2)
                        {
                            worksheet.Cells[r + 1, 5].Value = "双代理";
                        }
                        else
                        {
                            worksheet.Cells[r + 1, 5].Value = "无代理";
                        }
                    }
                    else
                    {
                        worksheet.Cells[r + 1, 5].Value = "无代理";
                    }

                    worksheet.Cells[r + 1, 6].Value = item.Payment;

                    if (item.State == OrderState.Paid)
                    {
                        worksheet.Cells[r + 1, 7].Value = item.PaymentTime.HasValue ? item.PaymentTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "无";
                    }
                    else
                    {
                        worksheet.Cells[r + 1, 7].Value = "无";
                    }

                    worksheet.Cells[r + 1, 8].Value = item.TenantId != 0 ? teants.FirstOrDefault(t => t.Id == item.TenantId).Name : "无";

                    worksheet.Cells[r + 1, 9].Value = EnumHelper.EnumHelper.GetDescription(item.PaymentType);

                    worksheet.Cells[r + 1, 10].Value = EnumHelper.EnumHelper.GetDescription(item.State);

                    worksheet.Cells[r + 1, 11].Value = item.FamilyId;



                    r++;
                }

                //return package.GetAsByteArray();
                //var xFile = FileHelp.GetFileInfo(ORDERFILENAME);
                package.Save();

                return fileName;
            }

        }

        /// <summary>
        /// 导出Order为excel表,等待开发。
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