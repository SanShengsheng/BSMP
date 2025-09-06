
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


using MQKJ.BSMP.Common.Dtos;
using MQKJ.BSMP.Common;
using MQKJ.BSMP.Common.EnterprisePayments.Dtos;
using MQKJ.BSMP.Common.Adviertisements.Dtos;

namespace MQKJ.BSMP.Common.Adviertisements
{
    /// <summary>
    /// EnterpirsePaymentRecord应用层服务的接口方法
    ///</summary>
    public interface IAdviertisementApplicationService : IApplicationService
    {
        Task<PagedResultDto<GetAdviertisementsOutput>> GetAdviertisements(GetAdviertisementsInput input);

        Task<GetAdviertisementForEditOutput> GetAdviertisementForEdit(NullableIdDto<int> input);

        Task CreateOrUpdateAdviertisement(CreateOrUpdateAdviertisementInput input);

        Task DeleteAd(DeleteAdInput input);

        Task<ClickAdOutput> ClickAd(ClickAdInput input);
    }
}
