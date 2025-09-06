using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;


namespace MQKJ.BSMP.Utils.Tools
{
    public class ImageHelper
    {

        /// <summary>
        /// 生成缩略图重载方法，返回缩略图的Image对象
        /// </summary>
        /// <param name="width">缩略图的宽度</param>
        /// <param name="height">缩略图的高度</param>
        /// <param name="imageFrom">原Image对象路径</param>
        /// <param name="imageSavePath">原Image对象路径</param>
        /// <param name="isAutoScale">等比例缩放，默认自动，根据宽来自适应高度</param>
        /// <returns>保存结果</returns>
        public bool GetThumbnail(int width, int height, string imageFrom, string imageSavePath, bool isAutoScale = true)
        {
            try
            {
                // Image.Load(string path) is a shortcut for our default type. 
                // Other pixel formats use Image.Load<TPixel>(string path))
                using (Image<Rgba32> image = Image.Load(imageFrom))
                {
                    image.Mutate(x => x
                         .Resize(width, isAutoScale ? (image.Height * width) / image.Width : height));
                    image.Save(imageSavePath); // Automatic encoder selected based on extension.
                }
                return true;
            }
            catch (Exception)
            {
                //抛出异常
                return false;
            }

        }



    }
}
