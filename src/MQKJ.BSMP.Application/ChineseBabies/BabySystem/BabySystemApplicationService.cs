using Abp;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.BSMPFiles;
using MQKJ.BSMP.ChineseBabies.BabySystem.Dtos.HostDtos;
using MQKJ.BSMP.ChineseBabies.BabySystem.Dtos.HostDtos.ImpotExcelDto;
using MQKJ.BSMP.ChineseBabies.PropMall;
using MQKJ.BSMP.EnumHelper;
using MQKJ.BSMP.Utils.Extensions;
using MQKJ.BSMP.Utils.Tools;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Application.Services;
using Microsoft.Extensions.Configuration;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// Reward应用层服务的接口实现方法  
    ///</summary>
    //[AbpAuthorize]
    public class BabySystemAppService : AbpServiceBase, IBabySystemAppService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IRepository<Reward> _rewardRepository;
        private readonly IRepository<BabyEvent> _babyEventRepository;
        private readonly IRepository<EventGroupBabyEvent> _eventGroupBabyEventRepository;
        private readonly IRepository<EventGroup> _eventGroupRepository;
        private readonly IRepository<BabyEventOption> _babyEventOptionsRepository;
        private readonly IRepository<Baby> _babyRepository;
        private readonly IRepository<Family> _familyRepository;
        private readonly IRepository<BabyEnding> _endingRepository;
        private readonly IRepository<Profession> _professionRepository;
        //商场表
        private readonly IRepository<BabyProp> _babyPropRepository;
        private readonly IRepository<BabyPropFeature> _babyPropFeatureRepository;
        private readonly IRepository<BabyPropFeatureType, Guid> _babyPropFeatureTypeRepository;
        private readonly IRepository<BabyPropTerm> _babyPropTermRepository;
        private readonly IRepository<BabyPropBuyTermType, Guid> _babyPropTermTypeRepository;
        private readonly IRepository<BabyPropPrice> _babyPropPriceRepository;
        private readonly IRepository<BabyPropPropertyAward, Guid> _babyPropPropertyAwardRepository;
        private readonly IRepository<BabyPropType> _babyPropTypeRepository;
        private readonly IRepository<BabyPropBag, Guid> _babyPropBagRepository;
        private readonly IRepository<BabyPropBagAndBabyProp, Guid> _bigBagAndPropRepository;
        private IConfiguration _configuration;
        /// <summary>
        /// 导入数据记录
        /// </summary>
        private readonly IRepository<ImportDataRecord> _importDataRecordRepository;
        /// <summary>
        /// 构造函数 
        ///</summary>
        public BabySystemAppService(
                IHostingEnvironment hostingEnvironment,
                IRepository<Reward> rewardRepository,
                IRepository<BabyEvent> babyEventRepository,
                IRepository<EventGroupBabyEvent> eventGroupBabyEventRepository,
           IRepository<EventGroup> eventGroupRepository,
           IRepository<BabyEventOption> babyEventOptionsRepository,
           IRepository<Baby> babyRepository,
           IRepository<Family> familyRepository,
           IRepository<BabyEnding> endingRepository,
           IRepository<Profession> professionRepository,
           IRepository<BabyProp> babyPropRepository,
           IRepository<BabyPropFeature> babyPropFeatureRepository,
           IRepository<BabyPropTerm> babyPropTermRepository,
           IRepository<BabyPropPrice> babyPropPriceRepository,
           IRepository<BabyPropBuyTermType, Guid> babyPropTermTypeRepository,
           IRepository<BabyPropFeatureType, Guid> babyPropFeatureTypeRepository,
           IRepository<BabyPropPropertyAward, Guid> babyPropPropertyAwardRepository,
           IRepository<BabyPropType> babyPropTypeRepository,
           IRepository<BabyPropBag, Guid> babyPropBagRepository,
           IRepository<BabyPropBagAndBabyProp, Guid> bigBagAndPropRepository,
           IRepository<ImportDataRecord> importDataRecordRepository,
                IConfiguration configuration
            ) : base()
        {
            _hostingEnvironment = hostingEnvironment;
            _rewardRepository = rewardRepository;
            _babyEventRepository = babyEventRepository;
            _eventGroupBabyEventRepository = eventGroupBabyEventRepository;
            _eventGroupRepository = eventGroupRepository;
            _babyEventOptionsRepository = babyEventOptionsRepository;
            _babyRepository = babyRepository;
            _familyRepository = familyRepository;
            _endingRepository = endingRepository;
            _professionRepository = professionRepository;
            _importDataRecordRepository = importDataRecordRepository;
            _babyPropRepository = babyPropRepository;
            _babyPropFeatureRepository = babyPropFeatureRepository;
            _babyPropTermRepository = babyPropTermRepository;
            _babyPropPriceRepository = babyPropPriceRepository;
            _babyPropFeatureTypeRepository = babyPropFeatureTypeRepository;
            _babyPropTermTypeRepository = babyPropTermTypeRepository;
            _babyPropPropertyAwardRepository = babyPropPropertyAwardRepository;
            _babyPropTypeRepository = babyPropTypeRepository;
            _babyPropBagRepository = babyPropBagRepository;
            _bigBagAndPropRepository = bigBagAndPropRepository;
            _configuration = configuration;
        }
        #region 导入
        [UnitOfWork(IsDisabled = true)]
        public ImportDataOutput ImportData(ImportDataInput input)
        {
            var output = new ImportDataOutput();

            var _importRecord = new ImportDataRecord
            {
                FileName = input.FormFile.FileName,
                FileDataType = input.TableDataType,
                Size = ((double)input.FormFile.Length).ToHumanReadableSize(),
                State = ImportState.Pending
            };
            int _recordIdentity = _importDataRecordRepository.InsertAndGetId(_importRecord);
            var _dataTable = Abp.Threading.AsyncHelper.RunSync<DataTable>(() => ReadExcelData(input.FormFile));
            var _exel = new ExcelPackage(input.FormFile.OpenReadStream());
            Task.Run(() => ImportDataTaskAsync(new ImportDataTaskModel
            {
                TaskInput = _dataTable,
                TableDataType = input.TableDataType,
                TaskIdentity = _recordIdentity,
                ExcelFile = _exel

            }));
            return output;
        }
        public async Task<IList<ImportDataTaskOutput>> TaskListAsync()
        {
            return (await _importDataRecordRepository.GetAll().OrderByDescending(c => c.CreationTime).ToListAsync())
                .MapTo<List<ImportDataTaskOutput>>();
        }
        [RemoteService(IsEnabled = false)]
        public async Task ImportDataTaskAsync(ImportDataTaskModel input)
        {
            ImportDataRecord _currentRecord = null;
            Stopwatch _watch = new Stopwatch();
            _watch.Start();
            try
            {
                _currentRecord = _importDataRecordRepository.FirstOrDefault(input.TaskIdentity);
                if (_currentRecord == null) throw new Abp.UI.UserFriendlyException($"导入数据记录为空,标识{input.TaskIdentity}");
                //TODO: 职业表暂未开发,跳过更新
                if (input.TableDataType == TableDataType.Profession)
                {
                    throw new Exception("NotImplement");
                }
                using (var _uowHandler = UnitOfWorkManager.Begin())
                {
                    //获取导入的文件

                    if (input.TableDataType == TableDataType.BabyEventAndOptions)
                    {
                        // 导入事件和选项，因为特殊事件需要前置的事件编号有时需要导入两次
                        ImportBabyEventDataToDataSet(input.TaskInput);
                    }
                    else if (input.TableDataType == TableDataType.Reward || input.TableDataType == TableDataType.Consume)
                    {
                        //导入奖励&消耗
                        ImportRewardOrConsumeExcelDataToDataSet(input.TaskInput, input.TableDataType);
                    }
                    else if (input.TableDataType == TableDataType.EventGroup)
                    {
                        await ImportEventGroup(input.TaskInput);
                    }
                    else if (input.TableDataType == TableDataType.Ending)
                    {
                        await ImportEndingTable(input.TaskInput);
                    }
                    else if (input.TableDataType == TableDataType.Profession)
                    {
                        await ImportProfessionTable(input.TaskInput);
                    }
                    else if (input.TableDataType == TableDataType.BabyProp)
                    {
                        await ImportBabyPropTable(input.TaskInput, input.ExcelFile);
                    }
                    else if (input.TableDataType == TableDataType.PropertyAward)
                    {
                        await ImportPropPropertyAwardTable(input.TaskInput, input.ExcelFile);
                    }
                    else if (input.TableDataType == TableDataType.BigGiftBag)
                    {
                        await ImportBabyBigGifBagTable(input.TaskInput, input.ExcelFile);
                    }
                    _currentRecord.State = ImportState.Completed;
                    UnitOfWorkManager.Current.SaveChanges();
                    _uowHandler.Complete();
                }
            }
            catch (Exception e)
            {
                _currentRecord.Exception = e.ToString();
                _currentRecord.State = ImportState.Failed;
            }
            finally
            {
                _watch.Stop();
                _currentRecord.Elapsed = _watch.Elapsed.TotalSeconds;
                _importDataRecordRepository.Update(_currentRecord);
            }

        }
        /// <summary>
        /// 导入大礼包数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [RemoteService(IsEnabled = false)]
        public async Task ImportBabyBigGifBagTable(DataTable dataTable, ExcelPackage excelpackage)
        {
            var package = excelpackage;//new ExcelPackage(formFile.OpenReadStream());
            var workbook = package.Workbook;
            var worksheet = workbook.Worksheets.First();
            var rows = DataTableExtensions.ConvertSheetToObjects<ImportBabyBagData>(worksheet);

            var allBagGiftBags = await _babyPropBagRepository.GetAll().Include(bp => bp.BabyPropBagAndBabyProps).ToListAsync();

            var babyProps = _babyPropRepository.GetAll().Select(x => new BabyPropModel { Code = x.Code, Id = x.Id }).ToList();
            var addbagGiftBags = new List<BabyPropBag>();
            var updatebagGiftBags = new List<BabyPropBag>();

            var propPrices = _babyPropPriceRepository.GetAll().Select(p => new BigBagAndProp { Id = p.Id, Validate = p.Validity, BabyPropId = p.BabyPropId }).ToList();

            foreach (var item in rows)
            {
                var bagAndProps = new List<BabyPropBagAndBabyProp>();
                var bagGiftBag = allBagGiftBags.FirstOrDefault(b => b.Code == item.Code);

                if (bagGiftBag == null)
                {
                    addbagGiftBags.Add(AddBigBagData(item, propPrices, babyProps));
                }
                else
                {
                    UpdateBigBagData(bagGiftBag, item, propPrices, babyProps);
                    updatebagGiftBags.Add(bagGiftBag);
                }
            }
            //添加
            addbagGiftBags.ForEach(async item =>
            {
                await _babyPropBagRepository.InsertAsync(item);
            });

            //更新
            updatebagGiftBags.ForEach(async item =>
            {
                await _babyPropBagRepository.UpdateAsync(item);
            });
        }
        /// <summary>
        /// 更新大礼包数据
        /// </summary>
        /// <param name="babyPropBag"></param>
        /// <param name="babyBagData"></param>
        /// <param name="propPrices"></param>
        /// <param name="babyProps"></param>
        private BabyPropBag UpdateBigBagData(BabyPropBag babyPropBag, ImportBabyBagData babyBagData, List<BigBagAndProp> propPrices, List<BabyPropModel> babyProps)
        {
            babyPropBag.Code = babyBagData.Code;

            if (babyBagData.BuyCurrencyType == 1) //金币
            {
                babyPropBag.PriceGoldCoin = babyBagData.Price;
            }
            else if (babyBagData.BuyCurrencyType == 2)//待续
            {
                babyPropBag.PriceRMB = decimal.Parse(Math.Round(babyBagData.Price, 2).ToString());
            }
            babyPropBag.Gender = (Gender)babyBagData.Gender;

            babyPropBag.Title = babyBagData.Title;

            babyPropBag.Description = babyBagData.Description;

            babyPropBag.Order = babyBagData.Sort;

            if (babyBagData.GiveCurrencyType.HasValue)
            {
                babyPropBag.CurrencyType = (CurrencyTypeEnum)babyBagData.GiveCurrencyType;
                babyPropBag.CurrencyCount = babyBagData.GiveCount.Value;
            }
            var count = 1;
            foreach (var item in babyPropBag.BabyPropBagAndBabyProps)
            {
                switch (count)
                {
                    case 1:
                        UpdateBagAndProp(item, babyBagData.PropCode1, babyBagData.Count1, babyBagData.Validate1, propPrices, babyProps);
                        break;
                    case 2:
                        UpdateBagAndProp(item, babyBagData.PropCode2, babyBagData.Count2, babyBagData.Validate2, propPrices, babyProps);
                        break;
                    case 3:
                        UpdateBagAndProp(item, babyBagData.PropCode3, babyBagData.Count3, babyBagData.Validate3, propPrices, babyProps);
                        break;
                    case 4:
                        UpdateBagAndProp(item, babyBagData.PropCode4, babyBagData.Count4, babyBagData.Validate4, propPrices, babyProps);
                        break;
                    default:
                        break;
                }
                count++;
            }

            return babyPropBag;
        }

        private void UpdateBagAndProp(BabyPropBagAndBabyProp bigBagAndProp, int propCode, int Count, int validate, List<BigBagAndProp> propPrices, List<BabyPropModel> babyProps)
        {

            bigBagAndProp.Count = Count;
            var prop = babyProps.FirstOrDefault(p => p.Code == propCode);
            if (prop != null)
            {
                var propPrice = propPrices.FirstOrDefault(c => c.Validate == validate && c.BabyPropId == prop.Id);
                if (propPrice != null)
                {
                    bigBagAndProp.BabyPropPriceId = propPrice.Id;
                    bigBagAndProp.BabyPropId = prop.Id;
                }
                else
                {
                    throw new Exception("表中数据错误，请检查后在试");
                }

            }
        }

        /// <summary>
        /// 添加大礼包数据
        /// </summary>
        /// <param name="babyBagData"></param>
        /// <param name="propPrices"></param>
        /// <param name="babyProps"></param>
        /// <returns></returns>
        private BabyPropBag AddBigBagData(ImportBabyBagData babyBagData, List<BigBagAndProp> propPrices, List<BabyPropModel> babyProps)
        {
            var bagAndProps = new List<BabyPropBagAndBabyProp>();

            var bagGiftBag = new BabyPropBag();

            bagGiftBag.Code = babyBagData.Code;

            if (babyBagData.BuyCurrencyType == 1) //金币
            {
                bagGiftBag.PriceGoldCoin = babyBagData.Price;
            }
            else if (babyBagData.BuyCurrencyType == 2)//待续
            {
                bagGiftBag.PriceRMB = decimal.Parse(Math.Round(babyBagData.Price, 2).ToString());
            }
            bagGiftBag.Gender = (Gender)babyBagData.Gender;

            bagGiftBag.Title = babyBagData.Title;

            bagGiftBag.Description = babyBagData.Description;

            bagGiftBag.Order = babyBagData.Sort;

            if (babyBagData.GiveCurrencyType.HasValue)
            {
                bagGiftBag.CurrencyType = (CurrencyTypeEnum)babyBagData.GiveCurrencyType;
                bagGiftBag.CurrencyCount = babyBagData.GiveCount.Value;
            }
            bagAndProps.Add(SetBigBagAndProp(babyBagData.PropCode1, babyBagData.Count1, babyBagData.Validate1, propPrices, babyProps));
            bagAndProps.Add(SetBigBagAndProp(babyBagData.PropCode2, babyBagData.Count2, babyBagData.Validate2, propPrices, babyProps));
            bagAndProps.Add(SetBigBagAndProp(babyBagData.PropCode3, babyBagData.Count3, babyBagData.Validate3, propPrices, babyProps));
            bagAndProps.Add(SetBigBagAndProp(babyBagData.PropCode4, babyBagData.Count4, babyBagData.Validate4, propPrices, babyProps));
            bagGiftBag.BabyPropBagAndBabyProps = bagAndProps;
            return bagGiftBag;
        }

        /// <summary>
        /// 设置大礼包与道具中间表数据
        /// </summary>
        /// <param name="bigBagId"></param>
        /// <param name="propId"></param>
        /// <param name="Count"></param>
        /// <param name="propPrices"></param>
        /// <param name="validate"></param>
        private BabyPropBagAndBabyProp SetBigBagAndProp(int propCode, int Count, int validate, List<BigBagAndProp> propPrices, List<BabyPropModel> props)
        {
            var bigBagAndProp = new BabyPropBagAndBabyProp();
            //bigBagAndProp.BabyPropBagId = bigBagId;
            bigBagAndProp.Count = Count;
            var prop = props.FirstOrDefault(p => p.Code == propCode);
            if (prop != null)
            {
                var propPrice = propPrices.FirstOrDefault(c => c.Validate == validate && c.BabyPropId == prop.Id);
                if (propPrice != null)
                {
                    bigBagAndProp.BabyPropPriceId = propPrice.Id;
                    bigBagAndProp.BabyPropId = prop.Id;
                }
                else
                {
                    throw new Exception("表数据错误，请检查表数据");
                }
            }

            return bigBagAndProp;
        }

        /// <summary>
        /// 导入属性奖励表(竞技场)
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [RemoteService(IsEnabled = false)]
        public async Task ImportPropPropertyAwardTable(DataTable dataTable, ExcelPackage exelpackage)
        {
            var package = exelpackage; //new ExcelPackage(formFile.OpenReadStream());
            var workbook = package.Workbook;
            var worksheet = workbook.Worksheets.First();
            var rows = DataTableExtensions.ConvertSheetToObjects<ImportPropPropertyAwardTable>(worksheet);

            var addPropertyAwards = new List<BabyPropPropertyAward>();
            var updatePropertyAwards = new List<BabyPropPropertyAward>();

            var allPropertyAwards = await _babyPropPropertyAwardRepository.GetAll().ToListAsync();

            foreach (var item in rows)
            {
                var propertyAward = allPropertyAwards.FirstOrDefault(c => c.Code == item.Code);
                if (propertyAward == null)
                {
                    addPropertyAwards.Add(AddPropertyAwardData(item));
                }
                else
                {
                    updatePropertyAwards.Add(UpdatePropertyAwardData(propertyAward, item));
                }
            }

            //添加数据
            addPropertyAwards.ForEach(async item =>
            {
                await _babyPropPropertyAwardRepository.InsertAsync(item);
            });

            //更新数据
            updatePropertyAwards.ForEach(async item =>
            {
                await _babyPropPropertyAwardRepository.UpdateAsync(item);
            });
        }

        /// <summary>
        /// 添加道具属性数据
        /// </summary>
        /// <param name="propertyAward"></param>
        /// <param name="propertyAwardTable"></param>
        /// <returns></returns>
        private BabyPropPropertyAward AddPropertyAwardData(ImportPropPropertyAwardTable propertyAwardTable)
        {
            var propertyAward = new BabyPropPropertyAward();
            propertyAward.Intelligence = propertyAwardTable.IntelligenceValue;
            propertyAward.EmotionQuotient = propertyAwardTable.EmotionQuotientValue;
            propertyAward.Physique = propertyAwardTable.PhysiqueValue;
            propertyAward.WillPower = propertyAwardTable.WillPowerValue;
            propertyAward.Imagine = propertyAwardTable.ImagineValue;
            propertyAward.Charm = propertyAwardTable.CharmValue;
            propertyAward.Code = propertyAwardTable.Code;
            propertyAward.EventAdditionType = EventAdditionType.PropAddititon;

            return propertyAward;
        }

        /// <summary>
        /// 更新道具属性数据
        /// </summary>
        /// <param name="propertyAward"></param>
        /// <param name="propertyAwardTable"></param>
        /// <returns></returns>
        private BabyPropPropertyAward UpdatePropertyAwardData(BabyPropPropertyAward propertyAward, ImportPropPropertyAwardTable propertyAwardTable)
        {
            propertyAward.Intelligence = propertyAwardTable.IntelligenceValue;
            propertyAward.EmotionQuotient = propertyAwardTable.EmotionQuotientValue;
            propertyAward.Physique = propertyAwardTable.PhysiqueValue;
            propertyAward.WillPower = propertyAwardTable.WillPowerValue;
            propertyAward.Imagine = propertyAwardTable.ImagineValue;
            propertyAward.Charm = propertyAwardTable.CharmValue;
            propertyAward.EventAdditionType = EventAdditionType.PropAddititon;

            return propertyAward;
        }

        /// <summary>
        /// 导入道具表(商场)
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private async Task ImportBabyPropTable(DataTable dataTable, ExcelPackage excelpackage)
        {
            //var importBabyPropDto = dataTable.ToList<ImportBabyPropTable>();

            //var package = new ExcelPackage(file.OpenReadStream());
            var package = excelpackage;
            var workbook = package.Workbook;
            var worksheet = workbook.Worksheets.First();
            var rows = DataTableExtensions.ConvertSheetToObjects<ImportPropTable>(worksheet);

            var propAwards = _babyPropPropertyAwardRepository.GetAll().Select(c => new { c.Id, c.Code });

            var propList = await _babyPropRepository.GetAllIncluding(p => p.Prices, t => t.BabyPropTerms, f => f.BabyPropFeatures).ToListAsync();

            //var maxCode = allPropCodes.Max();

            var propTypes = _babyPropTypeRepository.GetAll().Select(c => new { c.Id, c.Code });

            //var addRows = rows.Where(p => !allPropCodes.Contains(p.Code));


            var addProps = new List<BabyProp>();
            var updateProps = new List<BabyProp>();

            foreach (var item in rows)
            {
                var prices = new List<BabyPropPrice>();
                var terms = new List<BabyPropTerm>();
                var fetatures = new List<BabyPropFeature>();
                //道具表
                var babyProp = propList.FirstOrDefault(p => p.Code == item.PropCode);
                if (babyProp == null)
                {
                    babyProp = new BabyProp();
                    babyProp.Code = item.PropCode;
                    babyProp.Title = item.Title;
                    babyProp.IsInheritAble = item.IsInheritAble;
                    babyProp.MaxPurchasesNumber = item.MaxPurchasesNumber;
                    babyProp.Level = (PropLevel)item.Level;
                    babyProp.IsDefault = item.IsDefault;
                    babyProp.GetWay = (GetWay)item.GetWay;
                    babyProp.Gender = (Gender)item.Gender;
                    babyProp.TriggerShowPropCode = item.TriggerShowPropCode;
                    babyProp.IsAfterBuyPlayMarquees = item.IsAfterBuyPlayMarquees;
                    babyProp.TriggerNextShowPropCode = item.TriggerNextShowPropCode;
                    var propAward = propAwards.FirstOrDefault(c => c.Code == item.AwardId);
                    if (propAward != null)
                    {
                        babyProp.BabyPropPropertyAwardId = propAward.Id;
                    }
                    var propType = propTypes.FirstOrDefault(c => c.Code == item.BabyPropTypeCode);
                    if (propType != null)
                    {
                        babyProp.BabyPropTypeId = propType.Id;
                    }

                    //价格表
                    var price1 = SetPropPrice(item.PropValue1, item.Price1, item.Interval1, item.CurrencyType);
                    var price2 = SetPropPrice(item.PropValue2, item.Price2, item.Interval2, item.CurrencyType);
                    var price3 = SetPropPrice(item.PropValue3, item.Price3, item.Interval3, item.CurrencyType);
                    if (price1 != null)
                    {
                        price1.Sort = 1;
                        prices.Add(price1);
                    }
                    if (price2 != null)
                    {
                        price2.Sort = 2;
                        prices.Add(price2);
                    }
                    if (price3 != null)
                    {
                        price3.Sort = 3;
                        prices.Add(price3);
                    }
                    babyProp.Prices = prices;

                    //条件表
                    var term1 = SetPropTerm(item.Term1, item.TermArgumet1);
                    var term2 = SetPropTerm(item.Term2, item.TermArgumet2);
                    var term3 = SetPropTerm(item.Term3, item.TermArgumet3);
                    var term4 = SetPropTerm(item.Term4, item.TermArgumet4);
                    if (term1 != null) terms.Add(term1);
                    if (term2 != null) terms.Add(term2);
                    if (term3 != null) terms.Add(term3);
                    if (term4 != null) terms.Add(term4);
                    babyProp.BabyPropTerms = terms;

                    //功能特性表
                    var feature = SetPropFeature(item.FeatureType, item.FeatureValue);
                    if (feature != null) fetatures.Add(feature);
                    babyProp.BabyPropFeatures = fetatures;

                    addProps.Add(babyProp);
                }
                else //更新
                {
                    babyProp.Title = item.Title;
                    babyProp.IsInheritAble = item.IsInheritAble;
                    babyProp.MaxPurchasesNumber = item.MaxPurchasesNumber;
                    babyProp.Level = (PropLevel)item.Level;
                    babyProp.IsDefault = item.IsDefault;
                    babyProp.GetWay = (GetWay)item.GetWay;
                    babyProp.Gender = (Gender)item.Gender;
                    babyProp.TriggerShowPropCode = item.TriggerShowPropCode;
                    babyProp.IsAfterBuyPlayMarquees = item.IsAfterBuyPlayMarquees;
                    babyProp.TriggerNextShowPropCode = item.TriggerNextShowPropCode;

                    var propAward = propAwards.FirstOrDefault(c => c.Code == item.AwardId);
                    if (propAward != null)
                    {
                        babyProp.BabyPropPropertyAwardId = propAward.Id;
                    }
                    var propType = propTypes.FirstOrDefault(c => c.Code == item.BabyPropTypeCode);
                    if (propType != null)
                    {
                        babyProp.BabyPropTypeId = propType.Id;
                    }

                    //价格
                    var priceList = babyProp.Prices.ToList();
                    if (priceList.Count >= 1)
                    {
                        UpdatePropPriceData(item.PropValue1, item.Price1, item.Interval1, item.CurrencyType, priceList[0], babyProp);
                    }
                    else
                    {
                        var price1 = SetPropPrice(item.PropValue1, item.Price1, item.Interval1, item.CurrencyType);
                        if (price1 != null) babyProp.Prices.Add(price1);

                    }
                    if (priceList.Count >= 2)
                    {
                        UpdatePropPriceData(item.PropValue2, item.Price2, item.Interval2, item.CurrencyType, priceList[1], babyProp);
                    }
                    else
                    {
                        var price2 = SetPropPrice(item.PropValue2, item.Price2, item.Interval2, item.CurrencyType);
                        if (price2 != null) babyProp.Prices.Add(price2);

                    }
                    if (priceList.Count >= 3)
                    {
                        UpdatePropPriceData(item.PropValue3, item.Price3, item.Interval3, item.CurrencyType, priceList[2], babyProp);
                    }
                    else
                    {
                        var price3 = SetPropPrice(item.PropValue3, item.Price3, item.Interval3, item.CurrencyType);
                        if (price3 != null) babyProp.Prices.Add(price3);
                    }

                    //条件
                    //UpdatePropTerm(item,babyProp);
                    var propTermList = babyProp.BabyPropTerms.ToList();
                    if (propTermList.Count >= 1)
                    {
                        UpdatePropTermData(item.Term1, item.TermArgumet1, propTermList[0], babyProp);
                    }
                    else
                    {
                        var term1 = SetPropTerm(item.Term1, item.TermArgumet1);
                        if (term1 != null)
                        {
                            term1.BabyPropId = babyProp.Id;
                            babyProp.BabyPropTerms.Add(term1);
                        }
                    }
                    if (propTermList.Count >= 2)
                    {
                        UpdatePropTermData(item.Term2, item.TermArgumet2, propTermList[1], babyProp);
                    }
                    else
                    {
                        var term2 = SetPropTerm(item.Term2, item.TermArgumet2);
                        if (term2 != null)
                        {
                            term2.BabyPropId = babyProp.Id;
                            babyProp.BabyPropTerms.Add(term2);
                        }
                    }
                    if (propTermList.Count >= 3)
                    {
                        UpdatePropTermData(item.Term3, item.TermArgumet3, propTermList[2], babyProp);
                    }
                    else
                    {
                        var term3 = SetPropTerm(item.Term3, item.TermArgumet3);
                        if (term3 != null)
                        {
                            term3.BabyPropId = babyProp.Id;
                            babyProp.BabyPropTerms.Add(term3);
                        }
                    }
                    if (propTermList.Count >= 4)
                    {
                        UpdatePropTermData(item.Term4, item.TermArgumet4, propTermList[3], babyProp);
                    }
                    else
                    {
                        var term4 = SetPropTerm(item.Term4, item.TermArgumet4);
                        if (term4 != null)
                        {
                            term4.BabyPropId = babyProp.Id;
                            babyProp.BabyPropTerms.Add(term4);
                        }
                    }
                    //功能特征
                    var featureList = babyProp.BabyPropFeatures.ToList();
                    if (featureList.Count >= 1)
                    {
                        UpdatePropFeatureData(item.FeatureType, item.FeatureValue, featureList[0], babyProp);
                    }
                    else
                    {
                        var feature = SetPropFeature(item.FeatureType, item.FeatureValue);
                        if (feature != null)
                        {
                            feature.BabyPropId = babyProp.Id;
                            babyProp.BabyPropFeatures.Add(feature);
                        }
                    }
                    updateProps.Add(babyProp);
                }
            }

            if (addProps.Count != 0)
            {
                addProps.ForEach(async prop =>
                {
                    await _babyPropRepository.InsertAsync(prop);
                });
            }

            if (updateProps.Count != 0)
            {
                updateProps.ForEach(async prop =>
                {
                    await _babyPropRepository.UpdateAsync(prop);
                });
            }

            //删除
            //var codes = rows.Select(c => c.Id);
            //var deleteProps = propList.Where(c => !codes.Contains(c.Code)).ToList();
            //if (deleteProps.Count != 0)
            //{
            //    deleteProps.ForEach(async prop =>
            //    {
            //        await _babyPropRepository.DeleteAsync(prop);
            //    });
            //}
        }

        private BabyPropFeature UpdatePropFeatureData(int? featureType, double? featureValue, BabyPropFeature babyPropFeature, BabyProp babyProp)
        {
            var propFeatureType = _babyPropFeatureTypeRepository.FirstOrDefault(c => c.Code == featureType);
            if (propFeatureType != null)
            {
                if (featureType.HasValue && featureValue.HasValue)
                {
                    babyPropFeature.BabyPropFeatureTypeId = propFeatureType.Id;
                    babyPropFeature.Value = featureValue.Value;
                    return babyPropFeature;
                }


            }
            babyProp.BabyPropFeatures.Remove(babyPropFeature);
            return null;
        }

        /// <summary>
        /// 更新道具条件表
        /// </summary>
        /// <param name="term"></param>
        /// <param name="termArgument"></param>
        /// <param name="propTerm"></param>
        private BabyPropTerm UpdatePropTermData(int? term, int? termArgument, BabyPropTerm babyPropTerm, BabyProp babyProp)
        {
            if (term.HasValue && termArgument.HasValue)
            {
                var propTermType = _babyPropTermTypeRepository.FirstOrDefault(c => c.Code == term);
                if (propTermType != null)
                {
                    //babyPropTerm.BabyPropId = propId;
                    babyPropTerm.BabyPropBuyTermId = propTermType.Id;
                    babyPropTerm.MinValue = termArgument.Value;
                    //todo MaxValue??
                    return babyPropTerm;
                }
            }
            babyProp.BabyPropTerms.Remove(babyPropTerm);
            return null;
        }

        /// <summary>
        /// 更新条件表
        /// </summary>
        /// <param name="propValue"></param>
        /// <param name="price"></param>
        /// <param name="interval"></param>
        /// <param name="currencyType"></param>
        /// <param name="propPrice"></param>
        /// <returns></returns>
        private BabyPropPrice UpdatePropPriceData(double? propValue, double? price, double? interval, int currencyType, BabyPropPrice propPrice, BabyProp babyProp)
        {
            if (propValue.HasValue && price.HasValue && interval.HasValue)
            {
                propPrice.Price = price.Value;
                propPrice.PropValue = propValue.Value;
                propPrice.Validity = interval.Value;
                propPrice.CurrencyType = (CurrencyType)currencyType;
                return propPrice;
            }
            else
            {
                babyProp.Prices.Remove(propPrice);
            }
            return null;
        }

        private BabyPropFeature SetPropFeature(int? featureType, double? featureValue)
        {
            var feature = new BabyPropFeature();
            var propFeatureType = _babyPropFeatureTypeRepository.FirstOrDefault(c => c.Code == featureType);
            if (propFeatureType != null)
            {
                //feature.BabyPropId = propId;
                feature.BabyPropFeatureTypeId = propFeatureType.Id;
                feature.Value = featureValue.Value;
                return feature;
            }

            return null;
        }


        /// <summary>
        /// 条件表设置数据
        /// </summary>
        /// <param name="term"></param>
        /// <param name="termArgument"></param>
        /// <param name="propId"></param>
        /// <returns></returns>
        private BabyPropTerm SetPropTerm(int? term, int? termArgument)
        {
            var babyPropTerm = new BabyPropTerm();
            if (term.HasValue && termArgument.HasValue)
            {
                var propTermType = _babyPropTermTypeRepository.FirstOrDefault(c => c.Code == term);
                if (propTermType != null)
                {
                    //babyPropTerm.BabyPropId = propId;
                    babyPropTerm.BabyPropBuyTermId = propTermType.Id;
                    babyPropTerm.MinValue = termArgument.Value;
                    return babyPropTerm;
                }
            }
            return null;
        }
        //道具价格表设置数据
        private BabyPropPrice SetPropPrice(double? propValue, double? price, double? interval, int currencyType)
        {
            var propPrice = new BabyPropPrice();
            if (propValue.HasValue && price.HasValue && interval.HasValue)
            {
                //propPrice.BabyPropId = propId;
                propPrice.Price = price.Value;
                propPrice.PropValue = propValue.Value;
                propPrice.Validity = interval.Value;
                propPrice.CurrencyType = (CurrencyType)currencyType;
                return propPrice;
            }

            return null;
        }
        [RemoteService(IsEnabled = false)]
        public void ImportBabyEventDataToDataSet(DataTable dataTable)
        {
            var importEventDto = dataTable.ToList<ImportEventDto>();

            var enumList = typeof(StudyType).GetAllItems();
            var babyEvents = _babyEventRepository.GetAll().AsNoTracking().ToList();
            var rewards = _rewardRepository.GetAll().ToList();
            foreach (var item in importEventDto)
            {
                if (item.Code == 0 || item.groupid == 0)
                {
                    continue;
                }
                var group = _eventGroupRepository.FirstOrDefault(s => s.Code == item.groupid);
                if (group == null)
                {
                    continue;
                }
                //事件&选项
                var babyEvent = babyEvents.FirstOrDefault(s => s.Code == item.Code);
                var babyEventId = babyEvent?.Id;
                if (babyEvent != null)
                {
                    item.Id = (int)babyEventId;
                }
                var babyEventOutput = ObjectMapper.Map<BabyEvent>(item);
                var specialEvent = babyEvents.FirstOrDefault(s => s.Code == item.EventCode);
                if (specialEvent != null)
                {
                    babyEventOutput.EventId = specialEvent.Id;
                }
                //学习类型单独处理
                var studyType = enumList.FirstOrDefault(s => s.Description == item.own)?.Value;
                if (studyType != null)
                {
                    babyEventOutput.StudyType = (StudyType)(studyType);
                }
                babyEventOutput = _babyEventRepository.InsertOrUpdate(babyEventOutput);

                #region 选项
                var optionRewardsAndConsumesTable = new List<int?>() { item.reward_1, item.reward_2, item.reward_3, item.deplete_1, item.deplete_2, item.deplete_3 };

                var optionRewardsAndConsumesDic = rewards.Where(s => optionRewardsAndConsumesTable.Contains(s.Code)).DistinctBy(s => s.Code).ToDictionary(s => s.Code, s => s.Id);
                //var optionList = new List<BabyEventOption>();
                //var optionIdList = new List<int?>() {item.Option_1_code,item.Option_2_code,item.Option_3_code };
                //var options = _babyEventOptionsRepository.GetAllList(s => optionIdList.Contains(s.Code));
                var _allopts = _babyEventOptionsRepository.GetAll().Where(b=>b.BabyEventId==babyEventId).AsNoTracking();
                //选项一
                var option1 = _allopts.FirstOrDefault(s => s.Code == item.Option_1_code);
                BabyEventOption _newOptions1 = new BabyEventOption
                {
                    Code = item.Option_1_code,
                    BabyEventId = babyEventOutput.Id,
                    ConsumeId = optionRewardsAndConsumesDic.ContainsKey(item.deplete_1) ? (int?)optionRewardsAndConsumesDic[item.deplete_1] : null,
                    Content = item.depict_1,
                    RewardId = optionRewardsAndConsumesDic[item.reward_1],
                    Sort = 1,
                    IsProp = item.means_1 == 1 ? true : false
                };
                if (option1 != null)
                {
                    _newOptions1.Id = option1.Id;
                    _babyEventOptionsRepository.Update(_newOptions1);
                }
                else {
                    if (_allopts.Any())
                    {
                        var _oldopts1 = _allopts.FirstOrDefault(s => s.Sort == 1);
                        _newOptions1.Id = _oldopts1.Id;
                        _babyEventOptionsRepository.Update(_newOptions1);
                    }
                    else {
                        _babyEventOptionsRepository.Insert(_newOptions1);
                    }
                }

                //选项二
               
                var option2 = _allopts.FirstOrDefault(s => s.Code == item.Option_2_code);
                BabyEventOption _newOptions2 = new BabyEventOption
                {
                    Code = item.Option_2_code,
                    BabyEventId = babyEventOutput.Id,
                    ConsumeId = optionRewardsAndConsumesDic.ContainsKey(item.deplete_2) ? (int?)optionRewardsAndConsumesDic[item.deplete_2] : null,
                    Content = item.depict_2,
                    RewardId = optionRewardsAndConsumesDic[item.reward_2],
                    Sort = 2,
                    IsProp = item.means_2 == 1 ? true : false
                };
                if (option2 != null)
                {
                    _newOptions2.Id = option2.Id;
                    _babyEventOptionsRepository.Update(_newOptions2);
                }
                else {
                    if (_allopts.Any())
                    {
                        var _oldopts2 = _allopts.FirstOrDefault(s => s.Sort == 2);
                        _newOptions2.Id = _oldopts2.Id;
                        _babyEventOptionsRepository.Update(_newOptions2);
                    }
                    else
                    {
                        _babyEventOptionsRepository.Insert(_newOptions2);
                    }
                }

                //选项三
                BabyEventOption _newOptions3 = new BabyEventOption
                {
                    Code = item.Option_3_code,
                    BabyEventId = babyEventOutput.Id,
                    ConsumeId = optionRewardsAndConsumesDic.ContainsKey(item.deplete_3) ? (int?)optionRewardsAndConsumesDic[item.deplete_3] : null,
                    Content = item.depict_3,
                    RewardId = optionRewardsAndConsumesDic[item.reward_3],
                    Sort = 3,
                    IsProp = item.means_3 == 1 ? true : false
                };
                var option3 = _allopts.FirstOrDefault(s => s.Code == item.Option_3_code);
                if (option3 != null)
                {
                    _newOptions3.Id = option3.Id;
                    _babyEventOptionsRepository.Update(_newOptions3);
                }
                else
                {
                    if (_allopts.Any())
                    {
                        var _oldopts3 = _allopts.FirstOrDefault(s => s.Sort == 3);
                        _newOptions3.Id = _oldopts3.Id;
                        _babyEventOptionsRepository.Update(_newOptions3);
                    }
                    else
                    {
                        _babyEventOptionsRepository.Insert(_newOptions3);
                    }
                }
                //var option3 = _allopts.FirstOrDefault(s => s.Code == item.Option_3_code);
                //if (option3 == null)
                //{
                //    option3 = new BabyEventOption();
                //}
                //option3.Code = item.Option_3_code;
                //option3.BabyEventId = babyEventOutput.Id;
                //option3.ConsumeId = optionRewardsAndConsumesDic.ContainsKey(item.deplete_3) ? (int?)optionRewardsAndConsumesDic[item.deplete_3] : null;
                //option3.Content = item.depict_3;
                //option3.RewardId = optionRewardsAndConsumesDic[item.reward_3];
                //option3.Sort = 3;
                //option3.IsProp = item.means_3 == 1 ? true : false;

                //_babyEventOptionsRepository.InsertOrUpdate(option3);
                #endregion
                //babyEvent.Options = optionList;
                //_babyEventRepository.InsertOrUpdate(babyEvent);
                //事件组&事件组和事件中间表

                var eventGroupBabyEventData = _eventGroupBabyEventRepository.GetAll().AsNoTracking().FirstOrDefault(s => s.BabyEvent == babyEventOutput && s.EventGroup == group);
                var eventGroupBabyEvent = new EventGroupBabyEvent()
                {
                    EventId = babyEventOutput.Id,
                    GroupId = group.Id,
                };
                if (eventGroupBabyEventData != null)
                {
                    eventGroupBabyEvent.Id = eventGroupBabyEventData.Id;
                }
                _eventGroupBabyEventRepository.InsertOrUpdate(eventGroupBabyEvent);
            }


        }
        private void ImportRewardOrConsumeExcelDataToDataSet(DataTable dataTable, TableDataType tableDataType)
        {
            try
            {

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    //id - > code
                    var column = dataTable.Rows[i];
                    var column0 = column[0];
                    if (column0 == null)
                    {
                        continue;
                    }
                    else
                    {
                        var rewardCode = Convert.ToInt32(column0);
                        var reward = _rewardRepository.FirstOrDefault(s => s.Code == rewardCode);
                        reward = reward == null ? new Reward() { Code = rewardCode } : reward;
                        if (tableDataType == TableDataType.Reward)
                        {
                            reward.Type = RewardType.Reward;
                        }
                        else if (tableDataType == TableDataType.Consume)
                        {
                            reward.Type = RewardType.Consume;
                        }
                        for (int k = 1; k < column.ItemArray.Length; k++)
                        {
                            var cell = column[k];
                            var cellIntValue = 0;
                            var nextCell = column[k + 1];
                            var nextCellIntValue = 0;
                            ++k;
                            if (cell == null || nextCell == null || !int.TryParse(nextCell.ToString(), out nextCellIntValue) || !int.TryParse(cell.ToString(), out cellIntValue))
                            {
                                continue;
                            }
                            switch (cellIntValue)
                            {
                                case 1:
                                    reward.Energy = nextCellIntValue;
                                    break;
                                case 2:
                                    reward.Healthy = nextCellIntValue;
                                    break;
                                case 3:
                                    reward.Intelligence = nextCellIntValue;
                                    break;
                                case 4:
                                    reward.Physique = nextCellIntValue;
                                    break;
                                case 5:
                                    reward.Imagine = nextCellIntValue;
                                    break;
                                case 6:
                                    reward.EmotionQuotient = nextCellIntValue;
                                    break;
                                case 7:
                                    reward.WillPower = nextCellIntValue;
                                    break;
                                case 8:
                                    reward.Charm = nextCellIntValue;
                                    break;
                                case 9:
                                    reward.Happiness = nextCellIntValue;
                                    break;
                                case 10:
                                    reward.CoinCount = nextCellIntValue;
                                    break;
                            }
                        }

                        _rewardRepository.InsertOrUpdate(reward);
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [RemoteService(IsEnabled = false)]
        public async Task ImportEventGroup(DataTable dataTable)
        {
            var importEventDto = dataTable.ToList<ImportDataEventGroup>();
            var eventGroups = _eventGroupRepository.GetAllList(s => s.Code != null).ToDictionary(s => s.Code, s => s.Id);
            foreach (var item in importEventDto)
            {
                int? preGroupId = null;
                if (eventGroups.Keys.Any(s => s.Value == item.PreGroupCode))
                {
                    preGroupId = eventGroups[item.PreGroupCode];
                }
                var group = _eventGroupRepository.FirstOrDefault(s => s.Code == item.GroupCode);
                if (group == null)
                {
                    var groupId = await _eventGroupRepository.InsertAndGetIdAsync(new EventGroup()
                    {
                        Code = item.GroupCode,
                        PrevGroupId = preGroupId,
                        Description = item.Description
                    });
                    eventGroups.Add(item.GroupCode, groupId);
                }
                else
                {
                    group.Code = item.GroupCode;
                    group.PrevGroupId = preGroupId;
                    group.Description = item.Description;
                    var groupId = _eventGroupRepository.Update(group);
                }
            }
        }

        public async Task<DataTable> ReadExcelData(IFormFile formFile)
        {
            DataTable dataTable = null;//接受从excel读取的数据
            var excelSavePath = await SaveExcelFile(formFile);
            dataTable = ExcelUtility.ExcelToDataTable(excelSavePath, true);
            return dataTable;
        }

        public async Task<string> SaveExcelFile(IFormFile formFile)
        {
            var fileContent = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition);
            //原文件名 不包括路径
            var fileName = Path.GetFileName(fileContent.FileName.Trim('"'));
            //获取文件扩展名
            var fileExtensionName = Path.GetExtension(fileName);
            //扩展名
            string tempExtensionName = fileExtensionName.Replace(".", "");
            string newFileGuidName = Guid.NewGuid().ToString();
            //新文件名
            string newFileName = newFileGuidName + fileExtensionName;
            var newPath = string.Empty;
            newPath = $"Files/chineseBaby/excel";
            //var flieType = FileType.Null;
            var folder = Path.Combine(_hostingEnvironment.WebRootPath, newPath);
            if (!Directory.Exists(folder))//路径不存在则创建
            {
                Directory.CreateDirectory(folder);
            }
            //新文件路径
            string path = folder;
            //文件大小  单位K
            string fullFilePath = Path.Combine(path, newFileName);
            using (var stream = new FileStream(fullFilePath, FileMode.CreateNew))
            {
                await formFile.CopyToAsync(stream);
                stream.Flush();
            }
            return fullFilePath;
        }
        [RemoteService(IsEnabled = false)]
        public async Task ImportEndingTable(DataTable dataTable)
        {
            var importEventDto = dataTable.ToList<ImportEndingTable>();
            var endingDic = _endingRepository.GetAll().AsNoTracking().Where(s => s.Code != 0 && s.Code != null && !s.IsDeleted).ToDictionary(s => s.Code, s => s.Id);
            foreach (var item in importEventDto.Where(s => s.Code != 0))
            {
                if (endingDic != null && endingDic.Keys.Any(s => s.Value == item.Code))
                {
                    var endingNew = ObjectMapper.Map<BabyEnding>(item);
                    endingNew.Id = endingDic[item.Code];
                    await _endingRepository.UpdateAsync(endingNew);
                }
                else
                {
                    var ending = ObjectMapper.Map<BabyEnding>(item);
                    var endingId = await _endingRepository.InsertAsync(ending);
                }
            }
        }
        [RemoteService(IsEnabled = false)]
        public async Task ImportProfessionTable(DataTable dataTable)
        {
            var importEventDto = dataTable.ToList<ImportProfessionTable>();
            var endingDic = _professionRepository.GetAll().AsNoTracking().Where(s => s.Code != 0 && s.Code != null && !s.IsDeleted).ToDictionary(s => s.Code, s => s.Id);
            foreach (var item in importEventDto.Where(s => s.Code != 0))
            {
                if (endingDic != null && endingDic.Keys.Any(s => s.Value == item.Code))
                {
                    var endingNew = ObjectMapper.Map<Profession>(item);
                    endingNew.Id = endingDic[item.Code];
                    await _professionRepository.UpdateAsync(endingNew);
                }
                else
                {
                    var ending = ObjectMapper.Map<Profession>(item);
                    var endingId = await _professionRepository.InsertAsync(ending);
                }
            }
        }
        #endregion


        public async Task<ChineseBabyRankOutput> Rank(ChineseBabyRankInput input)
        {
            var response = new ChineseBabyRankOutput();
            try
            {
                var baby = await _babyRepository.GetAllIncluding(b => b.Family).FirstOrDefaultAsync(b => b.Id == input.BabyId);
                //判断年龄段
                var ageMin = 0;
                var ageMax = 0;
                var ageRange = "";
                if (baby.AgeDouble < 2)
                {
                    ageMin = 0;
                    ageMax = 2;
                    ageRange = "2岁以下排行榜";
                }
                else if (baby.AgeDouble < 5)
                {
                    ageRange = "2至5岁排行榜";
                    ageMin = 2;
                    ageMax = 5;
                }
                else
                {
                    ageRange = "5岁以上排行榜";
                    ageMin = 5;
                    ageMax = 100;
                }
                var babies = await _babyRepository.GetAllIncluding(b => b.Family).Where(s => s.Name != null && s.AgeDouble < ageMax && s.AgeDouble >= ageMin && s.State == BabyState.UnderAge).OrderByDescending(s => s.PropertyTotal).ThenByDescending(s => s.Healthy).ToListAsync();
                #region 家庭排行榜
                var families = await _familyRepository.GetAllIncluding(s => s.Babies).Where(s => s.Level == baby.Family.Level && s.Babies.Any(b => b.Name != null && b.State == BabyState.UnderAge)).OrderByDescending(s => s.Deposit).ThenByDescending(s => s.Happiness).ToListAsync();
                var topFamily = families.Take(50).ToList();
                var babySort = families.IndexOf(baby.Family);
                var result = topFamily.Select((s, sort) => new ChineseBabyRankOutputFamilyRankDto
                {
                    BabyName = s.Baby.Name,
                    BabyRank = sort + 1,
                    Deposit = s.Deposit,
                    Happiness = s.Happiness,
                    HappinessTitle = s.HappinessTitle
                });
                var babyRank = new ChineseBabyRankOutputFamilyRankDto
                {
                    BabyName = baby.Name,
                    BabyRank = babySort + 1,
                    Deposit = baby.Family.Deposit,
                    Happiness = baby.Family.Happiness,
                    HappinessTitle = baby.Family.HappinessTitle
                };
                result = result.Append(babyRank);//将当前宝宝加入到队列最后
                response.FamilyRankingList = result.ToList();
                #endregion
                #region 宝宝排行榜
                //宝宝排行榜
                var currentBabyRank = babies.IndexOf(baby);
                var babiesRank = babies.Take(50).Select((s, sort) => new ChineseBabyRankOutputDto
                {
                    BabyName = s.Name,
                    BabyRank = sort + 1,
                    GrowthTotal = s.PropertyTotal,
                    Healthy = s.Healthy,
                    HealthyTitle = s.HealthyTitle
                });
                var currentBabyRankDto = new ChineseBabyRankOutputDto
                {
                    BabyName = baby.Name,
                    BabyRank = currentBabyRank + 1,
                    GrowthTotal = baby.PropertyTotal,
                    Healthy = baby.Healthy,
                    HealthyTitle = baby.HealthyTitle
                };
                babiesRank = babiesRank.Append(currentBabyRankDto);//将当前宝宝加入到队列最后
                response.BabyRankingList = babiesRank.ToList();
                #endregion
                //排行榜信息
                response.RankInfo = new ChineseBabyRankOutputRankInfo()
                {
                    BabyAgeRangeRankInfo = ageRange,//暂定
                    HomeLevelRankInfo = baby.Family.Level.GetDescription() + "排行榜"
                };
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("排行榜出错啦", ex.Message, ex.StackTrace);
                throw ex;
            }
            return response;
        }

        public async Task<IList<GetMiniProgramsOutput>> GetMiniPrograms(GetMiniProgramsInput input)
        {
            var family = await _familyRepository.FirstOrDefaultAsync(f => f.Id == input.FamilyId &&
                                                                          (f.FatherId == input.PlayerId || f.MotherId == input.PlayerId)
            );

            if (family == null)
            {
                throw new AbpException("未找到指定的家庭，请查看参数");
            }

            var baseUrl = _configuration.GetValue(typeof(string), "EntranceMiniProgram");

            var result = WeChatPayHelper.HttpPost<IList<GetMiniProgramsOutput>>(baseUrl + "/Player/GetMiniPrograms", input);

            return result;
        }
    }
}


