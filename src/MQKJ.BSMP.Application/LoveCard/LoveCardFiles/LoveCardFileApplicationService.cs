
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.BSMPFiles;
using MQKJ.BSMP.LoveCard.LoveCardFiles.Dtos;
using MQKJ.BSMP.LoveCardFiles.Authorization;
using MQKJ.BSMP.LoveCardFiles.DomainService;
using MQKJ.BSMP.LoveCardFiles.Dtos;
using MQKJ.BSMP.LoveCards;
using MQKJ.BSMP.LoveCards.Dtos;
using MQKJ.BSMP.Utils.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MQKJ.BSMP.LoveCardFiles
{
    /// <summary>
    /// LoveCardFile应用层服务的接口实现方法  
    ///</summary>
    //[AbpAuthorize]
    public class LoveCardFileAppService : BSMPAppServiceBase, ILoveCardFileAppService
    {
        private string[] _imageTypeArr = new string[3] { "png", "jpg", "JPEG" };

        private string[] _videoTypeArr = new string[4] { "mp4", "avi", "mpeg", "wmv" };

        private string[] _audioTypeArr = new string[4] { "mp3", "WMA", "AAC","silk" };

        /// <summary>
        /// 存放名片文件的路径
        /// </summary>
        private string LoveCardFilePath = "LoveCardFiles";

        private const string FileRootPath = "Files";

        private const string RecordPath = "Records";

        private const string ImagePath = "Images";

        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly IRepository<LoveCardFile, Guid> _entityRepository;

        private readonly ILoveCardFileManager _entityManager;

        private readonly IRepository<BSMPFile, Guid> _fileRepository;

        private readonly ILoveCardAppService  _loveCardAppService;
        /// <summary>
        /// 构造函数 
        ///</summary>
        public LoveCardFileAppService(
        IRepository<LoveCardFile, Guid> entityRepository
        ,ILoveCardFileManager entityManager
        ,IHostingEnvironment hostingEnvironment
        ,IRepository<BSMPFile, Guid> fileRepository
        ,ILoveCardAppService loveCardAppService
        )
        {
            _hostingEnvironment = hostingEnvironment;
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _fileRepository = fileRepository;
            _loveCardAppService = loveCardAppService;
        }


        /// <summary>
        /// 获取LoveCardFile的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[AbpAuthorize(LoveCardFilePermissions.Query)]
        public async Task<PagedResultDto<LoveCardFileListDto>> GetPaged(GetLoveCardFilesInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<LoveCardFileListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<LoveCardFileListDto>>();

            return new PagedResultDto<LoveCardFileListDto>(count, entityListDtos);
        }

        [HttpGet]
        public async Task<UpLoadLoveCardFileOutput> UploaCardFileAsync(UploadLoveCardFileDto input)
        {
            var fileContent = ContentDispositionHeaderValue.Parse(input.FormFile.ContentDisposition);

            //原文件名 不包括路径
            var fileName = Path.GetFileName(fileContent.FileName.Trim('"'));

            //原文件名包括路径
            //var FullfilPath = ContentDispositionHeaderValue.Parse(fileinput.ContentDisposition).FileName;

            //获取文件扩展名
            var fileExtensionName = Path.GetExtension(fileName);

            string newFileGuidName = Guid.NewGuid().ToString();

            //新文件名
            string newFileName = newFileGuidName + fileExtensionName;

            //扩展名
            string tempExtensionName = fileExtensionName.Replace(".", "");

            var newPath = string.Empty;

            var flieType = FileType.Null;

            if (_imageTypeArr.Contains(tempExtensionName))
            {
                newPath = $"{FileRootPath}/{LoveCardFilePath}/{ImagePath}";
                flieType = FileType.Image;
            }
            else if (_audioTypeArr.Contains(tempExtensionName))
            {
                newPath = $"{FileRootPath}/{LoveCardFilePath}/{RecordPath}";
                flieType = FileType.Audio;
            }
            else if (_videoTypeArr.Contains(tempExtensionName))
            {
                flieType = FileType.Video;
            }
            var folder = Path.Combine(_hostingEnvironment.WebRootPath, newPath);

            if (!Directory.Exists(folder))//路径不存在则创建
            {
                Directory.CreateDirectory(folder);
            }

            //新文件路径
            string path = folder;

            //文件大小  单位K
            double size = Math.Round(input.FormFile.Length / 1024.0, 2);

            string fullFilePath = Path.Combine(path, newFileName);


            using (var stream = new FileStream(fullFilePath, FileMode.CreateNew))
            {
                //if (tempExtensionName == "silk")
                //{
                //    stream.
                //    Base64Tool.DecodeBase64("");
                //}
                await input.FormFile.CopyToAsync(stream);

                stream.Flush();

            }

            var bsmpFile = new BSMPFile();

            bsmpFile.FileName = fileName;

            bsmpFile.NewFileName = newFileName;

            bsmpFile.Size = size;

            bsmpFile.type = (FileType)flieType;

            bsmpFile.FilePath ="/"+ newPath+"/"+newFileName;

            if (flieType == FileType.Image)
            {

                //保存缩略图
                bsmpFile.ThumbnailImagePath = CreateImageThumbnail(bsmpFile.FilePath);
            }

            Guid fileId = Guid.Empty;

            if (input.LoveCardId.HasValue)
            {
                var cardFile = await _entityRepository.GetAll().Include(x => x.BSMPFile).FirstOrDefaultAsync(b => b.LoveCardId == input.LoveCardId.Value && b.BSMPFile.type == flieType);

                if (cardFile != null)
                {

                    cardFile.BSMPFile.FileName = fileName;

                    cardFile.BSMPFile.FilePath = "/" + newPath + "/" + newFileName;

                    cardFile.BSMPFile.NewFileName = newFileName;

                    cardFile.BSMPFile.Size = size;

                    cardFile.BSMPFile.type = (FileType)flieType;

                    await _fileRepository.UpdateAsync(cardFile.BSMPFile);

                    fileId = cardFile.BSMPFile.Id;
                    bsmpFile = cardFile.BSMPFile;
                }
                else
                {
                    fileId = await _fileRepository.InsertAndGetIdAsync(bsmpFile);

                    LoveCardFileEditDto dto = new LoveCardFileEditDto();

                    dto.BSMPFileId = fileId;

                    dto.UserId = input.PlayerId;

                    dto.LoveCardId = input.LoveCardId.Value;

                    await _entityRepository.InsertAsync(dto.MapTo<LoveCardFile>());
                }

                return new UpLoadLoveCardFileOutput()
                {
                    FilePath = bsmpFile.FilePath
                };
            }
            else
            {
                return new UpLoadLoveCardFileOutput()
                {
                    FilePath = null
                };
            }

        }


        /// <summary>
        /// 通过指定id获取LoveCardFileListDto信息
        /// </summary>
        [AbpAuthorize(LoveCardFilePermissions.Query)]
        public async Task<LoveCardFileListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<LoveCardFileListDto>();
        }

        public async Task<string> GetLoveCardFileByCardId(GetLoveCardFileByCardIdInput input)
        {
            var CardFile = await _entityRepository.GetAll()
                .Include(f => f.BSMPFile).OrderByDescending(o=>o.CreationTime)
                .FirstOrDefaultAsync(x => x.LoveCardId == input.LoveCardId && x.BSMPFile.type == input.FileType);

            return CardFile.BSMPFile.FilePath;
        }

        /// <summary>
        /// 获取编辑 LoveCardFile
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(LoveCardFilePermissions.Create, LoveCardFilePermissions.Edit)]
        public async Task<GetLoveCardFileForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetLoveCardFileForEditOutput();
            LoveCardFileEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<LoveCardFileEditDto>();

                //loveCardFileEditDto = ObjectMapper.Map<List<loveCardFileEditDto>>(entity);
            }
            else
            {
                editDto = new LoveCardFileEditDto();
            }

            output.LoveCardFile = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改LoveCardFile的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(LoveCardFilePermissions.Create, LoveCardFilePermissions.Edit)]
        public async Task CreateOrUpdate(CreateOrUpdateLoveCardFileInput input)
        {

            if (input.LoveCardFile.Id.HasValue)
            {
                await Update(input.LoveCardFile);
            }
            else
            {
                await Create(input.LoveCardFile);
            }
        }


        /// <summary>
        /// 新增LoveCardFile
        /// </summary>
        [AbpAuthorize(LoveCardFilePermissions.Create)]
        protected virtual async Task<LoveCardFileEditDto> Create(LoveCardFileEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LoveCardFile>(input);
            var entity = input.MapTo<LoveCardFile>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<LoveCardFileEditDto>();
        }

        /// <summary>
        /// 编辑LoveCardFile
        /// </summary>
        [AbpAuthorize(LoveCardFilePermissions.Edit)]
        protected virtual async Task Update(LoveCardFileEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除LoveCardFile信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(LoveCardFilePermissions.Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除LoveCardFile的方法
        /// </summary>
        [AbpAuthorize(LoveCardFilePermissions.BatchDelete)]
        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        private string CreateImageThumbnail(string filePath, int width = 150)
        {
            try
            {
                //获取文件扩展名
                var fileExtensionName = Path.GetExtension(filePath);
                string fileName = Path.GetFileName(filePath);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                //新文件名
                var path = Path.Combine(_hostingEnvironment.WebRootPath, FileRootPath, LoveCardFilePath);
                string fullFilePath = Path.Combine(path, fileName);

                ImageHelper imageHelper = new ImageHelper();
                string thumbnailName = fileNameWithoutExtension + "-thumbnail" + fileExtensionName;
                var thumbnailPath = "/Files/" + LoveCardFilePath + "/" + thumbnailName;
                string thumbnailImageFullFilePath = Path.Combine(path, thumbnailName);
                var saveThumbnailImageResult = imageHelper.GetThumbnail(150, 0, fullFilePath, thumbnailImageFullFilePath);
                return saveThumbnailImageResult ? thumbnailPath : null;
            }
            catch (Exception ex)
            {
                Logger.Error("创建缩略图出错啦~~" + ex.Message);
                return null;
            }

        }


        /// <summary>
        /// 导出LoveCardFile为excel表,等待开发。
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


