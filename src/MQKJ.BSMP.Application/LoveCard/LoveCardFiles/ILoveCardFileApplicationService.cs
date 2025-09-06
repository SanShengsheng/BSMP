
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


using MQKJ.BSMP.LoveCardFiles.Dtos;
using MQKJ.BSMP.LoveCardFiles;
using MQKJ.BSMP.LoveCard.LoveCardFiles.Dtos;

namespace MQKJ.BSMP.LoveCardFiles
{
    /// <summary>
    /// LoveCardFile应用层服务的接口方法
    ///</summary>
    public interface ILoveCardFileAppService : IApplicationService
    {
        /// <summary>
		/// 获取LoveCardFile的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LoveCardFileListDto>> GetPaged(GetLoveCardFilesInput input);


		/// <summary>
		/// 通过指定id获取LoveCardFileListDto信息
		/// </summary>
		Task<LoveCardFileListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLoveCardFileForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LoveCardFile的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateLoveCardFileInput input);


        /// <summary>
        /// 删除LoveCardFile信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LoveCardFile
        /// </summary>
        Task BatchDelete(List<Guid> input);

        Task<UpLoadLoveCardFileOutput> UploaCardFileAsync(UploadLoveCardFileDto input);

        /// <summary>
        /// 获取卡片的所有文件路径
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> GetLoveCardFileByCardId(GetLoveCardFileByCardIdInput input);

        /// <summary>
        /// 导出LoveCardFile为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();

    }
}
