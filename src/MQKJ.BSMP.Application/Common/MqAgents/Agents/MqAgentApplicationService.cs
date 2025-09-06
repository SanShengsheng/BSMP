
using Abp;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MQKJ.BSMP.Authorization.Users;
using MQKJ.BSMP.BigRisks.WeChat;
using MQKJ.BSMP.BigRisks.WeChat.WechatPay;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Authorization;
using MQKJ.BSMP.Common;
using MQKJ.BSMP.Common.Authorization.Users.Authorization;
using MQKJ.BSMP.Common.Companies;
using MQKJ.BSMP.Common.IncomeRecords;
using MQKJ.BSMP.Common.MqAgents;
using MQKJ.BSMP.Common.MqAgents.Agents;
using MQKJ.BSMP.Common.MqAgents.Agents.Dtos;
using MQKJ.BSMP.Common.MqAgents.Agents.Dtos.AutoRunner;
using MQKJ.BSMP.Common.MqAgents.Agents.Models;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.MiniappServices;
using MQKJ.BSMP.MiniappServices.Models;
using MQKJ.BSMP.MqAgents.Dtos;
using MQKJ.BSMP.Orders;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Utils.JpushTool.Dtos;
using MQKJ.BSMP.Utils.Tools;
using MQKJ.BSMP.Utils.WechatPay.Dtos;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MQKJ.BSMP
{
    /// <summary>
    /// MqAgent应用层服务的接口实现方法  
    ///</summary>
    //[AbpAuthorize]
    public class MqAgentAppService :
        BsmpApplicationServiceBase<MqAgent, int, MqAgentEditDto, MqAgentEditDto, GetMqAgentsInput, MqAgentListDto>, IMqAgentAppService
    {

        private const string AGENTINCOMESFILE = "{0}到{1}的代理业绩表.xlsx";

        private const string INCOMESFILE = "{0}到{1}的流水表.xlsx";

        private const string AGENTFAMILY = "{0}到{1}代理家庭统计表.xlsx";

        private readonly IRepository<Player, Guid> _playerRepository;

        private readonly IRepository<Order, Guid> _orderRepository;

        private readonly IRepository<Family, int> _familyRepository;

        private readonly IMiniappService _miniappService;

        private WechatpublicPlatformConfig _wechatConfig;

        private WechatPayConfig _wechatPayConfig;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IRepository<EnterpirsePaymentRecord, Guid> _enterprisePayRecordRepository;

        private readonly IPlayerAppService _playerAppService;

        private JpushMessageConfig _jpushMessageConfig;

        //private IDistributedCache _redisMemoryCache;

        //private RedisHelpers.CustomRedisHelper _redisHelper;

        private readonly UserManager _userManager;

        private readonly IRepository<AgentInviteCode> _agentInviteCodeRepository;
        private readonly IAutoRunnerJob _jobService;

        private const string CHECK_NAME = "FORCE_CHECK";

        private const string NO_CHECK = "NO_CHECK";

        //证书路径
        public const string WEIXIN_API_CERTIFICATE_ROOT_PATH = "api_certificate";
        public const string WEIXIN_API_CERTIFICATE_NAME = "apiclient_cert.p12";
        //private readonly IDistributedCache _redisCache;
        private readonly IRepository<AutoRunnerRecord, Guid> _configRecordRepository;

        private readonly IRepository<IncomeRecord, Guid> _incomeRecordRepository;
        private readonly IRepository<Company> _companyRepository;


        public MqAgentAppService(IRepository<MqAgent, int> repository
            , IRepository<Player, Guid> playerRepository
            , IRepository<Order, Guid> orderRepository
            , IRepository<Family, int> familyRepository
            , IOptions<WechatpublicPlatformConfig> option
            , IHostingEnvironment hostingEnvironment
            , IRepository<EnterpirsePaymentRecord, Guid> enterprisePayRecordRepository
            , IOptions<WechatPayConfig> wechatPayConfigOption
            , IOptions<JpushMessageConfig> jpushMessageOption
            , IMiniappService miniappService
            , IPlayerAppService playerAppService
            //, IDistributedCache redisMemoryCache
            , IRepository<AgentInviteCode> agentInviteCodeRepository
            , IRepository<IncomeRecord, Guid> incomeRecordRepository
            , IRepository<AutoRunnerRecord, Guid> configRecordRepository,
              IAutoRunnerJob jobService
            , UserManager userManager
            , IRepository<Company> companyRepository)
            : base(repository)
        {
            _playerRepository = playerRepository;

            _orderRepository = orderRepository;

            _familyRepository = familyRepository;

            _wechatConfig = option.Value;

            _wechatPayConfig = wechatPayConfigOption.Value;

            _jpushMessageConfig = jpushMessageOption.Value;

            _hostingEnvironment = hostingEnvironment;

            _miniappService = miniappService;

            _playerAppService = playerAppService;

            _enterprisePayRecordRepository = enterprisePayRecordRepository;

            _agentInviteCodeRepository = agentInviteCodeRepository;

            //_redisHelper = new RedisHelpers.CustomRedisHelper(redisMemoryCache);
            //_redisCache = redisMemoryCache;

            _incomeRecordRepository = incomeRecordRepository;
            _configRecordRepository = configRecordRepository;
            _jobService = jobService;
            _userManager = userManager;

            _companyRepository = companyRepository;
        }

        public async Task UpdateAgentState(UpdateAgenetStateInput input)
        {
            var entity = await _repository.GetAll().Include(a => a.UpperLevelMqAgent)
                .FirstOrDefaultAsync(a => a.Id == input.Id);


            if (entity.State == AgentState.UnAuditing || entity.State == AgentState.Auditing)
            {
                //判断收益比例是否合法  --新增加
                if (entity.Level == AgentLevel.SecondLevel) //二级
                {
                    var upAgent = entity.UpperLevelMqAgent;

                    if (upAgent.AgentWithdrawalRatio < entity.AgentWithdrawalRatio) //上级的小于二级的 不合法
                    {
                        throw new Exception("该代理的收益比例超出上级，审核失败");
                    }
                }
                else //一级
                {
                    if (entity.AgentWithdrawalRatio > 100 || entity.AgentWithdrawalRatio < 0) //一级代理总收益
                    {
                        throw new Exception("该代理的收益比例不合法，审核失败");
                    }

                    if (entity.PromoterWithdrawalRatio > 100 || entity.PromoterWithdrawalRatio < 0) //一级推广的推广总收益
                    {
                        throw new Exception("该代理的收益比例不合法，审核失败");
                    }

                    //获取所有二级的收益比例
                    var secondAgentRatios = await _repository.GetAll()
                        .Where(a => a.UpperLevelMqAgentId == input.Id)
                        .AsNoTracking()
                        .Select(a => new { a.AgentWithdrawalRatio, a.NickName })
                        .ToListAsync();
                    for (int i = 0; i < secondAgentRatios.Count; i++)
                    {
                        if (entity.AgentWithdrawalRatio < secondAgentRatios[i].AgentWithdrawalRatio)
                        {
                            throw new Exception($"该代理下的二级代理{secondAgentRatios[i].NickName}的收益比例不合法，审核失败");
                        }
                    }
                }

                entity.State = AgentState.Audited;
            }
            else if (entity.State == AgentState.Audited)
            {
                entity.State = AgentState.Suspend;
            }
            else if (entity.State == AgentState.Suspend)
            {
                if (entity.IdCardNumber != null && entity.UserName != null)
                {
                    entity.State = AgentState.Audited;
                }
                else
                {
                    entity.State = AgentState.UnAuditing;
                }
            }

            await _repository.UpdateAsync(entity);
        }

        public async Task<bool> UpdateAgentInfo(UpdateAgentInfoInput input)
        {
            var agent = await _repository.GetAsync(input.Id);

            agent.UserName = input.UserName;

            agent.IdCardNumber = input.IdCardNumber;

            agent.State = AgentState.Auditing;
            agent.AliPayCardNO = input.AliPayAccount;
            agent.CardNo = input.CardNo;
            await _repository.UpdateAsync(agent);

            return true;
        }

        internal override IQueryable<MqAgent> GetQuery(GetMqAgentsInput model)
        {
            int _agentWithdrawalRatio = -1, _promoterWithdrawalRatio = -1;
            return _repository.GetAllIncluding(a => a.UpperLevelMqAgent, p => p.Player, c => c.Company)
                .Where(a => a.Level != AgentLevel.UnKnow)
                .WhereIf(model.AgentState.HasValue && model.AgentState != 0, x => x.State == model.AgentState)
                .WhereIf(model.StartTime.HasValue, x => x.CreationTime >= model.StartTime.Value)
                .WhereIf(model.EndTime.HasValue, x => x.CreationTime < model.EndTime.Value)
                .WhereIf(model.AgentLevel.HasValue && model.AgentLevel != 0, x => x.Level == model.AgentLevel)
                .WhereIf(!string.IsNullOrEmpty(model.UserName), x => x.UserName.Contains(model.UserName) || x.NickName.Contains(model.UserName))
                .WhereIf(!string.IsNullOrEmpty(model.SourceName), a => a.Source.Contains(model.SourceName))
                .WhereIf(!string.IsNullOrWhiteSpace(model.UpperAgentNickName), w => w.UpperLevelMqAgent.NickName.Contains(model.UpperAgentNickName))
                .WhereIf(!string.IsNullOrWhiteSpace(model.Phone), w => w.PhoneNumber.Equals(model.Phone))
                .WhereIf(!string.IsNullOrWhiteSpace(model.AgentWithdrawalRatio) && int.TryParse(model.AgentWithdrawalRatio, out _agentWithdrawalRatio), w => w.AgentWithdrawalRatio == _agentWithdrawalRatio)
                .WhereIf(!string.IsNullOrWhiteSpace(model.PromoterWithdrawalRatio) && int.TryParse(model.PromoterWithdrawalRatio, out _promoterWithdrawalRatio), w => w.PromoterWithdrawalRatio == _promoterWithdrawalRatio)
                .WhereIf(!string.IsNullOrEmpty(model.Company),c => c.Company.Name.Contains(model.Company));
        }

        public async Task<CreateAgentOutput> CreateOrGetAgent(CreateAgentInput input)
        {
            var output = new CreateAgentOutput();

            //if (!input.AgentId.HasValue)
            //{
            //    throw new ArgumentNullException($"参数agentId:{input.AgentId}不能为空");
            //}


            var agentInviteCode = await _agentInviteCodeRepository.GetAll().Include(a => a.MqAgent).FirstOrDefaultAsync(c => c.Code == input.InviteCode);

            if (agentInviteCode == null)
            {
                throw new UserFriendlyException("您的邀请码无效，邀请码需要申请");
            }

            if (agentInviteCode.MqAgent != null)
            {
                if (agentInviteCode.MqAgent.Level == AgentLevel.FirstAgentLevel || agentInviteCode.MqAgent.Level == AgentLevel.FirstPromoterLevel)
                {
                    throw new Exception("该邀请码已使用");
                }
            }

            //var user = await _userManager.GetUserByIdAsync(input.UserId.Value);
            //if (user == null)
            //{
            //    throw new Exception("商户");
            //}
            var isHasAgent = _repository.GetAll().FirstOrDefaultAsync(a => a.PhoneNumber == input.PhoneNumber).Result == null ? false : true;
            //var isHasAgent = _repository.GetAll().FirstOrDefaultAsync(a => a.PhoneNumber == input.PhoneNumber && a.OpenId == user.UserName).Result == null ? false : true;

            if (isHasAgent)
            {
                throw new Exception("该手机号已注册!");
            }

            try
            {
                var msgId = RedisHelper.Get(input.PhoneNumber);

                //验证码验证
                var isValid = await ShortMessageCode.CodeIsValide(new JpushMessageInput()
                {
                    PhoneNumber = input.PhoneNumber,
                    AppKey = _jpushMessageConfig.AppKey,
                    MasterSecret = _jpushMessageConfig.MasterSecret,
                    SendMessageUrl = _jpushMessageConfig.SendMessageUrl,
                    MessageId = msgId.ToString(),
                    Code = input.Code
                });

                if (!isValid)
                {
                    throw new Exception("验证码错误");
                }
            }
            catch (Exception)
            {
                throw new Exception("验证码错误");
            }
            var user = await _userManager.GetUserByIdAsync(input.UserId.Value);
            var agent = await _repository.FirstOrDefaultAsync(a => a.OpenId == user.UserName);
            //var agent = _repository.Get(input.AgentId.Value);
            //var agent = await _repository.FirstOrDefaultAsync(a => a.OpenId == user.UserName);

            if (agentInviteCode.MqAgentCategory != MqAgentCategory.SecodeCategory) //一级
            {
                var codes = await _agentInviteCodeRepository.GetAll().AsNoTracking().Select(c => c.Code).ToListAsync();

                //生成专属自己的二级邀请码
                var code = Utils.Tools.RandomHelper.GetRandomCode(codes);
                await _agentInviteCodeRepository.InsertAsync(new AgentInviteCode()
                {
                    State = InviteCodeState.UnKnow,
                    Code = code,
                    MqAgentCategory = MqAgentCategory.SecodeCategory
                });

                if (agentInviteCode.MqAgentCategory == MqAgentCategory.SpreadCategory)
                {
                    agent.Level = AgentLevel.FirstPromoterLevel;
                    //一级推广自己的收益比例在后台设置，这里不需要设置
                    agent.AgentWithdrawalRatio = (int)AgentWithdrawMoneyRatio.FirstAgentRatio;
                }
                else
                {
                    agent.Level = AgentLevel.FirstAgentLevel;
                    agent.AgentWithdrawalRatio = (int)AgentWithdrawMoneyRatio.FirstAgentRatio;
                }
                agent.ParentInviteCode = input.InviteCode;
                agent.InviteCode = code;
            }
            else //二级
            {
                agent.ParentInviteCode = input.InviteCode;
                agent.Level = AgentLevel.SecondLevel;

                var upperLevelMqAgent = await _repository.FirstOrDefaultAsync(a => a.InviteCode == input.InviteCode);

                if (upperLevelMqAgent == null)
                {
                    throw new Exception("上级代理没找到");
                }
                agent.AgentWithdrawalRatio = (int)AgentWithdrawMoneyRatio.SecondAgentRatio;

                agent.UpperLevelMqAgentId = upperLevelMqAgent.Id;
            }

            //更新邀请码状态
            agentInviteCode.MqAgentId = agent.Id;
            agentInviteCode.State = InviteCodeState.UsedState;
            await _agentInviteCodeRepository.UpdateAsync(agentInviteCode);

            agent.PhoneNumber = input.PhoneNumber;
            agent.State = AgentState.UnAuditing;
            //if (agent.PlayerId != Guid.Empty)
            //{
            //    agent.PlayerId = input.PlayerId;
            //}
            //agent.IdCardNumber = input.IdCardNumber;
            //agent.UserName = input.UserName;

            var playerId = await GetGamePalyer(agent.UnionId, agent.OpenId, agent);
            //var playerId = await GetGamePalyer(user.Surname, user.UserName);
            if (playerId != Guid.Empty)
            {
                agent.PlayerId = playerId;
            }

            await _repository.UpdateAsync(agent);

            return output;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetVerificationCode(GetVerificationCodeInput input)
        {
            //调用接口发送验证码
            var msgId = await ShortMessageCode.SendMessageCode(new JpushMessageInput()
            {
                PhoneNumber = input.PhoneNumber,
                SendMessageUrl = _jpushMessageConfig.SendMessageUrl,
                AppKey = _jpushMessageConfig.AppKey,
                MasterSecret = _jpushMessageConfig.MasterSecret,
                TempId = _jpushMessageConfig.TempId
            });

            if (msgId != null)
            {
                return RedisHelper.Set(input.PhoneNumber, msgId, 3 * 60); //三分钟
            }

            return false;
        }

        public async Task<GetAllRunWaterRecordsListDto> GetAllRunWaterRecords(GetAllRunWaterRecordInput input)
        {
            input.IsAll = true;
            var incomeRecords = SearchIncomeRecords(input);
            if (incomeRecords == null)
            {
                return new GetAllRunWaterRecordsListDto()
                {
                    WaterRecords = new PagedResultDto<GetAllRunWaterRecordsListDtoWaterRecordModl>(0, new List<GetAllRunWaterRecordsListDtoWaterRecordModl>())
            };
            }
            var user = await _userManager.GetUserByIdAsync(AbpSession.UserId.Value);
            var company = new Company();
            if (user.CompanyId.HasValue)
            {
                company = await _companyRepository.GetAsync((int)user.CompanyId);
            }
            var totalIncome = incomeRecords.Sum(s => s.Income) * company?.RoyaltyRate * 0.01;
            var totalPayment = incomeRecords.Sum(p => p.Income);
            var totalAnchorIncome = incomeRecords.Sum(s => s.RealIncome);
            var count = await incomeRecords.CountAsync();
            var entityList = await incomeRecords
                      .OrderBy(input.Sorting).AsNoTracking()
                      .PageBy(input)
                      .ToListAsync();
            var entityListDtos = entityList.MapTo<List<GetAllRunWaterRecordsListDtoWaterRecordModl>>();

            var waterRecods = new PagedResultDto<GetAllRunWaterRecordsListDtoWaterRecordModl>(count, entityListDtos);
            return new GetAllRunWaterRecordsListDto()
            {
                BrokerTotalIncome = totalIncome,
                WaterRecords = waterRecods,
                RoyaltyRate = company?.RoyaltyRate,
                TotalPayment = totalPayment,
                TotalAnchorIncome = totalAnchorIncome
            };
        }


        private IQueryable<IncomeRecord> SearchIncomeRecords(GetAllRunWaterRecordInput input)
        {
            var tenantId = AbpSession.TenantId ?? 295;
            var user = _userManager.GetUserByIdAsync(AbpSession.UserId.Value).Result;
            var isGranted = PermissionChecker.IsGranted(ChinesePermissions.GetAllRunWaterRecords);
            if (!isGranted && !user.CompanyId.HasValue)
            {
                return null;
            }
            var incomeRecords = _incomeRecordRepository
            .GetAll()
            .Include(a => a.MqAgent).ThenInclude(a => a.UpperLevelMqAgent)
            .Include(a => a.MqAgent).ThenInclude(c => c.Company)
            .Include(o => o.Order).ThenInclude(o => o.Player)
            .Include(o => o.Order).ThenInclude(o => o.Family)
         .Include(a => a.SecondAgent)
         .Where(a => a.MqAgent.IsDeleted == false && a.Order.IsDeleted == false && a.Order.Family.IsDeleted == false && a.MqAgent.TenantId == tenantId&&a.IsDeleted==false)
         .WhereIf(input.Amount > 0, a => a.Income == input.Amount)
         .WhereIf(!isGranted && user.CompanyId.HasValue, a => a.MqAgent.CompanyId == user.CompanyId)
         .WhereIf(input.StartTime != null && input.EndTime != null, a => a.CreationTime >= input.StartTime && a.CreationTime <= input.EndTime)
         .WhereIf(!string.IsNullOrEmpty(input.OrderNo), a => a.Order.OrderNumber == input.OrderNo)
         .WhereIf(!string.IsNullOrEmpty(input.RechargerName), a => a.Order.Player.NickName.Contains(input.RechargerName))
         .WhereIf(!string.IsNullOrEmpty(input.BusinesserName), a => a.MqAgent.NickName.Contains(input.BusinesserName))
         .WhereIf(!string.IsNullOrEmpty(input.ProfiterName), a => a.MqAgent.NickName.Contains(input.ProfiterName))//  || a.MqAgent.UpperLevelMqAgent.NickName.Contains(input.ProfiterName)
         .WhereIf(input.IsAll, a => a.Income > 0)
         .WhereIf(input.RunWaterType != IncomeTypeEnum.NIL, s => s.IncomeTypeEnum == input.RunWaterType)
         .WhereIf(!string.IsNullOrEmpty(input.Company), a => a.MqAgent.Company.Name.Contains(input.Company))
         .WhereIf(PermissionChecker.IsGranted(UserPermissions.QuerySubsidy) && input.RunWaterType == IncomeTypeEnum.Subsidy, s => s.IncomeTypeEnum == input.RunWaterType)//当有权限才允许检索补贴流水
         .AsNoTracking();

            var list = new List<IncomeRecord>();

            if (!string.IsNullOrEmpty(input.ProfiterName))
            {
                foreach (var item in incomeRecords)
                {
                    if (item.MqAgent.Level == AgentLevel.SecondLevel)
                    {
                        var temp = item.MqAgent.UpperLevelMqAgent.NickName.Contains(input.ProfiterName);
                        if (!temp)
                        {
                            list.Add(item);
                        }
                    }
                    else
                    {
                        var temp = item.MqAgent.NickName.Contains(input.ProfiterName);
                        if (!temp)
                        {
                            list.Add(item);
                        }
                    }
                }
            }

            incomeRecords.Except(list);

            return incomeRecords;
        }

        public string ExportIncomeReocrdToExcel(GetAllRunWaterRecordInput input)
        {
            var incomeRecords = SearchIncomeRecords(input).OrderByDescending(x => x.CreationTime);
            var fileName = string.Format(INCOMESFILE, input.StartTime.Value.ToString("yyyyMMdd"), input.EndTime.Value.ToString("yyyyMMdd"));
            FileInfo file = new FileInfo(Path.Combine(_hostingEnvironment.WebRootPath, fileName));
            file.Delete();
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("提现记录表");//创建sheet
                worksheet.Cells[1, 1].Value = "创建日期";
                worksheet.Cells[1, 2].Value = "充值用户";
                worksheet.Cells[1, 3].Value = "充值金额";
                worksheet.Cells[1, 4].Value = "收益";
                worksheet.Cells[1, 5].Value = "收益比例";
                worksheet.Cells[1, 6].Value = "业务人";
                worksheet.Cells[1, 7].Value = "家庭Id";
                worksheet.Cells[1, 8].Value = "商户订单号";
                worksheet.Cells[1, 9].Value = "收益人";
                //worksheet.Cells.Style.ShrinkToFit = true;
                //worksheet.Cells.AutoFitColumns();

                //样式
                worksheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                int r = 1;
                foreach (var item in incomeRecords)
                {
                    worksheet.Cells[r + 1, 1].Value = item.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                    if (item.Order == null || item.Order.Player == null)
                    {
                        worksheet.Cells[r + 1, 2].Value = "无";
                    }
                    else
                    {
                        worksheet.Cells[r + 1, 2].Value = item.Order.Player.NickName;
                    }
                    worksheet.Cells[r + 1, 3].Value = item.Income;
                    worksheet.Cells[r + 1, 4].Value = item.RealIncome;
                    worksheet.Cells[r + 1, 5].Value = item.CurrentEarningRatio;
                    if (item.MqAgent == null)
                    {
                        worksheet.Cells[r + 1, 6].Value = "无";
                    }
                    else
                    {
                        worksheet.Cells[r + 1, 6].Value = item.MqAgent.NickName;
                    }
                    if (item.Order == null)
                    {
                        worksheet.Cells[r + 1, 7].Value = "无";
                        worksheet.Cells[r + 1, 8].Value = "无";
                    }
                    else
                    {
                        worksheet.Cells[r + 1, 7].Value = item.Order.FamilyId;
                        worksheet.Cells[r + 1, 8].Value = item.Order.OrderNumber;
                    }
                    worksheet.Cells[r + 1, 9].Value = item.SecondAgent == null ? "无" : item.SecondAgent.NickName;
                    r++;
                }

                package.Save();

                return fileName;
            }

        }

        public async Task<PagedResultDto<GetMoneyDetailedListDto>> GetAgentOrPromoterRuningWaterRecord(GetMoneyDetailedInput input)
        {
            if (!input.AgentId.HasValue)
            {
                throw new Exception("参数不能为空");
            }
            var agent = _repository.Get(input.AgentId.Value);

            if (agent == null)
            {
                throw new Exception("该代理不存在");
            }

            // var secondAgentPlayerIds = _repository.GetAll().Where(a => a.ParentInviteCode == agent.InviteCode).Select(a => a.Id);

            var incomeRecords = _incomeRecordRepository.GetAll()
                .Include(a => a.SecondAgent)
                .Where(r => r.MqAgentId == input.AgentId.Value && r.RunWaterRecordType == RunWaterRecordType.Second);

            #region 原来的代码
            //if (agent.Level == AgentLevel.FirstAgentLevel || agent.Level == AgentLevel.FirstPromoterLevel)
            //{
            //    foreach (var item in incomeRecords)
            //    {
            //        item.CurrentEarningRatio = agent.AgentWithdrawalRatio;

            //        item.RealIncome = Math.Round(item.Income * (agent.AgentWithdrawalRatio / 100.0),2);
            //    }
            //}
            #endregion

            return await GetMoneyDetailed(incomeRecords, input);
        }

        /// <summary>
        /// 获取自己的明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetMoneyDetailedListDto>> GetOwnRuningWaterRecord(GetMoneyDetailedInput input)
        {
            //var agent = _repository.Get(input.AgentId.Value);//原来的代码

            var incomeRecords = _incomeRecordRepository.GetAll().Include(a => a.MqAgent).Where(r => r.MqAgentId == input.AgentId.Value && r.RunWaterRecordType == RunWaterRecordType.First);

            #region 原来的代码
            //if (agent.Level == AgentLevel.SecondLevel)
            //{
            //    foreach (var item in incomeRecords)
            //    {
            //        item.CurrentEarningRatio = agent.AgentWithdrawalRatio;
            //        item.RealIncome = Math.Round(item.Income * (agent.AgentWithdrawalRatio / 100.0), 2);
            //    }
            //}
            #endregion

            return await GetMoneyDetailed(incomeRecords, input);
        }

        private async Task<PagedResultDto<GetMoneyDetailedListDto>> GetMoneyDetailed(IQueryable<IncomeRecord> incomeRecords, GetMoneyDetailedInput input)
        {
            var count = incomeRecords.Count();

            var entityList = await incomeRecords
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            var entityListDtos = entityList.MapTo<List<GetMoneyDetailedListDto>>();

            return new PagedResultDto<GetMoneyDetailedListDto>(count, entityListDtos);

        }

        public async Task<GetAgentInfoOutput> LoginAgentSystem(GetOpenIdInput input)
        {
            var user = await _userManager.GetUserByIdAsync(input.UserId);
            var agent = await _repository.FirstOrDefaultAsync(a => a.OpenId == user.UserName && a.TenantId == AbpSession.TenantId);

            if (agent == null)
            {
                agent = new MqAgent();
                var teant = await GetCurrentTenantAsync();
                agent.TenantId = teant.Id;
                agent.OpenId = user.UserName;//UserName为openid
                agent.UnionId = user.Surname;//Surname为unionID

                agent.HeadUrl = user.PasswordResetCode;//头像

                agent.State = AgentState.UnAuditing;

                agent.NickName = user.Name;
                var playerId = await GetGamePalyer(user.Surname, user.UserName, agent);

                if (playerId != Guid.Empty)
                {
                    agent.PlayerId = playerId;
                }
                await _repository.InsertAsync(agent);

                return null;
            }
            else
            {
                if (agent.PhoneNumber == null)
                {
                    return null;
                }

                var output = new GetAgentInfoOutput();

                output = agent.MapTo<GetAgentInfoOutput>();

                var playerId = await GetGamePalyer(user.Surname, user.UserName, agent);
                if (playerId != Guid.Empty && !agent.PlayerId.HasValue)
                {
                    agent.PlayerId = playerId;
                    await _repository.UpdateAsync(agent);
                }
                output.PlayerId = agent.PlayerId;

                var UnAuditeRecord = await _enterprisePayRecordRepository
                    .FirstOrDefaultAsync(a => a.AgentId == agent.Id && (a.State == WithdrawDepositState.Auditing || a.State == WithdrawDepositState.Fail || a.State == WithdrawDepositState.HangOrder));

                output.IsCanAudited = UnAuditeRecord == null ? true : false;

                output.CurrentUnHandAmount = UnAuditeRecord == null ? 0 : UnAuditeRecord.Amount;

                if (agent.Level == AgentLevel.FirstAgentLevel || agent.Level == AgentLevel.FirstPromoterLevel)
                {
                    output.InviteCode = agent.InviteCode;
                }

                return output;
            }
        }

        public async Task<GetPubOpenIdOutput> GetPubOpenId(GetOpenIdInput input)
        {
            var output = new GetPubOpenIdOutput();

            var getOpenIdResponse = _miniappService.GetOfficeAccountOpenId(new GetOpenIdInput
            {
                AppId = _wechatConfig.AppId,
                AppSecret = _wechatConfig.Secret,
                Code = input.Code
            });

            //var agent = _repository.GetAll().FirstOrDefaultAsync(a => a.ope)

            if (getOpenIdResponse.OpenId == null)
            {
                throw new UserFriendlyException("获取openid异常");
            }
            else
            {
                var agent = await _repository.FirstOrDefaultAsync(p => p.OpenId == getOpenIdResponse.OpenId);

                if (agent == null)
                {

                    //获取unionID
                    var getUnionIdResponse = _miniappService.GetOfficeAccountUnionId(new GetOfficeAccountUnionIdInput()
                    {
                        AccessToken = getOpenIdResponse.AccessToken,
                        OpenId = getOpenIdResponse.OpenId
                    });

                    if (getUnionIdResponse == null)
                    {
                        throw new UserFriendlyException("获取UnionId异常");
                    }
                    else
                    {
                        agent = new MqAgent();
                        var teant = await GetCurrentTenantAsync();
                        agent.TenantId = teant.Id;
                        agent.OpenId = getOpenIdResponse.OpenId;
                        agent.UnionId = getUnionIdResponse.UnionId;

                        agent.HeadUrl = getUnionIdResponse.HeadUrl;

                        agent.State = AgentState.UnAuditing;

                        agent.NickName = getUnionIdResponse.NickName;

                        output.AgentId = await _repository.InsertAndGetIdAsync(agent);
                    }
                    output.PlayerId = await GetGamePalyer(agent.UnionId, agent.OpenId, agent);
                }
                else
                {
                    if (!agent.PlayerId.HasValue || agent.PlayerId == null)
                    {
                        output.PlayerId = await GetGamePalyer(agent.UnionId, agent.OpenId, agent);

                        if (output.PlayerId != Guid.Empty)
                        {
                            agent.PlayerId = output.PlayerId;
                        }
                        await _repository.UpdateAsync(agent);
                    }
                    else
                    {
                        output.PlayerId = agent.PlayerId.Value;
                    }

                    output.AgentId = agent.Id;

                    if (agent.PhoneNumber != null)
                    {
                        output.IsRegiste = true;
                    }
                }

                if (agent.Level == AgentLevel.FirstAgentLevel || agent.Level == AgentLevel.FirstPromoterLevel)
                {
                    output.InviteCode = agent.InviteCode;
                }

                output.AgentLevel = agent.Level;

                output.AgentState = agent.State;
            }

            return output;
        }

        private async Task<Guid> GetGamePalyer(string unionId, string openId, MqAgent agent)
        {
            var playerId = Guid.Empty;

            var gamePlayer = await _playerRepository.FirstOrDefaultAsync(p => p.UnionId == unionId);
            if (gamePlayer != null)
            {
                playerId = gamePlayer.Id;

                if (!gamePlayer.IsAgenter)
                {
                    gamePlayer.IsAgenter = true;

                    await _playerRepository.UpdateAsync(gamePlayer);
                }
                if (gamePlayer.Id != agent.PlayerId)
                {
                    agent.PlayerId = gamePlayer.Id;
                }
                agent.NickName = gamePlayer.NickName;
                agent.HeadUrl = gamePlayer.HeadUrl;
            }
            return playerId;
        }

        private async Task<bool> TestCode(string code, string msgId)
        {
            var result = await ShortMessageCode.CodeIsValide(new JpushMessageInput()
            {
                AppKey = _jpushMessageConfig.AppKey,
                MasterSecret = _jpushMessageConfig.MasterSecret,
                SendMessageUrl = _jpushMessageConfig.SendMessageUrl,
                Code = code,
                MessageId = msgId
            });

            return true;
        }

        public async Task<PagedResultDto<GetSecondAgentListDto>> GetSecondAgent(GetSecondAgentInput input)
        {
            var agent = await _repository.GetAsync(input.AgentId);

            var mqagents = _repository.GetAll().Where(a => a.UpperLevelMqAgentId == input.AgentId);

            var count = mqagents.Count();

            var entityList = await mqagents
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            var entityListDtos = entityList.MapTo<List<GetSecondAgentListDto>>();

            foreach (var item in entityListDtos)
            {
                item.TotalIncome = GetOwnIncome(item.Id);
                item.CreationTime = Convert.ToDateTime(item.CreationTime).ToString("yyyy-MM-dd HH:mm");
            }

            return new PagedResultDto<GetSecondAgentListDto>(count, entityListDtos);
        }

        //private double GetSecondIncome(MqAgent agent)
        //{
        //    var secondAgentIds = _repository.GetAll().Where(c => c.ParentInviteCode == agent.InviteCode).AsNoTracking().Select(a => a.Id);

        //    var totalRealIncome = _incomeRecordRepository.GetAll().Where(i => secondAgentIds.Contains(i.MqAgentId)).Sum(i => i.RealIncome);

        //    return totalRealIncome;
        //}

        private double GetOwnIncome(int agentId)
        {
            var totaIncome = _incomeRecordRepository.GetAll().Where(i => i.MqAgentId == agentId).AsNoTracking().Sum(i => i.Income);

            return totaIncome;
        }

        public async Task<GetAgentInfoOutput> GetAgentInfo(GetAgentInfoInput input)
        {
            //if (!input.AgentId.HasValue)
            //{
            //    throw new ArgumentNullException("参数为空");
            //}
            var agent = await _repository.GetAsync(input.AgentId);

            if (agent == null)
            {
                throw new Exception("该用户不存在");
            }

            var output = agent.MapTo<GetAgentInfoOutput>();

            var UnAuditeRecord = await _enterprisePayRecordRepository
                .FirstOrDefaultAsync(a => a.AgentId == input.AgentId && (a.State == WithdrawDepositState.Auditing || a.State == WithdrawDepositState.Fail));

            output.IsCanAudited = UnAuditeRecord == null ? true : false;

            output.CurrentUnHandAmount = UnAuditeRecord == null ? 0 : UnAuditeRecord.Amount;

            //output.TotalIncome = GetAllOwnProfits(agent);

            return output;
        }

        public async Task<ModifyPasswordOutput> ModifyPassword(MqAgentsModifyPasswordInput input)
        {
            var output = new ModifyPasswordOutput();

            var agent = _repository.Get(input.AgentId);

            if (agent == null)
            {
                output.ErrMessage = $"不存在代理Id为{input.AgentId}的代理";
            }
            else
            {
                var oldPassword = EncryptHelper.EncryptWithMD5(input.OldPassword);

                if (oldPassword == agent.Password)
                {

                    output.ErrMessage = "原密码输入错误";
                }
                else
                {
                    var md5Password = EncryptHelper.EncryptWithMD5(input.Password);

                    agent.Password = md5Password;

                    await _repository.UpdateAsync(agent);

                    output.IsSuccess = true;
                }
            }

            return output;
        }

        public Task SendCode(SendCodeRequest request)
        {
            return RedisHelper.SetAsync(request.MobileNo, "123456", 15 * 60);
        }

        public async Task Register(RegisterRequest request)
        {
            if (!await VerfiyCode(request))
            {
                throw new AbpException("验证码不正确");
            }

            var player = GetPlayer(request);
            var find = await _repository.FirstOrDefaultAsync(q => q.PlayerId == player.Id);
            if (find != null)
            {
                throw new AbpException("用户已经注册");
            }

            request.PlayerId = player.Id;
            request.State = AgentState.UnAuditing;
            request.TenantId = player.TenantId;

            await Create(request);
        }

        private Player GetPlayer(RegisterRequest request)
        {
            return _playerRepository.Get(new Guid("8C458E71-4CC3-454A-C0BF-08D67170CFEB"));
        }

        private async Task<bool> VerfiyCode(RegisterRequest request)
        {
            var code = await RedisHelper.GetAsync(request.PhoneNumber);
            if (code.IsNullOrEmpty())
                return false;

            return code == request.Code;
        }

        public async Task<MqAgentListDto> GetAgent(GetMqAgentsInput request)
        {
            var find = await _repository.FirstOrDefaultAsync(a => a.PlayerId == new Guid("8C458E71-4CC3-454A-C0BF-08D67170CFEB"));
            if (find != null)
            {
                return find.MapTo<MqAgentListDto>();
            }

            return null;
        }

        public async Task StartAutoRun(StartAutoRunRequest request)
        {
            await _jobService.CheckCanAutoRunner(new CheckCanAutoRunnerRequest
            {
                FamilyId = request.FamilyId,
                PlayerId = request.PlayerId
            });

            var family = await _familyRepository.GetAsync(request.FamilyId);

            if (family != null)
            {
                family.AddOnStatus = AddOnStatus.Running;
                family.IsShow = true;

                await _familyRepository.UpdateAsync(family);
                await _configRecordRepository.InsertAsync(new AutoRunnerRecord
                {
                    ActionType = ActionType.StartAuto,
                    CreationTime = DateTime.UtcNow,
                    Description = $"我在{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}启动了机器人",
                    FamilyId = request.FamilyId,
                    PlayerId = request.PlayerId,
                    BabyId = family.Baby?.Id
                });
                BackgroundJob.Schedule<IAutoRunnerJob>(job => job.StartRunnerJob(new StartRunnerRequest
                {
                    FamilyId = request.FamilyId,
                    PlayerId = request.PlayerId
                }), TimeSpan.FromSeconds(5));
            }

        }

        public async Task ApllyWithdrawMoney(ApllyWithdrawMoneyInput input)
        {
            var agent = await _repository.GetAsync(input.Id);

            if (agent == null)
            {
                throw new Exception("不存在该代理");
            }

            agent.WithdrawMoneyState = WithdrawMoneyState.ApplyPassed;

            await _repository.UpdateAsync(agent);
        }

        public async Task UpdateAgentRatio(UpdateAgentRatioInput input)
        {
            var agent = await _repository.GetAll()
                .Include(a => a.UpperLevelMqAgent)
                .FirstOrDefaultAsync(a => a.Id == input.AgentId);

            if (agent == null)
            {
                throw new AbpException("该代理不存在");
            }

            //if (agent.Level == AgentLevel.FirstPromoterLevel)
            //{
            //    throw new Exception("身份不合法");
            //}

            if (input.Ratio > 100)
            {
                throw new AbpException("比例超过限制，请重新设置");
            }

            if (input.Ratio < 0)
            {
                throw new AbpException("设置的比例不合法，请重新设置");
            }

            if (agent.Level == AgentLevel.SecondLevel)
            {
                var upAgent = agent.UpperLevelMqAgent;

                if (upAgent == null)
                {
                    throw new AbpException("上级不存在，请核对数据重试");
                }
                if (input.Ratio > upAgent.AgentWithdrawalRatio)
                {
                    throw new AbpException("下级的比例不能超过Ta上级的，请重新设置");
                }
            }
            else
            {
                //获取他下面的所有代理的比例
                var secondRatios = _repository.GetAll().Where(a => a.UpperLevelMqAgentId == input.AgentId).Select(a => new { a.AgentWithdrawalRatio, a.NickName });

                foreach (var item in secondRatios)
                {
                    if (input.Ratio < item.AgentWithdrawalRatio)
                    {
                        throw new AbpException($"您的比例不能低于您的二级{item.NickName}的比例");
                    }
                }
            }

            agent.AgentWithdrawalRatio = input.Ratio;

            await _repository.UpdateAsync(agent);

            //设置他下面的二级代理
            //var secondAgents = _repository.GetAll().Where(a => a.UpperLevelMqAgentId == agent.Id);

            //foreach (var item in secondAgents)
            //{
            //    item.AgentWithdrawalRatio = 50 - input.Ratio;

            //    await _repository.UpdateAsync(item);
            //}
        }
        [Audited]
        public async Task UpdatePromoterRatio(UpdateAgentRatioInput input)
        {
            var agent = await _repository.GetAsync(input.AgentId);

            if (agent == null)
            {
                throw new AbpException("该代理不存在");
            }
            if (agent.Level != AgentLevel.FirstPromoterLevel)
            {
                throw new AbpException("该代理身份不合法");
            }
            if (input.Ratio > 100)
            {
                throw new AbpException("比例超过限制，请重新设置");
            }
            if (input.Ratio < 0)
            {
                throw new AbpException("设置的比例不合法，请重新设置");
            }
            agent.PromoterWithdrawalRatio = input.Ratio;

            await _repository.UpdateAsync(agent);
        }

        public async Task<GetAgentWithOpenIdOutput> GetAgentWithOpenId(GetAgentWithOpenIdInput input)
        {
            var output = new GetAgentWithOpenIdOutput();

            if (string.IsNullOrEmpty(input.OpenId))
            {
                throw new Exception("参数不能为空");
            }

            var agent = await _repository.GetAll().FirstOrDefaultAsync(a => a.OpenId == input.OpenId);

            if (agent == null)
            {
                //throw new Exception("用户不存在");
                return null;
            }

            output.AgentId = agent.Id;

            if (agent.PlayerId.HasValue)
            {
                output.PlayerId = agent.PlayerId.Value;
            }

            return output;
        }

        public async Task UpAgentSource(UpAgentSourceInput input)
        {
            var agent = await _repository.GetAsync(input.AgentId);

            if (agent == null)
            {
                throw new AbpException("出错了，请稍后再试!");
            }
            agent.Source = input.SourceName;

            await _repository.UpdateAsync(agent);
        }

        public GetAgentIncomesOutput GetAgentIncomes(GetAgentIncomesInput input)
        {
            var queryLst = SearchAgentIncomes(input);

            var count = queryLst.Count();
            var totalWater = queryLst.Sum(s => s.TotalRunWaterCount);
            var totalIncome = queryLst.Sum(s => s.TotalIncomeCount);
            var tempQuery = queryLst
                    .OrderBy(input.Sorting + " " + input.SortType)
                    .PageBy(input);

            var entityList = tempQuery.ToList();

            var entityListDtos = entityList.MapTo<List<GetAgentIncomesListDtos>>();

            var response = new GetAgentIncomesOutput()
            {
                Data = new PagedResultDto<GetAgentIncomesListDtos>(count, entityListDtos),
                TotalWater = totalWater,
                TotalIncome = totalIncome
            };
            return response;
        }

        private IQueryable<GetAgentIncomesListDtos> SearchAgentIncomes(GetAgentIncomesInput input)
        {
            var list = new List<GetAgentIncomesListDtos>();

            var tenantId = AbpSession.TenantId ?? 295;
            var incomeRecords = _incomeRecordRepository
                .GetAll()
                .Include(o => o.Order)
                .ThenInclude(f => f.Family)
                .Include(a => a.MqAgent)
                .ThenInclude(a => a.UpperLevelMqAgent)
                .WhereIf(!string.IsNullOrEmpty(input.AgentName), a => a.MqAgent.NickName.Contains(input.AgentName) || a.MqAgent.UserName.Contains(input.AgentName))
                .WhereIf(!string.IsNullOrEmpty(input.Phone), a => a.MqAgent.PhoneNumber.Contains(input.Phone))
                .Where(i => i.CreationTime >= input.StartTime && i.CreationTime <= input.EndTime && i.MqAgent.TenantId == tenantId/* && i.MqAgent.CompanyId == user.comp*/)
                .WhereIf(!string.IsNullOrEmpty(input.UpAgentName), a => a.MqAgent.UpperLevelMqAgent.NickName.Contains(input.UpAgentName))
                .WhereIf(!string.IsNullOrEmpty(input.Company), a => a.MqAgent.Company.Name.Contains(input.Company))
                .GroupBy(a => a.MqAgentId);

            var incomeRecordLst = incomeRecords
                .AsNoTracking()
                .ToList();

            var agentIds = incomeRecordLst.Select(a => a.Key);

            var withdrawMoneys = _enterprisePayRecordRepository.GetAll()
               //.Where(i => i.CreationTime >= input.StartTime && i.CreationTime <= input.EndTime)
               .Where(a => agentIds.Contains(a.AgentId) && (a.State == WithdrawDepositState.Success || a.State == WithdrawDepositState.Auditing));

            var withdrawMoneyLst = withdrawMoneys
                .AsNoTracking()
                .ToList();

            foreach (var item in incomeRecordLst)
            {
                var dto = new GetAgentIncomesListDtos();
                var agent = item.FirstOrDefault(a => a.MqAgentId == item.Key).MqAgent;
                dto.AgentId = agent.Id;
                dto.NickName = agent.NickName;
                dto.TotalIncomeCount = Math.Round(item.Sum(a => a.RealIncome), 2);//Math.Round(agent.TotalBalance,2);
                //var auditingCount = Math.Round(withdrawMoneyLst.Where(a => a.AgentId == agent.Id && a.State == WithdrawDepositState.Auditing).Sum(a => a.Amount), 2);
                dto.CanWithDrawedMoney = Math.Round(agent.Balance, 2) + Math.Round(agent.LockedBalance, 2);////这个时间跟流水时间不能对应
                dto.PhoneNumber = agent.PhoneNumber;
                var firstIncomeRecords = item.Where(a => a.RunWaterRecordType == RunWaterRecordType.First);
                var secondIncomeRecords = item.Where(a => a.RunWaterRecordType == RunWaterRecordType.Second);
                dto.FirstRunWaterCount = Math.Round(firstIncomeRecords.Sum(a => a.Income), 2);
                dto.SecondRunWaterCount = Math.Round(secondIncomeRecords.Sum(a => a.Income), 2);
                dto.TotalRunWaterCount = Math.Round(item.Sum(i => i.Income), 2);
                dto.WithDrawedMoney = Math.Round(withdrawMoneyLst.Where(a => a.AgentId == agent.Id && a.State == WithdrawDepositState.Success).Sum(a => a.Amount), 2);//这个时间跟流水时间不能对应
                dto.TotalOrderCount = item.Count();
                dto.AgentLevel = agent.Level;
                dto.HeadeUrl = agent.HeadUrl;
                dto.UserName = agent.UserName;
                dto.TotalSubsidyAmount = agent.TotalSubsidyAmount;
                if (agent.UpperLevelMqAgent != null)
                {
                    //dto.UpAgentId = agent.UpperLevelMqAgentId;
                    dto.UpAgentName = agent.UpperLevelMqAgent.NickName;
                }
                else
                {
                    dto.UpAgentName = "无";
                }
                list.Add(dto);
            }
            var queryLst = list.AsQueryable();

            return queryLst;
        }

        public string ExportAgentIncomesToExcel(GetAgentIncomesInput input)
        {
            var queryLst = SearchAgentIncomes(input).OrderBy(input.Sorting + " " + input.SortType);
            var fileName = string.Format(AGENTINCOMESFILE, input.StartTime.ToString("yyyyMMdd"), input.EndTime.Date.ToString("yyyyMMdd"));
            FileInfo file = new FileInfo(Path.Combine(_hostingEnvironment.WebRootPath, fileName));
            file.Delete();
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(fileName);//创建sheet
                worksheet.Cells[1, 1].Value = "微信昵称";
                worksheet.Cells[1, 2].Value = "真实姓名";
                worksheet.Cells[1, 3].Value = "手机号";
                worksheet.Cells[1, 4].Value = "身份";
                worksheet.Cells[1, 5].Value = "上级";
                worksheet.Cells[1, 6].Value = "订单数";
                worksheet.Cells[1, 7].Value = "总流水";
                worksheet.Cells[1, 8].Value = "自己的流水";
                worksheet.Cells[1, 9].Value = "下级流水";
                worksheet.Cells[1, 10].Value = "总收益";
                worksheet.Cells[1, 11].Value = "已提现收益";
                worksheet.Cells[1, 12].Value = "可提现收益";
                //worksheet.Cells.Style.ShrinkToFit = true;
                //worksheet.Cells.AutoFitColumns();sdsdsdsdsd
                //样式
                worksheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                int r = 1;
                foreach (var item in queryLst)
                {
                    worksheet.Cells[r + 1, 1].Value = item.NickName;
                    worksheet.Cells[r + 1, 2].Value = item.UserName;
                    worksheet.Cells[r + 1, 3].Value = item.PhoneNumber;
                    worksheet.Cells[r + 1, 4].Value = EnumHelper.EnumHelper.GetDescription(item.AgentLevel);
                    worksheet.Cells[r + 1, 5].Value = item.UpAgentName;
                    worksheet.Cells[r + 1, 6].Value = item.TotalOrderCount;
                    worksheet.Cells[r + 1, 7].Value = Math.Round(item.TotalRunWaterCount, 2);
                    worksheet.Cells[r + 1, 8].Value = Math.Round(item.FirstRunWaterCount, 2);
                    worksheet.Cells[r + 1, 9].Value = Math.Round(item.SecondRunWaterCount, 2);
                    worksheet.Cells[r + 1, 10].Value = Math.Round(item.TotalIncomeCount, 2);
                    worksheet.Cells[r + 1, 11].Value = Math.Round(item.WithDrawedMoney, 2);
                    worksheet.Cells[r + 1, 12].Value = Math.Round(item.CanWithDrawedMoney, 2);
                    r++;
                }
                package.Save();

                return string.Format(fileName);
            }
        }

        public async Task<string> QueryTest()
        {
            var id = 2056;
            var count = 10;
            while (count > 0)
            {
                var entity = await _repository.FirstOrDefaultAsync(c => c.Id == id);

                Console.WriteLine(entity.State);
                count--;
            }

            return "";
        }



        public async Task<PagedResultDto<AgentFamilyStatisticsOutput>> AgentFamilyStatistics(AgentFamilyStatisticsInput input)
        {

            var query1 = (await SearchAgentFamilyData(input)).OrderBy(input.Sorting + " " + input.SortType);
            var listDtos = query1.PageBy(input).ToList();

            return new PagedResultDto<AgentFamilyStatisticsOutput>(query1.Count(), listDtos);
        }

        private async Task<IQueryable<AgentFamilyStatisticsOutput>> SearchAgentFamilyData(AgentFamilyStatisticsInput input)
        {
            var query = _repository.GetAll()
                .Where(a => a.State == AgentState.Audited /*&& a.CreationTime >= input.StartTime && a.CreationTime <= input.EndTime*/)
                .WhereIf(!string.IsNullOrEmpty(input.Name), a => a.NickName.Contains(input.Name) || a.UserName.Contains(input.Name))
                .WhereIf(!string.IsNullOrEmpty(input.CompanyName),c => c.CompanyId.HasValue && c.Company.Name.Contains(input.CompanyName))
                .WhereIf(!string.IsNullOrEmpty(input.PhoneNumber), a => a.PhoneNumber.Contains(input.PhoneNumber))
                .Select(x => new { x.Id, x.NickName, x.PhoneNumber, x.UserName, x.CreationTime, x.PlayerId, x.HeadUrl })
                .AsNoTracking();

            var count = query.Count();

            var playerIds = await query.Select(a => a.PlayerId).ToListAsync();

            //获取所有代理的家庭
            var families = await _familyRepository.GetAll()
                .Where(f => playerIds.Contains(f.FatherId) || playerIds.Contains(f.MotherId))
                .Select(f => new FamilyOrderDto{
                    CreationTime = f.CreationTime,
                    FatherId = f.FatherId,
                    MotherId = f.MotherId,
                    Id = f.Id
                })
                .AsNoTracking().ToListAsync();

            //var newFamilies = families.Where(f => f.CreationTime >= input.StartTime && f.CreationTime <= input.EndTime);
            //var oldFamilies = families.Where(f => f.CreationTime < input.StartTime || f.CreationTime > input.EndTime);

            var familyIds = families.Select(f => f.Id).ToList();

            //获取所有代理家庭的订单
            var orders = _orderRepository.GetAll()
                .Include(o => o.Family)
                .Where(o => familyIds.Contains(o.FamilyId.Value) && o.State == OrderState.Paid && !o.Family.IsDeleted)
                .AsNoTracking();
                //.ToListAsync();
            var newOrders = orders.Where(o => o.CreationTime >= input.StartTime && o.CreationTime <= input.EndTime)
                .Select(o => new FamilyOrderModel {
                    CreationTime = o.CreationTime,
                    State = o.State,
                    FamilyId = o.FamilyId,
                    Family = new FamilyOrderDto() {
                        CreationTime = o.Family == null ? DateTime.Now : o.Family.CreationTime,
                        FatherId = o.Family == null ? Guid.Empty : o.Family.FatherId,
                        MotherId = o.Family == null ? Guid.Empty : o.Family.MotherId
                    }
                })
                .ToList();
            //var oldOrders = orders.Where(o => o.CreationTime < input.StartTime || o.CreationTime > input.EndTime);

            //获取所有代理的流水
            var agentIds = await query.Select(a => a.Id).ToListAsync();
            var incomeRecords = _incomeRecordRepository.GetAll().Include(o => o.Order).ThenInclude(f => f.Family)
                .Where(i => agentIds.Contains(i.MqAgentId) && i.Order != null && i.Order.Family != null)
                .Select(i => i.MapTo<AgentIncomeRecordModel>())
                //.Select(i => new AgentIncomeRecordModel {
                //    OrderId = i.OrderId.HasValue ? i.OrderId : Guid.Empty,
                //    AgentId = i.MqAgentId,
                //    CreationTime = i.CreationTime,
                //    Income = i.Income,
                //    Order = new FamilyOrderModel
                //    {
                //        CreationTime = i.Order == null ? DateTime.MinValue :i.Order.CreationTime,
                //        FamilyId = i.Order == null ? null : i.Order.FamilyId,
                //        State = i.Order == null ? OrderState.UnKnow : i.Order.State,
                //        IsDeleted = i.Order == null ? true : i.Order.IsDeleted,
                //        Family = new FamilyOrderDto()
                //        {
                //            CreationTime = i.Order == null ? DateTime.MinValue : i.Order.Family.CreationTime,
                //            FatherId = i.Order == null ? Guid.Empty : i.Order.Family.FatherId,
                //            MotherId = i.Order == null ? Guid.Empty : i.Order.Family.MotherId,
                //            IsDeleted = i.Order == null ? true : i.Order.Family.IsDeleted
                //        }
                //    }
                //})
                .DistinctBy(o => o.OrderId.Value);
            var newIncomeRecords = incomeRecords.Where(c => c.CreationTime >= input.StartTime && c.CreationTime <= input.EndTime).ToList();
            //var oldIncomeRecords = incomeRecords.Where(c => c.CreationTime < input.StartTime || c.CreationTime > input.EndTime);

            List<AgentFamilyStatisticsOutput> outputList = new List<AgentFamilyStatisticsOutput>();
            foreach (var item in query)
            {
                var agentFamilyStatistics = new AgentFamilyStatisticsOutput();
                agentFamilyStatistics.AgentId = item.Id;

                agentFamilyStatistics.NickName = item.NickName ?? "无";

                agentFamilyStatistics.UserName = item.UserName ?? "无";

                agentFamilyStatistics.HeadUrl = item.HeadUrl ?? "无";

                agentFamilyStatistics.PhoneNumber = item.PhoneNumber ?? "无";

                agentFamilyStatistics.NewCreateFamilyCount = families.Count(c => (c.CreationTime >= input.StartTime && c.CreationTime <= input.EndTime) &&
                (c.FatherId == item.PlayerId || c.MotherId == item.PlayerId));

                var NewFamilyOrders = newOrders.Where(c =>
                (c.Family.FatherId == item.PlayerId || c.Family.MotherId == item.PlayerId) &&
                (c.Family.CreationTime >= input.StartTime && c.Family.CreationTime <= input.EndTime) &&
                c.State == OrderState.Paid)
                .GroupBy(o => o.FamilyId);
                agentFamilyStatistics.NewRechargeFamilyCount = NewFamilyOrders.Count();

                //agentFamilyStatistics.CreateFamilyCount = families.Count(c => (c.CreationTime < input.StartTime || c.CreationTime > input.EndTime) &&
                //(c.FatherId == item.PlayerId || c.MotherId == item.PlayerId));

                var familyOrders = newOrders.Where(c =>
               (c.Family.FatherId == item.PlayerId || c.Family.MotherId == item.PlayerId) &&
               (c.Family.CreationTime < input.StartTime || c.Family.CreationTime > input.EndTime))
               .GroupBy(f => f.FamilyId);
                agentFamilyStatistics.RechargeFamilyCount = familyOrders.Count();

                //var groups = orders.Where(o => (o.CreationTime >= input.StartTime && o.CreationTime <= input.EndTime) &&
                //(o.Family.FatherId == item.PlayerId || o.Family.MotherId == item.PlayerId))
                //.GroupBy(o => o.FamilyId);
                var counts = NewFamilyOrders.Select(c => c.Count(f => f.FamilyId == c.Key.Value));
                agentFamilyStatistics.RepeatRechargeFamilyCount = counts.Count(c => c >= 2);

                //新家庭流水
                agentFamilyStatistics.NewFamilyWaterRunCount = Math.Round(
                     newIncomeRecords
                    .Where(c => c.Order != null && c.Order.Family != null &&
                    !c.Order.IsDeleted && !c.Order.Family.IsDeleted &&
                    c.Order.State == OrderState.Paid &&
                    c.Order.Family.CreationTime >= input.StartTime && c.Order.Family.CreationTime <= input.EndTime &&
                           (c.Order.Family.FatherId == item.PlayerId || c.Order.Family.MotherId == item.PlayerId))
                           .Sum(c => c.Income)
                    , 2);
                //时间段外家庭流水
                agentFamilyStatistics.FamilyWaterRunCount = Math.Round(
                     newIncomeRecords
                    .Where(c => c.Order != null && c.Order.Family != null &&
                          !c.Order.IsDeleted && !c.Order.Family.IsDeleted &&
                          c.Order.State == OrderState.Paid &&
                          (c.Order.Family.CreationTime < input.StartTime || c.Order.Family.CreationTime > input.EndTime) &&
                                 (c.Order.Family.FatherId == item.PlayerId || c.Order.Family.MotherId == item.PlayerId))
                                 .Sum(c => c.Income)
                , 2);

                //充值转化率
                agentFamilyStatistics.RechargeInversionRate = agentFamilyStatistics.NewRechargeFamilyCount == 0 ? 0 : Math.Round((agentFamilyStatistics.NewRechargeFamilyCount / (agentFamilyStatistics.NewCreateFamilyCount * 1.0)), 2);

                //客单价
                agentFamilyStatistics.PerCustomerTransaction = agentFamilyStatistics.NewFamilyWaterRunCount == 0 ? 0 : Math.Round((agentFamilyStatistics.NewFamilyWaterRunCount / agentFamilyStatistics.NewRechargeFamilyCount), 2);
                outputList.Add(agentFamilyStatistics);
            }

            return outputList.AsQueryable<AgentFamilyStatisticsOutput>();
        }

        public async Task<string> ExportAgentFamilyToExcel(AgentFamilyStatisticsInput input)
        {
            var query1 = await SearchAgentFamilyData(input);
            var listDtos = query1.OrderBy(input.Sorting + " " + input.SortType).ToList();
            var fileName = string.Format(AGENTFAMILY, input.StartTime.ToString("yyyyMMdd"), input.EndTime.ToString("yyyyMMdd"));
            FileInfo file = new FileInfo(Path.Combine(_hostingEnvironment.WebRootPath, fileName));
            file.Delete();
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("提现记录表");//创建sheet
                worksheet.Cells[1, 1].Value = "代理Id";
                worksheet.Cells[1, 2].Value = "昵称";
                worksheet.Cells[1, 3].Value = "姓名";
                worksheet.Cells[1, 4].Value = "手机号";
                worksheet.Cells[1, 5].Value = "新建家庭数量";
                worksheet.Cells[1, 6].Value = "新充值家庭数量";
                worksheet.Cells[1, 7].Value = "老家庭充值数量";
                worksheet.Cells[1, 8].Value = "重复家庭数量";
                worksheet.Cells[1, 9].Value = "新家庭流水";
                worksheet.Cells[1, 10].Value = "老家庭流水";
                worksheet.Cells[1, 11].Value = "充值转化率";
                worksheet.Cells[1, 12].Value = "客单价";
                //worksheet.Cells.Style.ShrinkToFit = true;
                //worksheet.Cells.AutoFitColumns();

                //样式
                worksheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                int r = 1;
                foreach (var item in listDtos)
                {
                    worksheet.Cells[r + 1, 1].Value = item.AgentId;
                    worksheet.Cells[r + 1, 2].Value = item.NickName;
                    worksheet.Cells[r + 1, 3].Value = item.UserName;
                    worksheet.Cells[r + 1, 4].Value = item.PhoneNumber;
                    worksheet.Cells[r + 1, 5].Value = item.NewCreateFamilyCount;
                    worksheet.Cells[r + 1, 6].Value = item.NewRechargeFamilyCount;
                    worksheet.Cells[r + 1, 7].Value = item.RechargeFamilyCount;
                    worksheet.Cells[r + 1, 8].Value = item.RepeatRechargeFamilyCount;
                    worksheet.Cells[r + 1, 9].Value = item.NewFamilyWaterRunCount;
                    worksheet.Cells[r + 1, 10].Value = item.FamilyWaterRunCount;
                    worksheet.Cells[r + 1, 11].Value = item.RechargeInversionRate;
                    worksheet.Cells[r + 1, 12].Value = item.PerCustomerTransaction;

                    r++;
                }

                package.Save();

                return fileName;
            }

        }

        public async Task<List<GetCompaniesDtos>> GetCompanyList()
        {
            var companies = await _companyRepository.GetAll().AsNoTracking().ToListAsync();

            return companies.MapTo<List<GetCompaniesDtos>>();
        }

        public async Task UpdateAgentCompany(UpdateAgentCompanyInput input)
        {
            var agent = await _repository.FirstOrDefaultAsync(a => a.Id == input.AgentId);

            if (agent != null)
            {
                if (input.CompanyId == 0)
                {
                    agent.CompanyId = null;
                }
                else
                {
                    agent.CompanyId = input.CompanyId;
                }
                await _repository.UpdateAsync(agent);
            }
        }
    }
}


