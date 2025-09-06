using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.BSMPFiles;
using MQKJ.BSMP.SceneFiles;
using MQKJ.BSMP.SceneFiles.Authorization;
using MQKJ.BSMP.SceneFiles.Dto;
using MQKJ.BSMP.SceneFiles.SceneFiles.Dto;
using MQKJ.BSMP.Scenes;
using MQKJ.BSMP.Utils.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MQKJ.BSMP.SceneManage.SceneFiles
{
    //[AbpAuthorize(SceneFileAppPermissions.SceneFile)]
    public class SceneFileAppService : BSMP.BSMPAppServiceBase, ISceneFileAppService
    {
        private string[] _imageTypeArr = new string[3] { "png", "jpg", "JPEG" };

        private string[] _audioTypeArr = new string[4] { "mp4", "avi", "mpeg", "wmv" };

        private string[] _videoTypeArr = new string[3] { "mp3", "WMA", "AAC" };

        /// <summary>
        /// 存放场景文件的路径
        /// </summary>
        private string SceneFilePath = "SceneFiles";

        private const string FileRootPath = "Files";

        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly IRepository<Scene> _sceneRepository;

        private readonly IRepository<SceneFile, Guid> _sceneFileRepository;

        private readonly IRepository<BSMPFile, Guid> _fileRepository;

        public SceneFileAppService(
            IRepository<Scene> sceneRepository,
            IRepository<SceneFile, Guid> sceneFileRepository,
            IHostingEnvironment hostingEnvironment,
            IRepository<BSMPFile, Guid> fileRepository
            )
        {
            _hostingEnvironment = hostingEnvironment;

            _sceneRepository = sceneRepository;

            _sceneFileRepository = sceneFileRepository;

            _fileRepository = fileRepository;
        }

        //[AbpAuthorize(SceneFileAppPermissions.SceneFile_CreateSceneFile)]
        public async Task UploaSceneFileAsync(UploadSceneFileDto input)
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

            var folder = Path.Combine(_hostingEnvironment.WebRootPath, FileRootPath, SceneFilePath);

            if (!Directory.Exists(folder))//路径不存在则创建
            {
                Directory.CreateDirectory(folder);
            }

            //新文件路径
            string path = folder;

            //文件大小  单位K
            double size = Math.Round(input.FormFile.Length / 1024.0, 2);

            var flieType = 0;

            string fullFilePath = Path.Combine(path, newFileName);

            using (var stream = new FileStream(fullFilePath, FileMode.CreateNew))
            {
                await input.FormFile.CopyToAsync(stream);

                stream.Flush();

            }
            string tempExtensionName = fileExtensionName.Replace(".", "");

            if (_imageTypeArr.Contains(tempExtensionName))
            {
                flieType = (int)FileType.Image;
            }
            else if (_audioTypeArr.Contains(tempExtensionName))
            {

            }
            else if (_videoTypeArr.Contains(tempExtensionName))
            {

            }

            input.FileName = fileName;

            input.FilePath = "/Files/" + SceneFilePath + "/" + newFileName;

            input.NewFileName = newFileName;

            input.Size = size;

            input.Type = (FileType)flieType;
            var file = input.MapTo<BSMPFile>();
            //保存缩略图
            file.ThumbnailImagePath = CreateImageThumbnail(input.FilePath);

            var result = await _fileRepository.InsertAsync(file);

            SceneFileDto dto = new SceneFileDto();

            dto.FileId = result.Id.ToString();

            dto.SceneId = input.SceneId;

            dto.IsDefault = true;

            dto.SceneFileName = input.SceneFileName;

            //更新SceneFile实体
            await _sceneFileRepository.InsertAsync(dto.MapTo<SceneFile>());

            var entity = await _sceneFileRepository.FirstOrDefaultAsync(x => x.IsDefault == true);

            if (entity != null)
            {
                entity.IsDefault = false;

                await _sceneFileRepository.UpdateAsync(entity);
            }

        }

        [AbpAuthorize(SceneFileAppPermissions.SceneFile_DeleteSceneFile)]
        public async Task DeleteSceneFile(SceneFileDto input)
        {
            var entity = _sceneFileRepository.FirstOrDefault(x => x.Id == input.Id);

            await _fileRepository.DeleteAsync(x => x.Id == entity.FileId);

            await _sceneFileRepository.DeleteAsync(entity);

            if (entity.IsDefault)
            {
                var sceneEntity = await _sceneRepository.GetAllIncluding(s => s.SceneFile).FirstOrDefaultAsync(x => x.Id == entity.SceneId);
                sceneEntity.SceneFile.IsDefault = true;
                await _sceneRepository.UpdateAsync(sceneEntity);
            }
        }

        public List<GetSceneFileOutput> GetSceneFile(EntityDto<int> input)
        {
            // var sceneFileList = new List<GetSceneFileOutput>();
            var s = _sceneFileRepository.GetAll();

            var sceneFiles = s.Include(f => f.File).Where(x => x.SceneId == input.Id);

            var sceneFileList = sceneFiles.MapTo<List<GetSceneFileOutput>>();

            return sceneFileList;
        }
        public IEnumerable<GetSceneFileOutput> GetAllAsync()
        {
            var s = _sceneFileRepository.GetAll();

            var sceneFiles = s.Include(f => f.File).AsNoTracking();

            var sceneFileList = sceneFiles.MapTo<List<GetSceneFileOutput>>();

            return sceneFileList;
        }

        [AbpAuthorize(SceneFileAppPermissions.SceneFile_EditSceneFile)]
        public async Task UpdateSceneFile(UpdateSceneFileInput input)
        {
            var defaultEntity = await _sceneFileRepository.FirstOrDefaultAsync(x => x.IsDefault == true && x.SceneId == input.SceneId);

            if (defaultEntity != null)
            {
                defaultEntity.IsDefault = false;

                await _sceneFileRepository.UpdateAsync(defaultEntity);
            }

            var entity = await _sceneFileRepository.FirstOrDefaultAsync(x => x.Id == input.SceneFileId);

            if (entity != null)
            {
                entity.IsDefault = true;

                await _sceneFileRepository.UpdateAsync(entity);
            }
        }

        /// <summary>
        /// 创建缩略图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpAuthorize(SceneFileAppPermissions.SceneFile_EditSceneFile)]
        public async Task CreateSceneImageThumbnail(EntityDto<Guid> input)
        {
            var entity = _fileRepository.Get(input.Id);

            if (entity != null)
            {
                string thumbNailFilePath = CreateImageThumbnail(entity.FilePath);
                entity.ThumbnailImagePath = thumbNailFilePath;
                entity.LastModificationTime = DateTime.Now;
                await _fileRepository.UpdateAsync(entity);
            }
            else
            {
               
            }

        }
        /// <summary>
        /// 创建缩略图
        /// </summary>
        /// <param name="filePath">源文件地址</param>
        /// <param name="width">宽度默认为150，高度等比缩放</param>
        /// <returns></returns>
        private string CreateImageThumbnail(string filePath, int width = 150)
        {
            try
            {
                //获取文件扩展名
                var fileExtensionName = Path.GetExtension(filePath);
                string fileName = Path.GetFileName(filePath);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                //新文件名
                var path = Path.Combine(_hostingEnvironment.WebRootPath, FileRootPath, SceneFilePath);
                string fullFilePath = Path.Combine(path, fileName);

                ImageHelper imageHelper = new ImageHelper();
                string thumbnailName = fileNameWithoutExtension + "-thumbnail" + fileExtensionName;
                var thumbnailPath = "/Files/" + SceneFilePath + "/" + thumbnailName;
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
    }
}
