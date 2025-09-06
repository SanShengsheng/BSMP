
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Json;
using Abp.Linq.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MQKJ.BSMP.Authorization;
using MQKJ.BSMP.BigRisks.WeChat;
using MQKJ.BSMP.BigRisks.WeChat.WechatPay;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Common.Authorization;
using MQKJ.BSMP.Common.DomainService;
using MQKJ.BSMP.Common.Dtos;
using MQKJ.BSMP.Common.EnterprisePayments.Dtos;
using MQKJ.BSMP.Common.IncomeRecords;
using MQKJ.BSMP.HttpContextHelper;
using MQKJ.BSMP.Orders;
using MQKJ.BSMP.Orders.Dtos;
using MQKJ.BSMP.Utils.Tools;
using MQKJ.BSMP.Utils.WechatPay;
using MQKJ.BSMP.Utils.WechatPay.Dtos;
using MQKJ.BSMP.Utils.WechatPay.Modes;
using MQKJ.BSMP.WeChatPay.Models;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Web;
using MQKJ.BSMP.Utils.Extensions;
namespace MQKJ.BSMP.Common
{
    /// <summary>
    /// EnterpirsePaymentRecord应用层服务的接口实现方法  
    ///</summary>
    //[AbpAuthorize]
    public class EnterpirsePaymentRecordAppService : BSMPAppServiceBase, IEnterpirsePaymentRecordAppService
    {
        private const string ENTERPRISEPAYMENTRECORDFILENAM = "提现记录表.xlsx";

        private readonly IRepository<EnterpirsePaymentRecord, Guid> _entityRepository;

        private readonly IRepository<MqAgent, int> _agentRepository;

        private readonly IEnterpirsePaymentRecordManager _entityManager;

        private readonly IRepository<Order, Guid> _orderRepository;

        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly IRepository<Family, int> _familyRepository;

        private readonly IRepository<IncomeRecord, Guid> _incomeRepository;

        private readonly IRepository<SystemSetting> _systemSettingRepository;

        private WechatpublicPlatformConfig _wechatConfig;

        private WechatPayConfig _wechatPayConfig;


        private const string CHECK_NAME = "FORCE_CHECK";

        private const string NO_CHECK = "NO_CHECK";

        //证书路径
        public const string WEIXIN_API_CERTIFICATE_ROOT_PATH = "api_certificate";
        public const string WEIXIN_API_CERTIFICATE_NAME = "apiclient_cert.p12";

        /// <summary>
        /// 构造函数 
        ///</summary>
        public EnterpirsePaymentRecordAppService(
        IRepository<EnterpirsePaymentRecord, Guid> entityRepository
        , IEnterpirsePaymentRecordManager entityManager
        , IRepository<MqAgent, int> agentRepository
        , IRepository<Order, Guid> orderRepository
            , IOptions<WechatpublicPlatformConfig> option
            , IOptions<WechatPayConfig> wechatPayConfigOption
        , IHostingEnvironment hostingEnvironment
        , IRepository<Family, int> familyRepository
        , IRepository<IncomeRecord, Guid> incomeRepository
            , IRepository<SystemSetting> systemSettingRepository
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;

            _agentRepository = agentRepository;

            _orderRepository = orderRepository;

            _hostingEnvironment = hostingEnvironment;

            _wechatConfig = option.Value;

            _wechatPayConfig = wechatPayConfigOption.Value;

            _familyRepository = familyRepository;

            _incomeRepository = incomeRepository;
            _systemSettingRepository = systemSettingRepository;
        }


        /// <summary>
        /// 获取EnterpirsePaymentRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		//[AbpAuthorize(EnterpirsePaymentRecordPermissions.Query)] 
        public async Task<PagedResultDto<EnterpirsePaymentRecordListDto>> GetPaged(GetEnterpirsePaymentRecordsInput input)
        {

            var query = SearchEnterpirsePaymentRecodLst(input);

            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            var entityListDtos = entityList.MapTo<List<EnterpirsePaymentRecordListDto>>();

            //foreach (var item in entityListDtos)
            //{
            //    if (item.PaymentData != null)
            //    {
            //        item.OutTradeNo = JObject.Parse(item.PaymentData)["PartnerTradeNo"].ToString();
            //        item.PaymentNo = JObject.Parse(item.PaymentData)["PaymentNo"].ToString();
            //    }
            //    else
            //    {
            //        item.OutTradeNo = "无";
            //        item.PaymentNo = "无";
            //    }
            //}

            return new PagedResultDto<EnterpirsePaymentRecordListDto>(count, entityListDtos);
        }

        private IQueryable<EnterpirsePaymentRecord> SearchEnterpirsePaymentRecodLst(GetEnterpirsePaymentRecordsInput input)
        {
            var query = _entityRepository.GetAll()
               .Include(a => a.MqAgent)
               .Where(a => a.MqAgent.IsDeleted == false)
               .WhereIf(input.WithdrawDepositState != WithdrawDepositState.All, x => x.State == input.WithdrawDepositState)
               .WhereIf(!string.IsNullOrEmpty(input.OrderNumber), x => x.OutTradeNo == input.OrderNumber || x.PaymentNo == input.OrderNumber)
               .Where(x => x.CreationTime >= input.StartTime && x.CreationTime <= input.EndTime && x.MqAgent.IsDeleted == false)
               .WhereIf(!string.IsNullOrEmpty(input.UserName), x => x.MqAgent.UserName.Contains(input.UserName) || x.MqAgent.NickName.Contains(input.UserName))
               .WhereIf(input.WithdrawMoneyType != WithdrawMoneyType.All, x => x.WithdrawMoneyType == input.WithdrawMoneyType);

            return query;
        }

        public async Task<PagedResultDto<GetAgentWithdrawMoneyRecordOutput>> GetAgentWithdrawMoneyRecords(GetAgentWithdrawMoneyRecordInput input)
        {
            var query = _entityRepository.GetAll().Where(a => a.AgentId == input.AgentId.Value).AsNoTracking();

            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            var entityListDtos = entityList.MapTo<List<GetAgentWithdrawMoneyRecordOutput>>();

            return new PagedResultDto<GetAgentWithdrawMoneyRecordOutput>(count, entityListDtos);
        }

        public async Task<RequestWithdrawMoneyOutput> RequestWithdrawMoney(RequestWithdrawMoneyInput input)
        {
            var output = new RequestWithdrawMoneyOutput();
            Console.WriteLine($"input is =>{input.ToJsonString()}");
            var agent = await _agentRepository.FirstOrDefaultAsync(a => a.Id == input.AgentId);

            if (agent.Balance < 1)
            {
                output.ErrorCode = ErrorCode.NotEnough;
            }
            else if (input.Amount > 100000)
            {
                output.ErrorCode = ErrorCode.ExceedingLimit;
            }
            else if (agent.State == AgentState.Suspend)
            {
                output.ErrorCode = ErrorCode.Frozen;
            }
            else if (agent.State == AgentState.UnAuditing)
            {
                output.ErrorCode = ErrorCode.UnAudite;
            }
            else
            {
                if (input.Amount <= agent.Balance)
                {
                    //失败状态的与 审核状态的
                    var isHasUnAuditeRecord = _entityRepository.GetAll().Any(a => a.AgentId == input.AgentId && (a.State == WithdrawDepositState.Auditing || a.State== WithdrawDepositState.HangOrder));
                    if (!isHasUnAuditeRecord)
                    {
                        await _entityRepository.InsertAsync(new EnterpirsePaymentRecord()
                        {
                            State = WithdrawDepositState.Auditing,
                            AgentId = agent.Id,
                            Amount = input.Amount,
                            RequestPlatform = input.RequestPlatform,
                            OutTradeNo = string.Format("{0}{1}{2}", _wechatPayConfig.MchId, DateTime.Now.ToString("yyyyMMddHHmm"), WeChatPayHelper.BuildRandomStr(6)),
                        });

                        agent.Balance -= input.Amount;
                        agent.LockedBalance += input.Amount;
                    }
                    else
                    {
                        output.ErrorCode = ErrorCode.HadUnHandleRecord;
                    }
                }
                else
                {
                    output.ErrorCode = ErrorCode.ExceedingLimit;
                }
            }

            return output;
        }
        /// <summary>
        /// 提现审核
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[AbpAuthorize(EnterpirsePaymentRecordPermissions.WithDrawMoneyForAgent,RequireAllPermissions = false)]
        public async Task<WithDrawMoneyForAgentOutput> WithDrawMoneyForAgent(WithDrawMoneyForAgentInput input)
        {
            var entity = await _entityRepository.GetAll().Include(a => a.MqAgent).FirstOrDefaultAsync(a => a.Id == input.Id);

            if (entity == null)
            {
                throw new Exception("不存在该记录");
            }

            if (entity.State == WithdrawDepositState.Refuse)
            {
                throw new Exception("该提现记录已拒绝");
            }

            if (entity.State == WithdrawDepositState.Success)
            {
                throw new Exception("该提现记录已提现成功，不能再次提现");
            }

            var output = new WithDrawMoneyForAgentOutput();

            var folder = Path.Combine(_hostingEnvironment.WebRootPath, WEIXIN_API_CERTIFICATE_ROOT_PATH, WEIXIN_API_CERTIFICATE_NAME);

            var response = new WechatEnterprisePayResult();

            var clientIp = input.IpStr;

            if (clientIp.Contains(":"))
            {
                clientIp = clientIp.Substring(clientIp.LastIndexOf(':') + 1);
            }
            if (!_systemSettingRepository.GetAll().Any(s => s.Value == "true" && s.Code == 2000))
            {
                clientIp = "140.143.18.154";
                //clientIp = "114.252.82.154";
                //clientIp = "111.192.162.193";
                //clientIp = "127.0.0.1";
            }
            Logger.Warn($"提现的客户端ip地址是---{clientIp}");

            var outTradeNo = string.Empty;

            if (entity.State == WithdrawDepositState.Fail)
            {
                outTradeNo = entity.OutTradeNo;
            }
            else
            {
                outTradeNo = string.Format("{0}{1}{2}", _wechatPayConfig.MchId, DateTime.Now.ToString("yyyyMMddHHmm"), WeChatPayHelper.BuildRandomStr(6));
            }

            var request = new EnterprisePayForPersonInput()
            {
                AppId = _wechatConfig.AppId,
                MchId = _wechatPayConfig.MchId,
                NonceStr = WeChatPayHelper.GetNoncestr(),
                OpenId = entity.MqAgent.OpenId,
                CheckName = NO_CHECK,
                UserName = entity.MqAgent.UserName,
                TotalAmount = Convert.ToDouble(entity.Amount) * 100.00,//totalAmount,
                Description = $"提现{entity.Amount}元成功",//$"代理{agenter.UserName}要提现{totalAmount}元",
                OutTradeNo = outTradeNo,
                Spbill_Create_IP = clientIp,
                Key = _wechatPayConfig.Key,
                Path = folder
            };
            response = EnterprisePaymentRequest.EnterprisePayForPerson(request);
            entity.RequestData = JsonHelper.GetJson(request);
            var agent = entity.MqAgent;

            entity.WithdrawMoneyType = WithdrawMoneyType.WeChatEnterPayType;
            if (response.IsSuccess())
            {
                entity.State = WithdrawDepositState.Success;

                entity.PaymentNo = response.PaymentNo;

                entity.OutTradeNo = response.PartnerTradeNo;

                entity.PaymentTime = DateTime.Parse(response.PaymentTime);

                if (agent == null)
                {
                    throw new ArgumentNullException("代理为空");
                }

                agent.LockedBalance -= entity.Amount;
                await _agentRepository.UpdateAsync(agent);

                output.IsSuccess = response.IsSuccess();
            }
            else
            {
                //提现失败还可以发起提现
                entity.State = WithdrawDepositState.Fail;
                Logger.Error($"提现失败，失败信息{response.ToJsonString()}");

                //agent.LockedBalance -=entity.Amount;

                //agent.Balance += entity.Amount;

                await _agentRepository.UpdateAsync(agent);
            }

            entity.PaymentData = response.ToJsonString();

            await _entityRepository.UpdateAsync(entity);

            output.ErrorMessage = response.ErrCodeDes;

            return output;

        }

        public async Task RefuseAuditeWithDrawMoney(RefuseAuditeWithDrawMoneyInput input)
        {
            var entity = await _entityRepository.GetAll().Include(a => a.MqAgent).FirstOrDefaultAsync(a => a.Id == input.Id);
            entity.CheckCondition(entity.State != WithdrawDepositState.HangOrder, "挂单中的交易不允许拒绝！");
            if (entity == null)
            {
                throw new Exception("不存在该记录");
            }

            entity.State = WithdrawDepositState.Refuse;

            var agent = entity.MqAgent;
            if (agent == null)
            {
                throw new ArgumentNullException("代理为空");
            }
            if (agent.LockedBalance < entity.Amount)
            {
                throw new Exception("锁定的金额小于提现申请的金额！");
            }
            await _entityRepository.UpdateAsync(entity);

            agent.LockedBalance -= entity.Amount;

            agent.Balance += entity.Amount;

            await _agentRepository.UpdateAsync(agent);

        }

        public string GetToExcel(GetEnterpirsePaymentRecordsInput input)
        {
            FileInfo file = new FileInfo(Path.Combine(_hostingEnvironment.WebRootPath, ENTERPRISEPAYMENTRECORDFILENAM));
            file.Delete();
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("提现记录表");//创建sheet
                worksheet.Cells[1, 1].Value = "创建时间";
                worksheet.Cells[1, 2].Value = "提现用户";
                worksheet.Cells[1, 3].Value = "微信昵称";
                worksheet.Cells[1, 4].Value = "身份";
                worksheet.Cells[1, 5].Value = "提现金额";
                worksheet.Cells[1, 6].Value = "提现状态";
                worksheet.Cells[1, 7].Value = "提现方式";
                worksheet.Cells[1, 8].Value = "商户订单号";
                worksheet.Cells[1, 9].Value = "微信订单号";
                worksheet.Cells[1, 10].Value = "提现日期";
                //worksheet.Cells.Style.ShrinkToFit = true;
                //worksheet.Cells.AutoFitColumns();

                //样式
                worksheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //worksheet.Cells.Style.WrapText = true;

                var queryEnterPaymentRecords = SearchEnterpirsePaymentRecodLst(input).OrderByDescending(x => x.CreationTime);
                int r = 1;
                foreach (var item in queryEnterPaymentRecords)
                {
                    worksheet.Cells[r + 1, 1].Value = item.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Cells[r + 1, 2].Value = item.MqAgent.UserName;
                    worksheet.Cells[r + 1, 3].Value = item.MqAgent.NickName;
                    worksheet.Cells[r + 1, 4].Value = EnumHelper.EnumHelper.GetDescription(item.MqAgent.Level);
                    worksheet.Cells[r + 1, 5].Value = item.Amount;
                    worksheet.Cells[r + 1, 6].Value = EnumHelper.EnumHelper.GetDescription(item.State);
                    worksheet.Cells[r + 1, 7].Value = item.WithdrawMoneyType == WithdrawMoneyType.All ? "无" : EnumHelper.EnumHelper.GetDescription(item.WithdrawMoneyType);
                    worksheet.Cells[r + 1, 8].Value = item.OutTradeNo;
                    if (item.State == WithdrawDepositState.Success)
                    {
                        worksheet.Cells[r + 1, 9].Value = item.PaymentNo;
                        worksheet.Cells[r + 1, 10].Value = item.PaymentTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        worksheet.Cells[r + 1, 9].Value = "无";
                        worksheet.Cells[r + 1, 10].Value = "无";
                    }
                    r++;
                }
                package.Save();
                //var data = package.GetAsByteArray();

                return ENTERPRISEPAYMENTRECORDFILENAM;

            }

        }

        public async Task<bool> UpdateWithdrawRecord(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                throw new Exception("参数不能为空");
            }
            var entity = await _entityRepository.GetAll().Include(a => a.MqAgent).FirstOrDefaultAsync(a => a.Id == id);

            if (entity.State == WithdrawDepositState.Auditing || entity.State == WithdrawDepositState.Fail || entity.State == WithdrawDepositState.HangOrder)
            {
                entity.State = WithdrawDepositState.Success;

                entity.WithdrawMoneyType = WithdrawMoneyType.ManualWithDrawType;

                var agent = entity.MqAgent;

                if (agent == null)
                {
                    throw new ArgumentNullException("代理为空");
                }

                agent.LockedBalance -= entity.Amount;

                await _agentRepository.UpdateAsync(agent);

                return true;
            }

            throw new Exception("该订单无法手动提现");
        }
    }
}


